using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.Common.ElmReferencedEntities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Services;


public interface IElmSyncService<TCrmEntity>
    where TCrmEntity : CrmEntity
{
    Task<List<TCrmEntity>> Sync();
}