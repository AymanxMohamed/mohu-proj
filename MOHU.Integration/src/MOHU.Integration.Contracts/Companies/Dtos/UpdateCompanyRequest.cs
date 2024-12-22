using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Companies.Enums;

namespace MOHU.Integration.Contracts.Companies.Dtos;

public record UpdateCompanyRequest(
    string Key,
    int SicCode,
    UpdateCompaniesKeyType KeyType = UpdateCompaniesKeyType.CompanyName,
    string? NewCompanyName = null,
    string? LicenseNumber = null)
{
    public Entity Update(List<Entity> entities, Action<string> fireNotFoundException)
    {
        var company = entities.FirstOrDefault(x => x.GetAttributeValue<string>(GetKeyLogicalName()) == Key);

        if (company == null)
        {
            fireNotFoundException($"No company found with the specified key: {Key}");
        }

        if (!string.IsNullOrWhiteSpace(NewCompanyName) && company!.Attributes.Contains("ldv_name"))
        {
            company["ldv_name"] = NewCompanyName;
        }
        
        company!["ldv_siccode"] = SicCode;

        if (!string.IsNullOrWhiteSpace(LicenseNumber))
        {
            company["ldv_licensenumber"] = LicenseNumber;
        }

        return company;
    }
    
    public static ColumnSet GetColumnSet() => new("ldv_name", "ldv_siccode", "ldv_licensenumber");

    public FilterExpression ToFilterExpression() => new()
    {
        Conditions =
        {
            new ConditionExpression(
                GetKeyLogicalName(),
                ConditionOperator.Equal,
                Key)
        }
    };
    
    private string GetKeyLogicalName() => KeyType switch
    {
        UpdateCompaniesKeyType.CompanyName => "ldv_name",
        UpdateCompaniesKeyType.Id => "ldv_companyid",
        _ => throw new ArgumentOutOfRangeException()
    };
}