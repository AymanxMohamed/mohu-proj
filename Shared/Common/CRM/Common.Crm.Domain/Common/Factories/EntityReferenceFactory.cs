namespace Common.Crm.Domain.Common.Factories;

public static class EntityReferenceFactory
{
    public static EntityReference Create(string entityLogicalName, Guid recordId) => new(entityLogicalName, recordId);
    public static EntityReference Create(string entityLogicalName) => new(entityLogicalName, Guid.NewGuid());
}