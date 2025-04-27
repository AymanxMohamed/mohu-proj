using Common.Crm.Infrastructure.Common.Extensions;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
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
        Task<List<UserWithRolesDto>> GetUserRolesAsync();
    }
}
