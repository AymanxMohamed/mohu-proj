using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface.Ticket;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KedanaController : BaseController
    {
        public readonly ITicketService _ticketService;
        public KedanaController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest request)
        {
            await _ticketService.UpdateStatus(request);
            return Ok(true);
        }



    }
}
