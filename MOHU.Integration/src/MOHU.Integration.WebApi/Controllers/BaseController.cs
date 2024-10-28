using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.WebApi.Controllers
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
