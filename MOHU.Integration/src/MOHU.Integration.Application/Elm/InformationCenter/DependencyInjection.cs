using MOHU.Integration.Application.Elm.InformationCenter.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups;

namespace MOHU.Integration.Application.Elm.InformationCenter;

public static class DependencyInjection
{
    internal static IServiceCollection AddInformationCenter(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCommon(configuration)
            .AddLookups(configuration);
    }
}