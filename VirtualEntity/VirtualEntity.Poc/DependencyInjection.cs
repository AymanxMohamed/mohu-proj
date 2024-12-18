using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;
using VirtualEntity.Poc.Contacts.Models;

namespace VirtualEntity.Poc;

public static class DependencyInjection
{
    public static IServiceCollection AddVirtualEntitiesSupport(this IServiceCollection services, IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddOData(options =>
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Contact>("Contacts");
            options.AddRouteComponents("odata", odataBuilder.GetEdmModel())
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .Count()
                .SetMaxTop(10000);
        });
        
        mvcBuilder.AddApplicationPart(VirtualEntityAssemblyMarker.Assembly);

        return services;
    }
}