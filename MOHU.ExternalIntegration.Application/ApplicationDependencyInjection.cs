using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MOHU.ExternalIntegration.Application.common;
using MOHU.ExternalIntegration.Application.Service.Kedana;
using MOHU.ExternalIntegration.Application.Service.ServiceDesk;
using MOHU.ExternalIntegration.Application.Service.Taasher;
using MOHU.ExternalIntegration.Application.Service.Ticket;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Contracts.Interface.Common;
using MOHU.ExternalIntegration.Contracts.ModelValidation.Tasher;
using MOHU.ExternalIntegration.Infrastructure.Localization;
using MOHU.ExternalIntegration.Infrastructure.Persistence;

using MOHU.ExternalIntegration.Contracts.ModelValidation.Tasher;
using CreateProfileValidator = MOHU.ExternalIntegration.Contracts.ModelValidation.Tasher.CreateProfileValidator;


namespace MOHU.ExternalIntegration.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {


            services.AddTransient<ITicketService, TicketService>();//
            services.AddTransient<ICreateProfileService, CreateProfileService>();//
          //  services.AddTransient<ITaasherUpdateStatusService, TaasherUpdateStatusService>();//
          //  services.AddTransient<IServiceDiskUpdateStatusService, ServiceDiskUpdateStatusService>();//
           // services.AddTransient<IKedanaUpdateStatusService, KedanaUpdateStatusService>();//
            // general update status 
           // KedanaUpdateStatusService : IUpdateStatusService
           services.AddTransient<IUpdateStatusService, KedanaUpdateStatusService>();

            services.AddTransient<ICommonMethod, CommonMethod>();
            services.AddValidatorsFromAssembly(typeof(CreateProfileValidator).Assembly);
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IStringLocalizer, MessageStringLocalizer>();


            return services;
        }
    }
}
