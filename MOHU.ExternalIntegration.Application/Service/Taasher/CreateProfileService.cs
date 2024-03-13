using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using MOHU.ExternalIntegration.Contracts.Enum;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using MOHU.ExternalIntegration.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Application.Service.Taasher
{
    public class CreateProfileService : ICreateProfileService
    {
        private readonly ICrmContext crmContext;

        private readonly IStringLocalizer _localizer;
        private readonly ICommonMethod _commonMethod;
        private readonly IValidator<CreateProfileResponse> _validator;

        public CreateProfileService(ICrmContext crmContext,
            IStringLocalizer localizer,
            ICommonMethod commonMethod,
          IValidator<CreateProfileResponse> validator
            )
        {
            this.crmContext = crmContext;
            _localizer = localizer;
            _commonMethod = commonMethod;

        }


        public async Task<Guid> CreateProfile(CreateProfileResponse model)
        {

            var results = await _validator.ValidateAsync(model);


            if (results?.IsValid == false)
            {

                throw new BadRequestException(results.Errors.FirstOrDefault().ErrorMessage);
            }

            var entity = new Entity(Individual.EntityLogicalName);


            var isEmailExist = await _commonMethod.CheckEmailAddressExist(model.PrimaryEmail);
            if (isEmailExist == true)
            {

                throw new BadRequestException(_localizer[ErrorMessageCodes.EmailisexistingBefore]);


            }
            var IsMobileExist = await _commonMethod.CheckMobileNumberDuplication(model.Phone1);
            if (IsMobileExist == true)
            {

                throw new BadRequestException(_localizer[ErrorMessageCodes.PhoneisexistingBefore]);
            }

            if (isEmailExist == false && IsMobileExist == false)
            {
                entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
                entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
                entity.Attributes.Add(Individual.Fields.MobileNumber, model.Phone1);
                entity.Attributes.Add(Individual.Fields.Email, model.PrimaryEmail);
                entity.Attributes.Add(Individual.Fields.TaasherRecID, model.RecID);
                entity.Attributes.Add(Individual.Fields.Origin, model.Origin);
                entity.Attributes.Add(Individual.Fields.Nationality,
              new EntityReference(Individual.EntityLogicalName, model.Nationality));


                entity.Attributes.Add(Individual.Fields.IDType,
                 new OptionSetValue(Convert.ToInt32(model.IdType)));

                if (model.IdType == IdTypeEnum.NationalIdentity)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.NationalIdentityWithidnumber]));
                    }

                    var IsIdnumberExist = await _commonMethod.CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {

                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));

                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Accommodation)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {

                        throw new BadRequestException((_localizer[ErrorMessageCodes.AccommodationWithIdNumber]));

                    }
                    var IsIdnumberExist = await _commonMethod.CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {

                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));
                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Gulfcitizen)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.GulfcitizenWithIdNumber]));
                    }
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.GulfcitizenWithPassportNumber]));
                    }

                    var IsIdnumberExist = await _commonMethod.CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));
                    }
                    var IsPassportExsting = await _commonMethod.CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {

                        throw new BadRequestException((_localizer[ErrorMessageCodes.PassportNumberDuplication]));

                    }

                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }
                else if (model.IdType == IdTypeEnum.Passport)
                {
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]));
                    }
                    var IsPassportExsting = await _commonMethod.CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {

                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]));
                    }
                    else
                    {
                        entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                    }
                }

                var customerId = await crmContext.ServiceClient.CreateAsync(entity);
                return customerId;
            }


            throw new BadRequestException(_localizer[ErrorMessageCodes.CustomerExist]);

        }


    }
}
