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
            .AddIndividuals()
            .AddNationalities()
            .AddTicketCategories()
            .AddTickets();
    }
    
    private static IServiceCollection AddCountries(this IServiceCollection services)
    {
        return services.AddTransient<ICountriesService, CountriesService>();
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