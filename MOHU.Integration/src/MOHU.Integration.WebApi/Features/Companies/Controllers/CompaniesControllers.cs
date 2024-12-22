using System.Net;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;
using MOHU.Integration.WebApi.Controllers;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;


[Route("api/companies")]
public class CompaniesControllers(ICompaniesService service) : BaseController
{
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(UpdateCompaniesRequest request)
    {
        await service.UpdateAsync(request);
        return NoContent();
    }
}