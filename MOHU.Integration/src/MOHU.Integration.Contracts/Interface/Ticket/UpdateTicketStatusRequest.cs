using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Contracts.Interface.Ticket;

public class UpdateTicketStatusRequest(
    string flagLogicalName,
    Guid ticketId,
    UpdateTicketStatusData data)
    : UpdateStatusRequest(ticketId, data)
{
    public string FlagLogicalName { get; init; } = flagLogicalName;

    public new Entity ToTicketEntity()
    {
        var entity = base.ToTicketEntity();
        entity.Attributes.Add(FlagLogicalName, true);
        return entity;
    }
}