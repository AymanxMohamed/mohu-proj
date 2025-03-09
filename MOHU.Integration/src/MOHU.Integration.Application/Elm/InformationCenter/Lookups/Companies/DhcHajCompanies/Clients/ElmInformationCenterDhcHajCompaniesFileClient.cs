using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Dtos.Responses;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;

internal class ElmInformationCenterDhcHajCompaniesFileClient : IElmInformationCenterDhcHajCompaniesClient
{
    private const string FilePath = "Files/Elm/InformationCenter/Lookups/Applicants/Data/dhc-haj-companies.json";

    public ErrorOr<List<ElmDhcHajCompanyResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetDataFromSource()
            .Then(x => x.EnsureNotNull())
            .Then(x => x.EnsureSuccessResult());

    private static ErrorOr<ElmInformationCenterResponseRoot<List<ElmDhcHajCompanyResponse>>?> GetDataFromSource()
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
            return JsonConvert.DeserializeObject<ElmInformationCenterResponseRoot<List<ElmDhcHajCompanyResponse>>>(data);
        }
        catch (Exception ex)
        {
            return Error.Failure("JsonParseError", $"Failed to parse JSON: {ex.Message}");
        }
    }
}