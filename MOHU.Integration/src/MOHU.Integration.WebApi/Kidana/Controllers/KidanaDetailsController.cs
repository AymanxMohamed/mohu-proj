using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Kidana.Common.Services;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;

namespace MOHU.Integration.WebApi.Kidana.Controllers;

[Route("api/kidana")]
[ApiController]
public class KidanaDetailsController(
    IKidanaDetailsService service,
    ILogger<KidanaDetailsController> logger) : ControllerBase
{
    [HttpGet("details/{kidanaNumber}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(KidanaDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
    public IActionResult GetDetails(string kidanaNumber)
    {
        var result = service.GetDetails(kidanaNumber);

        return result.Match(
                success => Ok(success),
                error =>
                {
                    logger.LogError("Kidana API error: {ErrorCode} - {ErrorMessage}",
                        error.First().Code,
                        error.First().Description);

                    return Problem(
                        title: "Kidana Service Error",
                        detail: error.First().Description,
                        statusCode: StatusCodes.Status502BadGateway
                    );
                }
            );
        }
    }

