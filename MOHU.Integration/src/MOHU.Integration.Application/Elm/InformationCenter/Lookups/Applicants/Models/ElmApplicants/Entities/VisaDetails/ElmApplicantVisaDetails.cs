using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.VisaDetails;

public class ElmApplicantVisaDetails
{
    private ElmApplicantVisaDetails(ApplicantResponse applicant)
    {
        HajVisaPermitStatus = applicant.AdHajVisaPermitStatus;
    }

    public string? HajVisaPermitStatus { get; init; }
    
    public static ElmApplicantVisaDetails Create(ApplicantResponse applicant) => new(applicant);
    
    internal IndividualVisaDetails ToIndividualInformation() => IndividualVisaDetails
        .Create(hajVisaPermitStatus:  HajVisaPermitStatus);
}