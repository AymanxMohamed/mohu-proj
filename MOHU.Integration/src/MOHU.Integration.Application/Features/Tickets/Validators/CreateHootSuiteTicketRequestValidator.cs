using MOHU.Integration.Contracts.Tickets.Dtos.Requests;

namespace MOHU.Integration.Application.Features.Tickets.Validators;

public class CreateHootSuiteTicketRequestValidator : AbstractValidator<CreateHootSuiteTicketRequest>
{
    public CreateHootSuiteTicketRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithErrorCode(ErrorMessageCodes.DescriptionisRequired);

        RuleFor(x => x.CaseType)
            .NotEmpty()
            .WithErrorCode(ErrorMessageCodes.CaseTypeisRequired);
    }
}