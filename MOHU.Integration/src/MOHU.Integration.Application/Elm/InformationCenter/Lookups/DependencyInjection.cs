using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Clients;
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
            .AddCompanies(useFileClients)
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
    
    private static IServiceCollection AddCompanies(this IServiceCollection services, bool useFileClients)
    {
        return useFileClients
            ? services.AddCompaniesFileClients()
            : services.AddCompaniesRealClients();
    }

    private static IServiceCollection AddCompaniesRealClients(this IServiceCollection services)
    {
        services.AddScoped<IElmInformationCenterSpcCompaniesClient, ElmInformationCenterSpcCompaniesClient>();
        services.AddScoped<IElmInformationCenterDhcHajCompaniesClient, ElmInformationCenterDhcHajCompaniesClient>();
        services.AddScoped<IElmInformationCenterIhcCompaniesClient, ElmInformationCenterIhcCompaniesClient>();
        services.AddScoped<IElmInformationCenterHajMissionCompaniesClient, ElmInformationCenterHajMissionCompaniesClient>();
        services.AddScoped<IElmInformationCenterHousesClient, ElmInformationCenterHousesClient>();
        return services;
    }
    
    private static IServiceCollection AddCompaniesFileClients(this IServiceCollection services)
    {
        services.AddScoped<IElmInformationCenterSpcCompaniesClient, ElmInformationCenterSpcCompaniesFileClient>();
        services.AddScoped<IElmInformationCenterDhcHajCompaniesClient, ElmInformationCenterDhcHajCompaniesFileClient>();
        services.AddScoped<IElmInformationCenterIhcCompaniesClient, ElmInformationCenterIhcCompaniesFileClient>();
        services.AddScoped<IElmInformationCenterHajMissionCompaniesClient, ElmInformationCenterHajMissionCompaniesFileClient>();
        services.AddScoped<IElmInformationCenterHousesClient, ElmInformationCenterHousesFileClient>();
        return services;
    }

    
    private static IServiceCollection AddNationalities(this IServiceCollection services, bool useFileClients)
    {
        return useFileClients
            ? services.AddScoped<IElmInformationCenterNationalitiesClient, ElmInformationCenterNationalitiesFileClient>()
            : services.AddScoped<IElmInformationCenterNationalitiesClient, ElmInformationCenterNationalitiesClient>();
    }
}