using System.Net;
using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Application.Features.EnhancedTickets.Repositories;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.WebApi.Common.Dtos.Requests;

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
    public ResponseMessage<PaginationResponse<Ticket>> GetTickets(
        Guid id, 
        [FromQuery] CrmPaginationParameters? paginationParameters = null,
        [FromBody] CreateFilterRequest? filter = null)
    {
        var tickets = ticketsRepository.GetCompanyTickets(id, filter?.ToExpression(), paginationParameters);
        return Ok(tickets);
    }
    
    [HttpPatch("{companyId:guid}/tickets/{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public NoContentResult UpdateTicket(Guid companyId, Guid id, UpdateTicketRequest ticket)
    {
        ticketsRepository.UpdateCompanyTicket(companyId, id, ticket);
        return NoContent();
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