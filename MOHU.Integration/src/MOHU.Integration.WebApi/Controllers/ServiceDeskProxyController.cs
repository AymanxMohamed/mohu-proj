using MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;
using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;
using SDIntegraion;

namespace MOHU.Integration.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceDeskProxyController(IServiceDeskTicketsClient serviceDeskTicketsClient) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<TicketResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<TicketResponse?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<TicketResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<TicketResponse> Post(ServiceDeskRequest request)
    {
        return await serviceDeskTicketsClient.GetOrCreateServiceDeskTicket(request);
    }
}