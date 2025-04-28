using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketClassification
{
    private NusukMasarTicketClassification(TicketClassification ticketClassification)
    {
        RequestType = ticketClassification.RequestType;
        Service = ticketClassification.RequestType;
        MainCategory = ticketClassification.RequestType;
        SubCategory = ticketClassification.RequestType;
        SecondarySubCategory = ticketClassification.RequestType;
    }
    
    public EntityReference? RequestType { get; init; }
    
    public EntityReference? Service { get; init; }
    
    public EntityReference? MainCategory { get; init; }
    
    public EntityReference? SubCategory { get; init; }
    
    public EntityReference? SecondarySubCategory { get; init; }

    public static implicit operator NusukMasarTicketClassification(TicketClassification ticketClassification)
        => new(ticketClassification);
}