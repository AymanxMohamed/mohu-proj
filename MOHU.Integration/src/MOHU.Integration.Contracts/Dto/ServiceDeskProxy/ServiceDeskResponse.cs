using Newtonsoft.Json;

namespace SDIntegraion
{
    public class ServiceDeskResponse
    {
        [JsonProperty("Interaction")]
        public ServiceDeskResponseDTO Interaction { get; set; }
        [JsonProperty("Messages")]
        public List<string> Messages { get; set; }

        [JsonProperty("ReturnCode")]
        public int ReturnCode { get; set; }
    }
}
