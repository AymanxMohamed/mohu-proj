using MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;

namespace MOHU.Integration.Application.T2SmsProvider.RichService;

public interface IT2RichServiceClient
{
    public ErrorOr<Success> SendSmsConfirmation(SendSmsConfirmationRequest request);
}