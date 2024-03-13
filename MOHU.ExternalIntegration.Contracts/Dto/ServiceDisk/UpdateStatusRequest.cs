//using Microsoft.Extensions.Localization;
//using MOHU.ExternalIntegration.Contracts.Enum;
//using MOHU.ExternalIntegration.Shared;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace MOHU.ExternalIntegration.Contracts.Dto.ServiceDisk
//{
   
//    public class UpdateStatusRequest
//    {
      
//        public Guid CustId { get; set; }

//        public Guid TicketId { get; set; }
        
//        [MaxLength(400)]
//        public string Resolution { get; set; }
//        public DateTime? ResolutionDate { get; set; }


//        // public ServiceDiskIntegrationStatusEnum IntegrationStatus { get; set; }

//        public ServiceDiskIntegrationStatusEnum IntegrationStatus { get; set; }

//        [IgnoreDataMember]
//        [JsonIgnore]
//        public bool IsServiceDeskUpdated { get; set; } = true;

//    }

//    public enum ServiceDiskIntegrationStatusEnum
//    {
//        [Display(Name = "Close The Ticket")]
//        CloseTheTicket = 1,
//        [Display(Name = "Cancel")]
//        Canceled = 2,
//    }




//}
