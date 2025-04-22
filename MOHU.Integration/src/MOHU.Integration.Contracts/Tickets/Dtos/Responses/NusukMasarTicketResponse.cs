using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketResponse
{
    private NusukMasarTicketResponse(Ticket ticket)
    {
        Id = ticket.Id.Id;
        
        TicketNumber = ticket.BasicInformation.Title;
        
        StatusReason = ticket.BasicInformation.StatusReason?.Name;
        
        State = ticket.BasicInformation.Status?.ToString();

        Status = ticket.BasicInformation.StatusReasonOop?.ToString();
        
        Origin = ticket.BasicInformation.Origin?.ToString();
        
        Priority = ticket.BasicInformation.Priority?.ToString();

        CustomerName = ticket.CustomerInformation?.CustomerReference?.Name;
        
        CreatedOn = ticket.BasicInformation.CreatedOn;
        
        ModifiedOn = ticket.BasicInformation.ModifiedOn;
    }

    public Guid Id { get; init; }
    
    public string? TicketNumber { get; init; }
    
    public string? StatusReason { get; init; }
    
    public string? State { get; init; }
    
    public string? Status { get; init; }

    public string? Origin { get; init; }

    public string? Priority { get; init; }

    public string? CustomerName { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public DateTime? ModifiedOn { get; set; }
    
    public static implicit operator NusukMasarTicketResponse(Ticket ticket) => new(ticket);

    public static NusukMasarTicketResponse Create(Ticket ticket) => new(ticket);
}