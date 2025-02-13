using System.Text;
using System.Text.Json;
using MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;
using MOHU.Integration.Contracts.Dto.ServiceDeskProxy;
using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;
using SDIntegraion;

namespace MOHU.Integration.WebApi.Features.Tickets.ServiceDesk.Proxies;

[ApiController]
[Route("api/[controller]")]
public class ServiceDeskProxyController(IHttpClientFactory httpClientFactory, IConfigurationService configuration,
IServiceDeskTicketsClient serviceDeskTicketsClient)
    : BaseController
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

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<object?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<object>), StatusCodes.Status500InternalServerError)]
    [HttpPost("{CallID}")]
    public async Task<object> PostUpdate(ServiceDeskRequestUpdate request, string CallID)
    {
        return await serviceDeskTicketsClient.UpdateTicket(request, CallID);
    }
}