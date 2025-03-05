using System.Text.Json;

namespace MOHU.Integration.WebApi.Common.WebHooks;

public abstract class AppWebHook : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public abstract Task<ResponseMessage<string>> HandleWebhook([FromBody] JsonElement jsonElement);
}