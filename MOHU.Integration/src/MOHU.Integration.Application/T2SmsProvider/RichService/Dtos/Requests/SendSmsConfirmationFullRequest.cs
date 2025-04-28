namespace MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;

public class SendSmsConfirmationFullRequest
{
    public required string UserName { get; init; }

    public required string Password { get; init; }

    public required string Number { get; init; }

    public required string Message { get; init; }

    public required string Sender { get; init; }
}