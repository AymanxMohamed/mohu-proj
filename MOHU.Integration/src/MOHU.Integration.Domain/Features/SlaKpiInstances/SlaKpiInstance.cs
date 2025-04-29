using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.SlaKpiInstances.Constants;
using MOHU.Integration.Domain.Features.SlaKpiInstances.Enums;
using MOHU.Integration.Domain.Features.Tasks.Constants;

namespace MOHU.Integration.Domain.Features.SlaKpiInstances;

public class SlaKpiInstance : CrmEntity
{
    private SlaKpiInstance(Entity entity) 
        : base(entity)
    {
        ApplicableFromValue = entity.GetAttributeValue<DateTime>(SlaKpiInstanceConstants.Fields.ApplicableFromValue);
        ComputedFailureTime = entity.GetAttributeValue<DateTime>(SlaKpiInstanceConstants.Fields.ComputedFailureTime);
        ComputedWarningTime = entity.GetAttributeValue<DateTime>(SlaKpiInstanceConstants.Fields.ComputedWarningTime);
        FailureTime = entity.GetAttributeValue<DateTime>(SlaKpiInstanceConstants.Fields.FailureTime);
        SucceedOn = entity.GetAttributeValue<DateTime>(SlaKpiInstanceConstants.Fields.SucceedOn);
        WarningTime = entity.GetAttributeValue<DateTime>(SlaKpiInstanceConstants.Fields.WarningTime);
        ElapsedTime = entity.GetAttributeValue<int>(SlaKpiInstanceConstants.Fields.ElapsedTime);
        Status = entity.GetEnumValue<SlaKpiInstanceStatusEnum>(SlaKpiInstanceConstants.Fields.Status);
        WarningTimeReached = entity.GetEnumValue<WarningTimeReachedEnum>(SlaKpiInstanceConstants.Fields.WarningTimeReached);
    }

    public DateTime? ApplicableFromValue { get; init; }
    
    public DateTime? ComputedFailureTime { get; init; }
    
    public DateTime? ComputedWarningTime { get; init; }
    
    public int? ElapsedTime { get; init; }
    
    public DateTime? FailureTime { get; init; }
    
    public SlaKpiInstanceStatusEnum? Status { get; init; }
    
    public DateTime? SucceedOn { get; init; }
    
    public DateTime? WarningTime { get; init; }
    
    public WarningTimeReachedEnum? WarningTimeReached { get; init; }

    public static SlaKpiInstance? Create(Entity? entity) => entity is null 
        ? null 
        : new SlaKpiInstance(entity);
    
    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        UpdateEntity(entity);
        
        return entity;
    }

    private void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(SlaKpiInstance), SlaKpiInstanceConstants.LogicalName);
        
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.ApplicableFromValue, ApplicableFromValue);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.ComputedFailureTime, ComputedFailureTime);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.ComputedWarningTime, ComputedWarningTime);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.ElapsedTime, ElapsedTime);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.FailureTime, FailureTime);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.SucceedOn, SucceedOn);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.WarningTime, WarningTime);
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.Status, Status.ToOptionSetValue());
        entity.AssignIfNotNull(SlaKpiInstanceConstants.Fields.WarningTimeReached, WarningTimeReached.ToOptionSetValue());
    }
}