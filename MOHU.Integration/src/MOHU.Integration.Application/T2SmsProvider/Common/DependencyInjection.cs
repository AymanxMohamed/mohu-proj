using MOHU.Integration.Application.T2SmsProvider.Common.Clients;
using Throw;

namespace MOHU.Integration.Application.T2SmsProvider.Common;

public static class DependencyInjection
{
    internal static IServiceCollection AddCommon(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddClients(configuration);
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiClientSettings = configuration
            .GetSection(T2ApiSettings.SectionName)
            .Get<T2ApiSettings>();
        
        apiClientSettings
            .ThrowIfNull("T2 Settings must be provided in the appsettings.json");
        
        services.AddSingleton(apiClientSettings);
        
        services.AddSingleton<IT2Client, T2Client>();
        
        return services;
    }
}