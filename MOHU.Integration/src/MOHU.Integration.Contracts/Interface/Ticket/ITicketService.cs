using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.Sahab;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Contracts.Interface.Ticket
{
    public interface ITicketService
    {
        Task<TicketListResponse> GetAllTicketsAsync(Guid customerId, int pageNumber = 1, int pageSize = 10);
        
        Task<TicketDetailsResponse> GetTicketDetailsAsync(Guid customerId, string ticketNumber);
        Task<TicketDetailsResponse> GetTicketDetailByNumberAsync(string ticketNumber);

        Task<TicketStatusResponse> GetTicketStatusAsync(Guid customerId, string? ticketNumber);
        
        Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request);
        
        Task<SubmitTicketResponse> SubmitHootSuiteTicketAsync(Guid customerId, CreateHootSuiteTicketRequest request);
        
        Task<List<TicketTypeResponse>> GetTicketTypesAsync();
        
        Task<bool> UpdateStatusAsync(UpdateTicketStatusRequest request);
        Task<bool> UpdateSahabTicket(UpdateSahabTicket ticket);


        Task<Guid> GetTicketByIntegrationTicketNumberAsync(string integrationTicketNumber, string ticketNumberSchemaName);
    }
}
