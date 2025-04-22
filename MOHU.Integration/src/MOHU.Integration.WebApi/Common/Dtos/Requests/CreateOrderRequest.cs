using Microsoft.Xrm.Sdk.Query;

namespace MOHU.Integration.WebApi.Common.Dtos.Requests;

public class CreateOrderRequest
{
    public required string OrderColumn { get; init; }
    
    public string Operator { get; init; } = OrderType.Ascending.ToString();

    public OrderExpression ToExpression()
    {
        if (!Enum.TryParse<OrderType>(Operator, ignoreCase: true, out var sortOperator))
        {
            throw new BadRequestException($"Invalid sort operator: {Operator}");
        }

        return new OrderExpression(OrderColumn, sortOperator);
    }
}