namespace MOHU.Integration.WebApi.Common.Configurations.AzureKeyVault;

public class AzureKeyVaultSettings
{
    public const string SectionName = nameof(AzureKeyVaultSettings);

    public string VaultName { get; init; } = null!;
}