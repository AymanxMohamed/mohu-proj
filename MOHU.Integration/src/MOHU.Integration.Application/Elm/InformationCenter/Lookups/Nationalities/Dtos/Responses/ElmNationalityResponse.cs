using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Countries.Enums;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Dtos.Responses;

public partial class ElmNationalityResponse : ElmEntity<Country>
{
    [JsonProperty("nationalityNameAr")]
    public string? ArabicName { get; set; }

    [JsonProperty("nationalityNameEn")]
    public string? EnglishName { get; set; }

    [JsonProperty("moiNo")]
    public string? MoiNumber { get; set; }

    [JsonProperty("mofaNo")]
    public string? MofaNumber { get; set; }

    [JsonProperty("state")]
    public string? State { get; set; }

    public override Country ToCrmEntity(EntityReference? id = null)
    {
        return Country
            .Create(
                id: id,
                elmEntityType: ElmEntityTypeEnum.Nationality,
                elmReferenceId: Id,
                ldvId: Id.ToString(),
                arabicName: ArabicName,
                englishName: EnglishName,
                code: Id.ToString());
    }
}