namespace MOHU.Integration.Contracts.Dto.Ticket;

public class TicketStatusResponse(TicketDetailsResponse ticketDetailsResponse)
{
    public Guid Id { get; init; } = ticketDetailsResponse.Id;

    public string TicketNumber { get; init; } = ticketDetailsResponse.TicketNumber;

    public string Status { get; init; } = ticketDetailsResponse.Status;
    
    public static TicketStatusResponse Create(TicketDetailsResponse ticketDetailsResponse) => 
        new(ticketDetailsResponse);
}