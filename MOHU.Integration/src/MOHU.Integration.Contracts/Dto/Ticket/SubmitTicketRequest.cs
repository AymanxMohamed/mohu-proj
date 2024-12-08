
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Contracts.Dto.Ticket;

public class SubmitTicketRequest
{
    public string Description { get; set; } = null!;
    public Guid CaseType { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public Guid? SubCategoryId1 { get; set; } 
    public int? BeneficiaryType { get; set; }
    public int? Location { get; set; }

    public Entity ToTicket(Guid customerId, int origin, (EntityReference Process, EntityReference ParentService) service)
    {
        var entity = new Entity(Incident.EntityLogicalName);

        entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, customerId));
        entity.Attributes.Add(Incident.Fields.ldv_Description, Description);
        entity.Attributes.Add(Incident.Fields.CaseOriginCode, new OptionSetValue(origin));
        entity.Attributes.Add(Incident.Fields.ldv_serviceid, new EntityReference(ldv_service.EntityLogicalName, CaseType));
        entity.Attributes.Add(Incident.Fields.ldv_MainCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, CategoryId));
        entity.Attributes.Add(Incident.Fields.ldv_SubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, SubCategoryId));
        entity.Attributes.Add(Incident.Fields.ldv_processid, new EntityReference("workflow", service.Process.Id));
        entity.Attributes.Add(Incident.Fields.ldv_requesttypeid,  service.ParentService);
        entity.Attributes.Add(Incident.Fields.ldv_IsSubmitted, true);

        if (SubCategoryId1.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_SecondarySubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, SubCategoryId1.Value));

        if (BeneficiaryType.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Beneficiarytypecode, new OptionSetValue(BeneficiaryType.Value));

        if (Location.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Locationcode, new OptionSetValue(Location.Value));

        return entity;
    }
}