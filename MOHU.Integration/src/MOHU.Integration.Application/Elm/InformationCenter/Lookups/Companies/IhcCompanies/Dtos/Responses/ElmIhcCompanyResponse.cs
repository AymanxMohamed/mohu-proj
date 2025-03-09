using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Enums;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Dtos.Responses;

public class ElmIhcCompanyResponse : ElmCompanyResponse
{
    public override Company ToCrmEntity(EntityReference? id = null)
    {
        var company = base.ToCrmEntity(id);

        company.SetTypeInformation(companyType: ElmCompanyTypeEnum.IhcHajCompany);
        
        return company;
    }
}