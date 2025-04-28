using System.Text.Json;
using Common.Crm.Domain.Common.Constants;
using Common.Crm.Infrastructure.Factories;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;

namespace MOHU.Integration.WebApi.Common.Dtos.Requests;

public class CreateFilterConditionRequest
{
    public required string ColumnName { get; init; } 

    public string Operator { get; init; } = ConditionOperator.Equal.ToString();

    public JsonElement? Value { get; init; }

    public ConditionExpression ToExpression()
    {
        if (!Enum.TryParse<ConditionOperator>(Operator, ignoreCase: true, out var conditionOperator))
        {
            throw new InvalidOperationException($"Invalid condition operator: {Operator}");
        }

        if (ColumnName == CommonConstants.Fields.Status)
        {
            var intValue = int.TryParse(Value?.ToObject<object>()?.ToString() ?? "-1", out var x);
            return ConditionExpressionFactory
                .CreateConditionExpression(ColumnName, conditionOperator, value: x);
        }
        
        
        var value = Value?.ToObject<object>();
        
        return ConditionExpressionFactory
            .CreateConditionExpression(ColumnName, conditionOperator, value: value);
    }
}