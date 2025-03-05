using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Countries.Enums;
using Newtonsoft.Json;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;

public partial class ElmCountryResponse : ElmEntity<Country>
{
    [JsonProperty("countryNameAr")]
    public string? ArabicName { get; set; }
    
    [JsonProperty("countryNameEn")]
    public string? EnglishName { get; set; }
    
    [JsonProperty("countryMoiNumber")]
    public string? MoiNumber { get; set; }
    
    [JsonProperty("countryMofaNumber")]
    public string? MofaNumber { get; set; }
    
    [JsonProperty("cntryState")]
    public string? State { get; set; }
    
    public override Country ToCrmEntity(EntityReference? id = null)
    {
        return Country
            .Create(
                id: id,
                elmEntityType: ElmEntityTypeEnum.Country,
                elmReferenceId: Id,
                ldvId: Id.ToString(),
                arabicName: ArabicName,
                englishName: EnglishName,
                code: Id.ToString());
    }
}