using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using DocumentFormat.OpenXml.Wordprocessing;
using MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Constants;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository
{
    public PaginationResponse<NusukMasarTicketResponse> GetCompanyTickets(
        Guid companyId, 
        FilterExpression? filterExpression = null, 
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null)
    {
        return GetPaginated(GetQuery(
                filterExpression: filterExpression,
                paginationParameters: paginationParameters,
                orderExpressions: orderExpressions,
                conditionExpressions: [ConditionExpressionFactory
                    .CreateConditionExpression(
                        columnLogicalName: TicketsConstants.BasicInformation.Fields.Company,
                        conditionOperator: ConditionOperator.Equal,
                        value: companyId)]))
            .Convert(x => NusukMasarTicketResponse.Create(Ticket.Create(x)));
    }
    
    public PaginationResponse<NusukMasarTicketListResponse> GetCompanyTicketsV2(
        Guid companyId, 
        FilterExpression? filterExpression = null, 
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null)
    {
        return GetPaginated(GetQuery(
                filterExpression: filterExpression,
                paginationParameters: paginationParameters,
                orderExpressions: orderExpressions,
                conditionExpressions: [ConditionExpressionFactory
                    .CreateConditionExpression(
                        columnLogicalName: TicketsConstants.BasicInformation.Fields.Company,
                        conditionOperator: ConditionOperator.Equal,
                        value: companyId)]))
            .Convert(x => NusukMasarTicketListResponse.Create(Ticket.Create(x)));
    }

    public Ticket GetCompanyTicket(Guid companyId, Guid ticketId, ColumnSet? columnSet = null)
    {
        var ticket = Get(GetQuery(
                columnSet: columnSet,
                conditionExpressions: [
                    ConditionExpressionFactory.CreateConditionExpression(
                        columnLogicalName: TicketsConstants.BasicInformation.Fields.Company,
                        conditionOperator: ConditionOperator.Equal,
                        value: companyId),
                    ConditionExpressionFactory.CreateConditionExpression(
                        columnLogicalName: TicketsConstants.BasicInformation.Fields.Id,
                        conditionOperator: ConditionOperator.Equal,
                        value: ticketId)
                ]))
            .Select(Ticket.Create)
            .FirstOrDefault();

        if (ticket is null)
        {
            throw new NotFoundException($"Their is no ticket with this id: {ticketId} found for this company: {companyId}.");
        }

        return ticket;
    }
}