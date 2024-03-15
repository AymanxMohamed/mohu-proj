using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Dto.Ticket;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface ITicketService
    {
        Task UpdateStatus(UpdateStatusRequest request);
        Task<bool> CancelTicket(CancelTicketResponse ticket);
        Task<bool> IsTicketExists(Guid ticketId); 


    }
}
