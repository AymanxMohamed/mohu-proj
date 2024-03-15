using MOHU.Integration.Contracts.Interface.Cache;

namespace MOHU.Integration.Infrastructure.Service
{
    public class CacheKeyGeneratorService : ICacheKeyGeneratorService
    {
        public string GenerateKey(string key, string language)
        {
            return $"{key}_{language}";
        }
    }
}
