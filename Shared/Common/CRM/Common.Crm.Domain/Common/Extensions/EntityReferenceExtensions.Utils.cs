namespace Common.Crm.Domain.Common.Extensions;

public static class EntityReferenceExtensions
{
    public static string GetPrimaryKey(this EntityReference reference)
    {
        return reference.LogicalName + "id";
    }

    public static EntityReference GetEntityReference(this Entity entity, string logicalName)
    {
        return entity.GetAttributeValue<EntityReference>(logicalName);
    }

    public static Entity ToEntity(this EntityReference reference)
    {
        return new Entity
        {
            Id = reference.Id,
            LogicalName = reference.LogicalName,
        };
    }
}