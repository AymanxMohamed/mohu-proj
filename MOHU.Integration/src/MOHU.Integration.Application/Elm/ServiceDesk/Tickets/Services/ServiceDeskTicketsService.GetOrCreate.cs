using System.Net.Http.Json;
using System.Text.Json;
using MOHU.Integration.Application.Common.Extensions;
using MOHU.Integration.Application.Elm.ServiceDesk.Configurations;
using MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;
using SDIntegraion;

namespace MOHU.Integration.Application.Elm.ServiceDesk.Tickets.Services;

public partial class ServiceDeskTicketsClient
{
    public async Task<TicketResponse> GetOrCreateServiceDeskTicket(ServiceDeskRequest request)
    {
        request.AdjustPhoneNumber();
        
        var sdConfigurations = await ServiceDeskConfigurations.Create(configurationService);

        var getInteractionResponse = await GetServiceDeskTicketAsync(request, sdConfigurations);

        if (getInteractionResponse.Content?.FirstOrDefault() != null)
        {
            return getInteractionResponse.ToTicketResponse();
        }

        return await CreateServiceDeskTicketAsync(request, sdConfigurations);
    }

    private async Task<TicketResponse> CreateServiceDeskTicketAsync(
        ServiceDeskRequest request,
        ServiceDeskConfigurations sdConfigurations)
    {
        return await sdConfigurations.HttpClient
            .DeserializeSendAsync<TicketResponse>(request: sdConfigurations.GetCreateMessage(request));
    }
    
    private async Task<GetInteractionCallIdResponse> GetServiceDeskTicketAsync(
        ServiceDeskRequest request,
        ServiceDeskConfigurations sdConfigurations)
    {
       return await sdConfigurations.HttpClient
           .DeserializeSendAsync<GetInteractionCallIdResponse>(
               request: sdConfigurations.GetGetMessage(request.Interaction.Title));
    }

    private async Task<object> CreateServiceDeskTicketOld(ServiceDeskRequest request)
    {
        request.AdjustPhoneNumber();

        var username = await configurationService.GetConfigurationValueAsync("SD_User Name");
        var password = await configurationService.GetConfigurationValueAsync("SD_Password");
        var serviceDeskUrl = await configurationService.GetConfigurationValueAsync("SD_URL");
        var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, serviceDeskUrl);
        httpRequestMessage.Headers.Add("Authorization", "Basic " + encoded);
        httpRequestMessage.Content = JsonContent.Create(
            request, 
            options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var httpClient = httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        var contentStream = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\\", "")
            .Trim(['"']);
        return contentStream;
    }
}