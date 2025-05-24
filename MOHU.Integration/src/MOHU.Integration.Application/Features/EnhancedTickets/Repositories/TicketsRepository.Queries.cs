using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using Core.Domain.ErrorHandling.Exceptions;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Features.Tasks.Repositories;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Constants;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository(
    ILogger<TicketsRepository> logger,
    IGenericRepository genericRepository,
    ITasksRepository tasksRepository) : ITicketsRepository
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
}