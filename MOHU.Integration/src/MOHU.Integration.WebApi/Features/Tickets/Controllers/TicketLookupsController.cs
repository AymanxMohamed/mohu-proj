using MOHU.Integration.Application.Features.TicketCategories;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using MOHU.Integration.WebApi.Features.Tickets.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.Tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketLookupsController(ITicketService ticketService, ITicketCategoriesService ticketCategoriesService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<List<TicketTypeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<List<TicketTypeResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route(nameof(Types))]
    public async Task<ResponseMessage<List<TicketTypeResponse>>> Types()
    {
        var result = await ticketService.GetTicketTypesAsync();
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<ResponseMessage<string>> ValidateCategories([FromBody] ValidateCategoriesRequest request)
    {
        await ticketCategoriesService.EnsureValidCategoriesAsync(request.CategoryIds);
        return Ok("Categories are valid");
    }
}