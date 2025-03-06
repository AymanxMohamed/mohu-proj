using System.Net;
using System.Net.Http.Json;
using System.ServiceModel;
using System.Text.Json;
using MOHU.Integration.Application.Elm.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.Nusuk.Tickets.Dtos.Requests;
using MOHU.Integration.Infrastructure.Settings;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MOHU.Integration.Application.Elm.Nusuk.Tickets;

internal class NusukTicketsClient(
    IConfigurationService configurationService,
    NusukSettings nusukSettings,
    IHttpClientFactory httpClientFactory) : INusukTicketsClient
{
    public async Task<NusukResponseRoot> UpdateAsync(UpdateNusukTicketRequest request)
    {
        var token = nusukSettings.AccessToken;
        
        var updateUrl = nusukSettings.NusukUpdateStatusUrl;

        using var httpClient = httpClientFactory.CreateClient();
        using var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, updateUrl, token, request);

        var response = await httpClient.SendAsync(httpRequestMessage);
        
        var responseString = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode 
            ? DeserializeResponse<NusukResponseRoot>(responseString) 
            : HandleFailureResponse(responseString, response.StatusCode);
    }
    
    private static HttpRequestMessage CreateHttpRequestMessage(
        HttpMethod method,
        string url, 
        string token, 
        object content)
    {
        var httpRequestMessage = new HttpRequestMessage(method, url)
        {
            Content = JsonContent.Create(content, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        };
        
        httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");
        
        return httpRequestMessage;
    }
    
    private static T DeserializeResponse<T>(string responseString) where T : class
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var response = JsonSerializer.Deserialize<T>(responseString, options);
        return response ?? throw new FaultException("Nusuk ticket update failed");
    }
    
    private static NusukResponseRoot HandleFailureResponse(string responseString, HttpStatusCode statusCode)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var nusukFailureResponse = JsonSerializer.Deserialize<NusukFaultResponse>(responseString, options);

        nusukFailureResponse?.Fault.Throw();
    
        throw new FaultException($"Failed to process update nusuk ticket request: " +
                                 $"Response Status Code {statusCode} response: {responseString}");
    }
}