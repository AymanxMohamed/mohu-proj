using MOHU.Integration.Application.Elm.ServiceDesk.Common.Clients;
using Throw;

namespace MOHU.Integration.Application.Elm.ServiceDesk.Common;

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
            .GetSection(ElmInformationCenterApiSettings.SectionName)
            .Get<ElmInformationCenterApiSettings>();
        
        apiClientSettings
            .ThrowIfNull("ELM Information Center Api Settings must be provided in the appsettings.json");
        
        services.AddSingleton(apiClientSettings);
        
        services.AddSingleton<IServiceDeskClient, ServiceDeskClient>();
        
        return services;
    }
}