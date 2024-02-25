using Microsoft.AspNetCore.Mvc;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Upload()
        {
            return await Task.FromResult("Uploaded");
        }
    }
}
