using Newtonsoft.Json;

namespace SDIntegraion
{

    public class ServiceDeskRequest
    {
        [JsonProperty("Interaction")]
        public InteractionDto Interaction { get; set; }
    }
}
