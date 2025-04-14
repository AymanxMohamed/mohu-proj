using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;

namespace MOHU.Integration.Application.Features.Companies.Companies.Services;

public partial class CompaniesService(ICrmContext crmContext, IGenericRepository genericRepository) : ICompaniesService
{
    public async Task UpdateAsync(UpdateCompaniesRequest request)
    {
        if (request == null || request.Requests.Count == 0)
        {
            throw new BadRequestException("The request list is empty.");
        }

        var companies = await GetEntitiesAsync(request.ToQueryExpression());
        
        var updatedCompanies = request.Update(companies);
        
        foreach (var updatedCompany in updatedCompanies)
        {
            genericRepository.Update(updatedCompany);
        }
    }
}