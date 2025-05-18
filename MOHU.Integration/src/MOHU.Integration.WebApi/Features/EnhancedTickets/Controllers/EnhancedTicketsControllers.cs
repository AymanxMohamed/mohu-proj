using AngleSharp.Dom;
using MOHU.Integration.Application.Features.Customers.Repositories;
using MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses.DetailsResponse;
using MOHU.Integration.Application.Features.EnhancedTickets.Repositories;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.EnhancedTickets.Controllers;

[Route("api/tickets")]
public class EnhancedTicketsControllers(
    ITicketsRepository ticketsRepository,
    ICustomersRepository customersRepository,
    IDocumentService documentService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(ticketsRepository.GetById(id));
    }
    
    [HttpGet("{id:guid}/documents")]
    public async Task<IActionResult> GetTicketDocuments(Guid id)
    {
        var documentResult = await documentService.GetFilesInFolderAsync(id.ToString());
        return Ok(documentResult);
    }
    
    [HttpGet("/api/v2/tickets/{id:guid}")]
    public IActionResult GetByIdV2(Guid id)
    {
        NusukMasarTicketDetailsResponse ticket = ticketsRepository.GetById(id);
        return Ok(ticket);
    }
    
    [HttpGet("by-title/{title}")]
    public IActionResult GetByTitle(string title)
    {
        NusukMasarTicketDetailsResponse ticket = ticketsRepository.GetByTitle(title);
        return Ok(ticket);
    }
    
    [HttpGet("customers/{customerId:guid}")]
    public IActionResult GetCustomerById(Guid customerId)
    {
        var customer = customersRepository.EnsureExistenceById(customerId);

        return customer.IsIndividual ? Ok(customer.ToIndividual()) : Ok(customer.ToCompany());
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    [Route(nameof(ResolveTicket))]
    public async Task<ActionResult> ResolveTicket(ResolveTicketRequest request)
    {
        var result = await ticketsRepository.ResolveTicketAsync(request);
        return Ok(result);
    }
}