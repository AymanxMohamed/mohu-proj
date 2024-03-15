using MOHU.ExternalIntegration.Contracts.Dto.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface ITicketService
    {
        Task<bool> CancelTicket(CancelTicketResponse ticket);


    }
}
