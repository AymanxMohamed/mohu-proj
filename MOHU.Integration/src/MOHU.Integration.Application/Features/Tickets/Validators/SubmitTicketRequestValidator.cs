namespace MOHU.Integration.Application.Features.Tickets.Validators
{
    public class SubmitTicketRequestValidator : AbstractValidator<SubmitTicketRequest>
    {
        public SubmitTicketRequestValidator()

        {
            RuleFor(x => x.Description)
           .NotEmpty()
           .WithErrorCode(ErrorMessageCodes.DescriptionisRequired);

            RuleFor(x => x.CaseType)
           .NotEmpty()
           .WithErrorCode(ErrorMessageCodes.CaseTypeisRequired);

            RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithErrorCode(ErrorMessageCodes.CategoryRequired);

            RuleFor(x => x.SubCategoryId)
            .NotEmpty()
            .WithErrorCode(ErrorMessageCodes.SubCategoryIdRequired);
        }
    }
}
