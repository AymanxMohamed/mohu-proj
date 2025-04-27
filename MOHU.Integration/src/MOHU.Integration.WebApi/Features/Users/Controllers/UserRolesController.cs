
using Common.Crm.Infrastructure.Common.Extensions;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using MOHU.Integration.Contracts.Dto.CreateProfile;


namespace MOHU.Integration.WebApi.Features.Users.Controllers
{

    [ApiController]
    [Route("api/Users")]

    public class UserRolesController(IUserService userService) : BaseController
    {

 


        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<List<UserWithRolesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<List<UserWithRolesDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<List<UserWithRolesDto>>), StatusCodes.Status404NotFound)]
        [HttpGet("AllUsersWithRoles")]
        public async Task<ResponseMessage<List<UserWithRolesDto>>> Get()
        {

        

            var result = await userService.GetUserRolesAsync();
            return Ok(result);
        }
    }
}


//Task<PaginationResponse<UserWithRolesDto>> GetUserRolesAsync(int pageSize, int pageNumber)