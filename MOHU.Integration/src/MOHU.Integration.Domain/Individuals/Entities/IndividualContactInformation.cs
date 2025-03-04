using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualNationalityDetails
{
    private IndividualNationalityDetails(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualNationalityDetails), IndividualConstants.LogicalName);
        
        NationalityId = entity.GetAttributeValue<EntityReference>(IndividualConstants.Fields.NationalityInformation.Nationality);
        
        CountryOfResidence = entity.GetAttributeValue<EntityReference>(IndividualConstants.Fields.NationalityInformation.CountryOfResidence);
    }
    

    public EntityReference? NationalityId { get; init; }

    public EntityReference? CountryOfResidence { get; init; }
    
    public static IndividualNationalityDetails Create(Entity entity) => new(entity);
}