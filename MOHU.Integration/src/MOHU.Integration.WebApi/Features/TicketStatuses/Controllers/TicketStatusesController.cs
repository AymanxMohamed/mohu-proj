using Common.Crm.Infrastructure.Common.Extensions;
using MOHU.Integration.Application.Features.TicketStatuses.Repositories;
using MOHU.Integration.Domain.Features.TicketStatuses;
using MOHU.Integration.WebApi.Common.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.TicketStatuses.Controllers;

[Route("api/ticket-statuses")]
public class TicketStatusesController(ITicketStatusesRepository ticketStatusesRepository) : BaseController
{
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseMessage<PaginationResponse<TicketStatus>>),StatusCodes.Status200OK)]
    public ResponseMessage<PaginationResponse<TicketStatus>> GetTickets(
        [FromBody] PaginationWithFilterRequest? request = null)
    {
        var tickets = ticketStatusesRepository
            .GetAll(
                request?.Filter?.ToExpression(), 
                request?.PaginationParameters,
                request?.OrderExpressions);
        
        return Ok(tickets);
    }
}