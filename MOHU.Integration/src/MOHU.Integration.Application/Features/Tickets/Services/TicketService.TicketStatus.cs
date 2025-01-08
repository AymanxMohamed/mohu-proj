namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    public async Task<TicketStatusResponse> GetTicketStatusAsync(Guid customerId, string? ticketNumber)
    {

        if (ticketNumber is null)
        {
            ticketNumber = await DoesCustomerHaveActiveTicketsAsync(customerId);
        }
        if(ticketNumber is null)
        {
            throw new NotFoundException($"Customer with this Id: '{customerId}' has no active ticket");
        }
        return TicketStatusResponse.Create(await GetTicketDetailsAsync(customerId, ticketNumber));
    }
}