namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Services;

public static class ElmSyncServiceUtils
{
    public static List<Task> GetElmSyncServiceTasks(this IServiceProvider serviceProvider)
    {
        return GetElmSyncServiceTypes()
            .Select(serviceProvider.GetService)
            .Where(service => service != null)
            .Select(service => service!.GetType().GetMethod("Sync")?.Invoke(service, null))
            .Where(task => task is Task)
            .Cast<Task>()
            .ToList();
    }
    
    public static List<Type> GetElmSyncServiceTypes() => typeof(IElmSyncService<>)
        .Assembly
        .GetTypes()
        .Where(t => t
            .GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IElmSyncService<>)))
        .ToList();
}