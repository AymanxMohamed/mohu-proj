using Common.Crm.Domain.Common.Constants;
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
    public List<OrderExpression> OrderExpressions
    {
        get
        {
            if (OrderRequests.Count == 0)
            {
                OrderRequests.Add(new CreateOrderRequest
                {
                    OrderColumn = CommonConstants.Fields.CreatedOn,
                    Operator = OrderType.Descending.ToString()
                });
            }
            
            return OrderRequests
                .Select(x => x.ToExpression())
                .ToList();
        }
    }
}