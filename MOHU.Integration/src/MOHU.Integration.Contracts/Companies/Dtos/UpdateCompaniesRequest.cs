using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Contracts.Companies.Dtos;

public record UpdateCompaniesRequest(List<UpdateCompanyRequest> Requests)
{
    public List<Entity> Update(List<Entity> entities) =>
        Requests
            .SelectMany(request => request.Update(entities))
            .ToList();

    public QueryExpression ToQueryExpression() => new(CompaniesConstants.EntityLogicalName)
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