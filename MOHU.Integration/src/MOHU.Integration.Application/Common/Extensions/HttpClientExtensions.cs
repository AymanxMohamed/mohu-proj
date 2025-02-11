using System.Text.Json;

namespace MOHU.Integration.Application.Common.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TResult> DeserializeSendAsync<TResult>(this HttpClient httpClient, HttpRequestMessage request)
    {
        var response = await httpClient.SendAsync(request);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }

        var contentStream = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<TResult>(
            contentStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ) ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}