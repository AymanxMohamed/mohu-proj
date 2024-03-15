using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Dto.Common;

using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
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

        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest request)
        {
            await _ticketService.UpdateStatus(request);
            return Ok(true);
        }
    }
}
