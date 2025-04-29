using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.ServiceDefinitions.Constants;

namespace MOHU.Integration.Domain.Features.ServiceDefinitions;

public class ServiceDefinition : CrmEntity
{
    private ServiceDefinition(Entity entity)
        : base(entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(ServiceDefinition), ServiceDefinitionConstants.LogicalName);
        
        Name = entity.GetAttributeValue<string>(ServiceDefinitionConstants.Fields.Name);
        Code = entity.GetAttributeValue<string>(ServiceDefinitionConstants.Fields.Code);
        EnglishName = entity.GetAttributeValue<string>(ServiceDefinitionConstants.Fields.EnglishName);
        ArabicName = entity.GetAttributeValue<string>(ServiceDefinitionConstants.Fields.ArabicName);
        ParentService = entity.GetAttributeValue<EntityReference>(ServiceDefinitionConstants.Fields.ParentService);
    }
    
    public string? Name { get; init; }

    public string? Code { get; init; }

    public string? EnglishName { get; init; }

    public string? ArabicName { get; init; }

    public EntityReference? ParentService { get; init; }

    public static ServiceDefinition Create(Entity entity) => new(entity);
    
    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        entity.EnsureCanCreateFrom(objectToCreate: nameof(ServiceDefinition), ServiceDefinitionConstants.LogicalName);
        entity.AssignIfNotNull(ServiceDefinitionConstants.Fields.Name, Name);
        entity.AssignIfNotNull(ServiceDefinitionConstants.Fields.Code, Code);
        entity.AssignIfNotNull(ServiceDefinitionConstants.Fields.EnglishName, EnglishName);
        entity.AssignIfNotNull(ServiceDefinitionConstants.Fields.ArabicName, ArabicName);
        entity.AssignIfNotNull(ServiceDefinitionConstants.Fields.ParentService, ParentService);

        return entity;
    }
}