using Common.Crm.Infrastructure.Factories;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;

namespace MOHU.Integration.WebApi.Common.Dtos.Requests;

public class CreateFilterRequest
{
    public bool IsOrFilter { get; init; }

    public List<CreateFilterRequest> ChildFilters => GetChildFilters();
    
    public string? SerializedChildFilters { get; init; }

    public List<CreateFilterRequest> GetChildFilters()
    {
       return  SerializedChildFilters is null
            ? []
            : JsonConvert.DeserializeObject<List<CreateFilterRequest>>(SerializedChildFilters) ?? [];
    }
    
    public List<CreateFilterConditionRequest> FilterConditions { get; init; } = [];

    public FilterExpression ToExpression()
    {
        return FilterExpressionFactory
            .CreateFilterExpression(
                isOrFilter: IsOrFilter,
                childFilters: ChildFilters.Select(x => x.ToExpression()).ToList(),
                conditionExpressions: FilterConditions.Select(x => x.ToExpression()).ToArray());
    }
}