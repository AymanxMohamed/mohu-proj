using Microsoft.Extensions.DependencyInjection;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Infrastructure.Persistence;
using MOHU.Integration.Infrastructure.Repository;
using MOHU.Integration.Infrastructure.Service;

namespace MOHU.Integration.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICrmContext, CrmContext>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddMemoryCache();
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));

            return services;
        }
    }
}
