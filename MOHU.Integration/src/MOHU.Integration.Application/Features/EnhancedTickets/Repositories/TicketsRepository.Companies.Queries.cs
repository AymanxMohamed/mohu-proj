using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Constants;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository
{
    public List<Ticket> GetCompanyTicketsAsync(Guid companyId)
    {
        return GetAsync(GetQuery(
                conditionExpressions: [ConditionExpressionFactory
                    .CreateConditionExpression(
                        columnLogicalName: TicketsConstants.BasicInformation.Fields.Company,
                        conditionOperator: ConditionOperator.Equal,
                        value: companyId)]))
            .Select(Ticket.Create)
            .ToList();
    }
}