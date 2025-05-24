using Microsoft.Xrm.Sdk.Messages;
using MOHU.Integration.Application.Common.Extensions;
using MOHU.Integration.Contracts.Dto.Hootsuite;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Features.ServiceDefinitions.Constants;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Enums;
using MOHU.Integration.Domain.Features.TicketStatuses.Constants;
using OfficeDevPnP.Core.Extensions;

namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    public async Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request)
    {
        await ticketCategoriesService.EnsureValidCategoriesAsync(request.CategoryIds);
        (await validator.ValidateAsync(request)).EnsureValidResult();
        await EnsureNoActiveTicketForCustomerAsync(customerId);

        var ticket = request.ToTicket(
            customerId,
            requestInfo.Origin,
            await GetServiceAsync(request.CaseType));

        return await CreateTicketEntityAsync(ticket);
    }

    public async Task<SubmitTicketResponse> SubmitHootSuiteTicketAsync(Guid customerId, CreateHootSuiteTicketRequest request)
    {
        await ticketCategoriesService.EnsureValidCategoriesAsync(request.CategoryIds);
        (await createHootSuiteTicketValidator.ValidateAsync(request)).EnsureValidResult();
        await EnsureNoActiveTicketForCustomerAsync(customerId);

        var ticket = request.ToTicket(customerId, await GetServiceAsync(request.CaseType));

        return await CreateTicketEntityAsync(ticket);
    }
    public async Task<SubmitTicketResponse> SubmitHootSuiteTicketWithCategoryAsync(Guid customerId, CreateHootsuiteTicketWithCategoryRequest request)
    {
        //(await createHootSuiteTicketValidator.ValidateAsync(request)).EnsureValidResult();
        //await EnsureNoActiveTicketForCustomerAsync(customerId);
        Contracts.Services.Service service = new Contracts.Services.Service();
        Entity ticket;
        if (request.CaseType != Guid.Empty)
        {

            service = await GetServiceAsync(request.CaseType);

        }
        ticket = request.ToTicket(customerId, service);

        return await CreateTicketEntityAsync(ticket);
    }

    private async Task<SubmitTicketResponse> CreateTicketEntityAsync(Entity entity)
    {
        var caseId = await crmContext.ServiceClient.CreateAsync(entity);

        return SubmitTicketResponse.Create(await GetCaseByIdAsync(caseId));
    }

    private async Task<Entity> GetCaseByIdAsync(Guid id)
    {
        return await crmContext.ServiceClient.RetrieveAsync(
            Incident.EntityLogicalName, id,
            new ColumnSet(Incident.Fields.Title));
    }

    private async Task<Contracts.Services.Service> GetServiceAsync(Guid serviceId)
    {
        var ticketTypeEntity = await crmContext.ServiceClient
            .RetrieveAsync(ldv_service.EntityLogicalName, serviceId,
                new ColumnSet(
                    ldv_service.Fields.ldv_processid,
                    ldv_service.Fields.ldv_serviceparentid));

        var process = ticketTypeEntity.GetAttributeValue<EntityReference>(ldv_service.Fields.ldv_processid);
        var parentService = ticketTypeEntity.GetAttributeValue<EntityReference>(ldv_service.Fields.ldv_serviceparentid);

        return Contracts.Services.Service.Create(process, parentService);

    }
    public async Task<List<TicketCategoryLevel>> GetCategoriesLevel(List<Guid> categoriesIds)
    {
        List<TicketCategoryLevel> categories = new List<TicketCategoryLevel>();

        var executeMultipleRequest = new ExecuteMultipleRequest
        {
            Requests = new OrganizationRequestCollection(),
            Settings = new ExecuteMultipleSettings
            {
                ContinueOnError = false,
                ReturnResponses = true
            }
        };

        foreach (Guid id in categoriesIds)
        {
            // Build QueryExpression to retrieve categories based on IDs
            QueryExpression query = new QueryExpression(ldv_casecategory.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(ldv_casecategory.Fields.Id, ldv_casecategory.Fields.ParentCategory, ldv_casecategory.Fields.SubCategory)
            };

            // Apply filter to get only matching records
            query.Criteria.AddCondition(ldv_casecategory.Fields.Id, ConditionOperator.Equal, id);
            query.Criteria.AddCondition(ldv_casecategory.Fields.StateCode, ConditionOperator.Equal, 0);

            // Add RetrieveMultipleRequest to batch request
            executeMultipleRequest.Requests.Add(new RetrieveMultipleRequest { Query = query });
        }

        // Execute the batch request
        var executeMultipleResponse = (ExecuteMultipleResponse)await crmContext.ServiceClient.ExecuteAsync(executeMultipleRequest);

        // Process the responses
        foreach (var responseItem in executeMultipleResponse.Responses)
        {
            if (responseItem.Response != null && responseItem.Response is RetrieveMultipleResponse retrieveResponse)
            {
                foreach (var entity in retrieveResponse.EntityCollection.Entities)
                {
                    var id = entity.GetAttributeValue<Guid>(ldv_casecategory.Fields.Id);

                    var parentCategory = entity.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.ParentCategory);
                    var subCategory = entity.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.SubCategory);
                    var secondry = entity.GetAttributeValue<EntityReference>(ldv_casecategory.Fields.SecondarySubCategory);
                    if (parentCategory is null)
                    {
                        categories.Add(new TicketCategoryLevel { Id = id, CategoryLevel = CategoryLevelsEnum.ParentCategory });
                    }
                    if (parentCategory is not null && subCategory is null)
                    {
                        categories.Add(new TicketCategoryLevel { Id = id, CategoryLevel = CategoryLevelsEnum.SubCategory, ParentId = parentCategory.Id });
                    }
                    if (parentCategory is not null && subCategory is not null)
                    {
                        categories.Add(new TicketCategoryLevel { Id = id, CategoryLevel = CategoryLevelsEnum.SecondryCategory, ParentId = parentCategory.Id });
                    }
                }
            }
            else if (responseItem.Fault != null)
            {
                // Handle any errors from individual requests
                Console.WriteLine($"Error in request: {responseItem.Fault.Message}");
            }
        }

        var retrievedCategories = (ExecuteMultipleResponse)await crmContext.ServiceClient
            .ExecuteAsync(executeMultipleRequest);

        return categories;

    }


    public async Task<bool> ResolveTicketAsync(ResolveTicketRequest request)
    {
        // 1. Retrieve ticket AND linked ServiceDefinition's flag in one query
        var ticket = GetTicketWithServiceDefinitionFlag(request.CRMTicketNumber);

        // 2. Validate the flag
        var canAutoResolve = ticket.GetAttributeValue<AliasedValue>(
            "sd." + ServiceDefinitionConstants.Fields.AutomaticResolveForApi
        )?.Value as bool? ?? false;

        if (!canAutoResolve)
        {
            throw new InvalidOperationException("This service cannot be resolved via the API.");
        }

        // 3. Resolve the ticket
        ResolveTicket(ticket);

        return true;
    }

    #region HelpersForResolveTicket
    private Entity GetTicketWithServiceDefinitionFlag(string ticketNumber)
    {
        // Build the LinkEntity to ServiceDefinition
        var serviceLink = new LinkEntity(
            linkFromEntityName: TicketsConstants.LogicalName,
            linkToEntityName: ServiceDefinitionConstants.LogicalName,
            linkFromAttributeName: TicketsConstants.BasicInformation.Fields.Service, // Ticket's lookup field
            linkToAttributeName: ServiceDefinitionConstants.Fields.Id, // ServiceDefinition's ID
            joinOperator: JoinOperator.Inner
        );
        serviceLink.Columns.AddColumn(ServiceDefinitionConstants.Fields.AutomaticResolveForApi);
        serviceLink.EntityAlias = "sd"; // Alias for ServiceDefinition fields

        // Build the query using your existing GetQuery method
        var query = ticketsRepository.GetQuery(
            columnSet: new ColumnSet(
                TicketsConstants.BasicInformation.Fields.Id,
                TicketsConstants.BasicInformation.Fields.Service // Include the lookup field
            ),
            linkEntities: new[] { serviceLink }, // Inject the LinkEntity
            conditionExpressions: new ConditionExpression(
                TicketsConstants.BasicInformation.Fields.Title,
                ConditionOperator.Equal,
                ticketNumber
            )
        );

        // Execute the query
        var result = crmContext.ServiceClient.RetrieveMultiple(query);
        var ticket = result.Entities.FirstOrDefault();

        if (ticket == null)
        {
            throw new NotFoundException($"Ticket {ticketNumber} not found.");
        }

        // Validate ServiceDefinition link exists
        if (ticket.GetAttributeValue<EntityReference>(
                TicketsConstants.BasicInformation.Fields.Service) == null)
        {
            throw new InvalidOperationException("Ticket has no linked Service Definition.");
        }

        return ticket;
    }

    private void ResolveTicket(Entity ticket)
    {
        // Get the resolved status reference
        var resolvedStatusRef = new EntityReference(
            TicketStatusesConstants.LogicalName,
            TicketStatusesConstants.Statuses.Resolved
        );

        // Update the ticket's PortalStatus and StatusReason fields
        var ticketUpdate = new Entity(TicketsConstants.LogicalName, ticket.Id)
        {
            [TicketsConstants.BasicInformation.Fields.PortalStatus] = resolvedStatusRef,
            [TicketsConstants.BasicInformation.Fields.StatusReason] = resolvedStatusRef
        };
        crmContext.ServiceClient.Update(ticketUpdate);

        var resolveRequest = Ticket.CreateIncidentResolutionActivity(
            ticket.Id,
            (int)TicketResolvedStatusReasonEnum.TicketResolved
        );
        crmContext.ServiceClient.Execute(resolveRequest);
    }
    #endregion


}