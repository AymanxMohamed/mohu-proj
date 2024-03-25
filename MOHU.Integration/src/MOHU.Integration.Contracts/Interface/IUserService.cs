using MOHU.Integration.Contracts.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IUserService
    {
        Task<LookupDto> GetUserByUsernameAsync(string username);
    }
}
