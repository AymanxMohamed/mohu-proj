using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.Contracts.Dto.Config;
using MOHU.Integration.Contracts.Interface.Cache;

namespace MOHU.Integration.Infrastructure.Service
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheConfig _config;
        public CacheService(IMemoryCache cache, IOptions<MemoryCacheConfig> memoryCacheConfig)
        {
            _cache = cache;    
            _config = memoryCacheConfig.Value;
        }
        public Task Clear()
        {
            if (_cache is MemoryCache concreteMemoryCache)
            {
                concreteMemoryCache.Compact(1.0);
            }
            return Task.CompletedTask;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.FromResult(_cache.TryGetValue(key, out T value) ? value : default);
        }

       

        public Task Remove(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); //TimeSpan.FromMinutes(_config.ExpirationInMinutes)
            _cache.Set(key, value,cacheEntryOptions);
            return Task.CompletedTask ;
        }
    }
}
