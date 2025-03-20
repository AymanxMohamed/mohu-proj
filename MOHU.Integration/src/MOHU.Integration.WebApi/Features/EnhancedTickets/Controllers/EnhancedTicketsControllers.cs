using MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

namespace MOHU.Integration.WebApi.Features.EnhancedTickets.Controllers;

[Route("api/tickets")]
public class EnhancedTicketsControllers(ITicketsRepository ticketsRepository) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(ticketsRepository.GetById(id));
    }
    
    [HttpGet("{title}")]
    public IActionResult GetByTitle(string title)
    {
        return Ok(ticketsRepository.GetByTitle(title));
    }
}