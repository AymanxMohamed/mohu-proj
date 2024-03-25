using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CaseTypes
{
    public  class TicketTypeResponse
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TicketCategoryDto> Categories { get; set; } // MainCategories 

        public TicketTypeResponse()
        {
            Categories = new List<TicketCategoryDto>();

        }



    }
}
