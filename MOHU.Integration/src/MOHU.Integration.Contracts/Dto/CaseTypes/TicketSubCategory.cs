using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CaseTypes
{
    public class TicketSubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SecondarySubCategory> secondarySubCategories { get; set; }


        public Guid ParentCategoryId { get; set; }
        public Guid TicketTypeId { get; set; }
        public TicketSubCategory()
        {
            secondarySubCategories = new List<SecondarySubCategory>();
        }

    }
}
