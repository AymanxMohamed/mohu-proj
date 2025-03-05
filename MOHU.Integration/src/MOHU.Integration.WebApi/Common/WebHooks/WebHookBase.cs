using System.Text.Json;

namespace MOHU.Integration.WebApi.Common.WebHooks;

public abstract class WebHookBase : BaseController
{
    [HttpPost]
    public abstract IActionResult HandleWebhook([FromBody] JsonElement jsonElement);
}