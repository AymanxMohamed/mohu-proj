using MOHU.Integration.Application.Features.Countries.Services;

namespace MOHU.Integration.WebApi.Features.Countries.Controllers;

[ApiController]
[Route("api/countries")]
public class CountriesControllers(ICountriesService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok(await service.Sync());
    }
}