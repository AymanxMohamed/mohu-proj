using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Clients;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.ProxiesControllers;

[Route("api/elm/information-center/proxies/lookups")]
[ApiController]
public class LookupsProxyController(
    IElmInformationCenterApplicantDataClient applicantDataClient,
    IElmInformationCenterCountriesClient countriesClient,
    IElmInformationCenterNationalitiesClient nationalitiesClient,
    IElmInformationCenterSpcCompaniesClient spcCompaniesClient,
    IElmInformationCenterDhcHajCompaniesClient dhcHajCompaniesClient,
    IElmInformationCenterIhcCompaniesClient ihcCompaniesClient,
    IElmInformationCenterHajMissionCompaniesClient hajMissionsClient,
    IElmInformationCenterHousesClient housesClient)  : ControllerBase
{
    [HttpPost("applicants")]
    public IActionResult GetApplicantData(ElmFilterRequest? filterRequest)
    {
        var entities = applicantDataClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("countries")]
    public IActionResult GetCountries(ElmFilterRequest? filterRequest)
    {
        var entities = countriesClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("nationalities")]
    public IActionResult GetNationalities(ElmFilterRequest? filterRequest)
    {
        var entities = nationalitiesClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("spc-companies")]
    public IActionResult GetSpcCompanies(ElmFilterRequest? filterRequest)
    {
        var entities = spcCompaniesClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("dhc-haj-companies")]
    public IActionResult GetDhcHajCompanies(ElmFilterRequest? filterRequest)
    {
        var entities = dhcHajCompaniesClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("ihc-companies")]
    public IActionResult GetIhcCompanies(ElmFilterRequest? filterRequest)
    {
        var entities = ihcCompaniesClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("haj-missions")]
    public IActionResult GetHajMissions(ElmFilterRequest? filterRequest)
    {
        var entities = hajMissionsClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
    
    [HttpPost("houses")]
    public IActionResult GetHouses(ElmFilterRequest? filterRequest)
    {
        var entities = housesClient.GetAll(filterRequest).ToValueOrException();

        return Ok(entities);
    }
}