using MOHU.Integration.Contracts.Dto.Sahab;
using MOHU.Integration.Contracts.Enum;
using System.Diagnostics;

namespace MOHU.Integration.Application.Service.Sahab;

public class SahabService : ISahabService
{
    private readonly ICrmContext _crmContext;
    private readonly ITicketService _ticketService;

    public SahabService(ITicketService ticketService,ICrmContext crmContext)
    {
        _crmContext = crmContext;
        _ticketService = ticketService;
    }

    public async Task<bool> UpdateStatusAsync(SahabUpdateStatusRequest request)
    {
        var ticketId = await _ticketService.GetTicketByIntegrationTicketNumberAsync(
            integrationTicketNumber: request.TicketNumber, 
            ticketNumberSchemaName: Incident.Fields.SahabTicketNumber);
        
        return  await _ticketService.UpdateStatusAsync(request.ToUpdateTicketStatusRequest(ticketId));
    }
    public async Task<bool> CreateInspectionDetails(SahabCreateInspectionDetailsRequest request)
    {
        var inspectionDetails = new Entity(ldv_inspectiondetails.EntityLogicalName);
        //TODO: Check Case is exists
        var ticketId = await _ticketService.GetTicketByIntegrationTicketNumberAsync(
           integrationTicketNumber: request.TicketId,
           ticketNumberSchemaName: Incident.Fields.SahabTicketId);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_caseid, new EntityReference(Incident.EntityLogicalName,ticketId));
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_comment, request.Comment);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_inspector, request.Inspector);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_actiondate, request.ActionDate);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_statuscode, SahabStatusEnum.Assigned);
        Guid? inspectionDetailsId =  await _crmContext.ServiceClient.CreateAsync(inspectionDetails);
        return inspectionDetailsId != null ? true: false;
    }

}