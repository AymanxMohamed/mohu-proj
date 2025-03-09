using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Enums;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Dtos.Responses;

public class ElmSpcCompanyResponse : ElmCompanyResponse
{
    public override Company ToCrmEntity(EntityReference? id = null)
    {
        var company = base.ToCrmEntity(id);

        company.SetTypeInformation(companyType: ElmCompanyTypeEnum.SpcCompany);
        
        return company;
    }
}