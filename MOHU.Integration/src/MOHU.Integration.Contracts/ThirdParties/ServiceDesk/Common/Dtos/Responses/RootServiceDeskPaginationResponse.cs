using Newtonsoft.Json;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Common.Dtos.Responses;

public class RootServiceDeskPaginationResponse<TContent> : BaseServiceDeskResponse
{
    [JsonProperty("@start")]
    public int Start { get; set; }

    [JsonProperty("@count")]
    public int Count { get; set; }

    public string ResourceName { get; set; } = null!;

    [JsonProperty("@totalCount")]
    public int TotalCount { get; set; }

    [JsonProperty("content")]
    public List<TContent>? Content { get; set; }
}