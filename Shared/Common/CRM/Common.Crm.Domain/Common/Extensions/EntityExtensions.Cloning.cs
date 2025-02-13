using Common.Crm.Domain.Common.Constants;

namespace Common.Crm.Domain.Common.Extensions;

public static partial class EntityExtensions
{
    
    public static void MapEntitiesMatchingCriteria(
        this IEnumerable<Entity> sourceCompanies, 
        List<Entity> destinationCompanies,
        Func<Entity, Entity, bool> predicate)
    {
        foreach (var sourceCompany in sourceCompanies)
        {
            var matchingCompanies = destinationCompanies
                .Where(destinationCompany => sourceCompany.LogicalName == destinationCompany.LogicalName &&
                                             predicate(sourceCompany, destinationCompany));
            
            sourceCompany.MapEntityFieldsIfNullInDestination(matchingCompanies);
        }
    }
    
    public static void MapEntityFieldsIfNullInDestination(this Entity sourceEntity, IEnumerable<Entity> destinationEntities)
    {
        foreach (var destinationEntity in destinationEntities)
        {
            foreach (var field in sourceEntity.Attributes.Keys)
            {
                if (!sourceEntity.HasAttributeValue(field) || destinationEntity.HasAttributeValue(field))
                {
                    continue;
                }
                
                destinationEntity.Attributes[field] = sourceEntity.Attributes[field];
            }
        }
    }
    
    public static Entity Clone(this Entity entity, List<string>? excludedFields = null, Guid? id = null)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }
            
        excludedFields ??= [];
            
        excludedFields.AddRange(CommonConstants.BpfFields);
        excludedFields.AddRange(CommonConstants.StatusFields);
            
        excludedFields = excludedFields.Distinct().ToList();
            
        var clonedEntity = new Entity(entity.LogicalName);
            
        foreach (var attribute in entity.Attributes)
        {
            var attributeName = attribute.Key;
            if (!excludedFields.Contains(attributeName))
            {
                clonedEntity[attributeName] = entity[attributeName];
            }
        }
            
        clonedEntity.ReplaceId(id);
            
        return clonedEntity;
    }
        
        
    private static List<string> AddPrimaryKeyToList(this Entity entity, List<string>? excludedFields = null)
    {
        var entityPrimaryKey = entity.GetPrimaryKey();
            
        if (excludedFields is null)
        {
            excludedFields = [entityPrimaryKey];
        }
        else
        {
            excludedFields.Add(entityPrimaryKey);
        }

        return excludedFields;
    }
        
    public static Entity GetEmptyUpdateRequest(this Entity entity)
    {
        var updateEntity = new Entity(entity.LogicalName)
        {
            Id = entity.Id
        };

        return updateEntity;
    }

}