using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Companies.Enums;
using MOHU.Integration.Domain.Features.Companies;

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

        if (!string.IsNullOrWhiteSpace(NewCompanyName))
        {
            company![CompaniesConstants.Fields.Name] = NewCompanyName;
        }
        
        company![CompaniesConstants.Fields.SicCode] = SicCode;

        if (!string.IsNullOrWhiteSpace(LicenseNumber))
        {
            company[CompaniesConstants.Fields.LicenseNumber] = LicenseNumber;
        }

        return company;
    }
    
    public static ColumnSet GetColumnSet() => new(
        CompaniesConstants.Fields.Name, 
        CompaniesConstants.Fields.SicCode, 
        CompaniesConstants.Fields.LicenseNumber);

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
        UpdateCompaniesKeyType.CompanyName => CompaniesConstants.Fields.Name,
        UpdateCompaniesKeyType.Id => CompaniesConstants.Fields.Id,
        _ => throw new ArgumentOutOfRangeException()
    };
}