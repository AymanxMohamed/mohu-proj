using MOHU.Integration.Contracts.Tickets.Dtos;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Contracts.Dto.Kidana;

public class KidanaUpdateStatusRequest : UpdateTicketStatusData
{
    public int TicketId { get; init; }
}