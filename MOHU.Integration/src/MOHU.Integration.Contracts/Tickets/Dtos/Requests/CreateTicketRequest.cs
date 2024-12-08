using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public class CreateTicketRequest
{
    public Guid CaseType { get; init; }

    public string Description { get; init; } = null!;
    
    public virtual Entity ToTicket(
        Service service,
        int origin,
        bool isSubmitted = true)
    {
        var ticket = new Entity(Incident.EntityLogicalName);
        ticket.Attributes.Add(Incident.Fields.CaseOriginCode, new OptionSetValue(origin));
        ticket.Attributes.Add(Incident.Fields.ldv_Description, Description);
        ticket.Attributes.Add(Incident.Fields.ldv_serviceid,  new EntityReference(ldv_service.EntityLogicalName, CaseType));
        ticket.Attributes.Add(Incident.Fields.ldv_requesttypeid,  service.ParentService);
        ticket.Attributes.Add(Incident.Fields.ldv_processid, new EntityReference("workflow", service.Process.Id));
        ticket.Attributes.Add(Incident.Fields.ldv_IsSubmitted, isSubmitted);
        return ticket;
    }
}