using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests
{
    public class ResolveTicketRequest
    {
        [Required]
        public string CRMTicketNumber { get; init; } = null!;
    }

    public class ResolveTicketResponse
    {
        public string Success { get; init; } 

    }
}