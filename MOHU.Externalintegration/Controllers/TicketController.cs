using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Dto.Ticket;
using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [Route(nameof(CancelCase))]
        public async Task<ResponseMessage<bool>> CancelCase(CancelTicketResponse ticket)
        {
            var result = await _ticketService.CancelTicket(ticket);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };
        }



    }
}
