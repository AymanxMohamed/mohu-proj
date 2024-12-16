using System.Text.Json.Serialization;

namespace VirtualEntity.Poc.Common;

public class OdataResponse<TResponse>
{
    public List<TResponse> Value { get; init; } = [];
}