using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

public partial interface ITicketsRepository
{
    Ticket GetById(Guid ticketId);
    
    Ticket GetByTitle(string ticketNumber);
    
    PaginationResponse<Ticket> GetCompanyTickets(Guid companyId, FilterExpression? filterExpression, CrmPaginationParameters? paginationParameters);
    
    Ticket GetCompanyTicket(Guid companyId, Guid ticketId);

    QueryBase GetQuery(
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        CrmPaginationParameters? paginationParameters = null,
        params ConditionExpression[] conditionExpressions);
}