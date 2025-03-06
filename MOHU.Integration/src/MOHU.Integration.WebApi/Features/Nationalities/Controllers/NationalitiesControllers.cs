using MOHU.Integration.Application.Features.Nationalities.Services;

namespace MOHU.Integration.WebApi.Features.Nationalities.Controllers;

[ApiController]
[Route("api/nationalities")]
public class CountriesControllers(INationalitiesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}