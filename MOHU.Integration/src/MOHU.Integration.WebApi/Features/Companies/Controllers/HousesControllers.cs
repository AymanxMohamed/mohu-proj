using MOHU.Integration.Application.Features.Companies.Houses.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/houses")]
public class HousesControllers(IHousesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}