using MOHU.Integration.Contracts.Dto.Sahab;

namespace MOHU.Integration.WebApi.Features.Tickets.Sahab.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SahabController(ISahabService sahabService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    [Route(nameof(UpdateStatus))]
    public async Task<ResponseMessage<bool>> UpdateStatus(SahabUpdateStatusRequest request)
    {
        var result = await sahabService.UpdateStatusAsync(request);
        return Ok(result);
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    [Route(nameof(CreateInspectionDetails))]
    public async Task<ResponseMessage<bool>> CreateInspectionDetails(SahabCreateInspectionDetailsRequest request)
    {
        var result = await sahabService.CreateInspectionDetails(request);
        return Ok(result);
    }
}