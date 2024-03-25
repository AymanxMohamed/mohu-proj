using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CaseTypes
{
    public class TicketSubCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SecondarySubCategoryDto> SecondarySubCategories { get; set; }

        public Guid ParentCategoryId { get; set; }
        public Guid TicketTypeId { get; set; }
        public TicketSubCategoryDto()
        {
            SecondarySubCategories = new List<SecondarySubCategoryDto>();
        }

    }
}
