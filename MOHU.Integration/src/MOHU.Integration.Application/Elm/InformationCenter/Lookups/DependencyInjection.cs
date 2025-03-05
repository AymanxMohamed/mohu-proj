using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups;

public static class DependencyInjection
{
    internal static IServiceCollection AddLookups(this IServiceCollection services)
    {
        return services
            .AddApplicantData()
            .AddCountries();
    }
    
    private static IServiceCollection AddApplicantData(this IServiceCollection services)
    {
        return services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataClient>();
        // return services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataFileClient>();
    }
    
    private static IServiceCollection AddCountries(this IServiceCollection services)
    {
        // return services.AddScoped<IElmInformationCenterCountriesClient, ElmInformationCenterCountriesClient>();
        return services.AddScoped<IElmInformationCenterCountriesClient, ElmInformationCenterCountriesFileClient>();
    }
}