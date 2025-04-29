using MOHU.Integration.Application.Features.Tasks.Dtos.Responses;
using MOHU.Integration.Application.Features.Tasks.Repositories;
using MOHU.Integration.WebApi.Common.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.Tasks.Controllers;

[Route("api/v1/tickets/{id:guid}/tasks")]
public class TicketTasksController(ITasksRepository repository) : ControllerBase
{
    [HttpPost]
    public IActionResult Get(Guid id, [FromBody] PaginationWithFilterRequest? request = null)
    {
        return Ok(repository
            .GetTicketTasks(
                id,
                request?.Filter?.ToExpression(),
                request?.PaginationParameters,
                request?.OrderExpressions)
            .Convert(NusukMasarCompanyTaskResponse.Create));
    }
}

