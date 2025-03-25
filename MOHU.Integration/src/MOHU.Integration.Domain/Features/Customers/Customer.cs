using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Individuals;
using MOHU.Integration.Domain.Features.Individuals.Constants;

namespace MOHU.Integration.Domain.Features.Customers;

public class Customer : CrmEntity
{
    protected Customer(Entity entity) : base(entity)
    {
        UnderlyingEntity = entity;
    }
    
    public Entity UnderlyingEntity { get; set; }

    public bool IsIndividual => UnderlyingEntity.LogicalName == IndividualConstants.LogicalName;

    public static Customer Create(Entity entity) => new(entity);

    public Individual ToIndividual()
    {
        if (!IsIndividual)
        {
            throw new InvalidOperationException("Customer is not Individual");
        }
        
        return Individual.Create(UnderlyingEntity);
    }
    
    public Company ToCompany()
    {
        if (IsIndividual)
        {
            throw new InvalidOperationException("Customer is not Company");
        }
        
        return Company.Create(UnderlyingEntity);
    }
}