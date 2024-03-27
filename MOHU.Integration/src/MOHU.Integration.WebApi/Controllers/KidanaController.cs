using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidanaController : BaseController
    {
        public readonly IKidanaService _kedanaService;
        public KidanaController(IKidanaService kedanaService)
        {
            _kedanaService = kedanaService;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest request)
        {
            var result = await _kedanaService.UpdateStatus(request);
            return Ok(result);
        }



    }
}
