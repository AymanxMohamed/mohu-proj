using MOHU.Integration.Application.Features.Companies.SpcCompanies.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/spc-companies")]
public class SpcCompaniesControllers(ISpcCompaniesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}