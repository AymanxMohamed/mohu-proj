using MOHU.Integration.Contracts.Interface.Cache;

namespace MOHU.Integration.WebApi.Features.Caches.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CacheController(ICacheService cacheService) : ControllerBase
{
    [HttpGet]
    public async Task ClearCache(string cacheKey)
    {
        if (!string.IsNullOrEmpty(cacheKey))
            await cacheService.Remove(cacheKey);
        else
            await cacheService.Clear();
    }
}