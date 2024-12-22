using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MOHU.Integration.Contracts.Companies.Dtos;

public record UpdateCompaniesRequest(List<UpdateCompanyRequest> Requests)
{
    public List<Entity> Update(List<Entity> entities, Action<string> fireNotFoundException)
    {
        return Requests.Select(request => request.Update(entities, fireNotFoundException)).ToList();
    }
    
    public QueryExpression ToQueryExpression() => new()
        {   
            ColumnSet = UpdateCompanyRequest.GetColumnSet(),
            Criteria = ToFilterExpression()
        };

    private FilterExpression ToFilterExpression()
    {
        var filterExpression = new FilterExpression
        {
            FilterOperator = LogicalOperator.Or,
        };
            
        foreach (var request in Requests)
        {
            filterExpression.AddFilter(request.ToFilterExpression());
        }
        
        return filterExpression;
    }
}