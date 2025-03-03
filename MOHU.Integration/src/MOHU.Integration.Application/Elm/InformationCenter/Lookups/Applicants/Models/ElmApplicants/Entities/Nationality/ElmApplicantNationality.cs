namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

public class ElmApplicantNationality
{
    private ElmApplicantNationality(ApplicantResponse applicant)
    {
        CurrentNationalityId = applicant.AdCurrentNationalityId;
        ResidenceCountryId = applicant.AdResidenceCountryId;
    }
    
    public long? CurrentNationalityId { get; init; }
    
    public long? ResidenceCountryId { get; init; }

    public static ElmApplicantNationality Create(ApplicantResponse applicant) => new(applicant);
}