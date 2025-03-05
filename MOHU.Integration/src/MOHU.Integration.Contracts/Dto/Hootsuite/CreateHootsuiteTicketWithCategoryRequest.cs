using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Services;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Features.Tickets.Enums;

namespace MOHU.Integration.Contracts.Dto.Hootsuite;
public class CreateHootsuiteTicketWithCategoryRequest : CreateTicketRequest
{
    public List<TicketCategoryLevel> Categories { get; set; } = [];
    public Entity ToTicket(Guid customerId, Service service)
    {
      
        var entity = base.ToTicket(service, (int)CaseOriginEnum.SocialMedia);

        entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, customerId));
        foreach(var category in Categories)
        {
            if (category.CategoryLevel == CategoryLevelsEnum.ParentCategory)
            {
                entity.Attributes.Add(Incident.Fields.ldv_MainCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, category.Id));
            }
            else if (category.CategoryLevel == CategoryLevelsEnum.SecondryCategory)
            {
                entity.Attributes.Add(Incident.Fields.ldv_SecondarySubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, category.Id));

            }
            else if (category.CategoryLevel == CategoryLevelsEnum.SubCategory)
            {
                entity.Attributes.Add(Incident.Fields.ldv_SubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, category.Id));

            }
        }
        return entity;
    }
}