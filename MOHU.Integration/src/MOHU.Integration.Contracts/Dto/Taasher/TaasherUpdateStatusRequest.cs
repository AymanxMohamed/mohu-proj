using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Contracts.Dto.Taasher;

public class TaasherUpdateStatusRequest : UpdateTicketStatusData
{
    [Required]
    public string TicketNumber { get; init; } = null!;
}