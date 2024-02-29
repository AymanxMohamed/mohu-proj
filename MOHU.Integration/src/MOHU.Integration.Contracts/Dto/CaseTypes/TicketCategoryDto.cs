using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CaseTypes
{
    public class TicketCategoryDto
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TicketSubCategoryDto> SubCategories { get; set; }

        public TicketCategoryDto()
        {
            SubCategories = new List<TicketSubCategoryDto>();
        }

    }
}
