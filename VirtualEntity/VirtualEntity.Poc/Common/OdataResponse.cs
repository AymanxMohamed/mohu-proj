using System.Text.Json.Serialization;

namespace VirtualEntity.Poc.Common;

public class OdataResponse<TResponse>
{
    [JsonPropertyName("value")]
    public List<TResponse> Value { get; init; } = [];
}