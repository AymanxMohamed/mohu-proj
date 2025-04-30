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
        public string TicketId { get; init; } = null!;
        public string ZzpartySource { get; init; } = null!;
        public string SiteId { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string ClassStructureId { get; init; } = null!; //CategoryDetails
        public string Status { get; init; } = null!; //ExternalStatus
        public string ReportDate { get; init; } = null!; //ExternalReportDate
        public string StatusDate { get; init; } = null!; //ExternalStatusDate
        public string ZzTicketId { get; init; } = null!; //KidanaExternalTicketId
        public string ZzExtParty { get; init; } = null!; //ExternalParty
        public string ZzRequestor { get; init; } = null!; //ApplicantName
        public string ApplicantPhoneNumber { get; init; } = null!; //ApplicantPhoneNumber
        public string AssetNumber { get; init; } = null!;
        public string Location { get; init; } = null!;


    }
}
