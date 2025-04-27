using Common.Crm.Domain.Common.OptionSets.Constants;
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketResponse
{
    private NusukMasarTicketResponse(Ticket ticket)
    {
        Id = ticket.Id.Id;
        
        TicketNumber = ticket.BasicInformation.Title;
        
        RequestType = ticket.Classification.RequestType;
        
        StatusReason = ticket.BasicInformation.StatusReason?.Name;
        
        State = ticket.BasicInformation.Status;

        Status = ticket.BasicInformation.StatusReasonOop;

        Origin = ticket.BasicInformation.Origin;
        
        Priority = ticket.BasicInformation.Priority;

        CustomerName = ticket.CustomerInformation?.CustomerReference?.Name;
        
        CreatedOn = ticket.BasicInformation.CreatedOn;
        
        ModifiedOn = ticket.BasicInformation.ModifiedOn;
    }

    public Guid Id { get; init; }

    public EntityReference? RequestType { get; init; }
    
    public string? TicketNumber { get; init; }
    
    public string? StatusReason { get; init; }
    
    public TicketStatusEnum? State { get; init; }
    
    public TicketStatusReasonEnum? Status { get; init; }

    public CaseOriginEnum? Origin { get; init; }

    public TicketPriorityEnum? Priority { get; init; }

    public string? CustomerName { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public DateTime? ModifiedOn { get; set; }
    
    public static implicit operator NusukMasarTicketResponse(Ticket ticket) => new(ticket);

    public static NusukMasarTicketResponse Create(Ticket ticket) => new(ticket);
}