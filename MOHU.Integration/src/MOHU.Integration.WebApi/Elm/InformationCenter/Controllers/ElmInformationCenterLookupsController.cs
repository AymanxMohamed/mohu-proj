using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Features.Countries.Services;
using MOHU.Integration.Application.Features.Nationalities.Services;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.Controllers;

[Route("api/elm/information-center/lookups")]
public class ElmInformationCenterLookupsController(
    ICountriesService countriesService,
    INationalitiesService nationalitiesService,
    IServiceProvider serviceProvider) : ControllerBase
{
    [HttpPost("sync")]
    public async Task<IActionResult> Create()
    {
        await countriesService.Sync();
        await nationalitiesService.Sync();
        
        foreach (var task in serviceProvider.GetElmSyncServiceTasks())
        {
            await task;
        }

        return Ok("Sync process completed.");
    }
}