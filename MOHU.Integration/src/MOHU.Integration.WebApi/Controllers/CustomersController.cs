using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        private readonly IIvrService _ivrService;
        public CustomersController(IIvrService ivrService)
        {
            _ivrService = ivrService;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<object>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<object>),StatusCodes.Status404NotFound)]
        [HttpPost(nameof(CustomerProfileUrl))]
        public async Task<ResponseMessage<string>> CustomerProfileUrl(GetCustomerProfileRequest reqest)
        {
            var result = await _ivrService.GetCustomerProfileUrlAsync(reqest);
            return new ResponseMessage<string> { Result = result,Status = Contracts.Enum.Status.Success,StatusCode = StatusCodes.Status200OK};
        }
    }
}
