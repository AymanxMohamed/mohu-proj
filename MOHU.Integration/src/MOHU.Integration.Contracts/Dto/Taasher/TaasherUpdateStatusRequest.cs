using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Contracts.Dto.Taasher;

public class TaasherUpdateStatusRequest : UpdateTicketStatusData
{
    public string TicketNumber { get; init; } = null!;
}