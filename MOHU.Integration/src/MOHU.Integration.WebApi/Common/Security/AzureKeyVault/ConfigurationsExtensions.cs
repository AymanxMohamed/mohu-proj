namespace MOHU.Integration.WebApi.Common.Security.AzureKeyVault;

public static class ConfigurationsExtensions
{
    public static void ConfigureAzureKeyVault(this ConfigurationManager configuration)
    {
        var azureKeyVaultSettings = configuration
            .GetSection(AzureKeyVaultSettings.SectionName).Get<AzureKeyVaultSettings>();
        
        azureKeyVaultSettings?.Configure(configuration);
    }
}