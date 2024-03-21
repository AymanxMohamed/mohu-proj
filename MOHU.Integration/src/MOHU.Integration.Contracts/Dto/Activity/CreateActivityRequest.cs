using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Contracts.Dto.Activity
{
    public class CreateActivityRequest
    {
        public string ActivityName { get; set; }
        public LookupDto? From { get; set; }
        public IEnumerable<LookupDto> To { get; set; }
        public LookupDto Owner { get; set; }
        public Dictionary<string,object> ExtraProperties { get; set; }
        public CreateActivityRequest()
        {
            To = new List<LookupDto>();
            ExtraProperties = new Dictionary<string, object>();
        }
    }

}
