namespace MOHU.Integration.Contracts.Interface.Common
{
    public interface IConfigurationService
    {
        Task<TValue?> GetConfigurationValueAsync<TValue>(string key);
        
        Task<string> GetConfigurationValueAsync(string key);

        Task SetOrUpdateConfigurationValueAsync(string key, string value);
    }
}
