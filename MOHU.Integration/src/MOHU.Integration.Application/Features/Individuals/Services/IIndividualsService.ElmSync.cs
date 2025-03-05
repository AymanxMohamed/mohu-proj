using MOHU.Integration.Application.Elm.InformationCenter.Common.Constants;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Features.Individuals.Services;

public partial class IndividualsService
{
    public async Task<List<Individual>> SyncWithElm()
    {
        var lastSyncedPage = await configurationService
            .GetConfigurationValueAsync<int>(
                key: ElmConstants.ConfigurationKeys.SyncKeys.LastSyncedApplicantDataPage);

        ++lastSyncedPage;
        
        List<Individual> individuals = [];
        
        while (lastSyncedPage != 0)
        {
            var (nextPage, result) = await SyncPageAsync(lastSyncedPage);
            individuals.AddRange(result);
            lastSyncedPage = nextPage;
        }

        return individuals;
    }

    private async Task<(int NextPage, List<Individual> Result)> SyncPageAsync(int page)
    {
        var elmApplicants = client
            .GetAll(ElmFilterRequest.Create(page: page))
            .ToValueOrException();
        
        if (elmApplicants.Count == 0)
        {
            return (NextPage: 0, Result: []);
        }
        
        var existingIndividuals = GetByElmReferenceIds(elmApplicants.Select(x => x.Id)
            .ToList())
            .ToDictionary(x => (int)x.IntegrationDetails.ElmReferenceId!);
        
        foreach (var elmApplicant in elmApplicants)
        {
            SyncIndividual(elmApplicant, existingIndividuals);
        }
        
        _genericRepository.Commit();

        if (elmApplicants.Count != ElmFilterRequest.DefaultPageSize)
        {
            return (NextPage: 0, Result: elmApplicants.Select(x => x.ToCrmEntity()).ToList());
        }     
        
        await configurationService
            .SetOrUpdateConfigurationValueAsync(
                key: ElmConstants.ConfigurationKeys.SyncKeys.LastSyncedApplicantDataPage,
                value: page.ToString());
        
        return (NextPage: page + 1, Result: elmApplicants.Select(x => x.ToCrmEntity()).ToList());
    }

    private void SyncIndividual(ElmApplicant elmApplicant, Dictionary<int, Individual> existingIndividuals)
    {
        if (existingIndividuals.TryGetValue(elmApplicant.Id, out var existingIndividual))
        {
            _genericRepository.Update(elmApplicant.ToEntity(existingIndividual.Id));
            return;
        }

        var entity = elmApplicant.ToEntity();
        
        _genericRepository.Create(entity);
    }
}