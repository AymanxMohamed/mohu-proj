using System.Text.Json;
using MOHU.Integration.WebApi.Common.WebHooks;
using MOHU.Integration.WebApi.Features.Hootsuite.Common;

namespace MOHU.Integration.WebApi.Features.Hootsuite.Webhooks.ConversationResolved;

public class ConversationResolvedWebHook : AppWebHook
{
    public override IActionResult HandleWebhook(JsonElement jsonElement)
    {
        var conversation = jsonElement.ToObject<HootsuiteBaseEvent<ConversationResolvedPayloadEvent>>();

        if (conversation == null)
        {
            return Ok();
        }
        
        throw new NotImplementedException();
    }
}