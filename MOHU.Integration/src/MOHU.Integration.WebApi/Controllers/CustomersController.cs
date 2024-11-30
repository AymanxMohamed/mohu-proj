using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.WebApi.Common.Extension;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IIvrService ivrService, ICustomerService customerService) : BaseController
    {
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(ProfileUrl))]
        public async Task<ResponseMessage<string>> ProfileUrl(string mobileNumber)
        {
            var internationalFormatNumber = mobileNumber.ConvertPhoneNumberToInternationalFormat();
            
            var result = await ivrService.GetCustomerProfileUrlAsync(internationalFormatNumber);
            
            return Ok(result);
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<Guid?>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Guid>>> Post(CreateProfileRequest model)
        {
            try
            {
                var result = await customerService.CreateProfileAsync(model);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ResponseMessage<Guid?> { StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = ex.Message });
            }
          
        }
        
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<Guid?>), StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(CreatePhoneCall))]
        public async Task<ResponseMessage<Guid>> CreatePhoneCall(CreatePhoneCallRequest request)
        {
            var result = await ivrService.CreatePhoneCall(request);
            return Ok(result);
        }
    }
}
