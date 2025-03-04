using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.ProxiesControllers;

[Route("api/elm/information-center/proxies/lookups")]
public class LookupsProxyController(IElmInformationCenterApplicantDataClient client) : ControllerBase
{
    [HttpGet("applicants")]
    public IActionResult GetApplicantData(ElmFilterRequest? filterRequest)
    {
        var applicants = client.GetAll(filterRequest).ToValueOrException();

        return Ok(applicants);
    }
}