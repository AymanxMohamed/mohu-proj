using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Ticket
{
    public class SubmitTicketRequest
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public Guid CaseType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid? SubCategoryId1 { get; set; }
        public int? BeneficiaryType { get; set; }
        public int? Location { get; set; }
    }
}
