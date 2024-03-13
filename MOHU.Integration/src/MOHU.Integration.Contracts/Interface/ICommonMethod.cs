using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface ICommonMethod
    {
        Task<bool> CheckEmailAddressExist(string Email);
        Task<bool> CheckIDNumberIsExsting(string IDNumber);

        Task<bool> CheckPassportNumberIsExsting(string PassportNo);

        Task<bool> CheckMobileNumberDuplication(string MobileNo); 





    }
}
