using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Ticket
{
    public class SubmitTicketRequest
    {

        public Guid TicketId { get; set; }
        public string TicketNumber { get; set; }

    }
}
