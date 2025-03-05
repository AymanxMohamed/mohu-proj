using MOHU.Integration.Domain.Features.Individuals.Constants;

namespace MOHU.Integration.Domain.Features.Individuals.Entities;

public class IndividualVisaDetails
{
    private IndividualVisaDetails(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualVisaDetails), IndividualConstants.LogicalName);
        
        HajVisaPermitStatus = entity.GetAttributeValue<string>(IndividualConstants.Fields.VisaDetails.HajVisaPermitStatus);
    }

    private IndividualVisaDetails(string? hajVisaPermitStatus)
    {
        HajVisaPermitStatus = hajVisaPermitStatus;
    }

    public string? HajVisaPermitStatus { get; init; }

    
    public static IndividualVisaDetails Create(Entity entity) => new(entity);

    public static IndividualVisaDetails Create(string? hajVisaPermitStatus) => new(hajVisaPermitStatus);
    
    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualVisaDetails), IndividualConstants.LogicalName);

        entity.AssignIfNotNull(
            IndividualConstants.Fields.VisaDetails.HajVisaPermitStatus, 
            HajVisaPermitStatus);
    }
}