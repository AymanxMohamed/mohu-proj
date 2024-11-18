using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface.Ticket;
using MOHU.Integration.WebApi.Common.Controllers;

namespace MOHU.Integration.WebApi.Features.Tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketLookupsController(ITicketService ticketService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<List<TicketTypeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<List<TicketTypeResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route(nameof(Types))]
    public async Task<ResponseMessage<List<TicketTypeResponse>>> Types()
    {
        var result = await ticketService.GetTicketTypesAsync();
        return Ok(result);
    }
}