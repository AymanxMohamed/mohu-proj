using Common.Crm.Domain.Common.Constants;
using Common.Crm.Domain.Common.Enums;
using Common.Crm.Domain.Common.OptionSets.Constants;

namespace Common.Crm.Infrastructure.Factories;

public static class ConditionExpressionFactory
{
    public static ConditionExpression CreateFilterByEntityCondition(
        EntityReference reference, 
        string lookupLogicalName)
    {
        return new ConditionExpression(
            lookupLogicalName, 
            ConditionOperator.Equal, 
            reference.Id);
    }

    public static ConditionExpression CreateIsnullCondition(string attributeName)
    {
        return new ConditionExpression(attributeName, ConditionOperator.Null);
    }

    public static ConditionExpression CreateConditionExpression(
        string columnLogicalName,
        ConditionOperator conditionOperator,
        object? value = null) => 
        value is null 
            ?  new ConditionExpression(columnLogicalName, conditionOperator)
            :  new ConditionExpression(columnLogicalName, conditionOperator, value);
    
    public static ConditionExpression CreateStatusCondition(StatusEnum status) =>
        new(
            CommonConstants.Fields.Status,
            ConditionOperator.Equal,
            (int)status);
}