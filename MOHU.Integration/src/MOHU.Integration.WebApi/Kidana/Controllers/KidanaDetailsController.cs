using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Kidana.Common.Services;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using System.Net.Http;
using System.Text;
using MOHU.Integration.Application.Kidana.Common.Dtos.Requests;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Domain.Features.Tickets;
using ErrorOr;
using MOHU.Integration.Contracts.Enum;

namespace MOHU.Integration.WebApi.Kidana.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KidanaDetailsController(
    IKidanaDetailsService service,
    ILogger<KidanaDetailsController> logger) : ControllerBase
{

    [HttpGet(nameof(ValidateTicket))]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<TicketValidationResult>), 200)]
    [ProducesResponseType(typeof(ResponseMessage<string>), 400)]
    [ProducesResponseType(typeof(ResponseMessage<string>), 404)]
    [ProducesResponseType(typeof(ResponseMessage<string>), 502)]
    public async Task<ActionResult<ResponseMessage<TicketValidationResult>>> ValidateTicket([FromQuery] string ticketId)
    {
        if (string.IsNullOrWhiteSpace(ticketId))
        {
            return BadRequest(new ResponseMessage<string>
            {
                Status = Status.Failure,
                Result = "Ticket ID is required",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        var result = await service.ValidateTicketWithCrmCheck(ticketId);

        return result.Match(
            success => Ok(success),
            errors =>
            {
                // Fix: Check if there are errors before accessing the first one
                var firstError = errors.Any() ? errors[0] : Error.Unexpected();
                logger.LogError("Validation failed: {Code} - {Message}",
                    firstError.Code, firstError.Description);

                return firstError.Type switch
                {
                    ErrorType.Validation => BadRequest(new ResponseMessage<string>
                    {
                        Status = Status.Failure,
                        Result = firstError.Description,
                        StatusCode = StatusCodes.Status400BadRequest
                    }),
                    ErrorType.NotFound => NotFound(new ResponseMessage<string>
                    {
                        Status = Status.Failure,
                        Result = firstError.Description,
                        StatusCode = StatusCodes.Status404NotFound
                    }),
                    _ => StatusCode(StatusCodes.Status502BadGateway, new ResponseMessage<string>
                    {
                        Status = Status.Failure,
                        Result = "Service unavailable",
                        StatusCode = StatusCodes.Status502BadGateway
                    })
                };
            }
        );
    }

}



