using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface.Ticket;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskController : ControllerBase
    {

        public readonly ITicketService _ticketService;
        public ServiceDeskController(
              ITicketService ticketService
            )
        {
            _ticketService = ticketService;
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest model)
        {
            await _ticketService.UpdateStatus(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = true };
        }

    }
}
