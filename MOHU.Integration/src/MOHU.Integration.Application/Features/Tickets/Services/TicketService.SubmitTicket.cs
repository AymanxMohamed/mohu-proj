using Microsoft.Xrm.Sdk.Messages;
using MOHU.Integration.Application.Common.Extensions;
using MOHU.Integration.Contracts.Dto.Hootsuite;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using OfficeDevPnP.Core.Extensions;

namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    public async Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request)
    {
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
            ticket = request.ToTicket(customerId, service);

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
                        categories.Add(new TicketCategoryLevel { Id = id, CategoryLevel = CategoryLevelsEnum.SubCategory , ParentId = parentCategory.Id });
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
}