using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface.CreateProfile
{
    public interface ICreateProfileService
    {

        Task<bool> CreateProfile(CreateProfileResponse model);
        // Task<Guid> CreateProfile(CreateProfileResponse model);
        // Task<bool> CreateProfile(CreateProfileResponse model, string number);


        Task<List<TicketType>> GetTicketTypes(); 





    }
}
