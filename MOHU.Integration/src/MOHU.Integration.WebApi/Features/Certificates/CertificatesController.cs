using MOHU.Integration.WebApi.Common.Security.Certificates;

namespace MOHU.Integration.WebApi.Features.Certificates;

[Route("api/certificates")]
public class CertificatesController : ControllerBase
{
    [HttpGet("{certificateThumbprint}")]
    public IActionResult Get(string certificateThumbprint)
    {
        var certificate = CertificatesFactory.GetByThumbprint(certificateThumbprint);

        return certificate is null
            ? ValidationProblem($"Their is no certificate found with this {certificateThumbprint}")
            : Ok(certificate.Thumbprint);
    }
}