using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository
{
    public Ticket UpdateCompanyTicket(Guid companyId, Guid ticketId, UpdateTicketRequest request)
    {
        var ticket = GetCompanyTicket(companyId, ticketId, columnSet: TicketIntegrationInformation.TicketUpdateColumnSet);
        ticket.EnsureCanUpdateAsCompany(companyId);
        request.Update(ticket);
        genericRepository.Update(ticket.ToCrmEntity());
        genericRepository.Commit();
        return ticket;
    }
}