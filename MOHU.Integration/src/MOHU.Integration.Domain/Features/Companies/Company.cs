using Common.Crm.Domain.Common.Enums;

namespace MOHU.Integration.Domain.Features.Companies;

public class Company
{
    public StatusEnum? Status { get; init; }

    private Company(Entity entity)
    {
        // Status = 
    }

    public static Company Create(Entity entity) => new(entity);
}