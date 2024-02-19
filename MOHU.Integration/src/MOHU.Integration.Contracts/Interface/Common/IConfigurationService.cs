namespace MOHU.Integration.Contracts.Interface.Common
{
    public interface IConfigurationService
    {
        Task<string> GetConfigurationValueAsync(string key);
    }
}
