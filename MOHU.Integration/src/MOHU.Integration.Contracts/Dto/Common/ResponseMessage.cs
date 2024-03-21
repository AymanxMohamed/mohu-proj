using MOHU.Integration.Contracts.Enum;
using System.Net;

namespace MOHU.Integration.Contracts.Dto.Common
{
    public class ResponseMessage<T>
    {
        public Status Status { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
    }
}
