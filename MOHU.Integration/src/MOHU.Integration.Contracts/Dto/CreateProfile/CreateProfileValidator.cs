using FluentValidation;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CreateProfile
{
    public  class CreateProfileValidator : AbstractValidator<CreateProfileResponse>
    {
        
        public CreateProfileValidator() 
        {

            
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("")
            .MaximumLength(75).WithMessage("")
            .Matches("^[a-zA-Z]*$").WithMessage("");

            RuleFor(x => x.LastName)
          .NotEmpty().WithMessage("")
          .MaximumLength(75).WithMessage("")
          .Matches("^[a-zA-Z]*$").WithMessage("");


            //ArabicName for arabic character only 
              RuleFor(x => x.ArabicName)
             .NotEmpty().WithMessage("Arabic name is required.")
             .Matches(@"^[\u0600-\u06FF\s]*$").WithMessage("Arabic name must contain only Arabic characters.")
             .MaximumLength(150).WithMessage("Arabic name cannot exceed 150 characters.");

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
           .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
           .EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(x => x.MobileCountryCode)
            .NotEmpty().WithMessage("MobileCountryCode is required.");

             RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Phone number is required.").MaximumLength(20)
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage("Nationality Field is required.");

            RuleFor(x => x.CountryOfResidence)
            .NotEmpty().WithMessage("CountryOfResidence Field is required.");

            RuleFor(x => x.IdType)
           .NotEmpty().WithMessage("IdType Field is required.");


        }


    }
}
