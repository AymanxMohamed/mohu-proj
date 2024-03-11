using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public KedanaController(IKedanaUpdateStatusService kedanaUpdateStatusService)
        {

            _kedanaUpdateStatusService = kedanaUpdateStatusService;

        }

        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(KedanaUpdateStatusResponse model)
        {
            var result = await _kedanaUpdateStatusService.KedanaUpdateStatus(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };

        }



    }
}
