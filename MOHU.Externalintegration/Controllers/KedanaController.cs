using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Dto.Kedana;
using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KedanaController : ControllerBase
    {

        private readonly IKedanaUpdateStatusService _kedanaUpdateStatusService;

        public readonly IUpdateStatusService _updateStatusService;
        public KedanaController(IKedanaUpdateStatusService kedanaUpdateStatusService  , IUpdateStatusService updateStatusService)
        {

            _kedanaUpdateStatusService = kedanaUpdateStatusService;
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
