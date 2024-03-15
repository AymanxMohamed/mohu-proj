using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface ICommonMethod
    {
        Task<bool> IsProfileWithSameEmailExists(string email);
        Task<bool> IsProfileWithSameIdNumberIExists(string idNumber);

        Task<bool> IsProfileWithSamePassportExists(string passportNumber);

        Task<bool> IsProfileWithSameMobileNumberExists(string mobileNumber); 





    }
}
