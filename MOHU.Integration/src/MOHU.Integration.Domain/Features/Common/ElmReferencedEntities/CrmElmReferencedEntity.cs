using MOHU.Integration.Domain.Features.Common.Constants;
using MOHU.Integration.Domain.Features.Common.CrmEntities;

namespace MOHU.Integration.Domain.Features.Common.ElmReferencedEntities;

public partial class CrmElmReferencedEntity : CrmEntity, IElmReferencedEntity
{
    protected CrmElmReferencedEntity(Entity entity)
        : this(
            id: entity.ToEntityReference(),
            elmReferenceId: entity.GetAttributeValue<int>(CommonConstants.Fields.IntegrationDetails.ElmReferenceId))
    {
    }
    
    protected CrmElmReferencedEntity(EntityReference id, int? elmReferenceId)
        : base(id)
    {
        ElmReferenceId = elmReferenceId;
    }

    public int? ElmReferenceId { get; init; }

    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        entity.AssignIfNotNull(
            CommonConstants.Fields.IntegrationDetails.ElmReferenceId, 
            ElmReferenceId);
        
        return entity;
    }
}