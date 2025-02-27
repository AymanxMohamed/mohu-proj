using Microsoft.AspNetCore.HttpLogging;
using MOHU.Integration.Application;
using MOHU.Integration.Contracts.Dto.Config;
using MOHU.Integration.Infrastructure;
using MOHU.Integration.WebApi.Common.HttpInterceptors;
using MOHU.Integration.WebApi.Common.SwaggerFilters;
using VirtualEntity.Poc;

namespace MOHU.Integration.WebApi;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInternalModules(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        return services
            .AddInfrastructure(configuration)
            .AddApplication(configuration)
            .AddPresentation(configuration)
            .AddSwaggerSupport()
            .AddApplicationCors()
            .AddApplicationHttpLogging()
            .AddCaching(configuration)
            .AddHttpClient();
    }

    private static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var mvcBuilder = services.AddControllers();
        services.AddVirtualEntitiesSupport(mvcBuilder);
        services.AddEndpointsApiExplorer();
        return services;
    }

    private static IServiceCollection AddSwaggerSupport(this IServiceCollection services) => 
        services.AddSwaggerGen(c => c.OperationFilter<AddHeaderParameter>());

    private static IServiceCollection AddApplicationCors(this IServiceCollection services) =>
        services.AddCors(x =>
        {
            x.AddPolicy("Dev", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                //("GET", "POST");
            });
        });


    private static IServiceCollection AddApplicationHttpLogging(
        this IServiceCollection services) =>
        services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.CombineLogs = true;
            })
            .AddHttpLoggingInterceptor<CorrelationIdHttpLoggingInterceptor>();

    private static IServiceCollection AddCaching(
        this IServiceCollection services, 
        IConfiguration configuration) => 
        services.Configure<MemoryCacheConfig>(configuration.GetSection(nameof(MemoryCacheConfig)));
}