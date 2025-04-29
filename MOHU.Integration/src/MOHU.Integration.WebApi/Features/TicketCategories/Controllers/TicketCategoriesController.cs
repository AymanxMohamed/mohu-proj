
using MOHU.Integration.Contracts.Dto.Category;


namespace MOHU.Integration.WebApi.Features.TicketCategories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketCategoriesController(ITicketCategoryService ticketCategoryService) : BaseController
    {
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route(nameof(UpsertCategories))]
       
             public async Task<ActionResult<ResponseMessage<Guid>>> UpsertCategories(UpsertCategoryRequest model)
        {
           

            try
            {
                var result = await ticketCategoryService.UpsertCategories(model);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ResponseMessage<Guid?> { StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = ex.Message });
            }
        }
    }
}
