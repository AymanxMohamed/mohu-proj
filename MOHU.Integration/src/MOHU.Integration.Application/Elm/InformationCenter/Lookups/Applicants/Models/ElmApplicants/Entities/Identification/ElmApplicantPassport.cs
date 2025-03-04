using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

public class ElmApplicantPassport : ElmApplicantId
{
    protected ElmApplicantPassport(
        string number,
        DateTime? issuanceDate,
        DateTime? expiryDate,
        string? issuancePlace,
        string? passportType) 
        : base(number, issuanceDate, expiryDate, Contracts.Enum.IdType.Passport)
    {
        IssuancePlace = issuancePlace;
        PassportType = passportType;
    }

    public string? IssuancePlace { get; init; }
    
    public string? PassportType { get; init; }

    public static ElmApplicantPassport? Create(ApplicantResponse applicant)
        => applicant.AdPassportNo is null
            ? null 
            : new ElmApplicantPassport(
                applicant.AdPassportNo,
                applicant.AdPassportIssueDate,
                applicant.AdPassportExpiryDate,
                applicant.AdPassportIssuePlace,
                applicant.AdPassportTypeId);
}