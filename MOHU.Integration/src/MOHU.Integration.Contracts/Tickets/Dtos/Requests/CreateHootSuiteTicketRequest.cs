using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public class CreateHootSuiteTicketRequest : CreateTicketRequest
{
    public Entity ToTicket(Guid customerId, Service service)
    {
        var entity  = base.ToTicket(service, (int)CaseOriginEnum.SocialMedia);
        
        entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, customerId));

        return entity;
    }
}