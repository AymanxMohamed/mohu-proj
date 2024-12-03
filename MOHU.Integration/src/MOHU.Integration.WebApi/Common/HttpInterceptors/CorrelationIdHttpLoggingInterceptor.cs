using Microsoft.AspNetCore.HttpLogging;

namespace MOHU.Integration.WebApi.Common.HttpInterceptors;

public class CorrelationIdHttpLoggingInterceptor(ICorrelationIdService correlationIdService)
    : IHttpLoggingInterceptor
{
    public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
    {
        logContext.AddParameter("Request-Id", correlationIdService.GenerateCorrelationId());
        return default;
    }

    public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
    {
        logContext.AddParameter("Correlation-Id", correlationIdService.GetCorrelationId());
        return default;

    }
}