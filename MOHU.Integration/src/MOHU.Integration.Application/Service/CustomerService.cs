using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Application.Exceptions;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Shared;
using FluentValidation;
using MOHU.Integration.Contracts.Dto.Common;
using System.Globalization;

namespace MOHU.Integration.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICrmContext _crmContext;

        private readonly IStringLocalizer _localizer;
        private readonly IValidator<CreateProfileRequest> _validator;

        public CustomerService(ICrmContext crmContext,
            IStringLocalizer localizer,
            IValidator<CreateProfileRequest> validator)
        {
            _crmContext = crmContext;
            _localizer = localizer;
            _validator = validator;


        }

        public async Task<Guid> CreateProfileAsync(CreateProfileRequest model)
        {
            var results = await _validator.ValidateAsync(model);

            if (results?.IsValid == false)
                throw new BadRequestException(results?.Errors?.FirstOrDefault()?.ErrorMessage);


            var entity = new Entity(Individual.EntityLogicalName);

            var profileId = await IsProfileExists($"{model.MobileCountryCode}{model.MobileNumber}",model.Email,model.IdNumber);
            if (profileId is not null)
                return profileId.Value;

            if (model.IdType == IdType.NationalIdentity)
            {
                if (string.IsNullOrEmpty(model.IdNumber))
                    throw new BadRequestException((_localizer[ErrorMessageCodes.NationalIdentityWithidnumber]));

                //var isIdnumberExist = await IsProfileWithSameIdNumberIExists(model.IdNumber);
                //if (isIdnumberExist)
                //    throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));

                entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

            }
            else if (model.IdType == IdType.Accommodation)
            {
                if (string.IsNullOrEmpty(model.IdNumber))
                    throw new BadRequestException((_localizer[ErrorMessageCodes.AccommodationWithIdNumber]));

                //var isIdnumberExist = await IsProfileWithSameIdNumberIExists(model.IdNumber);
                //if (isIdnumberExist)
                //    throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));

                entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
            }
            else if (model.IdType == IdType.Gulfcitizen)
            {
                if (string.IsNullOrEmpty(model.IdNumber))
                    throw new BadRequestException((_localizer[ErrorMessageCodes.GulfcitizenWithIdNumber]));


                if (string.IsNullOrEmpty(model.IdNumber))
                    throw new BadRequestException((_localizer[ErrorMessageCodes.GulfcitizenWithPassportNumber]));

                //var isIdnumberExist = await IsProfileWithSameIdNumberIExists(model.IdNumber);
                //if (isIdnumberExist)
                //    throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));

                entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
            }
            else if (model.IdType == IdType.Passport)
            {
                if (string.IsNullOrEmpty(model.IdNumber))
                    throw new BadRequestException((_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]));


                //var isPassportExsting = await IsProfileWithSamePassportExists(model.IdNumber);
                //if (isPassportExsting)
                //    throw new BadRequestException((_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]));
                //else
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.IdNumber);

            }
            entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
            entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
            
            entity.Attributes.Add(Individual.Fields.ArabicName, model.ArabicName);
            entity.Attributes.Add(Individual.Fields.Email, model.Email);
            entity.Attributes.Add(Individual.Fields.MobileCountryCode, model.MobileCountryCode);
            entity.Attributes.Add(Individual.Fields.MobileNumber, $"{model.MobileCountryCode}{model.MobileNumber}");
            if(model.DateOfBirth.HasValue)
                entity.Attributes.Add(Individual.Fields.BirthDate, model.DateOfBirth);
            if(model.Gender.HasValue)
                entity.Attributes.Add(Individual.Fields.Gender,
                  new OptionSetValue(Convert.ToInt32(model.Gender)));
            if (model.Age.HasValue)
                entity.Attributes.Add(Individual.Fields.Age, model.Age);
            entity.Attributes.Add(Individual.Fields.Nationality,
           new EntityReference(Individual.EntityLogicalName, model.Nationality));
            entity.Attributes.Add(Individual.Fields.CountryofResidence,
           new EntityReference(Individual.EntityLogicalName, model.CountryOfResidence));

            entity.Attributes.Add(Individual.Fields.IDType,
             new OptionSetValue(Convert.ToInt32(model.IdType)));

            if(model.DateOfBirth.HasValue && model.DateOfBirth !=default)
                entity.Attributes.Add(Individual.Fields.HijriBirthDate, GregorianToHijriDateConversion(model.DateOfBirth.Value));

            var customerId = await _crmContext.ServiceClient.CreateAsync(entity);
            return customerId;

            throw new BadRequestException(_localizer[ErrorMessageCodes.CustomerExist]);

        }

        public async Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber)
        {
            var query = new QueryExpression(Contact.EntityLogicalName)
            {
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.Or);
            filter.AddCondition(new ConditionExpression(Contact.Fields.MobilePhone, ConditionOperator.Equal, mobileNumber));
            filter.AddCondition(new ConditionExpression(Contact.Fields.MobilePhone, ConditionOperator.Equal, $"+{mobileNumber}"));
            query.AddOrder(Contact.Fields.CreatedOn, OrderType.Ascending);
            query.Criteria.AddFilter(filter);

            var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(query);

            if (result.Entities.Any())
                return new LookupDto { Id = result.Entities.FirstOrDefault().Id, EntityLogicalName = result.Entities.FirstOrDefault().LogicalName };

            return null;
        }

        public async Task<LookupDto> CreateProfilelAsync(string mobileNumber)
        {
            var entity = new Entity(Contact.EntityLogicalName);
            entity.Attributes.Add(Contact.Fields.MobilePhone, mobileNumber);
            var individualId = await _crmContext.ServiceClient.CreateAsync(entity);
            return new LookupDto { Id = individualId, EntityLogicalName = entity.LogicalName };
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

        private async Task<Guid?> IsProfileExists(string mobileNumber,string email, string? contextId = null)
        {
            var contactQuery = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.Or);
            contactQuery.Criteria.AddFilter(filter);
            filter.AddCondition(new ConditionExpression(Individual.Fields.Email, ConditionOperator.Equal, email));
            if (!string.IsNullOrEmpty(contextId))
            {
                filter.AddCondition(new ConditionExpression(Individual.Fields.PassportNumber, ConditionOperator.Equal, contextId));
                filter.AddCondition(new ConditionExpression(Individual.Fields.IDNumber, ConditionOperator.Equal, contextId));

            }
            var response = await _crmContext.ServiceClient.RetrieveMultipleAsync(contactQuery);
            return response.Entities.Count > 0? response?.Entities?.FirstOrDefault()?.Id : null;
        }
        public static DateTime GregorianToHijriDateConversion(DateTime gregorianDate)
        {
            Calendar umAlQura = new UmAlQuraCalendar();
            var hijriYear = umAlQura.GetYear(gregorianDate);
            var hijriMonth = umAlQura.GetMonth(gregorianDate);
            var hijriDay = umAlQura.GetDayOfMonth(gregorianDate);
            return new DateTime(hijriYear, hijriMonth, hijriDay);
        }
    }
}




