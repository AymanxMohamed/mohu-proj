using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

public class ElmApplicantCitizenId : ElmApplicantId
{
    private ElmApplicantCitizenId(
        string number,
        DateTime? issuanceDate,
        DateTime? expiryDate,
        string? source,
        long? issuanceDateHij,
        long? expiryDateHij) 
        : base(number, issuanceDate, expiryDate, Contracts.Enum.IdType.NationalIdentity)
    {
        Source = source;
        IssuanceDateHij = issuanceDateHij;
        ExpiryDateHij = expiryDateHij;
    }

    public string? Source { get; init; }

    public long? IssuanceDateHij { get; init; }

    public long? ExpiryDateHij { get; init; }

    public static ElmApplicantCitizenId? Create(ApplicantResponse applicant)
        => applicant.AdIdNo is null
            ? null 
            : new ElmApplicantCitizenId(
                applicant.AdIdNo,
                applicant.AdIdIssueDate,
                applicant.AdIdExpiryDate,
                applicant.AddIdSource,
                applicant.AdIdIssueDateHij,
                applicant.AdIdExpiryDateHij);
}