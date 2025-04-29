using MOHU.Integration.Application.T2SmsProvider.Common;
using MOHU.Integration.Application.T2SmsProvider.RichService;

namespace MOHU.Integration.Application.T2SmsProvider;

public static class DependencyInjection
{
    internal static IServiceCollection AddT2SmsProvider(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCommon(configuration)
            .AddRichService();
    }
    
    private static IServiceCollection AddRichService(this IServiceCollection services)
    {
        return services.AddTransient<IT2RichServiceClient, T2RichServiceClient>();
    }
}