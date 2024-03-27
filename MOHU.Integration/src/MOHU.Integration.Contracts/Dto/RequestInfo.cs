using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.Contracts.Dto
{
    public class RequestInfo : IRequestInfo
    {
        public int Origin { get; set; }
        public string Language { get; set; }
    }
}
