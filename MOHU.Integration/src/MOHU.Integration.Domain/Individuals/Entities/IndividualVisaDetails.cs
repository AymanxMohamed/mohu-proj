using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualVisaDetails
{
    private IndividualVisaDetails(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualVisaDetails), IndividualConstants.LogicalName);
        
        HajVisaPermitStatus = entity.GetAttributeValue<string>(IndividualConstants.Fields.VisaDetails.HajVisaPermitStatus);
    }
    
    public string? HajVisaPermitStatus { get; init; }

    
    public static IndividualVisaDetails Create(Entity entity) => new(entity);
}