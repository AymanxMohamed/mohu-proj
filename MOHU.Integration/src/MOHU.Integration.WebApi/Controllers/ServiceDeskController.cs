using MOHU.Integration.Contracts.Dto.ServiceDesk;

namespace MOHU.Integration.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceDeskController(IServiceDeskService serviceDeskService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    [Route(nameof(UpdateStatus))]
    public async Task<ResponseMessage<bool>> UpdateStatus(ServiceDeskUpdateStatusRequest request)
    {
        var result = await serviceDeskService.UpdateStatusAsync(request);
        return Ok(result);
    }
}