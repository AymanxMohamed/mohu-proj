using MOHU.Integration.Contracts.Tickets.Dtos;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Contracts.Dto.ServiceDesk;

public class ServiceDeskUpdateStatusRequest : UpdateTicketStatusData
{
    public string TicketNumber { get; init; } = null!;
}