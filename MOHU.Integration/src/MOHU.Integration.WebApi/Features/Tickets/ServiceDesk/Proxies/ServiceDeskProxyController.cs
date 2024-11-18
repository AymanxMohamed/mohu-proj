using System.Text;
using System.Text.Json;
using SDIntegraion;

namespace MOHU.Integration.WebApi.Features.Tickets.ServiceDesk.Proxies;

[ApiController]
[Route("api/[controller]")]
public class ServiceDeskProxyController(IHttpClientFactory httpClientFactory, IConfigurationService configuration)
    : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<object?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<object>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<object> Post(ServiceDeskRequest request)
    {
        var username = await configuration.GetConfigurationValueAsync("SD_User Name");
        var password = await configuration.GetConfigurationValueAsync("SD_Password");
        var servicedeskURL = await configuration.GetConfigurationValueAsync("SD_URL");
        var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, servicedeskURL.ToString());
        httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
        httpRequestMessage.Content = JsonContent.Create(request, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        var contentStream = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\\", "")
            .Trim(['"']);
        return contentStream;

    }
}