namespace Common.Crm.Domain.Common.Extensions
{
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
    }
}