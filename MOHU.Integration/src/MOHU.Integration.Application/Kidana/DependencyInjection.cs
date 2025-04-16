using Core.Infrastructure.Integrations.Clients.Settings;
using MOHU.Integration.Application.Kidana.Common.Clients;
using MOHU.Integration.Application.Kidana.Common.Services;
using MOHU.Integration.Application.Service.Kidana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace MOHU.Integration.Application.Kidana
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKidana(this IServiceCollection services, IConfiguration configuration)
        {
            var apiClientSettings = configuration
                .GetSection(KidanaApiSettings.SectionName)
                .Get<KidanaApiSettings>();

            apiClientSettings
                .ThrowIfNull("Kidana Api Settings must be provided in the appsettings.json");

            services.AddSingleton(apiClientSettings);

            services.AddSingleton<IKidanaClient, KidanaClient>();
            return services;
        }




    }
}
