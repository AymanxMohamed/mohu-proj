using Common.Crm.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Infrastructure.Localization;
using MOHU.Integration.Infrastructure.Logging;
using MOHU.Integration.Infrastructure.Persistence;
using MOHU.Integration.Infrastructure.Service;
using MOHU.Integration.Infrastructure.Settings;
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
            services.AddCommonInfrastructure();
            services.AddSingleton<Contracts.Logging.IAppLogger, SerilogLogger>();
            services.AddSingleton<ICrmContext, CrmContext>();
            services.AddSingleton<ICorrelationIdService, CorrelationIdService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICacheKeyGeneratorService,CacheKeyGeneratorService>();
            services.AddMemoryCache();
            services.AddTransient<IStringLocalizer, MessageStringLocalizer>();
            services.AddConfigurationOptions(configuration);

            return services;
        }

        private static IServiceCollection AddConfigurationOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<CrmContextSettings>(configuration.GetSection(nameof(CrmContextSettings)));
            var nusukSettings = configuration.GetSection(nameof(NusukSettings)).Get<NusukSettings>() 
                                ?? throw new ApplicationException("Nusuk Settings must exists in appsettings.json");
            
            services.AddSingleton(nusukSettings);
            return services;
        }
    }
}
