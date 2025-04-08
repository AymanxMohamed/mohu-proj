using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Category
{
    public class UpsertCategoryRequest
    {
        public string ArabicName { get; set; }

        public string EnglishName { get; set; }

        public string CategoryId { get; set; }

       

        public int Status { get; set; }

        public string TicketType { get; set; }
        public string ParentCategory { get; set; }
        public string SubCategory { get; set; }
        public string Priority { get; set; }
        public string ComplainType { get; set; }
        public string Season { get; set; }
        public string SLAHourLevel1 { get; set; }
        public string SLAHourLevel2 { get; set; }
        public string SLAHourLevel3 { get; set; }

    }
}
