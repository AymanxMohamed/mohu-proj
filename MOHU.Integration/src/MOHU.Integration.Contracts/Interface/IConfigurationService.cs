namespace MOHU.Integration.Contracts.Interface
{
    public interface IConfigurationService
    {
        Task<string> GetConfigurationValueAsync(string key);
    }
}
