using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses;

public class NusukMasarTicketListResponse
{
    private NusukMasarTicketListResponse(Ticket ticket)
    {
        Id = ticket.Id.Id;
        
        TicketNumber = ticket.BasicInformation.Title;
        
        RequestType = ticket.Classification.RequestType.ToLookup();
        
        StatusReason = ticket.BasicInformation.StatusReason.ToLookup();
        
        PortalStatus = ticket.BasicInformation.PortalStatus.ToLookup();
        
        IntegrationStatus = ticket.IntegrationInformation.IntegrationStatus.ToLookup();
        
        State = ticket.BasicInformation.Status.ToLookup();

        Origin = ticket.BasicInformation.Origin.ToLookup();
        
        Priority = ticket.BasicInformation.Priority.ToLookup();

        Customer = ticket.CustomerInformation?.CustomerReference.ToLookup();
        
        CreatedOn = ticket.BasicInformation.CreatedOn;
        
        ModifiedOn = ticket.BasicInformation.ModifiedOn;
    }

    public Guid Id { get; init; }

    public LookupResponse<Guid>? RequestType { get; init; }
    
    public string? TicketNumber { get; init; }
    
    public LookupResponse<Guid>? StatusReason { get; init; }
    
    public LookupResponse<Guid>? PortalStatus { get; init; }
    
    public LookupResponse<int>? IntegrationStatus { get; init; }
    
    public LookupResponse<int>? State { get; init; }
    
    public LookupResponse<int>? Origin { get; init; }

    public LookupResponse<int>? Priority { get; init; }

    public LookupResponse<Guid>? Customer { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public DateTime? ModifiedOn { get; set; }
    
    public static implicit operator NusukMasarTicketListResponse(Ticket ticket) => new(ticket);

    public static NusukMasarTicketListResponse Create(Ticket ticket) => new(ticket);
}