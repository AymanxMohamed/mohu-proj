using MOHU.Integration.Contracts.Dto.CreateProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface.Common
{
    public interface  ICreateProfileService
    {

        //  Task<bool> CreateProfile(CreateProfileDto model);

        Task<Guid> CreateProfile(CreateProfileDto model); 

    }
}
