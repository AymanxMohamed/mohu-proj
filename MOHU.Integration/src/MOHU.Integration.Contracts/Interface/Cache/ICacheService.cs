namespace MOHU.Integration.Contracts.Interface.Cache
{
    public interface ICacheService
    {
        Task<object> GetAsync(string key);
        Task SetAsync(string key, object value);
        Task Clear();
        Task Remove(string key);
    }
}
