using Common.Crm.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Crm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        return services
            .AddCommon();
    }
}