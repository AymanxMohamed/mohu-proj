using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface  IHelperMethod
    {

        Task<bool> CheckCustomerExist(Guid CustId);

        Task<bool> CheckTicketIdExist(Guid TicketId);



    }
}
