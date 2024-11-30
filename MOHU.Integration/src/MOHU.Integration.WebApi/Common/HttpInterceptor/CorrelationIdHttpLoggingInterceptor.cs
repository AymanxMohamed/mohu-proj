using Microsoft.AspNetCore.HttpLogging;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Common.HttpInterceptor
{
    public class CorrelationIdHttpLoggingInterceptor : IHttpLoggingInterceptor
    {
        private readonly ICorrelationIdService _correlationIdService;
        public CorrelationIdHttpLoggingInterceptor(ICorrelationIdService correlationIdService)
        {
            _correlationIdService = correlationIdService;
        }
        public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
        {
            logContext.AddParameter("Request-Id", _correlationIdService.GenerateCorrelationId());
            return default;
        }

        public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
        {
            logContext.AddParameter("Correlation-Id", _correlationIdService.GetCorrelationId());
            return default;

        }
    }
}
