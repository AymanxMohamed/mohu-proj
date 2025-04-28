using Common.Crm.Domain.Common.Constants;
using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.SlaKpiInstances;
using MOHU.Integration.Domain.Features.SlaKpiInstances.Constants;
using MOHU.Integration.Domain.Features.Tasks.Constants;
using MOHU.Integration.Domain.Features.Tasks.Enums;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Domain.Features.Tasks;

public class CrmTask : CrmEntity
{
    private CrmTask(Entity entity)
        : base(entity)
    {
        Subject = entity.GetAttributeValue<string>(TaskConstants.Fields.Subject);
        Status = entity.GetEnumValue<TaskStatusEnum>(CommonConstants.Fields.Status);
        Priority = entity.GetEnumValue<TicketPriorityEnum>(TaskConstants.Fields.Priority);
        TaskType = entity.GetEnumValue<TaskTypeEnum>(TaskConstants.Fields.TaskType);
        CreatedOn = entity.GetAttributeValue<DateTime>(CommonConstants.Fields.CreatedOn);
        ModifiedOn = entity.GetAttributeValue<DateTime>(CommonConstants.Fields.ModifiedOn);
        ActualEnd = entity.GetAttributeValue<DateTime>(TaskConstants.Fields.ActualEnd);
        
        Decision = entity.GetAttributeValue<string>(TaskConstants.Fields.Decision);
        DecisionMadeBy = entity.GetAttributeValue<EntityReference>(TaskConstants.Fields.DecisionMadeBy);
        Comment = entity.GetAttributeValue<string>(TaskConstants.Fields.Comment);
        
        ProcessingTimeInMinutes = entity.GetAttributeValue<int>(TaskConstants.Fields.ProcessingTimeInMinutes);
        IsResolvedBySla = entity.GetAttributeValue<bool>(TaskConstants.Fields.IsResolvedBySla);
        IsSendEscalationL1 = entity.GetAttributeValue<bool>(TaskConstants.Fields.IsSendEscalationL1);
        IsSendEscalationL2 = entity.GetAttributeValue<bool>(TaskConstants.Fields.IsSendEscalationL2);
        IsSendEscalationL3 = entity.GetAttributeValue<bool>(TaskConstants.Fields.IsSendEscalationL3);

        LevelOneSla = SlaKpiInstance.Create(entity.GetAliasedEntity(
            TaskConstants.RelatedEntities.SlaKpiInstance.SlaLevelOneTimer.Alies,
            SlaKpiInstanceConstants.LogicalName));
        
        LevelTwoSla = SlaKpiInstance.Create(entity.GetAliasedEntity(
            TaskConstants.RelatedEntities.SlaKpiInstance.SlaLevelTwoTimer.Alies,
            SlaKpiInstanceConstants.LogicalName));
        
        LevelThreeSla = SlaKpiInstance.Create(entity.GetAliasedEntity(
            TaskConstants.RelatedEntities.SlaKpiInstance.SlaLevelThreeTimer.Alies,
            SlaKpiInstanceConstants.LogicalName));
    }

    public string? Subject { get; init; }

    public TaskStatusEnum? Status { get; init; }

    public TicketPriorityEnum? Priority { get; init; }

    public TaskTypeEnum? TaskType { get; init; }
    
    public DateTime? CreatedOn { get; init; }
    
    public DateTime? ModifiedOn { get; init; }
    
    public DateTime? ActualEnd { get; init; }
    
    public string? Decision { get; init; }

    public EntityReference? DecisionMadeBy { get; init; }
    
    public string? Comment { get; init; }
    
    public int? ProcessingTimeInMinutes { get; init; }

    public bool? IsResolvedBySla { get; init; }

    public bool? IsSendEscalationL1 { get; init; }

    public bool? IsSendEscalationL2 { get; init; }

    public bool? IsSendEscalationL3 { get; init; }

    public SlaKpiInstance? LevelOneSla { get; init; }
    
    public SlaKpiInstance? LevelTwoSla { get; init; }
    
    public SlaKpiInstance? LevelThreeSla { get; init; }

    public static CrmTask Create(Entity entity) => new(entity);
    
    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
       UpdateEntity(entity);
        
        return entity;
    }

    private void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(CrmTask), TaskConstants.LogicalName);
        
        entity.AssignIfNotNull(TaskConstants.Fields.Subject, Subject);
        entity.AssignIfNotNull(CommonConstants.Fields.Status, Status.ToOptionSetValue());
        entity.AssignIfNotNull(TaskConstants.Fields.Priority, Priority.ToOptionSetValue());
        entity.AssignIfNotNull(TaskConstants.Fields.TaskType, TaskType.ToOptionSetValue());
        entity.AssignIfNotNull(CommonConstants.Fields.CreatedOn, CreatedOn);
        entity.AssignIfNotNull(CommonConstants.Fields.ModifiedOn, ModifiedOn);
        entity.AssignIfNotNull(TaskConstants.Fields.ActualEnd, ActualEnd);
        entity.AssignIfNotNull(TaskConstants.Fields.Decision, Decision);
        entity.AssignIfNotNull(TaskConstants.Fields.DecisionMadeBy, DecisionMadeBy);
        entity.AssignIfNotNull(TaskConstants.Fields.Comment, Comment);
        entity.AssignIfNotNull(TaskConstants.Fields.ProcessingTimeInMinutes, ProcessingTimeInMinutes);
        entity.AssignIfNotNull(TaskConstants.Fields.IsResolvedBySla, IsResolvedBySla);
        entity.AssignIfNotNull(TaskConstants.Fields.IsSendEscalationL1, IsSendEscalationL1);
        entity.AssignIfNotNull(TaskConstants.Fields.IsSendEscalationL2, IsSendEscalationL2);
        entity.AssignIfNotNull(TaskConstants.Fields.IsSendEscalationL3, IsSendEscalationL3);
    }
}