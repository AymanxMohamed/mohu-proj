using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MOHU.Integration.Domain.Entitiy.TicketCategory;

namespace MOHU.Integration.Contracts.Dto.Category
{
    public class UpsertCategoryRequest
    {
        public string? ArabicName { get; set; }

        public string? EnglishName { get; set; }

        public string? CategoryId { get; set; }

       

        public Status_OptionSet? Status { get; set; }

        public string? TicketType { get; set; }
        public string? ParentCategory { get; set; }
        public string? SubCategory { get; set; }
        public Priority_OptionSet? Priority { get; set; }
        public ComplainType_OptionSet? ComplainType { get; set; }
        public Season_OptionSet? Season { get; set; }
      
       

    }
}
