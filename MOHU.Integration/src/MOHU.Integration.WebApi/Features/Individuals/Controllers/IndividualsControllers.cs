using MOHU.Integration.Application.Features.Individuals.Services;   

namespace MOHU.Integration.WebApi.Features.Individuals.Controllers;

[ApiController]
[Route("api/individuals")]
public class IndividualsControllers(IIndividualsService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.SyncWithElm());
    }
}