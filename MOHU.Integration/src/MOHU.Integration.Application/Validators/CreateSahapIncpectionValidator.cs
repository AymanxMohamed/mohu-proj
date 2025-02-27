using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.Sahab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Validators;
public class CreateSahapIncpectionValidator : AbstractValidator<SahabCreateInspectionDetailsRequest>
{

    public CreateSahapIncpectionValidator()
    {
        RuleFor(req => req.CaseTicketNumber)
            .NotEmpty().WithMessage("Case Ticket Number Status is Required");
        RuleFor(req => req.CaseIntegrationStatus)
            .NotEmpty().WithMessage("Integration Status is Required");

        RuleFor(req => req.CaseClosureDateTime)
            .NotNull()
            .When(req => req.CaseIntegrationStatus == IntegrationStatus.CloseTheTicket)
            .WithMessage("Case Closure DateTime is required when Integration Status is CloseTheTicket");

        RuleFor(req => req.CaseClosureReason)
            .NotNull()
            .When(req => req.CaseIntegrationStatus == IntegrationStatus.CloseTheTicket)
            .WithMessage("Case Closure Reason is required when Integration Status is CloseTheTicket");

    
        RuleFor(req => req.InspectorName)
            .NotNull()
            .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
            .WithMessage("Inspector Name is required when Integration Status is Pending On Inspection");

        RuleFor(req => req.Comment)
            .NotNull()
            .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
            .WithMessage("Comment is required when Integration Status is Pending On Inspection");

        RuleFor(req => req.ActionDate)
            .NotNull()
            .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
            .WithMessage("Action Date is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.Status)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Status is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.Branch)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Branch is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.Checklist)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Checklist is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.City)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("City is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.Department)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Department is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.District)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("District is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.EndTime)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("End Time is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.VisitDate)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Visit Date is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.LicenseNumber)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("License Number is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.Region)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Region is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.Supervisor)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Supervisor is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.TicketStatus)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Ticket Status is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.TicketAge)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Ticket Age is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.UpdatedLocation)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Updated Location is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.VisitAge)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Visit Age is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.VisitCategory)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Visit Category is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.VisitCode)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Visit Code is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.VisitStatus)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Visit Status is required when Integration Status is Pending On Inspection");

        //RuleFor(req => req.VisitType)
        //    .NotNull()
        //    .When(req => req.CaseIntegrationStatus == IntegrationStatus.PendingOnInspection)
        //    .WithMessage("Visit Type is required when Integration Status is Pending On Inspection");
    }

}
