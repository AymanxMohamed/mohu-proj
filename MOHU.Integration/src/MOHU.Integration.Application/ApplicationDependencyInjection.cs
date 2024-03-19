using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Application.Service;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Application.Validators;
using MOHU.Integration.Infrastructure.Localization;
using MOHU.Integration.Infrastructure.Repository;
using MOHU.Integration.Application.Localization;
using MOHU.Integration.Contracts.Dto;

namespace MOHU.Integration.Application
{

    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IIvrService, IvrService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IFieldService, FieldService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddValidatorsFromAssembly(typeof(CreateProfileValidator).Assembly);
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IStringLocalizer, MessageStringLocalizer>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IRequestInfo, RequestInfo>();

            return services;
        }
    }
}
