using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Common.Constants;
using MOHU.Integration.Domain.Features.Individuals.Constants;
using MOHU.Integration.Domain.Features.Individuals.Enums;

namespace MOHU.Integration.Domain.Features.Individuals.Entities;

public class IndividualIntegrationDetails
{
    private IndividualIntegrationDetails(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualIntegrationDetails), IndividualConstants.LogicalName);
        
        OriginCode  = entity
            .GetOptionSetValue(IndividualConstants.Fields.IntegrationDetails.OriginCode)
            .ToEnum<OriginEnum>();
        
        
        ElmReferenceId = entity.GetAttributeValue<int>(CommonConstants.Fields.IntegrationDetails.ElmReferenceId);
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
    
    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualNationalityDetails), IndividualConstants.LogicalName);

        entity.AssignIfNotNull(
            IndividualConstants.Fields.IntegrationDetails.OriginCode, 
            OriginCode.ToOptionSetValue()
        );
        
        entity.AssignIfNotNull(
            CommonConstants.Fields.IntegrationDetails.ElmReferenceId, 
            ElmReferenceId);
    }
}