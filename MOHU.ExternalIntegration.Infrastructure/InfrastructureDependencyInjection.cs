using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Contracts.Interface.Cache;
using MOHU.ExternalIntegration.Contracts.Interface.Common;
using MOHU.ExternalIntegration.Contracts.Logging;
using MOHU.ExternalIntegration.Infrastructure.Localization;
using MOHU.ExternalIntegration.Infrastructure.Persistence;
using MOHU.ExternalIntegration.Infrastructure.Repository;
using MOHU.ExternalIntegration.Infrastructure.Service;

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

           services.AddTransient<IStringLocalizer, MessageStringLocalizer>();

         
            return services;
        }
    }
}
