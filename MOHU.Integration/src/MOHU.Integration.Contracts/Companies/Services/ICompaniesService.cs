using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Domain.Features.Companies;

namespace MOHU.Integration.Contracts.Companies.Services;

public interface ICompaniesService
{
    Task<Company> GetByElmReferenceId(long elmReferenceId);
    
    Task UpdateAsync(UpdateCompaniesRequest request);

    Task MapDeactivatedCompaniesToNewCompanies();
}