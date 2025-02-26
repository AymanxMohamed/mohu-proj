using System.Text.Json;

namespace MOHU.Integration.WebApi.Common.WebHooks;

public abstract class AppWebHook : BaseController
{
    [HttpPost]
    public abstract IActionResult HandleWebhook([FromBody] JsonElement jsonElement);
}