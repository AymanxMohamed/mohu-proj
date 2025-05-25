using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses;
using MOHU.Integration.Contracts.Dto.Taasher;
using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

public partial interface ITicketsRepository
{
    Ticket GetById(Guid ticketId);
    
    Ticket GetByTitle(string ticketNumber);
    
    PaginationResponse<NusukMasarTicketResponse> GetCompanyTickets(
        Guid companyId, 
        FilterExpression? filterExpression = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null);

    PaginationResponse<NusukMasarTicketListResponse> GetCompanyTicketsV2(
        Guid companyId,
        FilterExpression? filterExpression = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null);
    
    Ticket GetCompanyTicket(Guid companyId, Guid ticketId, ColumnSet? columnSet = null);

    QueryBase GetQuery(
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null,
        params ConditionExpression[] conditionExpressions);

     

}