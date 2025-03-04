using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualIntegrationDetails
{
    private IndividualIntegrationDetails(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualIntegrationDetails), IndividualConstants.LogicalName);
        
        OriginCode  = entity
            .GetOptionSetValue(IndividualConstants.Fields.IntegrationDetails.OriginCode)
            .ToEnum<OriginEnum>();
        
        
        ElmReferenceId = entity.GetAttributeValue<int>(IndividualConstants.Fields.IntegrationDetails.ElmReferenceId);
    }

    private IndividualIntegrationDetails(OriginEnum? originCode, int? elmReferenceId)
    {
        OriginCode = originCode;
        ElmReferenceId = elmReferenceId;
    }

    public OriginEnum? OriginCode { get; init; }
    
    public int? ElmReferenceId { get; init; }
    
    public static IndividualIntegrationDetails Create(Entity entity) => new(entity);

    public static IndividualIntegrationDetails Create(OriginEnum? originCode, int? elmReferenceId) =>
        new(originCode, elmReferenceId);
}