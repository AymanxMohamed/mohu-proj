using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace SDIntegraion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceDeskProxyController:ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceDeskProxyController(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        [HttpPost] 
        public async Task<object> Post(ServiceDeskRequest sdTicket)
        {
            var username = "mohcrm";
            var password = "P@ssword123456789";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + password));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://10.1.108.32/SM/9/rest/mohcrm");
            httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
            httpRequestMessage.Content = JsonContent.Create(sdTicket);
           


            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<object>(contentStream);
            
        }
    }
}
