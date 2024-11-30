namespace MOHU.Integration.WebApi.Common.Configurations.AzureKeyVault;

public class AzureKeyVaultSettings
{
    public const string SectionName = nameof(AzureKeyVaultSettings);

    public string VaultName { get; init; } = null!;
    
    public string ClientId { get; init; } = null!;

    public string CertificateThumbprint { get; init; } = null!;
    
    public bool IsValidSettings() => 
        !string.IsNullOrWhiteSpace(VaultName)
        && !string.IsNullOrWhiteSpace(ClientId)
        && !string.IsNullOrWhiteSpace(CertificateThumbprint);
}