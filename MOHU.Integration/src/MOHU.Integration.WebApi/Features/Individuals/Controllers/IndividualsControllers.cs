using MOHU.Integration.Application.Features.Individuals.Services;
using MOHU.Integration.Domain.Individuals;

namespace MOHU.Integration.WebApi.Features.Individuals.Controllers;

[Route("api/individuals")]
[ApiController]
public class IndividualsControllers(IIndividualsService service) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseMessage<List<Individual>>),StatusCodes.Status200OK)]
    public async Task<ResponseMessage<List<Individual>>> Create() => Ok(await service.SyncWithElm());
}