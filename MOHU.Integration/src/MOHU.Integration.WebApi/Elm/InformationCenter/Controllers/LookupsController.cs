using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.ApplicantData.Clients;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.Controllers;

[Route("api/elm/information-center/lookups")]
public class LookupsController(IElmInformationCenterApplicantDataClient client) : ControllerBase
{
    [HttpGet("applicant-data")]
    public IActionResult GetApplicantData(FilterRequest? filterRequest = null)
    {
        var data = client.GetLookups<object>(filterRequest);

        return Ok(data.ToValueOrException());
    }
}