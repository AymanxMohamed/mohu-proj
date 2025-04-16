using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Application.Elm;
using MOHU.Integration.Application.Features;
using MOHU.Integration.Application.Localization;
using MOHU.Integration.Application.Service;
using MOHU.Integration.Application.Service.Kidana;
using MOHU.Integration.Application.Service.Sahab;
using MOHU.Integration.Application.Service.ServiceDesk;
using MOHU.Integration.Application.Service.Taasher;
using MOHU.Integration.Application.Validators;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Infrastructure.Localization;
using MOHU.Integration.Application.Service.Hootsuite;
using MOHU.Integration.Application.Service.Almatar;
using MOHU.Integration.Application.Kidana;
using MOHU.Integration.Application.Kidana.Common.Services;

namespace MOHU.Integration.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddGenericRepository()
            .AddElm(configuration)
            .AddKidana(configuration)
            .AddFeatures()
            .AddApplicationServices();
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IIvrService, IvrService>();
        services.AddTransient<IActivityService, ActivityService>();
        services.AddTransient<IFieldService, FieldService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddValidatorsFromAssembly(typeof(CreateProfileValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateTicketStatusValidatior).Assembly);
        services.AddTransient<IMessageService, MessageService>();
        services.AddTransient<IStringLocalizer, MessageStringLocalizer>();
        services.AddTransient<ICommonService, CommonService>();
        services.AddTransient<IDocumentService, DocumentService>();
        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<IHootsuiteService, HootsuiteService>();
        services.AddSingleton<IRequestInfo, RequestInfo>();
        services.AddTransient<ITaasherService, TaasherService>();
        services.AddTransient<IKidanaService, KidanaService>();
        services.AddTransient<IServiceDeskService, ServiceDeskService>();
        services.AddTransient<ISahabService, SahabService>();
        services.AddTransient<ITicketCategoryService, CategorieServices>();
        services.AddTransient<IAlmatarService, AlmatarService>();
        services.AddTransient<IKidanaDetailsService, KidanaDetailsService>();

        return services;
    }

    private static IServiceCollection AddGenericRepository(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository>(provider =>
        {
            var crmContext = provider.GetRequiredService<ICrmContext>();
            return GenericRepositoriesFactory.CreateGenericRepository(crmContext.ServiceClient);
        });

        return services;
    }
}