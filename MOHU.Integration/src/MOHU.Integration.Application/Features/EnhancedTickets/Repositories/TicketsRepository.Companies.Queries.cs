using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Constants;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository
{
    public List<Ticket> GetCompanyTickets(Guid companyId)
    {
        return Get(GetQuery(
                conditionExpressions: [ConditionExpressionFactory
                    .CreateConditionExpression(
                        columnLogicalName: TicketsConstants.BasicInformation.Fields.Company,
                        conditionOperator: ConditionOperator.Equal,
                        value: companyId)]))
            .Select(Ticket.Create)
            .ToList();
    }

    public Ticket GetCompanyTicket(Guid companyId, Guid ticketId)
    {
        var ticket = Get(GetQuery(
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