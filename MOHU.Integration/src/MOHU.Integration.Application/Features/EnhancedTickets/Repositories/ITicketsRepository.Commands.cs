using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

public partial interface ITicketsRepository
{
    Ticket UpdateCompanyTicket(Guid companyId, Guid ticketId);
}

public class UpdateCompanyTicketCommand
{
    public string Comment { get; init; }

    public string UpdatedBy { get; init; }

    public DateTime UpdatedOn { get; init; }
}