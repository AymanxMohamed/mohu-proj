namespace MOHU.Integration.Infrastructure.Settings;

public class CrmContextSettings
{
    public string AuthType { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string Url { get; set; } = null!;

    public string GetConnectionString() => 
        $"AuthType={AuthType};ClientId={ClientId};Url={Url};ClientSecret={ClientSecret};";
}