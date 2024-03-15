using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto
{

    // general dto for update status 
    public class UpdateStatusRequest
    {

        public Guid CustId { get; set; }

        public Guid TicketId { get; set; }

        [MaxLength(400)]
        public string Resolution { get; set; }


        public DateTime? ResolutionDate { get; set; }

        public IntegrationStatusEnum IntegrationStatus { get; set; }

    }

    public enum IntegrationStatusEnum
    {
        [Display(Name = "Close The Ticket")]
        CloseTheTicket = 1,
        [Display(Name = "Cancel")]
        Canceled = 2,
    }


}
