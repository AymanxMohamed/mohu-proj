using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IvrController : BaseController
    {
        private readonly IIvrService _ivrService;
        public IvrController(IIvrService ivrService)
        {
            _ivrService = ivrService;
        }

        [HttpPost]
        public async Task<string> GetCustomerProfile(GetCustomerProfileRequest reqest)
        {
            var result = await _ivrService.GetCustomerProfileUrlAsync(reqest);
            return result;
        }
    }
}
