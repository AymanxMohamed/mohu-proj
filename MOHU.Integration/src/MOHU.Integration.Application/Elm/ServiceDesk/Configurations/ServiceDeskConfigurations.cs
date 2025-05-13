using System.Net.Http.Json;
using System.Text.Json;
using MOHU.Integration.Contracts.Dto.ServiceDeskProxy;
using SDIntegraion;

namespace MOHU.Integration.Application.Elm.ServiceDesk.Configurations;

public class ServiceDeskConfigurations
{
    private const string UserNameKey = "SD_User Name";
    private const string PasswordKey = "SD_Password";
    private const string MohuCrmUrlKey = "SD_URL";
    
    public ServiceDeskConfigurations(string userName, string password, string mohuCrmUrl)
    {
        UserName = userName;
        Password = password;
        MohuCrmUrl = mohuCrmUrl;
    }

    public string UserName { get; init; }

    public string Password { get; init; }

    public string MohuCrmUrl { get; init; }

    public string EncodedCredentials => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserName}:{Password}"));

    public HttpClient HttpClient
    {
        get
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(MohuCrmUrl)
            };
        
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {EncodedCredentials}");

            return httpClient;
        }
    }

    public HttpRequestMessage GetCreateMessage(ServiceDeskRequest request)
    {
        return new HttpRequestMessage(HttpMethod.Post, new Uri(MohuCrmUrl))
        {
            Content = JsonContent.Create(
                request,
                options: new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })
        };
    }
    
    public HttpRequestMessage GetUpdateMessage(ServiceDeskRequestUpdate request, string crmNumber)
    {
        return new HttpRequestMessage(HttpMethod.Post, new Uri($"{MohuCrmUrl}/{crmNumber}"))
        {
            Content = JsonContent.Create(
                request,
                options: new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })
        };
    }
    
    public HttpRequestMessage GetGetMessage(string title)
    {
        var crmNumber = title.Replace("-", string.Empty);
        // return new HttpRequestMessage(HttpMethod.Get, new Uri($"{MohuCrmUrl}?query=huic.crm.number%3D%22{crmNumber}%22"));
        return new HttpRequestMessage(HttpMethod.Get, new Uri($"{MohuCrmUrl}/{crmNumber}"));
    }

    public static async Task<ServiceDeskConfigurations> Create(IConfigurationService configurationService)
    {
        return new ServiceDeskConfigurations(
            userName: await configurationService.GetConfigurationValueAsync(UserNameKey),
            password: await configurationService.GetConfigurationValueAsync(PasswordKey),
            mohuCrmUrl: await configurationService.GetConfigurationValueAsync(MohuCrmUrlKey));
    }
}