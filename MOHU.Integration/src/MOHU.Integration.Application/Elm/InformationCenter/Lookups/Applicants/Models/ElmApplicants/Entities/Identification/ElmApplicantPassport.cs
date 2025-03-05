using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Features.Individuals.Enums;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

public class ElmApplicantPassport : ElmApplicantId
{
    private ElmApplicantPassport(
        string number,
        DateTime? issuanceDate,
        DateTime? expiryDate,
        string? issuancePlace,
        string? passportType) 
        : base(number, issuanceDate, expiryDate, IdTypeEnum.Passport)
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