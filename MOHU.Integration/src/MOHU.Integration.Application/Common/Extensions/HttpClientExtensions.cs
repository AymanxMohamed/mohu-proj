using Newtonsoft.Json;

namespace MOHU.Integration.Application.Common.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TResult> DeserializeSendAsync<TResult>(this HttpClient httpClient, HttpRequestMessage request)
    {
        var response = await httpClient.SendAsync(request);
        
        var contentStream = await response.Content.ReadAsStringAsync();

        if (contentStream == null)
        {
            throw new InvalidOperationException("Failed to deserialize response");
        }

        return JsonConvert.DeserializeObject<TResult>(contentStream) 
               ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}