using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Dtos.Responses
{
    public class KidanaDetailsResponse
    {
        public string Status { get; init; } = null!;
        public string TicketNumber { get; init; } = null!;
        public DateTime IssueDate { get; init; }
    }
}
