using MOHU.Integration.Application.Features.Customers.Repositories;
using MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses.DetailsResponse;
using MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

namespace MOHU.Integration.WebApi.Features.EnhancedTickets.Controllers;

[Route("api/tickets")]
public class EnhancedTicketsControllers(
    ITicketsRepository ticketsRepository,
    ICustomersRepository customersRepository) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(ticketsRepository.GetById(id));
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
}