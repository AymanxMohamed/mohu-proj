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
    INationalitiesService nationalitiesService,
    ICountriesService countriesService,
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
        var countries = await countriesService.Sync();
        
        var nationalities = await nationalitiesService.Sync();
        
        var individuals = await individualsService.Sync();
        
        var spcCompanies = await spcCompaniesService.Sync();
        
        var ihcCompanies = await ihcCompaniesService.Sync();
        
        var dhcHajCompanies = await dhcHajCompaniesService.Sync();
        
        var hajMissions = await hajMissionCompaniesService.Sync();
        
        var houses = await housesService.Sync();
            
        return Ok(new
        {
            Total = 
                countries.Count 
                + nationalities.Count 
                + individuals.Count 
                + spcCompanies.Count 
                + ihcCompanies.Count
                + dhcHajCompanies.Count
                + hajMissions.Count
                + houses.Count,
            Items = new
            {
                countries = new
                {
                    total = countries.Count,
                    items = countries
                },
                nationalities = new
                {
                    total = nationalities.Count,
                    items = nationalities
                },
                individuals = new
                {
                    total = individuals.Count,
                    items = individuals
                },
                companies = new
                {
                    spcCompanies = new
                    {
                        total = spcCompanies.Count,
                        items = spcCompanies
                    },
                    ihcCompanies  = new
                    {
                        total = ihcCompanies.Count,
                        items = ihcCompanies
                    },
                    dhcHajCompanies  = new
                    {
                        total = dhcHajCompanies.Count,
                        items = dhcHajCompanies
                    },
                    hajMissions  = new
                    {
                        total = hajMissions.Count,
                        items = hajMissions
                    },
                    houses = new
                    {
                        total = houses.Count,
                        items = houses
                    },
                }
            }
        });
    }
}