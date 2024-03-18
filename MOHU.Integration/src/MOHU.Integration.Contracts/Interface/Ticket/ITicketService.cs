using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.Ticket;

namespace MOHU.Integration.Contracts.Interface.Ticket
{
    public interface ITicketService
    {
        Task<TicketListResponse> GetAllTicketsAsync(Guid customerId, int pageNumber = 1, int pageSize = 10);
        Task<TicketDetailsResponse> GetTicketDetailsAsync(Guid customerId, string ticketNumber);
        Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request);
        Task<List<TicketTypeResponse>> GetTicketTypesAsync();
        Task<bool> UpdateStatus(UpdateStatusRequest request);
    } 
}
