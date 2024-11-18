using MOHU.Integration.Application.Features.Tickets.Enums;

namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    private async Task EnsureNoActiveTicketForCustomerAsync(Guid customerId)
    {
        var activeTicketNumber = await DoesCustomerHaveActiveTicketsAsync(customerId);
        
        if (activeTicketNumber is not null)
        {
            throw new BadRequestException($"Customer with this Id: '{customerId}' has already active ticket with this number '{activeTicketNumber}'");
        }
    }
    
    private async Task<string?> DoesCustomerHaveActiveTicketsAsync (Guid customerId)
    {
        var activeIncidentQuery = new QueryExpression(Incident.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(Incident.Fields.Title)
        }; 
        
        activeIncidentQuery.Criteria
            .AddCondition(
                Incident.Fields.CustomerId,
                ConditionOperator.Equal, 
                customerId);
        
        activeIncidentQuery.Criteria
            .AddCondition(
                Incident.Fields.StateCode,
                ConditionOperator.Equal,
                (int)TicketStateCodeEnum.Active);

        var activeIncidents = await _crmContext.ServiceClient.RetrieveMultipleAsync(activeIncidentQuery);

        return activeIncidents.Entities.FirstOrDefault()?.GetAttributeValue<string>(Incident.Fields.Title);
    }
}