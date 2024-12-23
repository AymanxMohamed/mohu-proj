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
            requestInfo.Origin,
            await GetServiceAsync(request.CaseType));

        return await CreateTicketEntityAsync(ticket);
    }

    public async Task<SubmitTicketResponse> SubmitHootSuiteTicketAsync(Guid customerId, CreateHootSuiteTicketRequest request)
    {
        (await createHootSuiteTicketValidator.ValidateAsync(request)).EnsureValidResult();
        await EnsureNoActiveTicketForCustomerAsync(customerId);

        var ticket = request.ToTicket(customerId, await GetServiceAsync(request.CaseType));
        
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

    private async Task<Contracts.Services.Service> GetServiceAsync(Guid serviceId)
    {
        var ticketTypeEntity = await crmContext.ServiceClient
            .RetrieveAsync(ldv_service.EntityLogicalName, serviceId, 
                new ColumnSet(
                    ldv_service.Fields.ldv_processid, 
                    ldv_service.Fields.ldv_serviceparentid));
        
        var process = ticketTypeEntity.GetAttributeValue<EntityReference>(ldv_service.Fields.ldv_processid);
        var parentService = ticketTypeEntity.GetAttributeValue<EntityReference>(ldv_service.Fields.ldv_serviceparentid);
        
        return Contracts.Services.Service.Create(process, parentService);

    }
}