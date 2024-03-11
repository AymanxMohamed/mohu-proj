using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Taasher
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileResponse>
    {

        public CreateProfileValidator()
        {

            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First name Field is required.")
           .MaximumLength(50).WithMessage("First name Field cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Last name is required.")
           .MaximumLength(50).WithMessage("First name Field cannot exceed 50 characters.");




            RuleFor(x => x.PrimaryEmail)
          .NotEmpty().WithMessage("Email is required.")
          .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
          .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone1)
           .NotEmpty().WithMessage("Phone number is required.");


            RuleFor(x => x.Phone1)
            .NotEmpty().WithMessage("Phone number is required.").MaximumLength(20)
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage("Invalid phone number format.");


            RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage("Nationality Field is required.");

            RuleFor(x => x.IdType)
           .NotEmpty().WithMessage("IdType Field is required.");



        }


    }
}
