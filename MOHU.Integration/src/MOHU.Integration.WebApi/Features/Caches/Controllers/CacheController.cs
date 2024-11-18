using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Interface.Cache;

namespace MOHU.Integration.WebApi.Features.Caches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet]
        public async Task ClearCache(string cacheKey)
        {
            if (!string.IsNullOrEmpty(cacheKey))
                await _cacheService.Remove(cacheKey);
            else
            await _cacheService.Clear();
        }
    }
}
