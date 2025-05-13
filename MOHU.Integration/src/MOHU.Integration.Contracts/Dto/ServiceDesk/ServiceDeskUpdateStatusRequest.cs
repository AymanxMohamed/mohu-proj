using MOHU.Integration.Contracts.Tickets.Dtos;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Contracts.Dto.ServiceDesk;

public class ServiceDeskUpdateStatusRequest : UpdateTicketStatusData
{
    [Required]
    public string TicketNumber { get; init; } = null!;
    
    public string Title => 
        TicketNumber.Length > 4 
            ? $"{TicketNumber[..4]}-{TicketNumber[4..]}" 
            : TicketNumber;
}