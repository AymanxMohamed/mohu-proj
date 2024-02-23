using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Interface.Common;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateProfileController : ControllerBase
    {
        private readonly ICreateProfileService _createProfileService; 
        public CreateProfileController(ICreateProfileService createProfileService )
        {
            _createProfileService = createProfileService;
        }



        // Task<Tuple<bool, string>> CreateProfile(CreateProfileDto model);


        [HttpPost]
        [Route(nameof(CreateProfile))]
        public async Task<Guid> CreateProfile(CreateProfileDto model)
        {

            return await _createProfileService.CreateProfile(model);

              // return OkResult(); 

          

        }








    }
}
