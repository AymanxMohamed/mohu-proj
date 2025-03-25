using MOHU.Integration.Domain.Features.Customers;

namespace MOHU.Integration.Application.Features.Customers.Repositories;

public partial interface ICustomersRepository
{
    Customer? GetById(Guid customerId);
    
    Customer EnsureExistenceById(Guid customerId);
}