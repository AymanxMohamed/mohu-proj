using MOHU.Integration.Domain.Features.Common.CrmEntities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Services;


public interface IElmSyncService<TCrmEntity>
    where TCrmEntity : CrmEntity
{
    Task<List<TCrmEntity>> Sync();
}