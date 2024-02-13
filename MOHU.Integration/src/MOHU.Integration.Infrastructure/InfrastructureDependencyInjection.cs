using Microsoft.Extensions.DependencyInjection;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Infrastructure.Persistence;

namespace MOHU.Integration.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICrmContext, CrmContext>();
            return services;
        }
    }
}
