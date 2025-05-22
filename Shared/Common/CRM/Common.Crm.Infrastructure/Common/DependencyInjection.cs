using Common.Crm.Application.Common.BackgroundJobs;
using Common.Crm.Infrastructure.Common.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Crm.Infrastructure.Common;

public static class DependencyInjection
{
    internal static IServiceCollection AddCommon(this IServiceCollection services)
    {
        return services
            .AddBackgroundJobs();
    }
    
    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<QueuedHostedService>();
        return services;
    }
}