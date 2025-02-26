
using Newtonsoft.Json;

namespace MOHU.Integration.WebApi.Features.Hootsuite.Webhooks.ConversationResolved;

public class ConversationResolvedPayloadEvent 
{
    [JsonProperty("conversation")]
    public object Conversation { get; set; }

    [JsonProperty("medium")]
    public object Medium { get; set; }

    [JsonProperty("channel")]
    public object Channel { get; set; }

    [JsonProperty("agent")]
    public object Agent { get; set; }

    [JsonProperty("contactProfile")]
    public object ContactProfile { get; set; }

    [JsonProperty("statusUpdatedReason")]
    public string StatusUpdatedReason { get; set; }

    [JsonProperty("statusUpdatedComment")]
    public string StatusUpdatedComment { get; set; }

    [JsonProperty("messages")]
    public List<object> Messages { get; set; } = new List<object>();

    [JsonProperty("contactAttributes")]
    public List<object> ContactAttributes { get; set; } = new List<object>();

    [JsonProperty("notes")]
    public List<object> Notes { get; set; } = [];

    [JsonProperty("topics")]
    public List<object> Topics { get; set; } = [];
}

