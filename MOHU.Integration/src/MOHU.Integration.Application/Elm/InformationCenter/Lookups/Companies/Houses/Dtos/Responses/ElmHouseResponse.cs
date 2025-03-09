using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Enums;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Dtos.Responses;

public partial class ElmHouseResponse : ElmEntity<Company>
{
    [JsonProperty("businessId")]
    public int BusinessId { get; set; }

    [JsonProperty("houseCommercialNameAr")]
    public string? ArabicName { get; set; }

    [JsonProperty("houseCommercialNameLa")]
    public string? EnglishName { get; set; }

    [JsonProperty("houseState")]
    public string? State { get; set; }

    [JsonProperty("timestamp")]
    public string? TimeStamp { get; set; }
    
    [JsonProperty("timestampHij")]
    public string? TimeStampHij { get; set; }

    [JsonProperty("houseCityId")]
    public int? CityId { get; init; }
    
    
    public override Company ToCrmEntity(EntityReference? id = null)
    {
        return Company
            .Create(
                id: id,
                elmReferenceId: Id,
                elmCompanyType: ElmCompanyTypeEnum.House,
                serviceType: ServiceTypeEnum.HajOnly,
                organizationArabicName: ArabicName,
                organizationEnglishName: EnglishName,
                sicCode: Id.ToString());
    }
}