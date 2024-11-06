using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Common.Extensions;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Features.Tickets.Services;

public partial class TicketService
{
    public async Task<SubmitTicketResponse> SubmitTicketAsync(Guid customerId, SubmitTicketRequest request)
    {
        var response = new SubmitTicketResponse();

        (await _validator.ValidateAsync(request)).EnsureValidResult();
        
        await EnsureNoActiveTicketForCustomerAsync(customerId);

        var entity = new Entity(Incident.EntityLogicalName);

        entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName, customerId));
        entity.Attributes.Add(Incident.Fields.ldv_Description, request.Description);
        entity.Attributes.Add(Incident.Fields.CaseOriginCode, new OptionSetValue(_requestInfo.Origin));
        entity.Attributes.Add(Incident.Fields.ldv_serviceid, new EntityReference(ldv_service.EntityLogicalName, request.CaseType));
        entity.Attributes.Add(Incident.Fields.ldv_MainCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, request.CategoryId));
        entity.Attributes.Add(Incident.Fields.ldv_SubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, request.SubCategoryId));
        entity.Attributes.Add(Incident.Fields.ldv_processid, new EntityReference("workflow", await GetTicketTypeProcessAsync(request.CaseType)));
        entity.Attributes.Add(Incident.Fields.ldv_IsSubmitted, true);

        if (request.SubCategoryId1.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_SecondarySubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, request.SubCategoryId1.Value));

        if (request.BeneficiaryType.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Beneficiarytypecode, new OptionSetValue(request.BeneficiaryType.Value));

        if (request.Location.HasValue)
            entity.Attributes.Add(Incident.Fields.ldv_Locationcode, new OptionSetValue(request.Location.Value));

        var caseId = await _crmContext.ServiceClient.CreateAsync(entity);
            
        //if (caseId !=null && caseId!=Guid.Empty)
        //{
        //    var createdEntity = new Entity(Incident.EntityLogicalName, caseId);
        //    createdEntity.Attributes.Add(Incident.Fields.ldv_IsSubmitted, true);
        //    await _crmContext.ServiceClient.UpdateAsync(createdEntity);
        //}

        var caseEntity = await _crmContext.ServiceClient.RetrieveAsync(Incident.EntityLogicalName, caseId, new ColumnSet(Incident.Fields.Title));
        response.TicketNumber = caseEntity.GetAttributeValue<string>(Incident.Fields.Title);
        response.TicketId = caseId;

        return response;
    }
}