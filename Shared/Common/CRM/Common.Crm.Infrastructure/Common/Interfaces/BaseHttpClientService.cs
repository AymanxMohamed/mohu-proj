using System.Net.Http.Headers;
using Common.Crm.Infrastructure.Common.Extensions;
using HttpHeaders = Common.Crm.Infrastructure.Common.Constants.HttpHeaders;

namespace Common.Crm.Infrastructure.Common.Interfaces
{
    public abstract class BaseHttpClientService : IHttpClientService
    {
        protected readonly HttpClient HttpClient;

        protected BaseHttpClientService(string baseUrl)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpHeaders.ApplicationJson));
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.ContentType, HttpHeaders.ApplicationJson);
        }

        public HttpClient GetHttpClient()
        {
            return HttpClient;
        }

        public async Task<T> ExecuteRequest<T>(HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await HttpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                return AppSerializer.Instance.Deserialize<T>(responseContent)!;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public HttpRequestMessage PrepareHttpRequest(
            HttpMethod method, 
            string resourceUrl, 
            object? body = null)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{HttpClient.BaseAddress}{resourceUrl}")
            }.AddRequestBody(body);

            return request;
        }
           

        public async Task<T> PrepareAndExecuteRequest<T>(
            HttpMethod method, 
            string resourceUrl, 
            object? body = null)
        {
            var request = PrepareHttpRequest(method, resourceUrl, body);
            return await ExecuteRequest<T>(request);
        }
    }
}