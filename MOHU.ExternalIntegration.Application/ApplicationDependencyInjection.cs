using Microsoft.Extensions.DependencyInjection;
using MOHU.ExternalIntegration.Application.common;
using MOHU.ExternalIntegration.Application.Service.Kedana;
using MOHU.ExternalIntegration.Application.Service.ServiceDesk;
using MOHU.ExternalIntegration.Application.Service.Taasher;
using MOHU.ExternalIntegration.Application.Service.Ticket;
using MOHU.ExternalIntegration.Contracts.Interface;


namespace MOHU.ExternalIntegration.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddTransient<IHttpExceptionService, HttpExceptionService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ICreateProfileService, CreateProfileService>();
            services.AddScoped<ITaasherUpdateStatusService, TaasherUpdateStatusService>();
            services.AddScoped<IServiceDiskUpdateStatusService, ServiceDiskUpdateStatusService>();
            services.AddScoped<IKedanaUpdateStatusService, KedanaUpdateStatusService>();

            return services;
        }
    }
}
