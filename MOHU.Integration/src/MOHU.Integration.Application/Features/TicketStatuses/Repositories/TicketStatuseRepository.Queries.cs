using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Domain.Features.TicketStatuses;
using MOHU.Integration.Domain.Features.TicketStatuses.Constants;

namespace MOHU.Integration.Application.Features.TicketStatuses.Repositories;

public class TicketStatusesRepository(IGenericRepository repository) : ITicketStatusesRepository
{
    public PaginationResponse<TicketStatus> GetAll(
        FilterExpression? filterExpression = null, 
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null)
    {
       return repository
           .ListAllPaginated(GetQuery(
               filterExpression: filterExpression, 
               paginationParameters: paginationParameters,
               orderExpressions: orderExpressions))
           .Convert(TicketStatus.Create);
    }

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
                entityLogicalName: TicketStatusesConstants.LogicalName,
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