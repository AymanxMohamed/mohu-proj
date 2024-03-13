using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Infrastructure.Localization;
using MOHU.Integration.Infrastructure.Persistence;
using MOHU.Integration.Infrastructure.Repository;
using MOHU.Integration.Infrastructure.Service;
using Serilog;
//using MOHU.Integration.Application.common;

namespace MOHU.Integration.Infrastructure
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
            services.AddSingleton<Contracts.Logging.IAppLogger, SerilogLogger>();
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
