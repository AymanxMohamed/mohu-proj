using Newtonsoft.Json;

namespace MOHU.Integration.Contracts.Dto.Document.Download
{
    public class DownloadDocumentFlowResponse
    {
        public Content Content { get; set; }
        public string Name { get; set; }

    }
    public class Content
    {
        [JsonProperty("$content-type")]
        public string ContentType { get; set; }

        [JsonProperty("$content")]
        public string FileContent { get; set; }
    }

}
