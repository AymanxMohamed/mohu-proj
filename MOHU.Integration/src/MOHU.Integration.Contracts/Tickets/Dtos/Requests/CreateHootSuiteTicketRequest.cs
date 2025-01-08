using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Tickets.Enums;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Requests;

public class CreateHootSuiteTicketRequest : CreateTicketRequest
{
    public Guid? CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public Guid? SubCategoryId1 { get; set; }
    public int? BeneficiaryType { get; set; }
    public int? Location { get; set; }

    public Entity ToTicket(Guid customerId, Service service)
    {
        var entity  = base.ToTicket(service, (int)CaseOriginEnum.SocialMedia, false);
        
        entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, customerId));

        if (CategoryId.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_MainCategoryid,
                new EntityReference(ldv_casecategory.EntityLogicalName, CategoryId.Value));
        if (SubCategoryId.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_SubCategoryid,
                new EntityReference(ldv_casecategory.EntityLogicalName, SubCategoryId.Value));
        if (SubCategoryId1.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_SecondarySubCategoryid,
                new EntityReference(ldv_casecategory.EntityLogicalName, SubCategoryId1.Value));
        if (BeneficiaryType.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Beneficiarytypecode, new OptionSetValue(BeneficiaryType.Value));

        if (Location.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Locationcode, new OptionSetValue(Location.Value));

        return entity;
    }
}