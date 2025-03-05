using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups;

public static class DependencyInjection
{
    internal static IServiceCollection AddLookups(this IServiceCollection services, IConfiguration configuration)
    {
        var userFileClients = configuration
            .GetSection(ElmInformationCenterApiSettings.SectionName)
            .Get<ElmInformationCenterApiSettings>()
            ?.UseFileClients ?? false;
        
        return services
            .AddApplicantData(userFileClients)
            .AddCountries(userFileClients);
    }
    
    private static IServiceCollection AddApplicantData(this IServiceCollection services, bool userFileClients)
    {
        return userFileClients 
            ? services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataFileClient>()
            : services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataClient>();
    }
    
    private static IServiceCollection AddCountries(this IServiceCollection services, bool userFileClients)
    {
        return userFileClients
            ? services.AddScoped<IElmInformationCenterCountriesClient, ElmInformationCenterCountriesFileClient>()
            : services.AddScoped<IElmInformationCenterCountriesClient, ElmInformationCenterCountriesClient>();
    }
}