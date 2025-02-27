namespace Core.Infrastructure.Integrations.Clients.Settings;

public class RestClientSettings
{
    public string BaseUrl { get; init; } = null!;
    
    public string? AuthorizationHeaderKey { get; init; }
    
    public string? AuthorizationHeaderValue { get; init; }
}