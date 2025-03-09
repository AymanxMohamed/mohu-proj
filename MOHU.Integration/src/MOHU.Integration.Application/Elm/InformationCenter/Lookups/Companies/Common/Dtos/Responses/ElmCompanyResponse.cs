using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Enums;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;

public partial class ElmCompanyResponse : ElmEntity<Company>
{
    [JsonProperty("businessId")]
    public int BusinessId { get; set; }
    
    [JsonProperty("companyNameAr")]
    public string? ArabicName { get; set; }

    [JsonProperty("companyNameEn")]
    public string? EnglishName { get; set; }

    [JsonProperty("hcState")]
    public string? State { get; set; }
    
    [JsonProperty("timestamp")]
    public string? TimeStamp { get; set; }

    public override Company ToCrmEntity(EntityReference? id = null)
    {
        return Company
            .Create(
                id: id,
                elmReferenceId: Id,
                elmCompanyType: ElmCompanyTypeEnum.SpcCompany,
                serviceType: ServiceTypeEnum.HajOnly,
                organizationArabicName: ArabicName,
                organizationEnglishName: EnglishName,
                sicCode: Id.ToString());
    }
}