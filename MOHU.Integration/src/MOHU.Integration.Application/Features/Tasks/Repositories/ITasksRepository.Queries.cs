using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Tasks;

namespace MOHU.Integration.Application.Features.Tasks.Repositories;

public partial interface ITasksRepository
{
    PaginationResponse<CrmTask> GetTicketTasks(
        Guid ticketId, 
        FilterExpression? filterExpression = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null);
}