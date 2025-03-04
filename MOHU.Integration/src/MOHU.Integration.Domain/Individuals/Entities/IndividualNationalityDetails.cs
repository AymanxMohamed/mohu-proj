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

    private IndividualNationalityDetails(EntityReference? nationalityId, EntityReference? countryOfResidence)
    {
        NationalityId = nationalityId;
        CountryOfResidence = countryOfResidence;
    }

    public EntityReference? NationalityId { get; init; }

    public EntityReference? CountryOfResidence { get; init; }
    
    public static IndividualNationalityDetails Create(Entity entity) => new(entity);

    public static IndividualNationalityDetails Create(
        EntityReference? nationalityId,
        EntityReference? countryOfResidence)
        => new(nationalityId, countryOfResidence);
    
    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualNationalityDetails), IndividualConstants.LogicalName);

        entity.AssignIfNotNull(
            IndividualConstants.Fields.NationalityInformation.Nationality, 
            NationalityId);
        
        entity.AssignIfNotNull(
            IndividualConstants.Fields.NationalityInformation.CountryOfResidence, 
            CountryOfResidence);
    }
}