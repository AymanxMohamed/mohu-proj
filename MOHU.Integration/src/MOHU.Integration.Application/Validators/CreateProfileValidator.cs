using MOHU.Integration.Contracts.Dto.CreateProfile;

namespace MOHU.Integration.Application.Validators
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileRequest>
    {

        private readonly IStringLocalizer _localizer;

        public CreateProfileValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;


            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.FirstnameFieldisRequired])
            .MaximumLength(75).WithMessage(_localizer[ErrorMessageCodes.FirstnameExceedingcharacter]);

            RuleFor(x => x.ArabicName)
             .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.ArabicNameisRequired])
             .MaximumLength(150).WithMessage(_localizer[ErrorMessageCodes.ArabicNameExceeding]);

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.EmailRequired])
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage(_localizer[ErrorMessageCodes.EmailValidator]);

            RuleFor(x => x.MobileCountryCode)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.MobileCountryCodeRequired]);


            RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateTime.Today).WithMessage(_localizer[ErrorMessageCodes.DateOfBirth]);

            RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.MobileNumberRequired]).MaximumLength(20)
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage(_localizer[ErrorMessageCodes.MobilePhoneValidator]);


            RuleFor(x => x.Nationality)
          .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.NationalityisRequired]);

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.NationalityisRequired])
                .Must(BeValidGuid).WithMessage(_localizer[ErrorMessageCodes.InvalidNationality]);


            RuleFor(x => x.CountryOfResidence)
            .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.CountryOfResidenceisrequired]);

            RuleFor(x => x.IdType)
           .NotEmpty().WithMessage(_localizer[ErrorMessageCodes.IdtypeRequired]);

        }
        private bool BeValidGuid(Guid nationality)
        {
            return nationality != Guid.Empty;
        }
    }
}
