using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Dtos.Responses
{
    public class TicketValidationResult
    {
        public bool TicketExists { get; set; }
        public string Status { get; set; } = null!;
        public bool IsClosed { get; set; } = false;
        public Guid? CrmRecordId { get; set; }
        public string? CrmRecordName { get; set; }
    }
}
