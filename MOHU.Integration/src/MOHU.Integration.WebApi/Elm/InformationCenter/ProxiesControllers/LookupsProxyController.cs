using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Clients;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.ProxiesControllers;

[Route("api/elm/information-center/proxies/lookups")]
[ApiController]
public class LookupsProxyController(
    IElmInformationCenterApplicantDataClient applicantDataClient,
    IElmInformationCenterCountriesClient countriesClient,
    IElmInformationCenterNationalitiesClient nationalitiesClient) : ControllerBase
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
}