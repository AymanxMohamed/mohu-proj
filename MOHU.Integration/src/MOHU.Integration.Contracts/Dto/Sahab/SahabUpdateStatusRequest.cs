using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Contracts.Tickets.Dtos;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Dto.Sahab;

public class SahabUpdateStatusRequest : UpdateTicketStatusData
{
    public string TicketNumber { get; init; } = null!;

    public UpdateTicketStatusRequest ToUpdateTicketStatusRequest(Guid ticketId) =>
        ToUpdateRequest(ticketId, Incident.Fields.IsSahabUpdated);
}