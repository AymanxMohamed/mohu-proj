using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses.DetailsResponse;
using MOHU.Integration.Application.Features.Tasks.Repositories;
using MOHU.Integration.Domain.Features.ServiceDefinitions.Constants;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Enums;
using MOHU.Integration.Domain.Features.TicketStatuses.Constants;


namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository(
    IGenericRepository genericRepository,
    ITasksRepository tasksRepository,
    ICrmContext crmContext) : ITicketsRepository
{
    public Ticket GetById(Guid ticketId)
    {
        var entity = genericRepository.GetById(TicketsConstants.LogicalName, ticketId);

        if (entity is null)
        {
            throw new NotFoundException($"Their is no ticket found with this id {ticketId}");
        }

        var ticket = Ticket.Create(entity);
        
        ticket.SetCrmTasks(tasksRepository.GetTicketTasks(ticket.Id.Id).Items);

        return ticket;
    }
    
    public Ticket GetByTitle(string ticketNumber)
    {
        var entity = Get(GetQuery(
            conditionExpressions: [ConditionExpressionFactory
                .CreateConditionExpression(
                    columnLogicalName: TicketsConstants.BasicInformation.Fields.Title,
                    conditionOperator: ConditionOperator.Equal,
                    value: ticketNumber)]))
            .FirstOrDefault();
        
        if (entity is null)
        {
            throw new NotFoundException($"Their is no ticket found with this number {ticketNumber}");
        }
        
        return Ticket.Create(entity);
    }

    public IEnumerable<Entity> Get(QueryBase queryExpression) => 
        genericRepository.ListAll(queryExpression);
    
    public PaginationResponse<Entity> GetPaginated(QueryBase queryExpression) => 
        genericRepository.ListAllPaginated(queryExpression);

    public QueryBase GetQuery(
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null,
        params ConditionExpression[] conditionExpressions)
    {
        return QueryExpressionFactory
            .CreateQueryExpression(
                entityLogicalName: TicketsConstants.LogicalName,
                columnSet,
                isOrFilter,
                filterExpression,
                childFilters, 
                linkEntities, 
                paginationParameters,
                orderExpressions: orderExpressions,
                conditionExpressions: conditionExpressions);
    }
    public Entity GetByTitle(string ticketNumber, ColumnSet columnSet)
    {
        var query = GetQuery(
            columnSet: columnSet,
            conditionExpressions: new[]
            {
            ConditionExpressionFactory.CreateConditionExpression(
                columnLogicalName: TicketsConstants.BasicInformation.Fields.Title,
                conditionOperator: ConditionOperator.Equal,
                value: ticketNumber
            )
            }
        );

        var entity = Get(query).FirstOrDefault();

        if (entity is null)
        {
            throw new NotFoundException($"No ticket found with number {ticketNumber}.");
        }

        return entity;
    }

    public async Task<ResolveTicketResponse> ResolveTicketAsync(ResolveTicketRequest request)
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

        return new ResolveTicketResponse { Success = "Ticket resolved successfully." };
    }

    #region Helpers
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
        var query = GetQuery(
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