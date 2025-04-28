using Newtonsoft.Json;

namespace MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;

public class SendSmsConfirmationFullRequest
{
    [JsonProperty("username")]
    public required string UserName { get; init; }

    [JsonProperty("password")]
    public required string Password { get; init; }

    [JsonProperty("number")]
    public required string Number { get; init; }

    [JsonProperty("message")]
    public required string Message { get; init; }

    [JsonProperty("sender")]
    public required string Sender { get; init; }
}