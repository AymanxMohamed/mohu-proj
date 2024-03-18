using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MOHU.Integration.Contracts.Dto
{
    public class UpdateStatusRequest
    {

        public Guid CustomerId { get; set; }

        public Guid TicketId { get; set; }

        [MaxLength(400)]
        public string Resolution { get; set; }


        public DateTime? ResolutionDate { get; set; }

        public IntegrationStatus IntegrationStatus { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public string FlagLogicalNameToUpdate { get; set; }

    }

    public enum IntegrationStatus
    {
        [Display(Name = "Close The Ticket")]
        CloseTheTicket = 1,
        [Display(Name = "Cancel")]
        Canceled = 2,
    }

}
