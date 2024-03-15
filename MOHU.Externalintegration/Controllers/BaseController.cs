using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Enum;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ResponseMessage<T> Ok<T>(T result)
        {
            return new ResponseMessage<T>
            {
                Status = Status.Success,
                Result = result,
                StatusCode = StatusCodes.Status200OK,

            };
        }
        protected ResponseMessage<T> Created<T>(T result)
        {
            return new ResponseMessage<T>
            {
                Status = Status.Success,
                Result = result,
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
