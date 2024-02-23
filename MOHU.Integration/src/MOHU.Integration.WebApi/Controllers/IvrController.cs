using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IvrController : BaseController
    {
        private readonly ICrmContext _context;
        private readonly IIvrService _ivrService;
        public IvrController(IIvrService ivrService, ICrmContext context)
        {
            _ivrService = ivrService;
            _context = context;
            throw new BadRequestException("test");
            var s = _context.ServiceClient;
            
        }

        [HttpPost]
        public async Task<string> GetCustomerProfile(GetCustomerProfileRequest reqest)
        {
            var result = await _ivrService.GetCustomerProfileUrlAsync(reqest);
            return result;
        }
    }
}
