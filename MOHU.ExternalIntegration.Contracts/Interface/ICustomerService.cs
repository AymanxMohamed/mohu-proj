using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface ICustomerService
    {

        Task<Guid> CreateProfile(CreateProfileRequest model);

    }
}
