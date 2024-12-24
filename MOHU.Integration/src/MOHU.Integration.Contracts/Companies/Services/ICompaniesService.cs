using MOHU.Integration.Contracts.Companies.Dtos;

namespace MOHU.Integration.Contracts.Companies.Services;

public interface ICompaniesService
{
    Task UpdateAsync(UpdateCompaniesRequest request);

    Task MapDeactivatedCompaniesToNewCompanies();
}