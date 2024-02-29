using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CaseTypes
{
    public  class TicketType
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TicketCategory> Categories { get; set; } // MainCategories 

        public TicketType()
        {
            Categories = new List<TicketCategory>();

        }



    }
}
