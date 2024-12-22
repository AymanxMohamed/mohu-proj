using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.WebApi.Controllers;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;


[Route("api/companies")]
public class CompaniesControllers : BaseController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult Update(UpdateCompaniesRequest request)
    {
        return NoContent();
    }
}