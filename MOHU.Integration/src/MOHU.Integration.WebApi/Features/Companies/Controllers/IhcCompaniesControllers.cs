using MOHU.Integration.Application.Features.Companies.IhcCompanies.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/ihc-companies")]
public class IhcCompaniesControllers(IIhcCompaniesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}