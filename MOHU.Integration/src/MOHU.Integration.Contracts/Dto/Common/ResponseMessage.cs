using MOHU.Integration.Contracts.Enum;

namespace MOHU.Integration.Contracts.Dto.Common
{
    public class ResponseMessage<T>
    {
        public Status Status { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
        public static ResponseMessage<T> Failure(string message, T? data = default)
        {
            return new ResponseMessage<T> { Status = Status.Failure, Result = data };
        }
        public static ResponseMessage<T> Success(T data)
        {
            return new ResponseMessage<T> { Status = Status.Success, Result = data };

        }
    }
}
