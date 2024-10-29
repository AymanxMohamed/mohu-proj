using System.ComponentModel.DataAnnotations;
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public class UpdateTicketStatusData
{
    protected UpdateTicketStatusData(UpdateTicketStatusData data)
    {
        Resolution = data.Resolution;
        ResolutionDate = data.ResolutionDate;
        IntegrationStatus = data.IntegrationStatus;
        UpdatedBy = data.UpdatedBy;
        LastActionDate = data.LastActionDate;
    }

    protected UpdateTicketStatusData()
    {
    }
    
    [MaxLength(400)] 
    public string Resolution { get; init; } = null!;
    
    public DateTime? ResolutionDate { get; init; }
    
    public IntegrationStatus IntegrationStatus { get; init; }

    public string? UpdatedBy { get; init; }

    public DateTime? LastActionDate { get; init; }

    public UpdateTicketStatusRequest ToUpdateRequest(Guid ticketId, string flagLogicalName) => 
        new(flagLogicalName, ticketId, data: this);

    protected Entity UpdateTicketEntity(Entity ticket)
    {
        ticket.Attributes.Add(Incident.Fields.IntegrationClosureReason, Resolution);
        ticket.Attributes.Add(Incident.Fields.IntegrationClosureDate, ResolutionDate);
        ticket.Attributes.Add(Incident.Fields.IntegrationStatus,
            new OptionSetValue(Convert.ToInt32(IntegrationStatus)));

        return ticket;
    }
}