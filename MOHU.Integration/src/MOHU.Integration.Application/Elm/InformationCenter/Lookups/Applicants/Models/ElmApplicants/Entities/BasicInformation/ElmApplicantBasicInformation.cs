using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Enums;

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

    public ElmGender Gender { get; init; }
    
    public ElmMartialStatus MartialStatus { get; init; }
    
    public static ElmApplicantBasicInformation Create(ApplicantResponse applicant) => new(applicant);
}