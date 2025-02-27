using System.Net.Http.Json;
using System.Text.Json;
using MOHU.Integration.Application.Common.Extensions;
using MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Configurations;
using MOHU.Integration.Contracts.Dto.ServiceDeskProxy;
using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

namespace MOHU.Integration.Application.Features.ThirdParties.ServiceDesk.Tickets.Services;

public partial class ServiceDeskTicketsClient
{
    public async Task<TicketResponse> UpdateTicket(ServiceDeskRequestUpdate request, string callId)
    {
        var sdConfigurations = await ServiceDeskConfigurations.Create(configurationService);

        return await sdConfigurations.HttpClient
            .DeserializeSendAsync<TicketResponse>(request: sdConfigurations.GetUpdateMessage(request, callId));
    }
    
    private async Task<object> OldVersion(ServiceDeskRequestUpdate request, string callId)
    {
        var username = await configurationService.GetConfigurationValueAsync("SD_User Name");
        var password = await configurationService.GetConfigurationValueAsync("SD_Password");
        var serviceDeskUrl = await configurationService.GetConfigurationValueAsync("SD_URL");
        var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, serviceDeskUrl + "/" + callId);
        httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
        httpRequestMessage.Content = JsonContent.Create(request, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        var contentStream = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\\", "")
            .Trim(['"']);
        return contentStream;
    }
}