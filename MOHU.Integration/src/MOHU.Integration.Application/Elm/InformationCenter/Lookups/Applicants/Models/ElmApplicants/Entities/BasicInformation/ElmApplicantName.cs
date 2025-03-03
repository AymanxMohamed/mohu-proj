using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BasicInformation;

public class ElmApplicantName
{
    public ElmApplicantName(string? firstName, string? fatherName, string? grandFatherName, string? familyName, string? fullName)
    {
        FirstName = firstName;
        FatherName = fatherName;
        GrandFatherName = grandFatherName;
        FamilyName = familyName;
        FullName = fullName;
    }

    public string? FirstName { get; init; }
    
    public string? FatherName { get; init; }

    public string? GrandFatherName { get; init; }

    public string? FamilyName { get; init; }

    public string? FullName { get; init; }

    public static ElmApplicantName CreateEnglishName(ApplicantResponse applicant) =>
        new(applicant.AdFirstNameEn, 
            applicant.AdFatherNameEn, 
            applicant.AdGrandFatherNameEn, 
            applicant.AdFamilyNameEn, 
            applicant.AdFullNameEn);
    
    public static ElmApplicantName CreatArabicName(ApplicantResponse applicant) =>
        new(applicant.AdFirstNameAr, 
            applicant.AdFatherNameAr, 
            applicant.AdGrandFatherNameAr, 
            applicant.AdFamilyNameAr, 
            applicant.AdFullNameAr);
}