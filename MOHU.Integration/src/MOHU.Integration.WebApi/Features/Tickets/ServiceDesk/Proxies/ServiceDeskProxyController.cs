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
    [HttpPost("/api/ServiceDeskProxy/{CallID}")]
    public async Task<object> PostUpdate(ServiceDeskRequestUpdate request, string CallID)
    {
        var username = await configuration.GetConfigurationValueAsync("SD_User Name");
        var password = await configuration.GetConfigurationValueAsync("SD_Password");
        var serviceDeskUrl = await configuration.GetConfigurationValueAsync("SD_URL");
        var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, serviceDeskUrl + "/" + CallID);
        httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
        httpRequestMessage.Content = JsonContent.Create(request, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        var contentStream = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\\", "")
            .Trim(['"']);
        return contentStream;
    }
}