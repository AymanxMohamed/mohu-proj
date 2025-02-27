using Core.Domain.ErrorHandling.Extensions;
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;

public class ElmInformationCenterResponseBase<TData>
{
    private const string SuccessCode = "0";

    private const string SuccessDescription = "Success";

    public string ResponseCode { get; init; } = null!;

    public string ResponseDesc { get; init; } = null!;

    [JsonProperty("requestUUID")] 
    public string RequestId { get; init; } = null!;
    
    [JsonProperty("traceError")] 
    public object? TraceError { get; init; }
    
    [JsonProperty("data")] 
    public TData? Data { get; init; }

    public ErrorOr<TData> EnsureSuccessResult()
    {
        if (ResponseCode == SuccessCode && ResponseDesc == SuccessDescription)
        {
            return Data.EnsureNotNull();
        }

        return Error.Validation(
            code: ResponseCode, 
            description: $"ELM Information center validation error with description: {ResponseDesc} for request ID: {RequestId}");
    }
}