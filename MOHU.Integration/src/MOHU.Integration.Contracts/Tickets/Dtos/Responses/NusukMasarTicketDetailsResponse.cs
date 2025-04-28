using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketDetailsResponse
{
    private NusukMasarTicketDetailsResponse(Ticket ticket)
    {
        Id = ticket.Id;
        BasicInformation = ticket.BasicInformation;
        IntegrationInformation = ticket.IntegrationInformation;
        CustomerInformation = ticket.CustomerInformation;
        Classification = ticket.Classification;
    }

    public EntityReference Id { get; init; }

    public NusukMasarTicketBasicInformation BasicInformation { get; init; }

    public NusukMasarTicketIntegrationInformation IntegrationInformation { get; init; }
    
    public NusukMasarTicketCustomerInformation CustomerInformation { get; init; }
    
    public NusukMasarTicketClassification Classification { get; init; }

    public static implicit operator NusukMasarTicketDetailsResponse(Ticket ticket)
        => new(ticket);

    public static NusukMasarTicketDetailsResponse Create(Ticket ticket)
        => new(ticket);
}