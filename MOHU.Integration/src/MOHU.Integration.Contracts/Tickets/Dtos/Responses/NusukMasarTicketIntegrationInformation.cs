using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tickets.Entities;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketIntegrationInformation
{
    private NusukMasarTicketIntegrationInformation(TicketIntegrationInformation ticketIntegrationInformation)
    {
        LastActionDate = ticketIntegrationInformation.LastActionDate;
        Comment = ticketIntegrationInformation.Comment;
        UpdatedBy = ticketIntegrationInformation.UpdatedBy;
        IntegrationStatus = ticketIntegrationInformation.IntegrationStatus.ToLookup();
    }
    
    public DateTime? LastActionDate { get; init; }

    public string? Comment { get; init; }

    public string? UpdatedBy { get; init; }

    public LookupResponse<int>? IntegrationStatus { get; init; }

    public static implicit operator NusukMasarTicketIntegrationInformation(TicketIntegrationInformation ticketIntegrationInformation)
        => new(ticketIntegrationInformation);
}