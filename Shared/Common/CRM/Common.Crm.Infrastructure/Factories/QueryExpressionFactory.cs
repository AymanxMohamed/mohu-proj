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
        params  ConditionExpression[] conditionExpressions)
    {
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
        };
            
        queryExpression.LinkEntities.AddRange(linkEntities);
            
        return queryExpression;
    }
}