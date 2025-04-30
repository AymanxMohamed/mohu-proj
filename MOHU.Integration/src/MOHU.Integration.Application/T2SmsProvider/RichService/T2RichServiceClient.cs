using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.T2SmsProvider.Common.Clients;
using MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;
using MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Response;

using RestSharp;

namespace MOHU.Integration.Application.T2SmsProvider.RichService;

internal class T2RichServiceClient(
    IT2Client t2Client, 
    T2ApiSettings t2ApiSettings) : IT2RichServiceClient
{
    public ErrorOr<Success> SendSmsConfirmation(SendSmsConfirmationRequest request)
    {
        return t2Client
            .PrepareAndExecuteRequest<SendSmsConfirmationResponse>(
                method: Method.Post,
                body: request.ToRequest(t2ApiSettings),
                resourceUrl: nameof(SendSmsConfirmation))
            .Then(x => x.EnsureNotNull())
            .Then(x => x.ToErrorOrSuccess(request.NormalizedMessage));
    }
}