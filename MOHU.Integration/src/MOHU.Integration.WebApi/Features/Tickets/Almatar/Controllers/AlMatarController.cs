using MOHU.Integration.Application.Service.Almatar;
using MOHU.Integration.Application.Service.Taasher;
using MOHU.Integration.Contracts.Dto.Almatar;
using MOHU.Integration.Contracts.Dto.Taasher;

namespace MOHU.Integration.WebApi.Features.Tickets.Almatar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlMatarController(IAlmatarService almatarService) : BaseController
    {
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ResponseMessage<bool>> UpdateStatus(AlmatarUpdateStatusRequest request)
        {
            var result = await almatarService.UpdateStatusAsync(request);
            return Ok(result);
        }
    }
}
