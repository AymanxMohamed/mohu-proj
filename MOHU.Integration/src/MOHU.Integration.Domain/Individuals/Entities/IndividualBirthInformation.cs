using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualBirthInformation
{
    private IndividualBirthInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualBirthInformation), IndividualConstants.LogicalName);
        
        BirthDate = entity.GetAttributeValue<DateTime>(IndividualConstants.Fields.BirthInformation.BirthDate);
        
        PlaceOfBirth = entity.GetAttributeValue<string>(IndividualConstants.Fields.BirthInformation.PlaceOfBirth);
        
        HijriBirthDate = entity.GetAttributeValue<string>(IndividualConstants.Fields.BirthInformation.HijriBirthDate);
    }

    private IndividualBirthInformation(DateTime? birthDate, string? placeOfBirth, string? hijriBirthDate)
    {
        BirthDate = birthDate?.ToUniversalTime();
        PlaceOfBirth = placeOfBirth;
        HijriBirthDate = hijriBirthDate;
    }

    public DateTime? BirthDate { get; init; }

    public string? PlaceOfBirth { get; init; }
    
    public string? HijriBirthDate { get; init; }
    
    public static IndividualBirthInformation Create(Entity entity) => new(entity);

    public static IndividualBirthInformation Create(DateTime? birthDate, string? placeOfBirth, string? hijriBirthDate)
        => new(birthDate, placeOfBirth, hijriBirthDate);
    
    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualBirthInformation), IndividualConstants.LogicalName);

        entity.AssignIfNotNull(
            IndividualConstants.Fields.BirthInformation.BirthDate, 
            BirthDate);
        
        entity.AssignIfNotNull(
            IndividualConstants.Fields.BirthInformation.PlaceOfBirth, 
            PlaceOfBirth);
        
        entity.AssignIfNotNull(
            IndividualConstants.Fields.BirthInformation.HijriBirthDate, 
            HijriBirthDate);
    }
}