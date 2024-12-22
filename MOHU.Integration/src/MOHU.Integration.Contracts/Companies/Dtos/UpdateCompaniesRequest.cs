namespace MOHU.Integration.Contracts.Companies.Dtos;

public record UpdateCompaniesRequest(
    string Key,
    int SicCode,
    UpdateCompaniesKeyType KeyType = UpdateCompaniesKeyType.CompanyName,
    string? NewCompanyName = null,
    string? LicenseNumber = null);

public enum UpdateCompaniesKeyType
{
    CompanyName,
    Id
}

// {
//     public string CompanyName { get; set; }
//
//     public string NewCompanyName { get; set; }
//
//     public long SicCode { get; set; }
//
//     public string? LicenseNumber { get; set; }
// }