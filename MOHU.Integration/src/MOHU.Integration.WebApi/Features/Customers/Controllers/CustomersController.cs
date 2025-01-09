using MOHU.Integration.Application.Features.Tickets.Services;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Interface.Customer;
using StringExtensions = MOHU.Integration.WebApi.Common.Extensions.StringExtensions;

namespace MOHU.Integration.WebApi.Features.Customers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(IIvrService ivrService, ICustomerService customerService, ITicketService ticketService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<string>),StatusCodes.Status400BadRequest)]
    [HttpGet(nameof(ProfileUrl))]
    public async Task<ResponseMessage<string>> ProfileUrl(string mobileNumber)
    {
        var internationalFormatNumber = StringExtensions.ConvertPhoneNumberToInternationalFormat(mobileNumber);
            
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

    [Route("TicketStatus/{mobileNumber}/{ticketNumber}")]
    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<TicketStatusResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResponseMessage<TicketStatusResponse>> GetStatusByMobile(string mobileNumber, string? ticketNumber)
    {
        var internationalFormatNumber = StringExtensions.ConvertPhoneNumberToInternationalFormat(mobileNumber);

        var individual = await customerService.GetIndividualByMobileNumberAsync(internationalFormatNumber) ?? throw new NotFoundException("Customer with mobile number does not exist");

        var customerId = individual.Id;

        var result = await ticketService.GetTicketStatusAsync(customerId, ticketNumber);

        return Ok(result);
    }
}