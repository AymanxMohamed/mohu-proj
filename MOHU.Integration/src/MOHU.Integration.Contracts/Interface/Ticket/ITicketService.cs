using MOHU.Integration.Contracts.Dto.Ticket;

namespace MOHU.Integration.Contracts.Interface.Ticket
{
    public interface ITicketService
    {
        Task<TicketListResponse> GetTicketsAsync(Guid customerId, int pageNumber = 1, int pageSize = 10);
        Task<TicketDetailsResponse> GetTicketDetailsAsync(Guid customerId, string ticketNumber);
    }
}
