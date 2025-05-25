using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.ServiceDefinitions.Constants;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses.DetailsResponse;

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

    public bool IsNusukEnayaService()
    {
        return Service?.Id != null 
               && (Service?.Id == ServiceDefinitionConstants.Services.MafkodenService 
                   || Service?.Id == ServiceDefinitionConstants.Services.ErshadService
                   || Service?.Id == ServiceDefinitionConstants.Services.SosService);
    }

    public static implicit operator NusukMasarTicketClassification(TicketClassification ticketClassification)
        => new(ticketClassification);
}