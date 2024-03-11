using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOHU.ExternalIntegration.Infrastructure.Persistence;
using MOHU.ExternalIntegration.Infrastructure.Repository;
using MOHU.ExternalIntegration.Infrastructure.Service;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Contracts.Logging;
using Serilog;

namespace MOHU.ExternalIntegration.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();
            services.AddSerilog();
            services.AddSingleton<IAppLogger, SerilogLogger>();
            services.AddSingleton<ICrmContext, CrmContext>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddMemoryCache();
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));




            return services;
        }
    }
}
