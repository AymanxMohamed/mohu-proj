using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Dto.ServiceDisk;
using MOHU.ExternalIntegration.Contracts.Interface;

namespace MOHU.Externalintegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskController : ControllerBase
    {

        private readonly IServiceDiskUpdateStatusService _serviceDiskUpdateStatusService;
        public ServiceDeskController(
            IServiceDiskUpdateStatusService serviceDiskUpdateStatusService
            )
        {
            _serviceDiskUpdateStatusService = serviceDiskUpdateStatusService;
        }

        [HttpPost]
        [Route(nameof(UpdateStatus))]
        public async Task<ResponseMessage<bool>> UpdateStatus(ServiceDiskUpdateStatusResponse model)
        {
            var result = await _serviceDiskUpdateStatusService.ServiceDiskUpdateStatus(model);
            return new ResponseMessage<bool> { StatusCode = StatusCodes.Status200OK, Result = result };
        }




    }
}
