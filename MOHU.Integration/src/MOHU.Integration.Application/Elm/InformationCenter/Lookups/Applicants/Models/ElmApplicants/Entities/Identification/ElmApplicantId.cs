namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

public class ElmApplicantId
{
    protected ElmApplicantId(string number, DateTime? issuanceDate, DateTime? expiryDate)
    {
        Number = number;
        IssuanceDate = issuanceDate;
        ExpiryDate = expiryDate;
    }

    public string Number { get; init; }
    
    public DateTime? IssuanceDate { get; init; }

    public DateTime? ExpiryDate { get; init; }
}