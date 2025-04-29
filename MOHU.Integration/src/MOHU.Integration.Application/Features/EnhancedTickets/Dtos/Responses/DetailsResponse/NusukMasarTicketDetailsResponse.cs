using MOHU.Integration.Application.Features.Tasks.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses.DetailsResponse;

public class NusukMasarTicketDetailsResponse
{
    private NusukMasarTicketDetailsResponse(Ticket ticket)
    {
        Id = ticket.Id.Id;
        BasicInformation = ticket.BasicInformation;
        IntegrationInformation = ticket.IntegrationInformation;
        CustomerInformation = ticket.CustomerInformation;
        Classification = ticket.Classification;
        LastCrmUserAction = ticket.LastCrmUserTask;
        HistoryLog = ticket.Tasks.Select(NusukMasarCrmTaskResponse.Create).ToList();
    }

    public Guid Id { get; init; }

    public NusukMasarTicketBasicInformation BasicInformation { get; init; }

    public NusukMasarTicketIntegrationInformation IntegrationInformation { get; init; }
    
    public NusukMasarCrmUserTaskResponse? LastCrmUserAction { get; init; }
    
    public NusukMasarTicketCustomerInformation CustomerInformation { get; init; }
    
    public NusukMasarTicketClassification Classification { get; init; }
    
    public List<NusukMasarCrmTaskResponse> HistoryLog { get; init; }

    public static implicit operator NusukMasarTicketDetailsResponse(Ticket ticket)
        => new(ticket);

    public static NusukMasarTicketDetailsResponse Create(Ticket ticket)
        => new(ticket);
}