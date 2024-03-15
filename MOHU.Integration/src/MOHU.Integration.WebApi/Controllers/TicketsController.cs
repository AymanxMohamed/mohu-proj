using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Interface.Ticket;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/{customerId}/[controller]")]
    [ApiController]
    public class TicketsController(ITicketService ticketService) : BaseController
    {
        private readonly ITicketService _ticketService = ticketService;

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<TicketDetailsResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<TicketDetailsResponse>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<TicketDetailsResponse>),StatusCodes.Status404NotFound)]
        [HttpGet("{ticketNumber}")]
        public async Task<ResponseMessage<TicketDetailsResponse>> Get(Guid customerId, string ticketNumber)
        {
            var result = await _ticketService.GetTicketDetailsAsync(customerId, ticketNumber);
            return Ok(result);
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<TicketListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<TicketListResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<TicketListResponse>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ResponseMessage<TicketListResponse>> GetAll(Guid customerId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _ticketService.GetAllTicketsAsync(customerId, pageNumber, pageSize);
            return Ok(result);
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<SubmitTicketResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<SubmitTicketResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<SubmitTicketResponse>), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ResponseMessage<SubmitTicketResponse>> Post(Guid customerId, [FromBody] SubmitTicketRequest request)
        {
           
                var result = await _ticketService.SubmitTicketAsync(customerId, request);
                return Ok(result);
        }

    }
}
