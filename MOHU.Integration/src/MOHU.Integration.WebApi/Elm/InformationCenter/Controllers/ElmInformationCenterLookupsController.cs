using MOHU.Integration.Application.Features.Companies.DhcHajCompanies.Services;
using MOHU.Integration.Application.Features.Companies.HajMissions.Services;
using MOHU.Integration.Application.Features.Companies.Houses.Services;
using MOHU.Integration.Application.Features.Companies.IhcCompanies.Services;
using MOHU.Integration.Application.Features.Companies.SpcCompanies.Services;
using MOHU.Integration.Application.Features.Countries.Services;
using MOHU.Integration.Application.Features.Individuals.Services;
using MOHU.Integration.Application.Features.Nationalities.Services;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.Controllers;

[Route("api/elm/information-center/lookups")]
public class ElmInformationCenterLookupsController(
    ICountriesService countriesService,
    INationalitiesService nationalitiesService,
    IIndividualsService individualsService,
    ISpcCompaniesService spcCompaniesService,
    IIhcCompaniesService ihcCompaniesService,
    IDhcHajCompaniesService dhcHajCompaniesService,
    IHajMissionCompaniesService hajMissionCompaniesService,
    IHousesService housesService) : ControllerBase
{
    [HttpPost("sync")]
    public async Task<IActionResult> Create()
    {
        return Ok(new
        {
            countries = await countriesService.Sync(),
            nationalities = await nationalitiesService.Sync(),
            individuals = await individualsService.Sync(),
            spcCompanies = await spcCompaniesService.Sync(),
            ihcCompanies = await ihcCompaniesService.Sync(),
            dhcHajCompanies = await dhcHajCompaniesService.Sync(),
            hajMissions = await hajMissionCompaniesService.Sync(),
            houses = await housesService.Sync(),
        });
    }
}