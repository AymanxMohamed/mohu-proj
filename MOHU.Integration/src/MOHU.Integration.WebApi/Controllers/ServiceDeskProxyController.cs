using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.WebApi.Controllers;
using MOHU.Integration.Contracts.Dto.Common;

namespace SDIntegraion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceDeskProxyController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfigurationService _configurationservice;

        public ServiceDeskProxyController(IHttpClientFactory httpClientFactory, IConfigurationService configuration)
        {

            _httpClientFactory = httpClientFactory;
            _configurationservice = configuration;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<object?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<object>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<object> Post(ServiceDeskRequest request)
        {
            var username = await _configurationservice.GetConfigurationValueAsync("SD_User Name");
            var password = await _configurationservice.GetConfigurationValueAsync("SD_Password");
            var servicedeskURL = await _configurationservice.GetConfigurationValueAsync("SD_URL");
            var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, servicedeskURL.ToString());
            httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
            httpRequestMessage.Content = JsonContent.Create(request, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var contentStream = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\\", "")
                                               .Trim(['"']);
            return contentStream;

        }
    }
}
