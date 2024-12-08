using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.Tickets.HootSuite.Controllers;

[Route("api/hoot-suite/tickets")]
public class HootSuiteTicketsController (ITicketService ticketService) : BaseController
{
    
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<SubmitTicketResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<SubmitTicketResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<SubmitTicketResponse>), StatusCodes.Status404NotFound)]
    public async Task<ResponseMessage<SubmitTicketResponse>> Post(CreateHootSuiteTicketRequest request)
    {
        var result = await ticketService.SubmitHootSuiteTicketAsync(request);
        return Ok(result);
    }
}