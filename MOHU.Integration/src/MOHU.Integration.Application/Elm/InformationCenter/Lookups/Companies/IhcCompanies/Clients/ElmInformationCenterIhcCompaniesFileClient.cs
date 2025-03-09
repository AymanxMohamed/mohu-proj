using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Dtos.Responses;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Clients;

internal class ElmInformationCenterIhcCompaniesFileClient : IElmInformationCenterIhcCompaniesClient
{
    private const string FilePath = "Files/Elm/InformationCenter/Lookups/Applicants/Data/ihc-companies.json";

    public ErrorOr<List<ElmIhcCompanyResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetDataFromSource()
            .Then(x => x.EnsureNotNull())
            .Then(x => x.EnsureSuccessResult());

    private static ErrorOr<ElmInformationCenterResponseRoot<List<ElmIhcCompanyResponse>>?> GetDataFromSource()
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
            return JsonConvert.DeserializeObject<ElmInformationCenterResponseRoot<List<ElmIhcCompanyResponse>>>(data);
        }
        catch (Exception ex)
        {
            return Error.Failure("JsonParseError", $"Failed to parse JSON: {ex.Message}");
        }
    }
}