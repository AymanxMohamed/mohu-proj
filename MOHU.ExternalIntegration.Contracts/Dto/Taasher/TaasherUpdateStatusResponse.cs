//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace MOHU.ExternalIntegration.Contracts.Dto.Taasher
//{
//    public class TaasherUpdateStatusResponse
//    {

//        public Guid CustId { get; set; }

//        public Guid TicketId { get; set; }   //ticketid not ticket number 

//        [MaxLength(400)]
//        public string Resolution { get; set; }  //IntegrationClosureReason

//        public DateTime? ResolutionDate { get; set; } //IntegrationClosureDate

//        public TaasherIntegrationStatusEnum IntegrationStatus { get; set; }

//        [IgnoreDataMember]
//        [JsonIgnore]
//        public bool IsTashirUpdated { get; set; } = true;

//    }


//    public enum TaasherIntegrationStatusEnum
//    {
//        [Display(Name = "Close The Ticket")]
//        CloseTheTicket = 1,
//        [Display(Name = "Cancel")]
//        Canceled = 2,
//    }

//    public enum TaasherIsServiceDeskUpdatedEnum
//    {
//        [Display(Name = "Yes")]
//        Yes = 1,
//        [Display(Name = "No")]
//        No = 0,
//    }

//}
