using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public record CreateHootSuiteTicketRequest(Guid CustomerId, Guid CaseType, string Description)
{
    public Entity ToTicket((EntityReference Process, EntityReference ParentService) service)
    {
        var ticket = new Entity(Incident.EntityLogicalName);
        ticket.Attributes.Add(Incident.Fields.CaseOriginCode, new OptionSetValue((int)CaseOriginEnum.SocialMedia));
        ticket.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, CustomerId));
        ticket.Attributes.Add(Incident.Fields.ldv_Description, Description);
        ticket.Attributes.Add(Incident.Fields.ldv_serviceid,  new EntityReference(ldv_service.EntityLogicalName, CaseType));
        ticket.Attributes.Add(Incident.Fields.ldv_requesttypeid,  service.ParentService);
        ticket.Attributes.Add(Incident.Fields.ldv_processid, new EntityReference("workflow", service.Process.Id));
        ticket.Attributes.Add(Incident.Fields.ldv_IsSubmitted, true);
        return ticket;
    }
}
