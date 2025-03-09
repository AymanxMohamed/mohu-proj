using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Enums;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Dtos.Responses;

public partial class ElmHajMissionCompanyResponse : ElmEntity<Company>
{
    [JsonProperty("businessId")]
    public int BusinessId { get; set; }
    
    [JsonProperty("nameAr")]
    public string? ArabicName { get; set; }

    [JsonProperty("nameEn")]
    public string? EnglishName { get; set; }

    [JsonProperty("hmState")]
    public string? State { get; set; }
    
    [JsonProperty("timestamp")]
    public string? TimeStamp { get; set; }

    [JsonProperty("countryId")]
    public int? CountryId { get; init; }

    public override Company ToCrmEntity(EntityReference? id = null)
    {
        return Company
            .Create(
                id: id,
                elmReferenceId: Id,
                elmCompanyType: ElmCompanyTypeEnum.HajMissionCompany,
                serviceType: ServiceTypeEnum.HajOnly,
                organizationArabicName: ArabicName,
                organizationEnglishName: EnglishName,
                sicCode: Id.ToString());
    }
}