namespace Core.Infrastructure.Integrations.Clients.Settings;

public class RestClientSettings
{
    public string BaseUrl { get; init; } = null!;
    
    public string? AuthorizationHeaderKey { get; init; }
    
    public string? AuthorizationHeaderValue { get; init; }

    public bool UseTestData { get; init; } = false;
    
    public string TestDataPath { get; init; } = null!;
}