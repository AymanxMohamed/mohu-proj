using Azure.Identity;
using MOHU.Integration.WebApi.Common.Security.Certificates;

namespace MOHU.Integration.WebApi.Common.Security.AzureKeyVault;

public class AzureKeyVaultSettings
{
    public const string SectionName = nameof(AzureKeyVaultSettings);

    public string VaultName { get; init; } = string.Empty;

    public string VaultSecretPrefix { get; init; } = string.Empty;
    
    public string TenantId { get; init; } = string.Empty;
    
    public string ClientId { get; init; } = string.Empty;

    public string CertificateThumbprint { get; init; } = string.Empty;

    public int ReloadIntervalInMinutes { get; init; } = 30;

    public Uri KeyVaultUri => new($"https://{VaultName}.vault.azure.net/");
    
    public ClientCertificateCredential ClientCertificateCredential => new(
        TenantId, 
        ClientId, 
        clientCertificate: CertificatesFactory.GetByThumbprint(CertificateThumbprint));
    
    public bool IsValidSettings() => 
        !string.IsNullOrWhiteSpace(VaultName)
        && !string.IsNullOrWhiteSpace(ClientId)
        && !string.IsNullOrWhiteSpace(TenantId)
        && !string.IsNullOrWhiteSpace(CertificateThumbprint);

    public void Configure(ConfigurationManager configuration)
    {
        if (!IsValidSettings())
        {
            return;
        }
        
        configuration.AddAzureKeyVault(
            KeyVaultUri, 
            ClientCertificateCredential, 
            ApplicationKeyVaultSecretManager.GetConfigurationOptions(VaultSecretPrefix, ReloadIntervalInMinutes));
    }
}