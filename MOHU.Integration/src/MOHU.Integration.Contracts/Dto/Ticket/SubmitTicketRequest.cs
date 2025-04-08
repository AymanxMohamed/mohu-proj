
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Dto.Ticket;

public class SubmitTicketRequest : CreateTicketRequest
{
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public Guid? SubCategoryId1 { get; set; } 
    public int? BeneficiaryType { get; set; }
    public int? Location { get; set; }

    public string? ExternalTicketNumber { get; set; }
    public string? ExternalId { get; set; }


    public List<Guid> CategoryIds => 
        new[] { CategoryId, SubCategoryId, SubCategoryId1 }
            .Where(id => id.HasValue && id.Value != Guid.Empty)
            .Select(id => id!.Value)
            .ToList();
    

    public Entity ToTicket(Guid customerId, int origin, Service service)
    {
        var entity = base.ToTicket(service, origin);

        entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, customerId));
        entity.Attributes.Add(Incident.Fields.ldv_MainCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, CategoryId));
        entity.Attributes.Add(Incident.Fields.ldv_SubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, SubCategoryId));

        if (SubCategoryId1.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_SecondarySubCategoryid,
                new EntityReference(ldv_casecategory.EntityLogicalName, SubCategoryId1.Value));

        if (BeneficiaryType.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Beneficiarytypecode, new OptionSetValue(BeneficiaryType.Value));

        if (Location.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Locationcode, new OptionSetValue(Location.Value));

        if (!string.IsNullOrWhiteSpace(ExternalTicketNumber))
            entity.Attributes.Add(Incident.Fields.ExternalTicketNumber, ExternalTicketNumber);

        if (!string.IsNullOrWhiteSpace(ExternalId))
            entity.Attributes.Add(Incident.Fields.ExternalTicketId, ExternalId);


        return entity;
    }
}
