using Common.Crm.Domain.Common.Constants;
using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Domain.Features.Tickets.Entities;

public class TicketBasicInformation
{
    private TicketBasicInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketBasicInformation), TicketsConstants.LogicalName);
        
        TicketNumber = entity.GetAttributeValue<string>(TicketsConstants.BasicInformation.Fields.TicketNumber);
        Title = entity.GetAttributeValue<string>(TicketsConstants.BasicInformation.Fields.Title);
        Description = entity.GetAttributeValue<string>(TicketsConstants.BasicInformation.Fields.Description);
        StatusReason = entity.GetEnumValue<TicketStatusReasonEnum>(CommonConstants.Fields.StatusReason);
        Status = entity.GetEnumValue<TicketStatusEnum>(CommonConstants.Fields.Status);
        Origin = entity.GetEnumValue<CaseOriginEnum>(TicketsConstants.BasicInformation.Fields.Origin);
        SubOrigin = entity.GetAttributeValue<EntityReference>(TicketsConstants.BasicInformation.Fields.SubOrigin);
        Origin = entity.GetEnumValue<CaseOriginEnum>(TicketsConstants.BasicInformation.Fields.Origin);
        Priority = entity.GetEnumValue<TicketPriorityEnum>(TicketsConstants.BasicInformation.Fields.Priority);
    }
    
    private TicketBasicInformation(
        string? ticketNumber,
        string? title,
        string? description,
        TicketStatusReasonEnum? statusReason,
        TicketStatusEnum? status,
        TicketPriorityEnum? priority)
    {
        TicketNumber = ticketNumber;
        Title = title;
        Description = description;
        StatusReason = statusReason;
        Status = status;
        Priority = priority;
    }

    public string? TicketNumber { get; init; }
    
    public string? Title { get; init; }

    public string? Description { get; init; }

    public CaseOriginEnum? Origin { get; init; }
    
    public EntityReference? SubOrigin { get; init; }

    public TicketStatusEnum? Status { get; private set; }
    
    public TicketStatusReasonEnum? StatusReason { get; private set; }

    public TicketPriorityEnum? Priority { get; init; }

    public static TicketBasicInformation Create(Entity entity) => new(entity);

    public static TicketBasicInformation Create(
        string? ticketNumber,
        string? title,
        string? description,
        TicketStatusReasonEnum? statusReason,
        TicketStatusEnum? status,
        TicketPriorityEnum? priority) => 
        new (ticketNumber, title, description, statusReason, status, priority);


    public void Activate(TicketActiveStatusReasonEnum statusReason = TicketActiveStatusReasonEnum.InProgress)
    {
        Status = TicketStatusEnum.Active;
        StatusReason = (TicketStatusReasonEnum)statusReason; 
    }
    
    public void Cancel(TicketCancelledStatusReasonEnum statusReason = TicketCancelledStatusReasonEnum.Cancelled)
    {
        Status = TicketStatusEnum.Cancelled;
        StatusReason = (TicketStatusReasonEnum)statusReason; 
    }
    
    public void Resolve(TicketResolvedStatusReasonEnum statusReason = TicketResolvedStatusReasonEnum.TicketResolved)
    {
        Status = TicketStatusEnum.Resolved;
        StatusReason = (TicketStatusReasonEnum)statusReason; 
    }

    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketBasicInformation), TicketsConstants.LogicalName);
        
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.TicketNumber, TicketNumber);
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Title, Title);
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Description, Description);
        entity.AssignIfNotNull(CommonConstants.Fields.Status, Status.ToOptionSetValue());
        entity.AssignIfNotNull(CommonConstants.Fields.StatusReason, StatusReason.ToOptionSetValue());
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Priority, Priority.ToOptionSetValue());
    }
}