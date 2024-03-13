using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaasherController : ControllerBase
    {
       // private readonly ICreateProfileService _createProfileService;

        private readonly ITaasherUpdateStatusService _taasherUpdateStatusService;
        public TaasherController(/*ICreateProfileService createProfileService,*/

            ITaasherUpdateStatusService taasherUpdateStatusService)
        {
           // _createProfileService = createProfileService;
            _taasherUpdateStatusService = taasherUpdateStatusService;
        }

        #region ori code 

        //[HttpPost]

        //[Route(nameof(CreateProfile))]
        //public async Task<ResponseMessage<Guid>> CreateProfile(CreateProfileResponse model)
        //{
        //    //var result = await _createProfileService.CreateProfile(model);
        //    //return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };
        //    var result = await _createProfileService.CreateProfile(model);
        //    return new ResponseMessage<Guid> { StatusCode = StatusCodes.Status200OK, Result = result };

        //}

        #endregion

        #region  controller with error msg 



        //[HttpPost]
        //[Route(nameof(CreateProfile))]
        //public async Task<ActionResult<ResponseMessage<Guid>>> CreateProfile(CreateProfileResponse model)
        //{
        //    try
        //    {
        //        var customerId = await _createProfileService.CreateProfile(model);
        //        return new ResponseMessage<Guid> { StatusCode = StatusCodes.Status200OK, Result = customerId };
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        #endregion

        #region another style with output error msg 

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<Guid?>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        //public async Task<ActionResult<ResponseMessage<Guid>>> Post(CreateProfileResponse model)
        //{
        //    try
        //    {
        //        var result = await _createProfileService.CreateProfile(model);
        //        return Ok(result);
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        return BadRequest(new ResponseMessage<Guid?> { StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = ex.Message });
        //    }

        //}

        #endregion 



        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(TaasherUpdateStatusResponse model)
        {
            var result = await _taasherUpdateStatusService.TaasherUpdateStatus(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };

        }





    }
}
