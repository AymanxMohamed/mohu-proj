using MOHU.Integration.Contracts.Dto.Kidana;

namespace MOHU.Integration.WebApi.Features.Tickets.Kidana.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KidanaController(IKidanaService kidanaService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    [Route(nameof(UpdateStatus))]
    public async Task<ResponseMessage<bool>> UpdateStatus(KidanaUpdateStatusRequest request)
    {
        var result = await kidanaService.UpdateStatusAsync(request);
        return Ok(result);
    }
}