using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public class CreateTicketRequest
{
    public Guid CaseType { get; init; }

    public string Description { get; init; } = null!;

    protected Entity ToTicket(
        Service service,
        int origin,
        bool isSubmitted = true)
    {
        var ticket = new Entity(Incident.EntityLogicalName);
        ticket.Attributes.Add(Incident.Fields.CaseOriginCode, new OptionSetValue(origin));
        ticket.Attributes.Add(Incident.Fields.ldv_Description, Description);
        if(CaseType != Guid.Empty)
        {
         ticket.Attributes.Add(Incident.Fields.ldv_serviceid,  new EntityReference(ldv_service.EntityLogicalName, CaseType));

        }

        if (service.ParentService is not null)
        {
            ticket.Attributes.Add(Incident.Fields.ldv_requesttypeid,  service.ParentService);
        }

        if (service.Process is not null)
        {
            ticket.Attributes.Add(Incident.Fields.ldv_processid, new EntityReference("workflow", service.Process.Id));
        }

        if (CaseType == Guid.Empty)
        {
        ticket.Attributes.Add(Incident.Fields.ldv_IsSubmitted, false);
        }
        else
        {
        ticket.Attributes.Add(Incident.Fields.ldv_IsSubmitted, isSubmitted);

        }
        
        return ticket;
    }
}