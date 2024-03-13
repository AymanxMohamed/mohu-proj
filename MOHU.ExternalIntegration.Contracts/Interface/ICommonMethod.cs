using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface ICommonMethod
    {

        Task<bool> CheckEmailAddressExist(string Email);

        Task<bool> CheckMobileNumberDuplication(string MobileNo);

        Task<bool> CheckPassportNumberIsExsting(string PassportNo);

        Task<bool> CheckIDNumberIsExsting(string IDNumber);

        Task<bool> CheckCustomerExist(Guid CustId);


        Task<bool> CheckTicketIdExist(Guid TicketId); 





    }
}
