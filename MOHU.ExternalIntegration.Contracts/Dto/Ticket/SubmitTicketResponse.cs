using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Ticket
{
    public class SubmitTicketResponse
    {


        [Required]
        [MaxLength(255)]
        public string ReportedBy { get; set; }   ///test auto generated 


        public Guid ProfileLink { get; set; } // customer field in crm 


        public string Subject { get; set; } //

        //OwnerTeam 
        [IgnoreDataMember]
        [EnumDataType(typeof(OriginEnum))]
        public int OwnerTeam { get; set; } = (int)OwnerTeamEnum.NusukAgents;

        //Status

        public StatusEnum Status { get; set; }


        public OwnerEnum Owner { get; set; }

        public SourceEnum Source { get; set; }

        //Service

        public SourceEnum Service { get; set; }

        public Guid Category { get; set; }

        public Guid Subcategory { get; set; }


        public ImpactEnum Impact { get; set; }

        public UrgencyEnum Urgency { get; set; }


        //public  Priority  { get; set; }


        public Guid Nationality { get; set; }


        public PreferredLanguageEnum PreferredLanguage { get; set; }

        // public Guid CountryofResidence { get; set; }

        public Guid CRM_CustomerResidentCountry { get; set; }


        [MaxLength(1000)]
        //CRM_CustomerEmail
        public string CRM_CustomerEmail { get; set; }

        public string CRM_ErrorMessages { get; set; }









    }
    public enum ImpactEnum
    {
        [Display(Name = "Low")]
        Low = 1,
        [Display(Name = "Medium")]
        Medium = 2,
        [Display(Name = "High")]
        High = 3,

    }

    public enum UrgencyEnum
    {
        [Display(Name = "Low")]
        Low = 1,
        [Display(Name = "Medium")]
        Medium = 2,
        [Display(Name = "High")]
        High = 3,
    }


    public enum OwnerTeamEnum
    {
        [Display(Name = "NusukAgents")]
        NusukAgents = 1,

    }

    public enum StatusEnum
    {
        [Display(Name = "Logged")]
        Logged = 1,
        [Display(Name = "Active")]
        Active = 2,
        [Display(Name = "Waiting for Customer")]
        WaitingforCustomer = 3,
        [Display(Name = "Waiting for 3rd Party")]
        Waitingfor3rdParty = 4,
        [Display(Name = "Waiting for Resolution")]
        WaitingforResolution = 5,
        [Display(Name = "Resolved")]
        Resolved = 6,
        //Closed
        [Display(Name = "Closed")]
        Closed = 7,
    }

    public enum OwnerEnum
    {

    }


    public enum SourceEnum
    {
        [Display(Name = "Email")]
        Email = 1,
        [Display(Name = "Phone")]
        Phone = 2,
        [Display(Name = "AutoTicket")]
        AutoTicket = 3,

        [Display(Name = "Change Request")]
        ChangeRequest = 4,

        [Display(Name = "Chat")]
        Chat = 5,

        [Display(Name = "Facebook")]
        Facebook = 6,

        [Display(Name = "Fax")]
        Fax = 7,
        [Display(Name = "FrontRange Voice")]
        FaFrontRangeVoice = 8,

        [Display(Name = "Instagram")]
        Instagram = 9,

        [Display(Name = "Instant Message")]
        InstantMessage = 10,

        //LiveChat
        [Display(Name = "LiveChat")]
        LiveChat = 11,

        [Display(Name = "Microsoft Teams")]
        MicrosoftTeams = 12,

        [Display(Name = "Walk-In")]
        WalkIn = 13,

        [Display(Name = "Whatsapp")]
        Whatsapp = 14,


        [Display(Name = "X")]
        X = 15,

    }


    public enum PreferredLanguageEnum
    {
        [Display(Name = "Arabic")]
        Arabic = 1,
        [Display(Name = "Chinese")]
        Chinese = 2,
        [Display(Name = "Dutch")]
        Dutch = 3,
        [Display(Name = "English")]
        English = 4,
        [Display(Name = "French")]
        French = 5,
        [Display(Name = "German")]
        German = 6,
        [Display(Name = "Italian")]
        Italian = 7,
        [Display(Name = "Japanese")]
        Japanese = 8,
        [Display(Name = "Portuguese")]
        Portuguese = 9,
        [Display(Name = "Russian")]
        Russian = 10,
        [Display(Name = "Spanish")]
        Spanish = 11,


    }

}




