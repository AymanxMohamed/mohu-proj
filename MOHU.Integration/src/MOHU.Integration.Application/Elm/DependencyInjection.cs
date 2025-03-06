using MOHU.Integration.Application.Elm.InformationCenter;
using MOHU.Integration.Application.Elm.Nusuk.Tickets;
using MOHU.Integration.Application.Elm.ServiceDesk.Tickets.Services;

namespace MOHU.Integration.Application.Elm;

public static class DependencyInjection
{
    internal static IServiceCollection AddElm(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddInformationCenter(configuration)
            .AddNusuk()
            .AddServiceDesk();
    }
    
    private static IServiceCollection AddServiceDesk(this IServiceCollection services)
    {
        return services.AddTransient<INusukTicketsClient, NusukTicketsClient>();
    }
    
    private static IServiceCollection AddNusuk(this IServiceCollection services)
    {
        return services.AddTransient<IServiceDeskTicketsClient, ServiceDeskTicketsClient>();
    }
}