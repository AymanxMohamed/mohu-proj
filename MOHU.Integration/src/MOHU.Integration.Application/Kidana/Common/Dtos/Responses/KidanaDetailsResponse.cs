using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Dtos.Responses
{
    public class KidanaDetailsResponse
    {
        public string Source { get; init; } = null!;
        public string TicketNumber { get; init; } = null!;
        public string PartySource { get; init; } = null!;
        public string SiteId { get; init; } = null!;
        public string KidanaDescription { get; init; } = null!;
        public string CategoryDetails { get; init; } = null!;
        public string ExternalStatus { get; init; } = null!;
        public string ExternalReportDate { get; init; } = null!;
        public string ExternalStatusDate { get; init; } = null!;
        public string KidanaExternalTicketId { get; init; } = null!;
        public string ExternalParty { get; init; } = null!;

    }
}
