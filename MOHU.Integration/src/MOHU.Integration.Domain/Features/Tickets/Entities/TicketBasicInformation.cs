using Common.Crm.Domain.Common.Constants;
using Common.Crm.Domain.Common.OptionSets.Extensions;
using ErrorOr;
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
        StatusReason = entity.GetAttributeValue<EntityReference>(TicketsConstants.BasicInformation.Fields.StatusReason);
        StatusReasonOop = entity.GetEnumValue<TicketStatusReasonEnum>(CommonConstants.Fields.StatusReasonOop);
        Status = entity.GetEnumValue<TicketStatusEnum>(CommonConstants.Fields.Status);
        Origin = entity.GetEnumValue<CaseOriginEnum>(TicketsConstants.BasicInformation.Fields.Origin);
        SubOrigin = entity.GetAttributeValue<EntityReference>(TicketsConstants.BasicInformation.Fields.SubOrigin);
        Company = entity.GetAttributeValue<EntityReference>(TicketsConstants.BasicInformation.Fields.Company);
        Origin = entity.GetEnumValue<CaseOriginEnum>(TicketsConstants.BasicInformation.Fields.Origin);
        Priority = entity.GetEnumValue<TicketPriorityEnum>(TicketsConstants.BasicInformation.Fields.Priority);
        CreatedOn = entity.GetAttributeValue<DateTime>(CommonConstants.Fields.CreatedOn);
        ModifiedOn = entity.GetAttributeValue<DateTime>(CommonConstants.Fields.ModifiedOn);
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
        StatusReasonOop = statusReason;
        Status = status;
        Priority = priority;
    }

    public string? TicketNumber { get; init; }
    
    public string? Title { get; init; }

    public string? Description { get; init; }

    public CaseOriginEnum? Origin { get; init; }
    
    public EntityReference? SubOrigin { get; init; }
    
    public EntityReference? Company { get; init; }

    public TicketStatusEnum? Status { get; private set; }
    
    public TicketStatusReasonEnum? StatusReasonOop { get; private set; }
    
    public EntityReference? StatusReason { get; private set; }

    public TicketPriorityEnum? Priority { get; init; }

    public DateTime CreatedOn { get; init; }

    public DateTime ModifiedOn { get; init; }

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
        StatusReasonOop = (TicketStatusReasonEnum)statusReason; 
    }
    
    public void Cancel(TicketCancelledStatusReasonEnum statusReason = TicketCancelledStatusReasonEnum.Cancelled)
    {
        Status = TicketStatusEnum.Cancelled;
        StatusReasonOop = (TicketStatusReasonEnum)statusReason; 
    }
    
    public void Resolve(TicketResolvedStatusReasonEnum statusReason = TicketResolvedStatusReasonEnum.TicketResolved)
    {
        Status = TicketStatusEnum.Resolved;
        StatusReasonOop = (TicketStatusReasonEnum)statusReason; 
    }

    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketBasicInformation), TicketsConstants.LogicalName);
        
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.TicketNumber, TicketNumber);
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Title, Title);
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Description, Description);
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Company, Company);
        entity.AssignIfNotNull(CommonConstants.Fields.Status, Status.ToOptionSetValue());
        entity.AssignIfNotNull(CommonConstants.Fields.StatusReasonOop, StatusReasonOop.ToOptionSetValue());
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.StatusReason, StatusReason);
        entity.AssignIfNotNull(TicketsConstants.BasicInformation.Fields.Priority, Priority.ToOptionSetValue());
    }
}