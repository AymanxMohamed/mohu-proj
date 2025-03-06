using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using MOHU.Integration.Application.Features.Countries.Services;
using MOHU.Integration.Application.Features.Nationalities.Services;
using MOHU.Integration.Domain.Features.Countries;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

internal class ElmInformationCenterApplicantDataFileClient(
    ICountriesService countriesService,
    INationalitiesService nationalitiesService) : IElmInformationCenterApplicantDataClient
{
    private const string FilePath = "Files/Elm/InformationCenter/Lookups/Applicants/Data/applicantData.json";
    
    public ErrorOr<List<ElmApplicant>> GetAll(ElmFilterRequest? request = null) =>
        GetDataFromSource()
            .Then(x => x.EnsureNotNull())
            .Then(x => x.EnsureSuccessResult())
            .Then(applicants => GetDependentCountries(applicants)
                .Then(countries => GetDependentNationalities(applicants)
                    .Then(nationalities => (applicants, countries, nationalities))))
            .Then(x => x.applicants
                .Select(y => y.ToElmApplicant(x.countries, x.nationalities))
                .ToList());

    private static ErrorOr<ElmInformationCenterResponseRoot<List<ApplicantResponse>>?> GetDataFromSource()
    {
        if (!File.Exists(FilePath))
        {
            return Error.Validation(
                code: "FileNotFound", 
                description: "The applicant data file was not found.");
        }

        try
        {
            var data = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<ElmInformationCenterResponseRoot<List<ApplicantResponse>>>(data);
        } 
        catch (Exception ex)
        {
            return Error.Failure("JsonParseError", $"Failed to parse JSON: {ex.Message}");
        }
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