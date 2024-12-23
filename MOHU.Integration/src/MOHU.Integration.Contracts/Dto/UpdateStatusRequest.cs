using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Dto;

public class UpdateStatusRequest : UpdateTicketStatusData
{
    protected UpdateStatusRequest(
        Guid ticketId, 
        UpdateTicketStatusData data,
        Guid? customerId = null)
        : base(data)
    {
        TicketId = ticketId;
        CustomerId = customerId;
    }

    protected UpdateStatusRequest()
    {
    }
        
    public Guid? CustomerId { get; init; }

    public Guid TicketId { get; init; }


    public Entity ToTicketEntity() => UpdateTicketEntity(new Entity(Incident.EntityLogicalName, TicketId));
}