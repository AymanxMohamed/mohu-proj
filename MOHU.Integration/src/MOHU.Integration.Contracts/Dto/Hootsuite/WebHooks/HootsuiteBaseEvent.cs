using Newtonsoft.Json;

namespace MOHU.Integration.WebApi.Features.Hootsuite.Common;

public class HootsuiteBaseEvent<TData>
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("idempotencyKey")]
    public Guid IdempotencyKey { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("data")]
    public TData Data { get; set; }
}