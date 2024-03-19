using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MOHU.Integration.Contracts.Dto.Ticket
{
    public class SubmitTicketRequest
    {
        public string Description { get; set; }
        public Guid CaseType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid? SubCategoryId1 { get; set; } 
        public int? BeneficiaryType { get; set; }
        public int? Location { get; set; }
    }
} 
