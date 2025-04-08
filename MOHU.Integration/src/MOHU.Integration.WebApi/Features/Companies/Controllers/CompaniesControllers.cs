using System.Net;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/companies")]
public class CompaniesControllers(ICompaniesService service) : ControllerBase
{
    
    [HttpGet("{elmReferenceId:long}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetByElmReferenceId(long elmReferenceId)
    {
        var company = await service.GetByElmReferenceId(elmReferenceId);
        return Ok(company);
    }
    
    
    [HttpPatch]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(UpdateCompaniesRequest request)
    {
        await service.UpdateAsync(request);
        return NoContent();
    }

    [HttpPost("populate-teams")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public IActionResult PopulateTeams()
    {
        Task.Run(service.MapDeactivatedCompaniesToNewCompanies);
        return Accepted();
    }
}