using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Domain.Features.Tickets.Entities;

public class TicketIntegrationInformation
{
    private TicketIntegrationInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketIntegrationInformation), TicketsConstants.LogicalName);
        
        ClosureReason = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureReason);
        UpdatedBy = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationUpdatedBy);
        ClosureDate = entity.GetAttributeValue<DateTime>(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureDate);
        LastActionDate = entity.GetAttributeValue<DateTime>(TicketsConstants.IntegrationInformation.Fields.IntegrationLastActionDate);
        Comment = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationComment);
        IntegrationStatus = entity.GetEnumValue<IntegrationStatus>(TicketsConstants.IntegrationInformation.Fields.IntegrationStatus);
    }

    private TicketIntegrationInformation(
        string? closureReason, 
        DateTime? closureDate,
        IntegrationStatus? integrationStatus, 
        string? comment, 
        string? updatedBy,
        DateTime? lastActionDate)
    {
        ClosureReason = closureReason;
        ClosureDate = closureDate;
        IntegrationStatus = integrationStatus;
        Comment = comment;
        UpdatedBy = updatedBy;
        LastActionDate = lastActionDate;
    }

    public string? ClosureReason { get; private set; }

    public DateTime? ClosureDate { get; private set; }

    public IntegrationStatus? IntegrationStatus { get; private set; }

    public string? Comment { get; private set; }

    public string? UpdatedBy { get; private set; }

    public DateTime? LastActionDate { get; private set; }

    public void Update(string comment, string updatedBy, IntegrationStatus integrationStatus)
    {
        ClosureReason = comment;
        ClosureDate = DateTime.UtcNow;
        LastActionDate = DateTime.UtcNow;
        Comment = comment;
        UpdatedBy = updatedBy;
        IntegrationStatus = integrationStatus;
    }

    public static TicketIntegrationInformation Create(Entity entity) => new(entity);

    public static TicketIntegrationInformation Create(
        string comment, 
        string updatedBy,
        IntegrationStatus integrationStatus) => 
        new(comment, DateTime.UtcNow, integrationStatus, comment, updatedBy, DateTime.UtcNow);

    public static TicketIntegrationInformation Create(
        string? closureReason,
        DateTime? closureDate,
        IntegrationStatus? integrationStatus,
        string? comment,
        string? updatedBy,
        DateTime? lastActionDate) =>
        new(closureReason, closureDate, integrationStatus, comment, updatedBy, lastActionDate);

    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketIntegrationInformation), TicketsConstants.LogicalName);
        
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureReason, ClosureReason);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureDate, ClosureDate);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationComment, Comment);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationLastActionDate, LastActionDate);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationUpdatedBy, UpdatedBy);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationStatus, IntegrationStatus.ToOptionSetValue());
    }
}