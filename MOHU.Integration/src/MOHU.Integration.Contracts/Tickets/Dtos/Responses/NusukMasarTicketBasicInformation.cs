using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketBasicInformation
{
    private NusukMasarTicketBasicInformation(TicketBasicInformation ticketBasicInformation)
    {
        TicketNumber = ticketBasicInformation.Title;
        Description = ticketBasicInformation.Description;
        Origin = ticketBasicInformation.Origin.ToLookup();
        Company = ticketBasicInformation.Company.ToLookup();
        PortalStatus = ticketBasicInformation.PortalStatus.ToLookup();
        Priority = ticketBasicInformation.Priority.ToLookup();
        CreatedOn = ticketBasicInformation.CreatedOn;
        ModifiedOn = ticketBasicInformation.ModifiedOn;
        Status = ticketBasicInformation.Status.ToLookup();
        StatusReason = ticketBasicInformation.StatusReason.ToLookup();
    }

    public string? TicketNumber { get; init; }
    
    public string? Description { get; init; }

    public LookupResponse<int>? Origin { get; init; }

    public LookupResponse<int>? Priority { get; init; }

    public LookupResponse<Guid>? Company { get; init; }

    public LookupResponse<Guid>? PortalStatus { get; init; }
    
    public LookupResponse<int>? Status { get; init; }
    
    public LookupResponse<Guid>? StatusReason { get; private set; }
    
    public DateTime CreatedOn { get; init; }

    public DateTime ModifiedOn { get; init; }

    public static implicit operator NusukMasarTicketBasicInformation(TicketBasicInformation ticketBasicInformation)
        => new(ticketBasicInformation);

    public static NusukMasarTicketBasicInformation Create(TicketBasicInformation ticketBasicInformation) => 
        new(ticketBasicInformation);
}