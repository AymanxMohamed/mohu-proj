using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaasherController : BaseController
    {
        private readonly ICustomerService _createProfileService;
        private readonly ITicketService _ticketService;
        public TaasherController(ICustomerService createProfileService,
             ITicketService ticketService)
        {
             _createProfileService = createProfileService;

            _ticketService = ticketService;
        }


        //[Consumes("application/json")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(ResponseMessage<Guid>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseMessage<Guid?>), StatusCodes.Status400BadRequest)]
        //[HttpPost("CreateProfile")]
        //public async Task<ResponseMessage<Guid>> Post(CreateProfileResponse model)
        //{
        //    var result = await _createProfileService.CreateProfile(model);
        //    return Ok(result);
        //}
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest request)
        {
             await _ticketService.UpdateStatus(request);
            return Ok(true);
        }
    }
}
