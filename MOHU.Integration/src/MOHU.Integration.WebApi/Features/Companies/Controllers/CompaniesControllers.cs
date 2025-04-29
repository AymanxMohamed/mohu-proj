using System.Net;
using Common.Crm.Infrastructure.Common.Extensions;
using MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses;
using MOHU.Integration.Application.Features.EnhancedTickets.Repositories;
using MOHU.Integration.Contracts.Companies.Dtos;
using MOHU.Integration.Contracts.Companies.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Features.Companies;
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
    
    [HttpPost("{id:guid}/tickets")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseMessage<PaginationResponse<NusukMasarTicketResponse>>),StatusCodes.Status200OK)]
    public ResponseMessage<PaginationResponse<NusukMasarTicketResponse>> GetTickets(
        Guid id, 
        [FromBody] PaginationWithFilterRequest? request = null)
    {
        var tickets = ticketsRepository
            .GetCompanyTickets(
                id,
                request?.Filter?.ToExpression(), 
                request?.PaginationParameters,
                request?.OrderExpressions);
        
        return Ok(tickets);
    }
    
    [HttpPost("/api/v2/companies/{id:guid}/tickets")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseMessage<PaginationResponse<NusukMasarTicketListResponse>>),StatusCodes.Status200OK)]
    public ResponseMessage<PaginationResponse<NusukMasarTicketListResponse>> GetTicketsV2(
        Guid id, 
        [FromBody] PaginationWithFilterRequest? request = null)
    {
        var tickets = ticketsRepository
            .GetCompanyTicketsV2(
                id,
                request?.Filter?.ToExpression(), 
                request?.PaginationParameters,
                request?.OrderExpressions);
        
        return Ok(tickets);
    }
    
    [HttpPut("{companyId:guid}/tickets/{id:guid}")]
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