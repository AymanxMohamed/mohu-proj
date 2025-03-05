using MOHU.Integration.Domain.Features.Individuals.Constants;

namespace MOHU.Integration.Domain.Features.Individuals.Entities;

public class IndividualContactInformation
{
    private IndividualContactInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualContactInformation), IndividualConstants.LogicalName);
        
        Email = entity.GetAttributeValue<string>(IndividualConstants.Fields.ContactInformation.Email);
        
        MobileNumber = entity.GetAttributeValue<string>(IndividualConstants.Fields.ContactInformation.MobileNumber);
    }

    private IndividualContactInformation(string? email, string? mobileNumber)
    {
        Email = email;
        MobileNumber = mobileNumber;
    }

    public string? Email { get; init; }

    public string? MobileNumber { get; init; }
    
    public static IndividualContactInformation Create(Entity entity) => new(entity);

    public static IndividualContactInformation Create(string? email, string? mobileNumber)
        => new(email, mobileNumber);
    
    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualContactInformation), IndividualConstants.LogicalName);

        entity.AssignIfNotNull(
            IndividualConstants.Fields.ContactInformation.Email, 
            Email);
        
        entity.AssignIfNotNull(
            IndividualConstants.Fields.ContactInformation.MobileNumber, 
            MobileNumber);
    }
}