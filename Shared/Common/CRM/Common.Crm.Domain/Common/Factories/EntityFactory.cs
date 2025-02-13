namespace Common.Crm.Domain.Common.Factories;

public static class EntityFactory
{
    public static Entity CreateNew(
        string entityLogicalName,
        Dictionary<string, object>? attributes = null)
    {
        var createdEntity = new Entity(entityLogicalName);

        if (attributes is null)
        {
            return createdEntity;
        }
        
        foreach (var entry in attributes)
        {
            createdEntity[entry.Key] = entry.Value;
        }

        return createdEntity;
    }
}