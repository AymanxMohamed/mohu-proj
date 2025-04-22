using Common.Crm.Infrastructure.Factories;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;

namespace MOHU.Integration.WebApi.Common.Dtos.Requests;

public class PaginationWithFilterRequest
{
    public CrmPaginationParameters? PaginationParameters { get; init; }

    public CreateFilterRequest? Filter { get; init; }

    public List<CreateOrderRequest> OrderRequests { get; init; } = [];
    
    [JsonIgnore]
    public List<OrderExpression> OrderExpressions => OrderRequests
        .Select(x => x.ToExpression())
        .ToList();
}