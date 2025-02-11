using Newtonsoft.Json;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class InteractionCallIdReadModel
{
    [JsonProperty("SLABreached")]
    public string CrmNumber { get; set; } = null!;
    
    [JsonProperty("CallID")]
    public string CallId { get; set; } = null!;
}