using MOHU.Integration.Contracts.Tickets.Dtos;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Contracts.Dto.Kidana;

public class KidanaUpdateStatusRequest : UpdateTicketStatusData
{
    [Required]
    public int TicketId { get; init; }
}