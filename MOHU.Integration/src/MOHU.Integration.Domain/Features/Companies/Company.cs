
namespace MOHU.Integration.Domain.Features.Companies;

public class Company
{
    private Company(Entity entity)
    {
        // Status = 
    }

    public static Company Create(Entity entity) => new(entity);
}