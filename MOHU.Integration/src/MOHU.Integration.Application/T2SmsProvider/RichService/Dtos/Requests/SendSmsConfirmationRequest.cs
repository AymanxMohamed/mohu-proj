using System.Text.RegularExpressions;
using MOHU.Integration.Application.T2SmsProvider.Common.Clients;

namespace MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;

public class SendSmsConfirmationRequest
{
    public required string Number { get; init; }

    public required string Message { get; init; }
    
    public string NormalizedMessage => NormalizeMessage(Message);

    public SendSmsConfirmationFullRequest ToRequest(T2ApiSettings t2ApiSettings, string? sender = null)
    {
        return new SendSmsConfirmationFullRequest
        {
            UserName = t2ApiSettings.UserName,
            Password = t2ApiSettings.Password,
            Number = Number,
            Message = NormalizedMessage,
            Sender = sender ?? t2ApiSettings.Sender
        };
    }

    private static string NormalizeMessage(string rawMessage)
    {
        return string.IsNullOrWhiteSpace(rawMessage) ? string.Empty :
            Regex.Replace(rawMessage, @" ?\n ?", "\n");
    }
}