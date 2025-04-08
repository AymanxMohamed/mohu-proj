using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;
using MOHU.Integration.Domain.Features.Companies;

namespace MOHU.Integration.Application.Features.Companies.Companies.Services;

public partial class CompaniesService(ICrmContext crmContext) : ICompaniesService
{
    public Task<Company> GetByElmReferenceId(long elmReferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UpdateCompaniesRequest request)
    {
        if (request == null || request.Requests.Count == 0)
        {
            throw new BadRequestException("The request list is empty.");
        }

        var companies = await GetAsync(request.ToQueryExpression());
        
        var updatedCompanies = request.Update(companies);
        
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