namespace MOHU.Integration.Contracts.Interface.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value);
        Task Clear();
        Task Remove(string key);
    }
}
