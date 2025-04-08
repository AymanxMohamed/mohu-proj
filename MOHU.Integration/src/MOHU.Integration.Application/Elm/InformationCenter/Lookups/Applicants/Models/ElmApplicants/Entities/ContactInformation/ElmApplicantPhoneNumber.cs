using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.ContactInformation;

public partial class ElmApplicantPhoneNumber
{
    private ElmApplicantPhoneNumber(string? mobileCountryCode, string? mobileNumber)
    {
        MobileCountryCode = mobileCountryCode;
        MobileNumber = mobileNumber;
    }

    public string? MobileCountryCode { get; init; }

    public string? MobileNumber { get; init; }

    public string FullNumber => $"{MobileCountryCode}{MobileNumber}";

    public static ElmApplicantPhoneNumber Create(ApplicantResponse applicant)
        => new(
            $"+{applicant.AdMobileCountryCode}",
            applicant.AdMobileNumber);
}