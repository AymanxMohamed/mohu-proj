using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Clients;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups;

public static class DependencyInjection
{
    internal static IServiceCollection AddLookups(this IServiceCollection services, IConfiguration configuration)
    {
        var useFileClients = configuration
            .GetSection(ElmInformationCenterApiSettings.SectionName)
            .Get<ElmInformationCenterApiSettings>()
            ?.UseFileClients ?? false;
        
        return services
            .AddApplicantData(useFileClients)
            .AddCountries(useFileClients)
            .AddNationalities(useFileClients);
    }
    
    private static IServiceCollection AddApplicantData(this IServiceCollection services, bool useFileClients)
    {
        return useFileClients 
            ? services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataFileClient>()
            : services.AddScoped<IElmInformationCenterApplicantDataClient, ElmInformationCenterApplicantDataClient>();
    }
    
    private static IServiceCollection AddCountries(this IServiceCollection services, bool useFileClients)
    {
        return useFileClients
            ? services.AddScoped<IElmInformationCenterCountriesClient, ElmInformationCenterCountriesFileClient>()
            : services.AddScoped<IElmInformationCenterCountriesClient, ElmInformationCenterCountriesClient>();
    }
    
    private static IServiceCollection AddNationalities(this IServiceCollection services, bool useFileClients)
    {
        return useFileClients
            ? services.AddScoped<IElmInformationCenterNationalitiesClient, ElmInformationCenterNationalitiesFileClient>()
            : services.AddScoped<IElmInformationCenterNationalitiesClient, ElmInformationCenterNationalitiesClient>();
    }
}