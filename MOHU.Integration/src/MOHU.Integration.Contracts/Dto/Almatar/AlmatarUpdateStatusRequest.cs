using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Almatar
{
    public class AlmatarUpdateStatusRequest: UpdateTicketStatusData
    {
        [Required]
        public string TicketNumber { get; init; } = null!;
    }
}
