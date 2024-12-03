using Microsoft.OpenApi.Models;
using MOHU.Integration.Shared;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MOHU.Integration.WebApi.Common.SwaggerFilters;

public class AddHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = Header.Origin,
            In = ParameterLocation.Header,
            Required = false,
        });
        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = Header.Language,
            In = ParameterLocation.Header,
            Required = false,
        });
    }
}