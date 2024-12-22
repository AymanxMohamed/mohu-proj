using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.Application.Features.Companies.Services;

public class CompaniesService(ICrmContext crmContext) : ICompaniesService
{
    public async Task UpdateAsync(UpdateCompaniesRequest request)
    {
        if (request == null || request.Requests.Count == 0)
        {
            throw new ArgumentException("The request list is empty.", nameof(request));
        }

        var companies = await GetAsync(request.ToQueryExpression());
        
        var updatedCompanies = request
            .Update(companies, message => throw new NotFoundException(message));
        
        foreach (var updatedCompany in updatedCompanies)
        {
            await crmContext.ServiceClient.UpdateAsync(updatedCompany);
        }
    }

    private async Task<List<Entity>> GetAsync(QueryExpression? queryExpression)
    {
        var companies = await crmContext.ServiceClient.RetrieveMultipleAsync(queryExpression);
        
        return companies?.Entities?.ToList() ?? [];
    }
}