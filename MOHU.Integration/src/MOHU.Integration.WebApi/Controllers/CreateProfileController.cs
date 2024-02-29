using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Interface.CreateProfile;
using System.Runtime.InteropServices;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateProfileController : BaseController
    {
        private readonly ICreateProfileService _createProfileService; 
        public CreateProfileController(ICreateProfileService createProfileService )
        {
            _createProfileService = createProfileService;
        }


       
        [HttpPost]
        [Route(nameof(CreateProfile))]
        public async Task<ResponseMessage<bool>> CreateProfile(CreateProfileResponse model)
        {
              var result =  await _createProfileService.CreateProfile(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };

        }












    }
}
