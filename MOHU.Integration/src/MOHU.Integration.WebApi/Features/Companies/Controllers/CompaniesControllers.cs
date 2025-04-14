using System.Net;
using MOHU.Integration.Application.Features.EnhancedTickets.Repositories;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;

[Route("api/companies")]
public class CompaniesControllers(ICompaniesService service, ITicketsRepository ticketsRepository) : BaseController
{
    [HttpGet("{elmReferenceId:long}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseMessage<Company>),StatusCodes.Status200OK)]
    public async Task<ResponseMessage<Company>> GetByElmReferenceId(long elmReferenceId)
    {
        var company = await service.GetByElmReferenceId(elmReferenceId);
        return Ok(company);
    }
    
    [HttpGet("{id:guid}/tickets")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseMessage<Ticket>),StatusCodes.Status200OK)]
    public ResponseMessage<List<Ticket>> GetTickets(Guid id)
    {
        var tickets = ticketsRepository.GetCompanyTicketsAsync(id);
        return Ok(tickets);
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