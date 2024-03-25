using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.WebApi.Controllers;
using System.Net.Http;
using System.Net;

namespace SDIntegraion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceDeskProxyController:BaseController
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
            var servicedeskURL = await _configurationservice.GetConfigurationValueAsync("SD_URL");

            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + password));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, servicedeskURL.ToString());
            httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
            httpRequestMessage.Content = JsonContent.Create(sdTicket);
           
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<object>(contentStream);
            
        }
        [HttpGet]
        public async Task<object> Get()
        {

            var url = "https://10.1.108.32/SM/9/rest/mohcrm";
            var username = _configurationservice.GetConfigurationValueAsync("SD_User Name");
            var password = _configurationservice.GetConfigurationValueAsync("SD_Password");
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                          .GetBytes(username + ":" + password));
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url.ToString());
            httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
            var client = _httpClientFactory.CreateClient();
            var res = await client.SendAsync(httpRequestMessage);
            var contentStream = await res.Content.ReadAsStringAsync();
            return contentStream;
        }
    }
}
