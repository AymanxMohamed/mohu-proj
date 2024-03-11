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

        public Task<object> GetAsync(string key)
        {
            var keyExists = _cache.TryGetValue(key, out var value);

            if (keyExists)
                return Task.FromResult(value);

            return Task.FromResult(key as object);
        }

       

        public Task Remove(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task SetAsync(string key, object value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetAbsoluteExpiration(TimeSpan.FromMinutes(_config.ExpirationInMinutes));
            _cache.Set(key, value,cacheEntryOptions);
            return Task.CompletedTask ;
        }
    }
}
