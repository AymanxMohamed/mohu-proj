using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Enums;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Dtos.Responses;

public class ElmDhcHajCompanyResponse : ElmCompanyResponse
{
    [JsonProperty("dhcTypeId")]
    public int? DhcTypeId { get; init; }

    [JsonProperty("hmId")]
    public int? HmId { get; init; }
    
    public override Company ToCrmEntity(EntityReference? id = null)
    {
        var company = base.ToCrmEntity(id);

        company.SetTypeInformation(companyType: ElmCompanyTypeEnum.DhcHajCompany);
        
        return company;
    }
}