using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public class UpdateTicketRequest
{
    public string Comment { get; init; } = null!;

    public string UpdatedBy { get; init; } = null!;

    public IntegrationStatus IntegrationStatus { get; init; }

    public Ticket Update(Ticket ticket)
    {
        ticket.UpdateIntegrationInformation(Comment, UpdatedBy, IntegrationStatus);
        return ticket;
    }
}
