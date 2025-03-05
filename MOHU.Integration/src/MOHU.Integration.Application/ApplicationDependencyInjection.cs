using MOHU.Integration.Application.Elm;
using MOHU.Integration.Application.Features.Countries.Services;
using MOHU.Integration.Application.Features.Individuals.Services;
using MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;
using MOHU.Integration.Application.Features.TicketCategories;
using MOHU.Integration.Application.Features.Tickets.Services;
using MOHU.Integration.Application.Localization;
using MOHU.Integration.Application.Nusuk.Tickets;
using MOHU.Integration.Application.Service;
using MOHU.Integration.Application.Service.Kidana;
using MOHU.Integration.Application.Service.Sahab;
using MOHU.Integration.Application.Service.ServiceDesk;
using MOHU.Integration.Application.Service.Taasher;
using MOHU.Integration.Application.Validators;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Infrastructure.Localization;

namespace MOHU.Integration.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddElm(configuration)
            .AddApplicationServices();
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
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
        services.AddTransient<IServiceDeskTicketsClient, ServiceDeskTicketsClient>();
        services.AddTransient<ITicketCategoriesService, TicketCategoriesService>();
        services.AddTransient<IIndividualsService, IndividualsService>();
        services.AddTransient<ICountriesService, CountriesService>();
            
        return services;
    } 
}