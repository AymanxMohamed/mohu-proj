using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository
{
    public Ticket UpdateCompanyTicket(Guid companyId, Guid ticketId, UpdateTicketRequest request)
    {
        var ticket = GetCompanyTicket(companyId, ticketId);
        ticket.EnsureCanUpdateAsCompany(companyId);
        request.Update(ticket);
        genericRepository.Update(ticket.ToCrmEntity());
        genericRepository.Commit();
        return ticket;
    }
}