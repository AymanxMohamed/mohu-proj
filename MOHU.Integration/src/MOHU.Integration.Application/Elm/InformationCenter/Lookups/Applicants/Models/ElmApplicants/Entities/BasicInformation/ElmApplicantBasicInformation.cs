using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Features.Individuals.Entities;
using MOHU.Integration.Domain.Features.Individuals.Enums;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BasicInformation;

public class ElmApplicantBasicInformation
{
    private ElmApplicantBasicInformation(ApplicantResponse applicant)
    {
        EnglishName = ElmApplicantName.CreateEnglishName(applicant);
        ArabicName = ElmApplicantName.CreatArabicName(applicant);
        Gender = applicant.AdGender;
        MartialStatus = applicant.AdMaritalStatusId;
    }
    
    public ElmApplicantName EnglishName { get; init; }
    public ElmApplicantName ArabicName { get; init; }

    public GenderEnum Gender { get; init; }
    
    public MartialStatusEnum MartialStatus { get; init; }
    
    public static ElmApplicantBasicInformation Create(ApplicantResponse applicant) => new(applicant);

    internal IndividualBasicInformation ToIndividualInformation() => IndividualBasicInformation
        .Create(
            firstName: EnglishName.FirstName,
            lastName: EnglishName.FamilyName,
            englishName: EnglishName.FullName,
            arabicName: ArabicName.FullName,
            gender: Gender,
            martialStatus: MartialStatus);
}