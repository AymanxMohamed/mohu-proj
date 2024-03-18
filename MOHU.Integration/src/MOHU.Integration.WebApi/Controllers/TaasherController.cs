using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaasherController : ControllerBase
    {
        public readonly IUpdateStatusService _updateStatusService;
        public TaasherController(
             IUpdateStatusService updateStatusService)
        {

            _updateStatusService = updateStatusService;
        }


        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(UpdateStatusRequest model)
        {
            var result = await _updateStatusService.UpdateStatus(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };

        }




    }
}
