using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

namespace MOHU.Integration.WebApi.Elm.InformationCenter.Controllers;

[Route("api/elm/information-center/lookups")]
public class LookupsController(IElmInformationCenterApplicantDataClient client) : ControllerBase
{
    [HttpGet("applicant-data")]
    public IActionResult GetApplicantData(FilterRequest? filterRequest = null)
    {
        var applicants = client.GetAll(filterRequest).ToValueOrException();

        return Ok(applicants);
    }
}