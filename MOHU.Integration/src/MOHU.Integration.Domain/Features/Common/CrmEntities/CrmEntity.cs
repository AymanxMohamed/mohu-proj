using Common.Crm.Domain.Common.Factories;

namespace MOHU.Integration.Domain.Features.Common.CrmEntities;

public partial class CrmEntity : ICrmEntity
{
    protected CrmEntity(Entity entity)
        : this(entity.LogicalName, entity.Id)
    {
    }

    protected CrmEntity(
        string entityLogicalName,
        Guid? id = null)
    {
        if (id is not null && id.Equals(Guid.Empty))
        {
            id = Guid.NewGuid();
        }
        
        Id = EntityReferenceFactory.Create(entityLogicalName, id ?? Guid.NewGuid());
    }
    
    protected CrmEntity(EntityReference id)
    {
        Id = id;
    }

    public EntityReference Id { get; init; }
    
    public Entity ToCrmEntity()
    {
        var entity = Id.ToEntity();
        
        return entity;
    }
}