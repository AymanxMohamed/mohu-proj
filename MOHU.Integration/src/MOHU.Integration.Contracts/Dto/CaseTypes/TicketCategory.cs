using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CaseTypes
{
    public class TicketCategory
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TicketSubCategory> SubCategories { get; set; }

        public TicketCategory()
        {
            SubCategories = new List<TicketSubCategory>();
        }

    }
}
