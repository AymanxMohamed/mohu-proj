using System.Text.Json;
using MOHU.Integration.Contracts.Dto.Hootsuite;
using MOHU.Integration.WebApi.Common.Controllers.Webhooks;
using MOHU.Integration.WebApi.Features.Hootsuite.Common;
using MOHU.Integration.WebApi.Features.Hootsuite.Webhooks.ConversationResolved;

namespace MOHU.Integration.WebApi.Features.Tickets.Hootsuite;

public class ConversationResolvedWebHook : AppWebHook
{
    private readonly IHootsuiteService _hootsuiteService;

    public ConversationResolvedWebHook(IHootsuiteService hootsuiteService)
    {
        _hootsuiteService = hootsuiteService;
    }
    public override async Task<ResponseMessage<string>> HandleWebhook(JsonElement jsonElement)
    {
        var conversation = jsonElement.ToObject<HootsuiteBaseEvent<ConversationResolvedPayloadEvent>>();


        if (conversation == null)
        {
            throw new BadRequestException("Bad Request");
        }
        var contactProfile = new ConversationResolvedRequest
        {
            PhoneNumber = conversation.Data.ContactProfile.SecondaryIdentifier,
            Email = conversation.Data.Agent.Email,
            Name = conversation.Data.ContactProfile.PrimaryIdentifier,
            Categories = conversation.Data.Topics,
            Notes = conversation.Data.Notes
        };
        var caseNumber = await _hootsuiteService.ConversationResolved(contactProfile);


        return Ok(caseNumber);
    }
}