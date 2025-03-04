using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

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

    public ElmApplicantId? MainIdentification => (ElmApplicantId?)CitizenId ?? (ElmApplicantId?)Iqama ?? Passport;

    public static ElmApplicantIdentification Create(ApplicantResponse applicant) => new(applicant);

    internal IndividualIdentification ToIndividualInformation() => IndividualIdentification
        .Create(
            idType:  MainIdentification?.IdType,
            idNumber: MainIdentification?.Number,
            passportNumber: Passport?.Number);
}