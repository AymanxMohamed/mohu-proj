namespace Common.Crm.Infrastructure.Factories;

public class FilterExpressionFactory
{
    public static FilterExpression CreateFilterExpression(
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        params ConditionExpression[] conditionExpressions)

    {
        filterExpression ??= new FilterExpression
        {
            FilterOperator = isOrFilter == true ? LogicalOperator.Or : LogicalOperator.And
        };

        if (conditionExpressions.Length == 0 && childFilters is null)
        {
            return filterExpression;
        }

        foreach (var condition in conditionExpressions)
        {
            filterExpression.AddCondition(condition);
        }

        if (childFilters == null) return filterExpression;
            
        foreach (var filter in childFilters)
        {
            filterExpression.AddFilter(filter);
        }

        return filterExpression;
    }
}