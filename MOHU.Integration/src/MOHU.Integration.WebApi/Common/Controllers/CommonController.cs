namespace MOHU.Integration.WebApi.Common.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommonController(ICommonService commonService, IRequestInfo requestInfo) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<IEnumerable<LookupValueDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<IEnumerable<LookupValueDto>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route(nameof(Lookup))]
    public async Task<ResponseMessage<IEnumerable<LookupValueDto>>> Lookup(string entityName)
    {
        var result = await commonService.GetLookups(entityName, requestInfo.Language);
        return Ok(result);
    }
}