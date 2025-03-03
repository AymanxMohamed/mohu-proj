namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

public class ElmApplicantIdentification
{
    private ElmApplicantIdentification(ApplicantResponse applicant)
    {
        CitizenId = ElmApplicantCitizenId.Create(applicant);
        Iqama = ElmApplicantIqama.Create(applicant);
        Passport = ElmApplicantPassport.Create(applicant);
    }
    
    public ElmApplicantCitizenId? CitizenId { get; init; }
    
    public ElmApplicantIqama? Iqama { get; init; }
    
    public ElmApplicantPassport? Passport { get; init; }

    public static ElmApplicantIdentification Create(ApplicantResponse applicant) => new(applicant);
}