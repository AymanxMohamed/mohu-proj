namespace MOHU.Integration.Contracts.Interface.Cache
{
    public interface ICacheService<T>
    {
        Task<T> GetAsync(string key);
        Task SetAsync(string key, T value);
        Task Clear();
        Task Remove(string key);
    }
}
