using Common.Crm.Infrastructure.Common.Extensions;
using MOHU.Integration.Application.Features.ServiceDefinitions.Repositories;
using MOHU.Integration.Domain.Features.ServiceDefinitions;
using MOHU.Integration.WebApi.Common.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.ServiceDefinitions.Controllers;

[Route("api/service-definitions")]
public class ServiceDefinitionsController(IServiceDefinitionRepository serviceDefinitionRepository) : BaseController
{
        
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseMessage<PaginationResponse<ServiceDefinition>>),StatusCodes.Status200OK)]
    public ResponseMessage<PaginationResponse<ServiceDefinition>> GetTickets(
        [FromBody] PaginationWithFilterRequest? request = null)
    {
        var tickets = serviceDefinitionRepository
            .GetAll(
                request?.Filter?.ToExpression(), 
                request?.PaginationParameters,
                request?.OrderExpressions);
        
        return Ok(tickets);
    }
}