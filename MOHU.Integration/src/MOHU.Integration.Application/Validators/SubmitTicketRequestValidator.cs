using FluentValidation;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Shared;

namespace MOHU.Integration.Application.Validators
{
    public class SubmitTicketRequestValidator : AbstractValidator<SubmitTicketRequest>
    {
        private readonly IStringLocalizer _localizer;
        public SubmitTicketRequestValidator(IStringLocalizer localizer)

        {
            _localizer = localizer;

            RuleFor(x => x.Description)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.DescriptionisRequired]);

            RuleFor(x => x.CaseType)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.CaseTypeisRequired]);

            RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.CategoryRequired]);

            RuleFor(x => x.SubCategoryId)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.SubCategoryIdRequired]);
        }
    }
}
