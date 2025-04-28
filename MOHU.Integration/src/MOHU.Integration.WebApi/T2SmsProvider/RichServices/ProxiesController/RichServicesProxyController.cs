using MOHU.Integration.Application.T2SmsProvider.RichService;
using MOHU.Integration.Application.T2SmsProvider.RichService.Dtos.Requests;

namespace MOHU.Integration.WebApi.T2SmsProvider.RichServices.ProxiesController;

[Route("api/t2/rich-services/proxies")]
[ApiController]
public class RichServicesProxyController(IT2RichServiceClient client) : ControllerBase
{
    [HttpPost("SendSmsConfirmation")]
    public IActionResult SendSmsConfirmation(SendSmsConfirmationRequest request)
    {
        var entities = client.SendSmsConfirmation(request).ToValueOrException();

        return Ok(entities);
    }
}