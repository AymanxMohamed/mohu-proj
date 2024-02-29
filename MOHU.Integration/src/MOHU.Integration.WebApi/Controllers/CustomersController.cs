using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Customer;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        private readonly IIvrService _ivrService;
        private readonly ICustomerService _customerService;
        public CustomersController(IIvrService ivrService, ICustomerService customerService)
        {
            _ivrService = ivrService;
            _customerService = customerService;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(ProfileUrl))]
        public async Task<ResponseMessage<string>> ProfileUrl(GetCustomerProfileRequest reqest)
        {
            var result = await _ivrService.GetCustomerProfileUrlAsync(reqest);
            return new ResponseMessage<string> { Result = result,Status = Contracts.Enum.Status.Success,StatusCode = StatusCodes.Status200OK};
        }

        [HttpPost]
        public async Task<ResponseMessage<Guid>> Post(CreateProfileResponse model)
        {
            var result = await _customerService.CreateProfile(model);
            return new ResponseMessage<Guid> { StatusCode = StatusCodes.Status200OK, Result = result };

        }
    }
}
