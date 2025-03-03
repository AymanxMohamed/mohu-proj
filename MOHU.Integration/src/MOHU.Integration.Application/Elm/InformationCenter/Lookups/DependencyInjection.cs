using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups;

public static class DependencyInjection
{
    internal static IServiceCollection AddLookups(this IServiceCollection services)
    {
        return services
            .AddApplicantData();
    }
    
    private static IServiceCollection AddApplicantData(this IServiceCollection services)
    {
        // return services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataClient>();
        return services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataFileClient>();
    }
}