using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

public partial interface ITicketsRepository
{
    Ticket GetById(Guid ticketId);
    
    Ticket GetByTitle(string ticketNumber);
    
    List<Ticket> GetCompanyTickets(Guid companyId);
    
    Ticket GetCompanyTicket(Guid companyId, Guid ticketId);

    QueryBase GetQuery(
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        params ConditionExpression[] conditionExpressions);
}