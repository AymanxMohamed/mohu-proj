using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Companies.Enums;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Contracts.Companies.Dtos;

public record UpdateCompanyRequest(
    string Key,
    int SicCode,
    string? NewCompanyName = null,
    string? LicenseNumber = null)
{
    public List<Entity> Update(List<Entity> entities)
    {
        var companies = entities
            .Where(x => x.GetAttributeValue<string>(
                GetKeyLogicalName())
                .Contains(Key, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var company in companies)
        {
            Update(company);
        }

        return companies;
    }
    
    public Entity Update(Entity entity)
    {
        if (!string.IsNullOrWhiteSpace(NewCompanyName))
        {
            entity[CompaniesConstants.Fields.Name] = NewCompanyName;
        }
        
        entity[CompaniesConstants.Fields.SicCode] = SicCode.ToString();

        if (!string.IsNullOrWhiteSpace(LicenseNumber))
        {
            entity[CompaniesConstants.Fields.LicenseNumber] = LicenseNumber;
        }

        return entity;
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
                ConditionOperator.Like,
                $"%{Key}%")
        }
    };
    
    private string GetKeyLogicalName() => UpdateCompaniesKeyType.CompanyName switch
    {
        UpdateCompaniesKeyType.CompanyName => CompaniesConstants.Fields.Name,
        UpdateCompaniesKeyType.Id => CompaniesConstants.Fields.Id,
        _ => throw new ArgumentOutOfRangeException()
    };
}