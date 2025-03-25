using MOHU.Integration.Domain.Features.Customers;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Domain.Features.Companies.Constants;
using MOHU.Integration.Domain.Features.Individuals.Constants;

namespace MOHU.Integration.Application.Features.Customers.Repositories;

internal class CustomersRepository(IGenericRepository genericRepository) : ICustomersRepository
{
    public Customer EnsureExistenceById(Guid customerId) => 
        GetById(customerId) ?? throw new NotFoundException($"Their is no customer with this id: {customerId}");

    public Customer? GetById(Guid customerId)
    {
        var account = genericRepository.GetById(CompaniesConstants.LogicalName, customerId);

        var entity = account ?? genericRepository.GetById(IndividualConstants.LogicalName, customerId);
        
        return entity is null ? null : Customer.Create(entity);
    }
}