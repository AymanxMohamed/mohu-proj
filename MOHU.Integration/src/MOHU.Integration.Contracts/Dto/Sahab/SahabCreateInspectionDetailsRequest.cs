using MOHU.Integration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Sahab;
public class SahabCreateInspectionDetailsRequest
{
    public string TicketId { get; set; } = string.Empty;
    public string Inspector { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty; 
    public DateTime ActionDate { get; set; } = DateTime.Now;
    public SahabStatusEnum Status { get; set; }
}
