using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;

namespace MOHU.Integration.WebApi.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : BaseController
    {
        private readonly ICommonService _commonService;
        private readonly IRequestInfo _requestInfo;

        public CommonController(ICommonService commonService, IRequestInfo requestInfo)
        {
            _commonService = commonService;
            _requestInfo = requestInfo;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<IEnumerable<LookupValueDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<IEnumerable<LookupValueDto>>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route(nameof(Lookup))]
        public async Task<ResponseMessage<IEnumerable<LookupValueDto>>> Lookup(string entityName)
        {
            var result = await _commonService.GetLookups(entityName, _requestInfo.Language);
            return Ok(result);
        }
    }
}
