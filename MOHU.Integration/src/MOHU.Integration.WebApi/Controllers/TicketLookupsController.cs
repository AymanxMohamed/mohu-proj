using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface.Ticket;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketLookupsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketLookupsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<List<TicketTypeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<List<TicketTypeResponse>>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route(nameof(Types))]
        public async Task<ResponseMessage<List<TicketTypeResponse>>> Types()
        {
            var ticketTypes = await _ticketService.GetTicketTypes();
            return new ResponseMessage<List<TicketTypeResponse>> { StatusCode = StatusCodes.Status200OK, Result = ticketTypes };

        }
    }
}
