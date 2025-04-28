namespace MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Response;

public class SendSmsConfirmationResponse
{
    public int Code { get; init; }

    public required string Description { get; init; }

    public bool HasError { get; init; }

    public ErrorOr<Success> ToErrorOrSuccess()
    {
        if (!HasError)
        {
            return Result.Success;
        }
        
        return Error.Validation("SmsRichServiceError", Description);
    }
}