using MOHU.Integration.Application.Features.Companies.DhcHajCompanies.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/dhc-haj-companies")]
public class DhcHajCompaniesControllers(IDhcHajCompaniesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}