using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Dto.Sahab;

public class SahabUpdateStatusRequest
{
    public string TicketNumber { get; set; } = null!;
    public string Resolution { get; set; } = null!;
    public DateTime? ResolutionDate { get; set; }
    public IntegrationStatus IntegrationStatus { get; set; }


    public UpdateTicketStatusRequest ToUpdateTicketStatusRequest(Guid ticketId) =>
        new()
        {
            TicketId = ticketId,
            IntegrationStatus = IntegrationStatus,
            FlagLogicalName = Incident.Fields.IsSahabUpdated,
            Resolution = Resolution,
            ResolutionDate = ResolutionDate
        };
}