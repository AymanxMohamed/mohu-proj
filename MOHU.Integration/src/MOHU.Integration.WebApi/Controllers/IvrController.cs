using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Logging;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IvrController : BaseController
    {
        private readonly ICrmContext _context;
        private readonly IIvrService _ivrService;
        private readonly IAppLogger _logger;
        public IvrController(IIvrService ivrService, ICrmContext context, IAppLogger logger)
        {
            _ivrService = ivrService;
            _context = context;
            _logger = logger;
            _logger.LogInfo("Teeeest from logging!!!!!!").Wait();
            throw new BadRequestException("test");
            var s = _context.ServiceClient;
            _logger = logger;
        }

        [HttpPost]
        public async Task<string> GetCustomerProfile(GetCustomerProfileRequest reqest)
        {
            var result = await _ivrService.GetCustomerProfileUrlAsync(reqest);
            return result;
        }
    }
}
