using Common.Crm.Domain.Common.Factories;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Entities;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Domain.Features.Tickets;

public class Ticket : CrmEntity
{
    private Ticket(Entity entity)
        : base(entity)
    {
        BasicInformation = TicketBasicInformation.Create(entity);
        Classification = TicketClassification.Create(entity);
        CustomerInformation = TicketCustomerInformation.Create(entity);
    }
    
    private Ticket(
        EntityReference? id, 
        TicketBasicInformation basicInformation,
        TicketClassification classification,
        TicketCustomerInformation customerInformation)
        : base(id ?? EntityReferenceFactory.Create(TicketsConstants.LogicalName))
    {
        BasicInformation = basicInformation;
        Classification = classification;
        CustomerInformation = customerInformation;
    }

    public TicketBasicInformation BasicInformation { get; init; }
    
    public TicketClassification Classification { get; init; }
    
    public TicketCustomerInformation CustomerInformation { get; init; }

    public static Ticket Create(Entity entity) => new(entity);

    public static Ticket Create(
        EntityReference? id,
        TicketBasicInformation basicInformation,
        TicketClassification classification,
        TicketCustomerInformation customerInformation) => 
        new (id, basicInformation, classification, customerInformation);


    public void Activate(TicketActiveStatusReasonEnum statusReason = TicketActiveStatusReasonEnum.InProgress)
        => BasicInformation.Activate(statusReason);
    
    public void Cancel(TicketCancelledStatusReasonEnum statusReason = TicketCancelledStatusReasonEnum.Cancelled)
        => BasicInformation.Cancel(statusReason);
    
    public void Resolve(TicketResolvedStatusReasonEnum statusReason = TicketResolvedStatusReasonEnum.TicketResolved)
        => BasicInformation.Resolve(statusReason);

    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        BasicInformation.UpdateEntity(entity);
        Classification.UpdateEntity(entity);
        CustomerInformation.UpdateEntity(entity);
        
        return entity;
    }
}
