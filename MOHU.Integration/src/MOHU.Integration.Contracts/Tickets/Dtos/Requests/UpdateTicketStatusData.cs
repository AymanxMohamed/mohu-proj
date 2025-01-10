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
        Comment = data.Comment;
        LastActionDate = data.LastActionDate;
    }

    protected UpdateTicketStatusData()
    {
    }
    
    [MaxLength(400)] 
    public string? Resolution { get; init; } = null!;
    
    public DateTime? ResolutionDate { get; init; }
    
    public IntegrationStatus IntegrationStatus { get; init; }

    public string? Comment { get; init; }

    public string? UpdatedBy { get; init; }

    public DateTime? LastActionDate { get; init; }

    public UpdateTicketStatusRequest ToUpdateRequest(Guid ticketId, string flagLogicalName) => 
        new(flagLogicalName, ticketId, data: this);

    protected Entity UpdateTicketEntity(Entity ticket)
    {
        ticket.Attributes.Add(Incident.Fields.IntegrationStatus,
           new OptionSetValue(Convert.ToInt32(IntegrationStatus)));

        if (!string.IsNullOrEmpty(Resolution))
            ticket.Attributes.Add(Incident.Fields.IntegrationClosureReason, Resolution);
        if ( ResolutionDate.HasValue )
            ticket.Attributes.Add(Incident.Fields.IntegrationClosureDate, ResolutionDate);

        if (!string.IsNullOrEmpty(Comment))
            ticket.Attributes.Add(Incident.Fields.IntegrationComment, Comment);
        if (!string.IsNullOrEmpty(UpdatedBy))
            ticket.Attributes.Add(Incident.Fields.IntegrationUpdatedBy, UpdatedBy);
        if (LastActionDate.HasValue)
            ticket.Attributes.Add(Incident.Fields.IntegrationLastActionDate, LastActionDate);

        return ticket;
    }
}