using System.Net.Http.Json;
using System.ServiceModel;
using System.Text.Json;
using MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Nusuk.Tickets.Constatns;
using MOHU.Integration.Application.Nusuk.Tickets.Dtos.Requests;
using MOHU.Integration.Contracts.Interface.Common;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MOHU.Integration.Application.Nusuk.Tickets;

internal class NusukTicketClient(
    IConfigurationService configurationService,
    IHttpClientFactory httpClientFactory) : INusukTicketClient
{
    public async Task<NusukRootResponse> UpdateAsync(UpdateNusukTicketRequest request)
    {
        var token = await configurationService.GetConfigurationValueAsync(NusukConfigurationKeys.Token);
        var updateUrl = await configurationService.GetConfigurationValueAsync(NusukConfigurationKeys.UpdateTicketStatusUrl);

        var httpClient = httpClientFactory.CreateClient();
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, updateUrl);
        httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");
        httpRequestMessage.Content = JsonContent.Create(request, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var response = await httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var nusukRootResponse = JsonSerializer.Deserialize<NusukRootResponse>(responseString);
            if (nusukRootResponse != null)
            {
                return nusukRootResponse;
            }
            
            throw new FaultException("Nusuk ticket update failed");
        }
        
        throw new FaultException($"Failed to process update nusuk ticket request: Response Status Code {response.StatusCode} response: {response.Content}");
    }
}