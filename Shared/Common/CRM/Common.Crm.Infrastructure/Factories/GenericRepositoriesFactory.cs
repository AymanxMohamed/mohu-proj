using Common.Crm.Infrastructure.Repositories.Concretes;
using Common.Crm.Infrastructure.Repositories.Interfaces;

namespace Common.Crm.Infrastructure.Factories;

public static class GenericRepositoriesFactory
{
    public static IGenericRepository CreateGenericRepository(IOrganizationService organizationService)
        => new GenericRepository(organizationService);
}