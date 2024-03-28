using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.ServiceDesk;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskController : BaseController
    {

        public readonly IServiceDeskService _serviceDeskService;
        public ServiceDeskController(
              IServiceDeskService serviceDeskService
            )
        {
            _serviceDeskService = serviceDeskService;
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(ServiceDeskUpdateStatusRequest request)
        {
            var result = await _serviceDeskService.UpdateStatusAsync(request);
            return Ok(result);
        }

    }
}
