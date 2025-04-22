using Common.Crm.Domain.Common.ColumnSets.Constants;

namespace Common.Crm.Infrastructure.Factories;

public static class QueryExpressionFactory
{
    public static QueryExpression CreateQueryExpression(
        string entityLogicalName, 
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null,
        params  ConditionExpression[] conditionExpressions)
    {
        paginationParameters ??= CrmPaginationParameters.Create();
        
        columnSet ??= ColumnSetConstants.AllColumns;

        filterExpression = FilterExpressionFactory.CreateFilterExpression(
            isOrFilter,
            filterExpression, 
            childFilters, 
            conditionExpressions);

        var queryExpression = new QueryExpression(entityLogicalName)
        {
            ColumnSet = columnSet,
            Criteria = filterExpression,
            PageInfo = new PagingInfo
            {
                PageNumber = paginationParameters.Page,
                Count = paginationParameters.PageSize,
                ReturnTotalRecordCount = true
            }
        };
        
        queryExpression.Orders.AddRange(orderExpressions ?? []);
        queryExpression.LinkEntities.AddRange(linkEntities);
            
        return queryExpression;
    }
}