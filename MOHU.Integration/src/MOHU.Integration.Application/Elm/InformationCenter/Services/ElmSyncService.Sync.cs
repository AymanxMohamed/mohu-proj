using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Common.CrmEntities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Services;

public partial class ElmSyncService<TElmClient, TElmEntity, TCrmEntity>(
    IConfigurationService configurationService,
    TElmClient client,
    ICrmContext crmContext,
    string entityLogicalName,
    Func<Entity, TCrmEntity> factory,
    Func<TCrmEntity, Entity> entityConverter,
    Func<TElmEntity, TCrmEntity, bool> comparisonPredicate) : IElmSyncService<TCrmEntity>
    where TElmClient : IElmEntityClient<TElmEntity>
    where TElmEntity : ElmEntity<TCrmEntity>
    where TCrmEntity : CrmEntity
{
    private TElmClient _client = client;
    private readonly IGenericRepository _genericRepository =
        GenericRepositoriesFactory.CreateGenericRepository(crmContext.ServiceClient);

    public async Task<List<TCrmEntity>> Sync()
    {
        var lastSyncedPage = await configurationService.GetConfigurationValueAsync<int>(GetSyncKey());

        ++lastSyncedPage;
        
        List<TCrmEntity> crmSyncedEntities = [];
        
        while (lastSyncedPage != 0)
        {
            var (nextPage, result) = await SyncPageAsync(lastSyncedPage);
            crmSyncedEntities.AddRange(result);
            lastSyncedPage = nextPage;
        }

        return crmSyncedEntities;
    }

    private async Task<(int NextPage, List<TCrmEntity> Result)> SyncPageAsync(int page)
    {
        var elmEntities = _client
            .GetAll(ElmFilterRequest.Create(page: page))
            .ToValueOrException();
        
        if (elmEntities.Count == 0)
        {
            return (NextPage: 0, Result: []);
        }
        
        var existingCrmEntities = GetCrmEntitiesByElmReferenceIds(
                elmEntities.Select(x => x.Id).ToList());
        
        foreach (var elmEntity in elmEntities)
        {
            SyncCrmEntity(elmEntity, existingCrmEntities);
        }
        
        _genericRepository.Commit();

        if (elmEntities.Count != ElmFilterRequest.DefaultPageSize)
        {
            return (NextPage: 0, Result: elmEntities.Select(x => x.ToCrmEntity()).ToList());
        }     
        
        await configurationService
            .SetOrUpdateConfigurationValueAsync(
                key: GetSyncKey(),
                value: page.ToString());
        
        return (NextPage: page + 1, Result: elmEntities.Select(x => x.ToCrmEntity()).ToList());
    }

    private void SyncCrmEntity(TElmEntity elmEntity, List<TCrmEntity> existingIndividuals)
    {
        var existingCrmEntity = existingIndividuals.FirstOrDefault(x => comparisonPredicate(elmEntity, x));
        
        if (existingCrmEntity is not null)
        {
            _genericRepository.Update(entityConverter(elmEntity.ToCrmEntity(existingCrmEntity.Id)));
            return;
        }

        var entity = elmEntity.ToCrmEntity();
        
        _genericRepository.Create(entityConverter(entity));
    }
}