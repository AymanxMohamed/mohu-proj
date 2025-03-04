using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

public class ElmApplicantIqama : ElmApplicantId
{
    protected ElmApplicantIqama(
        string number,
        DateTime? issuanceDate,
        DateTime? expiryDate,
        string? issuanceBy) 
        : base(number, issuanceDate, expiryDate, Contracts.Enum.IdType.Accommodation)
    {
        IssuanceBy = issuanceBy;
    }

    public string? IssuanceBy { get; init; }

    public static ElmApplicantIqama? Create(ApplicantResponse applicant)
        => applicant.AdIqamaNo is null
            ? null 
            : new ElmApplicantIqama(
                applicant.AdIqamaNo,
                applicant.AdIqamaIssueDate,
                applicant.AdIqamaExpiryDate,
                applicant.AdIqamaIssuedBy);
}