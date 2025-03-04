namespace Common.Crm.Infrastructure.Common.Extensions;

public static partial class EntityExtensions
{
    public static ConditionExpression GetFilterByConditionExpression(
        this Entity entity, 
        string lookupLogicalName)
    {
            
        return entity.ToEntityReference().GetFilterByConditionExpression(lookupLogicalName);
    }

    public static ConditionExpression GetFilterByConditionExpression(
        this EntityReference entityReference, 
        string lookupLogicalName)
    {
        return ConditionExpressionFactory.CreateFilterByEntityCondition(entityReference, lookupLogicalName);
    }
}