using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.ServiceDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Validators;
public class SDUpdateTicketStatusValidatior : AbstractValidator<ServiceDeskUpdateStatusRequest>
{

    public SDUpdateTicketStatusValidatior()
    {
        RuleFor(req => req.TicketNumber)
            .NotEmpty().WithMessage("Ticket Number is Required");

        RuleFor(req => req.IntegrationStatus)
            .NotEmpty().WithMessage("Integration Status is Required");

        RuleFor(req => req.Resolution)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.CloseTheTicket)
            .WithMessage("Resolution is required when Integration Status is CloseTheTicket");

        RuleFor(req => req.ResolutionDate)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.CloseTheTicket)
            .WithMessage("Resolution Date is required when Integration Status is CloseTheTicket");

        RuleFor(req => req.Comment)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.Canceled)
            .WithMessage("Comment is required when Integration Status is Canceled");

        RuleFor(req => req.LastActionDate)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.Canceled)
            .WithMessage("Last Action Date is required when Integration Status is Canceled");

        RuleFor(req => req.UpdatedBy)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.Canceled)
            .WithMessage("Updated By is required when Integration Status is Canceled");
    }

}
