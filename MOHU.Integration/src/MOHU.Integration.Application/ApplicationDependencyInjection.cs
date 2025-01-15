using Microsoft.Extensions.DependencyInjection;
using MOHU.Integration.Application.Features.Companies.Services;

using MOHU.Integration.Application.Features.Tickets.Services;
using MOHU.Integration.Application.Service;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Application.Validators;
using MOHU.Integration.Infrastructure.Localization;
using MOHU.Integration.Application.Localization;
using MOHU.Integration.Application.Nusuk.Tickets;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Application.Service.Taasher;
using MOHU.Integration.Application.Service.Kidana;
using MOHU.Integration.Application.Service.Sahab;
using MOHU.Integration.Application.Service.ServiceDesk;
using MOHU.Integration.Contracts.Companies.Services;

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
            services.AddValidatorsFromAssembly(typeof(UpdateTicketStatusValidatior).Assembly);
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IStringLocalizer, MessageStringLocalizer>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IRequestInfo, RequestInfo>();
            services.AddTransient<ITaasherService, TaasherService>();
            services.AddTransient<IKidanaService, KidanaService>();
            services.AddTransient<IServiceDeskService, ServiceDeskService>();
            services.AddTransient<ISahabService, SahabService>();
            services.AddTransient<INusukTicketsClient, NusukTicketsClient>();
            services.AddTransient<ICompaniesService, CompaniesService>();
            

            return services;
        }
    }
}
