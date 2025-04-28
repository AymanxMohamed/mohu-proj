using Common.Crm.Application.Common.Dtos.Responses;
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketClassification
{
    private NusukMasarTicketClassification(TicketClassification ticketClassification)
    {
        RequestType = ticketClassification.RequestType.ToLookup();
        Service = ticketClassification.Service.ToLookup();
        MainCategory = ticketClassification.MainCategory.ToLookup();
        SubCategory = ticketClassification.SubCategory.ToLookup();
        SecondarySubCategory = ticketClassification.SecondarySubCategory.ToLookup();
    }
    
    public LookupResponse<Guid>? RequestType { get; init; }
    
    public LookupResponse<Guid>? Service { get; init; }
    
    public LookupResponse<Guid>? MainCategory { get; init; }
    
    public LookupResponse<Guid>? SubCategory { get; init; }
    
    public LookupResponse<Guid>? SecondarySubCategory { get; init; }

    public static implicit operator NusukMasarTicketClassification(TicketClassification ticketClassification)
        => new(ticketClassification);
}