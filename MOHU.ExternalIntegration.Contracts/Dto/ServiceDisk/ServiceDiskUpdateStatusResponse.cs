using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.ServiceDisk
{
    public class ServiceDiskUpdateStatusResponse
    {
        public Guid CustId { get; set; }

        public Guid TicketId { get; set; }   //ticketid not ticket number 

        [MaxLength(400)]
        public string Resolution { get; set; }  //IntegrationClosureReason
        public DateTime? ResolutionDate { get; set; }
        public IntegrationStatusEnum IntegrationStatus { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public bool IsServiceDeskUpdated { get; set; } = true;

    }

    public enum IntegrationStatusEnum
    {
        [Display(Name = "Close The Ticket")]
        CloseTheTicket = 1,
        [Display(Name = "Cancel")]
        Canceled = 2,
    }

}
