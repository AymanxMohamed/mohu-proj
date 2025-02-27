using AngleSharp.Css.Values;
using FluentValidation;
using MOHU.Integration.Application.Validators;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Sahab;
using MOHU.Integration.Contracts.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MOHU.Integration.Application.Service.Sahab;

public class SahabService(ITicketService _ticketService, ICrmContext _crmContext,IValidator<UpdateTicketStatusData> _updateTicketValidator,IValidator<SahabCreateInspectionDetailsRequest> _incpectionValidator) : ISahabService
{

    public async Task<bool> UpdateStatusAsync(SahabUpdateStatusRequest request)
    {
        var results = await _updateTicketValidator.ValidateAsync(request);

        if (results?.IsValid == false)
        {
            throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }
        var ticket = await _ticketService.GetTicketDetailByNumberAsync(request.TicketNumber);
        return  await _ticketService.UpdateStatusAsync(request.ToUpdateTicketStatusRequest(ticket.Id));
    }
    public async Task<bool> CreateInspectionDetails(SahabCreateInspectionDetailsRequest request)
    {


        var results = await _incpectionValidator.ValidateAsync(request);

        if (results?.IsValid == false)
        {
            throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }
        var inspectionDetails = new Entity(ldv_inspectiondetails.EntityLogicalName);
        //TODO: Check Case is exists
        var ticket = await _ticketService.GetTicketDetailByNumberAsync(request.CaseTicketNumber!);

        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_caseid, new EntityReference(Incident.EntityLogicalName,ticket.Id));
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_comment, request.Comment);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_inspector, request.InspectorName);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_statuscode, SahabStatusEnum.Assigned);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_branch, request.Branch);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_checklist, request.Checklist);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_city, request.City);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_department, request.Department);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_district, request.District);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_endtime, request.EndTime);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_licensenumber, request.LicenseNumber);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_region, request.Region);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_supervisor, request.Supervisor);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_ticketstatus, request.TicketStatus);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_ticketage, request.TicketAge);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_updatedlocation, request.UpdatedLocation);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_visitage, request.VisitAge);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_visitcategory, request.VisitCategory);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_visitcode, request.VisitCode);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_visitdate, request.VisitDate);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_visitstatus, request.VisitStatus);
        inspectionDetails.Attributes.Add(ldv_inspectiondetails.Fields.ldv_visittype, request.VisitType);


        Guid? inspectionDetailsId =  await _crmContext.ServiceClient.CreateAsync(inspectionDetails);

        UpdateSahabTicket ticketToUpdate = new UpdateSahabTicket
        {
            TicketId = ticket.Id,
            StatusReason = request.CaseStatusReason,
            IntegrationStatus = request.CaseIntegrationStatus.GetValueOrDefault()
        };
        if (request.CaseIntegrationStatus.GetValueOrDefault() == IntegrationStatus.CloseTheTicket)
        {
            ticketToUpdate.ClouserReason = request.CaseClosureReason;
            ticketToUpdate.ClouserDateTime = request.CaseClosureDateTime;
        }

        await _ticketService.UpdateSahabTicket(ticketToUpdate);
        return inspectionDetailsId != null ? true: false;

    }
}