using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

internal class ElmInformationCenterApplicantDataFileClient : IElmInformationCenterApplicantDataClient
{
    private const string FilePath = "Files/Elm/InformationCenter/Lookups/Applicants/Data/applicantData.json";
    
    public ErrorOr<List<ElmApplicant>> GetAll(ElmFilterRequest? request = null) =>
        GetDataFromSource()
            .Then(x => x.EnsureNotNull())
            .Then(x => x.EnsureSuccessResult())
            .Then(x => x.Select(y => y.ToElmApplicant()).ToList());

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
}