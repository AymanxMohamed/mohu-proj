using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Dtos.Responses
{
    public class KidanaDetailsResponse
    {

        [JsonProperty("Msg")]
        public string Msg { get; set; } = null!;

        [JsonProperty("SOURCE")]
        public string Source { get; init; } = null!;

        [JsonProperty("TICKETID")]
        public string TicketId { get; init; } = null!;

        [JsonProperty("SITEID")]
        public string SiteId { get; init; } = null!;

        [JsonProperty("DESCRIPTION_LONGDESCRIPTION")]
        public string Description { get; init; } = null!;

        [JsonProperty("CLASSSTRUCTUREID")]
        public string ClassStructureId { get; init; } = null!; //CategoryDetails

        [JsonProperty("STATUS")]
        public string Status { get; init; } = null!; //ExternalStatus

        [JsonProperty("REPORTDATE")]
        public string ReportDate { get; init; } = null!; //ExternalReportDate

        [JsonProperty("STATUSDATE")]
        public string StatusDate { get; init; } = null!; //ExternalStatusDate

        [JsonProperty("ASSETNUM")]
        public string AssetNumber { get; init; } = null!;


        public string ZzTicketId { get; init; } = null!; //KidanaExternalTicketId
        public string ZzExtParty { get; init; } = null!; //ExternalParty
        public string ZzRequestor { get; init; } = null!; //ApplicantName
        public string ApplicantPhoneNumber { get; init; } = null!; //ApplicantPhoneNumber
        public string ZzpartySource { get; init; } = null!;
        public string Location { get; init; } = null!;
    }
}
