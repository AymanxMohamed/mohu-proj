using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Net;
using MOHU.Integration.Contracts.Interface.Common;

namespace SDIntegraion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceDeskProxyController:ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfigurationService _configurationservice;

        public ServiceDeskProxyController(IHttpClientFactory httpClientFactory , IConfigurationService configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configurationservice = configuration;
        }

        [HttpPost] 
        public async Task<object> Post(ServiceDeskRequest sdTicket)
        {
            var username = _configurationservice.GetConfigurationValueAsync("SD_User Name");
            var password = _configurationservice.GetConfigurationValueAsync("SD_Password");
            var servicedeskURL = _configurationservice.GetConfigurationValueAsync("SD_URL");

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + password));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, servicedeskURL.ToString());
            httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
            httpRequestMessage.Content = JsonContent.Create(sdTicket);
           


            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<object>(contentStream);
            
        }
    }
}
