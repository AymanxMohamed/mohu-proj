namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    public async Task<TicketStatusResponse> GetTicketStatusAsync(Guid customerId, string ticketNumber) => 
        TicketStatusResponse.Create(await GetTicketDetailsAsync(customerId, ticketNumber));
}