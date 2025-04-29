using MOHU.Integration.Application.T2SmsProvider.Common.Clients;

namespace MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;

public class SendSmsConfirmationRequest
{
    public required string Number { get; init; }

    public required string Message { get; init; }

    public SendSmsConfirmationFullRequest ToRequest(T2ApiSettings t2ApiSettings, string? sender = null)
    {
        return new SendSmsConfirmationFullRequest
        {
            UserName = t2ApiSettings.UserName,
            Password = t2ApiSettings.Password,
            Number = Number,
            Message = Message,
            Sender = sender ?? t2ApiSettings.Sender
        };
    }
}