using MOHU.Integration.Domain.Features.Common.CrmEntities;

namespace MOHU.Integration.Application.Elm.ServiceDesk.Common.Dtos.Responses;

public abstract class ElmEntity<TCrmEntity>
    where TCrmEntity : CrmEntity
{
    public int Id { get; set; }

    public abstract TCrmEntity ToCrmEntity(EntityReference? id = null);
}