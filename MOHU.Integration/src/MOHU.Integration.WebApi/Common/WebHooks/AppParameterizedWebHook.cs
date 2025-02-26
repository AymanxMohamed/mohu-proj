using System.Text.Json;

namespace MOHU.Integration.WebApi.Common.WebHooks;

public abstract class AppParameterizedWebHook : BaseController
{
    [HttpPost]
    public abstract IActionResult HandleWebhook(
        [FromBody] JsonElement jsonElement, 
        [FromQuery] string queryParameter);
}