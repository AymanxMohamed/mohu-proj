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

namespace MOHU.ExternalIntegration.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICrmContext _crmContext;

        private readonly IStringLocalizer _localizer;
        private readonly IValidator<CreateProfileRequest> _validator;

        public CustomerService(ICrmContext crmContext,
            IStringLocalizer localizer,
          IValidator<CreateProfileRequest> validator
            )
        {
            _crmContext = crmContext;
            _localizer = localizer;

        }


        public async Task<Guid> CreateProfile(CreateProfileRequest model)
        {

            var results = await _validator.ValidateAsync(model);

            if (results?.IsValid == false)
            {

                throw new BadRequestException(results?.Errors?.FirstOrDefault()?.ErrorMessage);
            }

            var entity = new Entity(Individual.EntityLogicalName);


            var isEmailExist = await IsProfileWithSameEmailExists(model.PrimaryEmail);
            if (isEmailExist == true)
            {

                throw new BadRequestException(_localizer[ErrorMessageCodes.EmailisexistingBefore]);


            }
            var IsMobileExist = await IsProfileWithSameMobileNumberExists(model.Phone1);
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

                if (model.IdType == IdType.NationalIdentity)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        throw new BadRequestException(_localizer[ErrorMessageCodes.NationalIdentityWithidnumber]);
                    }

                    var IsIdnumberExist = await IsProfileWithSameIdNumberIExists(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {

                        throw new BadRequestException(_localizer[ErrorMessageCodes.IdNumberisexistingBefore]);

                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdType.Accommodation)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {

                        throw new BadRequestException(_localizer[ErrorMessageCodes.AccommodationWithIdNumber]);

                    }
                    var IsIdnumberExist = await IsProfileWithSameIdNumberIExists(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {

                        throw new BadRequestException(_localizer[ErrorMessageCodes.IdNumberisexistingBefore]);
                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdType.Gulfcitizen)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        throw new BadRequestException(_localizer[ErrorMessageCodes.GulfcitizenWithIdNumber]);
                    }
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        throw new BadRequestException(_localizer[ErrorMessageCodes.GulfcitizenWithPassportNumber]);
                    }

                    var IsIdnumberExist = await IsProfileWithSameIdNumberIExists(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        throw new BadRequestException(_localizer[ErrorMessageCodes.IdNumberisexistingBefore]);
                    }
                    var IsPassportExsting = await IsProfileWithSamePassportExists(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {

                        throw new BadRequestException(_localizer[ErrorMessageCodes.PassportNumberDuplication]);

                    }

                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }
                else if (model.IdType == IdType.Passport)
                {
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        throw new BadRequestException(_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]);
                    }
                    var IsPassportExsting = await IsProfileWithSamePassportExists(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {

                        throw new BadRequestException(_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]);
                    }
                    else
                    {
                        entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                    }
                }

                var customerId = await _crmContext.ServiceClient.CreateAsync(entity);
                return customerId;
            }


            throw new BadRequestException(_localizer[ErrorMessageCodes.CustomerExist]);

        }

        public async Task<bool> IsProfileWithSameEmailExists(string Email)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.Email, ConditionOperator.Equal, Email));
            queryContact.Criteria.AddFilter(filter);
            var response = (await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsProfileWithSameIdNumberIExists(string IDNumber)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.IDNumber, ConditionOperator.Equal, IDNumber));
            queryContact.Criteria.AddFilter(filter);
            var response = (await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsProfileWithSamePassportExists(string PassportNo)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.PassportNumber, ConditionOperator.Equal, PassportNo));
            queryContact.Criteria.AddFilter(filter);
            var response = (await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsProfileWithSameMobileNumberExists(string MobileNo)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.MobileNumber, ConditionOperator.Equal, MobileNo));
            queryContact.Criteria.AddFilter(filter);
            var response = (await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

    }
}
