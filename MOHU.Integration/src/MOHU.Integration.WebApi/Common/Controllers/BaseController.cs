namespace MOHU.Integration.WebApi.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       protected ResponseMessage<T> Ok<T>(T result)
        {
            return new ResponseMessage<T>
            {
                Status = Contracts.Enum.Status.Success,
                Result = result,
                StatusCode = StatusCodes.Status200OK,
            };
        }
       
        protected ResponseMessage<T> Created<T>(T result)
        {
            return new ResponseMessage<T>
            {
                Status = Contracts.Enum.Status.Success,
                Result = result,
                StatusCode = StatusCodes.Status201Created,
            };
        }

    }
}
