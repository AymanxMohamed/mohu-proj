using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

namespace MOHU.Integration.WebApi.Common.Security.AzureKeyVault;

public class ApplicationKeyVaultSecretManager(string? prefix = null) : KeyVaultSecretManager
{
    private const int DefaultReloadIntervalInMinutes = 30;
    
    private readonly string _prefix = string.IsNullOrWhiteSpace(prefix) ? string.Empty : $"{prefix}-";
    
    public override bool Load(SecretProperties secret) => secret.Name.StartsWith(_prefix);

    public override string GetKey(KeyVaultSecret secret) => secret
        .Name[_prefix.Length..]
        .Replace("--", ConfigurationPath.KeyDelimiter);
    
    public static AzureKeyVaultConfigurationOptions GetConfigurationOptions(
        string? prefix = null,
        int reloadIntervalInMinutes = DefaultReloadIntervalInMinutes) => new()
    {
        Manager = Create(prefix), 
        ReloadInterval = TimeSpan.FromMinutes(reloadIntervalInMinutes)
    };

    private static ApplicationKeyVaultSecretManager Create(string? prefix = null) => new(prefix);
}