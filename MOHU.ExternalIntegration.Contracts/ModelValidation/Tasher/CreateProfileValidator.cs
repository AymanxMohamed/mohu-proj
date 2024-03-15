using FluentValidation;
using Microsoft.Extensions.Localization;
using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using MOHU.ExternalIntegration.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.ModelValidation.Tasher
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileResponse>
    {
        private readonly IStringLocalizer _localizer;

        public CreateProfileValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;


            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FirstnameFieldisRequired])
            .MaximumLength(50).WithMessage(_localizer[ErrorMessageCodes.FirstnameExceedingcharacter])
            .Matches("^[a-zA-Z]*$").WithMessage(_localizer[ErrorMessageCodes.EnglishLettersValidator]);

            RuleFor(x => x.LastName)
          .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.LastNameReuired])
          .MaximumLength(50).WithMessage(_localizer[ErrorMessageCodes.LastNameExceeding])
          .Matches("^[a-zA-Z]*$").WithMessage(_localizer[ErrorMessageCodes.EnglishLettersValidator]);


            RuleFor(x => x.Phone1)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.MobileNumberRequired]).MaximumLength(20)
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage(_localizer[ErrorMessageCodes.MobilePhoneValidator]);

            RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.NationalityisRequired]);


            RuleFor(x => x.IdType)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.IdtypeRequired]);


        }

    }
}
