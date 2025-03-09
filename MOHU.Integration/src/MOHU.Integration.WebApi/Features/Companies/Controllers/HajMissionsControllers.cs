using MOHU.Integration.Application.Features.Companies.HajMissions.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/haj-missions")]
public class HajMissionsControllers(IHajMissionCompaniesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}