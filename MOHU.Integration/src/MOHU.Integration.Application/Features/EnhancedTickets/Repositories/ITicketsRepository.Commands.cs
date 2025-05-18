using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

public partial interface ITicketsRepository
{
    Ticket UpdateCompanyTicket(Guid companyId, Guid ticketId, UpdateTicketRequest request);
    Task<ResolveTicketResponse> ResolveTicketAsync(ResolveTicketRequest request);
}