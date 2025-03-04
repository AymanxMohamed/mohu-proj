using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualContactInformation
{
    private IndividualContactInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualContactInformation), IndividualConstants.LogicalName);
        
        Email = entity.GetAttributeValue<string>(IndividualConstants.Fields.ContactInformation.Email);
        
        MobileNumber = entity.GetAttributeValue<string>(IndividualConstants.Fields.ContactInformation.MobileNumber);
    }
    
    public string? Email { get; init; }

    public string? MobileNumber { get; init; }
    
    public static IndividualContactInformation Create(Entity entity) => new(entity);
}