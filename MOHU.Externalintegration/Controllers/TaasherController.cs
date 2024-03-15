using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaasherController : ControllerBase
    {
        private readonly ICreateProfileService _createProfileService;

       

        public readonly IUpdateStatusService _updateStatusService;
        public TaasherController(ICreateProfileService createProfileService,
             IUpdateStatusService updateStatusService)
        {
             _createProfileService = createProfileService;
          
            _updateStatusService = updateStatusService;
        }


        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<Guid?>), StatusCodes.Status400BadRequest)]
        [HttpPost("CreateProfile")]
        public async Task<ActionResult<ResponseMessage<Guid>>> Post(CreateProfileResponse model)
        {
            try
            {
                var result = await _createProfileService.CreateProfile(model);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ResponseMessage<Guid?> { StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = ex.Message });
            }

        }

     



        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest model)
        {
            var result = await _updateStatusService.UpdateStatus(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };

        }






    }
}
