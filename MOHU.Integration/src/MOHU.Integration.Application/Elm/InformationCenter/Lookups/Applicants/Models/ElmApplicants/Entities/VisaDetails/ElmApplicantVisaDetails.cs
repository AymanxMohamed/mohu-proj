namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

public class ElmApplicantVisaDetails
{
    private ElmApplicantVisaDetails(ApplicantResponse applicant)
    {
        HajVisaPermitStatus = applicant.AdHajVisaPermitStatus;
    }

    public string? HajVisaPermitStatus { get; init; }
    
    public static ElmApplicantVisaDetails Create(ApplicantResponse applicant) => new(applicant);
}