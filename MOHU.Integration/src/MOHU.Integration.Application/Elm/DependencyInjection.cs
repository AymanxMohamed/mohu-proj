using MOHU.Integration.Application.Elm.InformationCenter;

namespace MOHU.Integration.Application.Elm;

public static class DependencyInjection
{
    internal static IServiceCollection AddElm(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddInformationCenter(configuration);
    }
}