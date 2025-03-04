using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Domain.Individuals.Enums;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;

public class ElmApplicantId
{
    protected ElmApplicantId(string number, DateTime? issuanceDate, DateTime? expiryDate, IdType idType)
    {
        Number = number;
        IssuanceDate = issuanceDate;
        ExpiryDate = expiryDate;
    }

    public IdTypeEnum IdType { get; set; }

    public string Number { get; init; }
    
    public DateTime? IssuanceDate { get; init; }

    public DateTime? ExpiryDate { get; init; }
}