using MOHU.Integration.Application.Common.Extensions;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    public async Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request)
    {
        (await validator.ValidateAsync(request)).EnsureValidResult();
        await EnsureNoActiveTicketForCustomerAsync(customerId);

        var ticket = request.ToTicket(
            customerId, 
            await GetTicketTypeProcessAsync(request.CaseType), 
            requestInfo.Origin);

        return await CreateTicketEntityAsync(ticket);
    }

    public async Task<SubmitTicketResponse> SubmitHootSuiteTicketAsync(CreateHootSuiteTicketRequest request)
    {
        (await createHootSuiteTicketValidator.ValidateAsync(request)).EnsureValidResult();
        await EnsureNoActiveTicketForCustomerAsync(request.CustomerId);

        var ticket = request.ToTicket(await GetTicketTypeProcessAsync(request.CaseType));
        
        return await CreateTicketEntityAsync(ticket);
    }

    private async Task<SubmitTicketResponse> CreateTicketEntityAsync(Entity entity)
    {
        var caseId = await crmContext.ServiceClient.CreateAsync(entity);

        return SubmitTicketResponse.Create(await GetCaseByIdAsync(caseId));
    }

    private async Task<Entity> GetCaseByIdAsync(Guid id)
    {
        return await crmContext.ServiceClient.RetrieveAsync(
            Incident.EntityLogicalName, id, 
            new ColumnSet(Incident.Fields.Title));
    }
}