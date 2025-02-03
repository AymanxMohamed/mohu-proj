using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Sahab;
public class UpdateSahabTicket
{
    public Guid TicketId { get; set; }
    public string? ClouserReason { get; set; } = null;
    public DateTime? ClouserDateTime { get; set; } = null;
    public string?  StatusReason { get; set; } = null;
    public IntegrationStatus  IntegrationStatus { get; set; }
    public bool IsSahabUpdated { get;} = true;
}
