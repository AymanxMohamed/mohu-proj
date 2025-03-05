using MOHU.Integration.Domain.Features.Common.CrmEntities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;

public abstract class ElmEntity<TCrmEntity>
    where TCrmEntity : CrmEntity
{
    public int Id { get; set; }
    
    public Entity ToEntity(EntityReference? id = null) => ToCrmEntity(id).ToCrmEntity();
    
    public abstract TCrmEntity ToCrmEntity(EntityReference? id = null);
}