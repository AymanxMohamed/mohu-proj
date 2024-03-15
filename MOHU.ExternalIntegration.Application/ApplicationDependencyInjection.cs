using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MOHU.ExternalIntegration.Application.common;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Contracts.Interface.Common;
using MOHU.ExternalIntegration.Infrastructure.Localization;
using MOHU.ExternalIntegration.Application.Service;
using MOHU.ExternalIntegration.Application.Validator;


namespace MOHU.ExternalIntegration.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddValidatorsFromAssembly(typeof(CreateProfileValidator).Assembly);
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IStringLocalizer, MessageStringLocalizer>();
            return services;
        }
    }
}
