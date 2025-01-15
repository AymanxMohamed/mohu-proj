using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.ServiceDesk;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Validators;
public class UpdateTicketStatusValidatior : AbstractValidator<UpdateTicketStatusData>
{

    public UpdateTicketStatusValidatior()
    {
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
            .When(req => req.IntegrationStatus == IntegrationStatus.NeedMoreDetails)
            .WithMessage("Comment is required when Integration Status is Need More Details");

        RuleFor(req => req.LastActionDate)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.NeedMoreDetails)
            .WithMessage("Last Action Date is required when Integration Status is Need More Details");

        RuleFor(req => req.UpdatedBy)
            .NotNull()
            .When(req => req.IntegrationStatus == IntegrationStatus.NeedMoreDetails)
            .WithMessage("Updated By is required when Integration Status is Need More Details");
    }

}
