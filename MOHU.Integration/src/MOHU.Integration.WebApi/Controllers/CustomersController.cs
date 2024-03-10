using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Exceptions;
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
            return Ok(result);
        }

      

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<Guid?>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Guid>>> Post(CreateProfileResponse model)
        {
            try
            {
                var result = await _customerService.CreateProfile(model);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ResponseMessage<Guid?> { StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = ex.Message });
            }
          
        }









    }
}
