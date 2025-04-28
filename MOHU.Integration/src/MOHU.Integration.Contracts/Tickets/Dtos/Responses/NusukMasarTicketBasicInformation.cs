using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Features.Tickets.Entities;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketBasicInformation
{
    private NusukMasarTicketBasicInformation(TicketBasicInformation ticketBasicInformation)
    {
        Title = ticketBasicInformation.Title;
        Description = ticketBasicInformation.Description;
        Origin = ticketBasicInformation.Origin;
        Company = ticketBasicInformation.Company;
        PortalStatus = ticketBasicInformation.PortalStatus;
        Priority = ticketBasicInformation.Priority;
        CreatedOn = ticketBasicInformation.CreatedOn;
        ModifiedOn = ticketBasicInformation.ModifiedOn;
    }

    public string? Title { get; init; }
    
    public string? Description { get; init; }

    public CaseOriginEnum? Origin { get; init; }

    public TicketPriorityEnum? Priority { get; init; }

    public EntityReference? Company { get; init; }

    public EntityReference? PortalStatus { get; init; }
    
    public DateTime CreatedOn { get; init; }

    public DateTime ModifiedOn { get; init; }

    public static implicit operator NusukMasarTicketBasicInformation(TicketBasicInformation ticketBasicInformation)
        => new(ticketBasicInformation);

    public static NusukMasarTicketBasicInformation Create(TicketBasicInformation ticketBasicInformation) => 
        new(ticketBasicInformation);
}