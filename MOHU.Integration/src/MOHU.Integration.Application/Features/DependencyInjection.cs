using MOHU.Integration.Application.Features.Companies.DhcHajCompanies.Services;
using MOHU.Integration.Application.Features.Companies.HajMissions.Services;
using MOHU.Integration.Application.Features.Companies.Houses.Services;
using MOHU.Integration.Application.Features.Companies.IhcCompanies.Services;
using MOHU.Integration.Application.Features.Companies.SpcCompanies.Services;
using MOHU.Integration.Application.Features.Countries.Services;
using MOHU.Integration.Application.Features.Individuals.Services;
using MOHU.Integration.Application.Features.Nationalities.Services;
using MOHU.Integration.Application.Features.TicketCategories;
using MOHU.Integration.Application.Features.Tickets.Services;

namespace MOHU.Integration.Application.Features;

public static class DependencyInjection
{
    internal static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        return services
            .AddCountries()
            .AddCompanies()
            .AddIndividuals()
            .AddNationalities()
            .AddTicketCategories()
            .AddTickets();
    }
    
    private static IServiceCollection AddCountries(this IServiceCollection services)
    {
        return services.AddTransient<ICountriesService, CountriesService>();
    }
    
    private static IServiceCollection AddCompanies(this IServiceCollection services)
    {
        services.AddTransient<ISpcCompaniesService, SpcCompaniesService>();
        services.AddTransient<IDhcHajCompaniesService, DhcHajCompaniesService>();
        services.AddTransient<IIhcCompaniesService, IhcCompaniesService>();
        services.AddTransient<IHajMissionCompaniesService, HajMissionCompaniesService>();
        services.AddTransient<IHousesService, HousesService>();
        return services;
    }
    
    private static IServiceCollection AddIndividuals(this IServiceCollection services)
    {
        return services.AddTransient<IIndividualsService, IndividualsService>();
    }
    
    private static IServiceCollection AddNationalities(this IServiceCollection services)
    {
        return services.AddTransient<INationalitiesService, NationalitiesService>();
    }
    
    private static IServiceCollection AddTicketCategories(this IServiceCollection services)
    {
        return services.AddTransient<ITicketCategoriesService, TicketCategoriesService>();
    }
    
    private static IServiceCollection AddTickets(this IServiceCollection services)
    {
        return services.AddTransient<ITicketService, TicketService>();
    }
}