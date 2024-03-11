using FluentValidation;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace MOHU.Integration.Contracts.Dto.CreateProfile
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileResponse>
    {
        private readonly IStringLocalizer _localizer;

        public CreateProfileValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;


            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FirstnameFieldisRequired])
            .MaximumLength(75).WithMessage(_localizer[ErrorMessageCodes.FirstnameExceedingcharacter])
            .Matches("^[a-zA-Z]*$").WithMessage(_localizer[ErrorMessageCodes.EnglishLettersValidator]);

            RuleFor(x => x.LastName)
          .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.LastNameReuired])
          .MaximumLength(75).WithMessage(_localizer[ErrorMessageCodes.LastNameExceeding])
          .Matches("^[a-zA-Z]*$").WithMessage(_localizer[ErrorMessageCodes.EnglishLettersValidator]);


            RuleFor(x => x.ArabicName)
             .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.ArabicNameisRequired])
             .Matches(@"^[\u0600-\u06FF\s]*$").WithMessage(_localizer[ErrorMessageCodes.ArabicLettersValidator])
             .MaximumLength(150).WithMessage(_localizer[ErrorMessageCodes.ArabicNameExceeding]);

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.EmailRequired])
           .MaximumLength(100).WithMessage(_localizer[ErrorMessageCodes.EmailExceeding])
           .EmailAddress().WithMessage(_localizer[ErrorMessageCodes.EmailValidator]);

            RuleFor(x => x.MobileCountryCode)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FieldIsRequired]);

            RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FieldIsRequired])
            .LessThanOrEqualTo(DateTime.Today).WithMessage(_localizer[ErrorMessageCodes.DateOfBirth]);

            RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FieldIsRequired]).MaximumLength(20)
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage(_localizer[ErrorMessageCodes.MobilePhoneValidator]);

            RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FieldIsRequired]);

            RuleFor(x => x.CountryOfResidence)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FieldIsRequired]);

            RuleFor(x => x.IdType)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FieldIsRequired]);


        }



    }
}
