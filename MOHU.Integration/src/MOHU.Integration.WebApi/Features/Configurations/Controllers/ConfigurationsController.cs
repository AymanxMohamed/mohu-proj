namespace MOHU.Integration.WebApi.Features.Configurations.Controllers;

[Route("api/configurations")]
public class ConfigurationsController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var result = configuration.AsEnumerable().ToDictionary(kv => kv.Key, kv => kv.Value);
        return result.Count == 0 ? ValidationProblem("No configurations available") : Ok(result);
    }
    
    [HttpGet("{configurationKey}")]
    public IActionResult Get(string configurationKey)
    {
        var result = configuration.GetValue<object>(configurationKey);
        return result is null 
            ? ValidationProblem($"No Configuration found with this key {configurationKey}") 
            : Ok(result);
    }
}