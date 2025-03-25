using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Features.Countries.Services;
using MOHU.Integration.Application.Features.Nationalities.Services;
using MOHU.Integration.Domain.Features.Countries;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

internal class ElmInformationCenterApplicantDataClient(
    IElmInformationCenterClient client,
    ICountriesService countriesService,
    INationalitiesService nationalitiesService,
    ElmInformationCenterApiSettings settings) 
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: "applicant-data",
            client,
            settings), 
        IElmInformationCenterApplicantDataClient
{
    public ErrorOr<List<ElmApplicant>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();
        
        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ApplicantResponse.Timestamp).ToLower()));
        
        return GetLookups<List<ApplicantResponse>>(request)
                .Then(applicants => GetDependentCountries(applicants)
                    .Then(countries => GetDependentNationalities(applicants)
                        .Then(nationalities => (applicants, countries, nationalities))))
                .Then(x => x.applicants
                    .Select(y => y.ToElmApplicant(x.countries, x.nationalities))
                    .ToList());
    }

    private ErrorOr<Dictionary<int, Country>> GetDependentCountries(List<ApplicantResponse> applicants)
    {
        var ids = applicants
            .SelectMany(x => new[] { x.AdResidenceCountryId })
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .ToHashSet();
        
        return countriesService
            .GetCrmEntitiesByElmReferenceIds([..ids])
            .ToDictionary(x => (int)x.ElmReferenceId!);
    }

    private ErrorOr<Dictionary<int, Country>> GetDependentNationalities(List<ApplicantResponse> applicants)
    {
        var ids = applicants
            .SelectMany(x => new[] { x.AdCurrentNationalityId, x.AdPreviousNationalityId })
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .ToHashSet();
        
        return nationalitiesService
            .GetCrmEntitiesByElmReferenceIds([..ids])
            .ToDictionary(x => (int)x.ElmReferenceId!);
    }
}