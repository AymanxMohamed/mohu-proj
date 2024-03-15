using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MOHU.Integration.Contracts.Interface.Common;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Shared;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICrmContext _crmContext;
      
        private readonly IStringLocalizer _localizer;
        private readonly IValidator<CreateProfileResponse> _validator;
        private readonly ICommonMethod _commonmethod;

        public CustomerService(ICrmContext crmContext , 
            
            IStringLocalizer localizer,
            ICommonMethod commonmethod ,
            IValidator<CreateProfileResponse> validator)
            
            
        {
            _crmContext = crmContext;
            _localizer = localizer;
            _validator=validator;
            _commonmethod=commonmethod;


        }


      
      
        public async Task<Guid> CreateProfile(CreateProfileResponse model)
        {
          
            

            var results = await _validator.ValidateAsync(model);

            if (results?.IsValid == false )
            {
                throw new BadRequestException(results.Errors.FirstOrDefault().ErrorMessage);
            }
            
            var entity = new Entity(Individual.EntityLogicalName);

            var isEmailExist = await _commonmethod.CheckEmailAddressExist(model.Email);
            if (isEmailExist == true)
            {
               
                throw new BadRequestException(_localizer[ErrorMessageCodes.EmailisexistingBefore]);

            }
            var IsMobileExist = await _commonmethod.CheckMobileNumberDuplication(model.MobileNumber);
            if (IsMobileExist == true)
            {
               
                throw new BadRequestException(_localizer[ErrorMessageCodes.PhoneisexistingBefore]);

            }

            if (isEmailExist == false && IsMobileExist == false)
            {
                entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
                entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
                entity.Attributes.Add(Individual.Fields.Age, model.Age);
                entity.Attributes.Add(Individual.Fields.ArabicName, model.ArabicName);
                entity.Attributes.Add(Individual.Fields.Email, model.Email);
                entity.Attributes.Add(Individual.Fields.MobileCountryCode, model.MobileCountryCode);
                entity.Attributes.Add(Individual.Fields.MobileNumber, model.MobileNumber);
                entity.Attributes.Add(Individual.Fields.BirthDate, model.DateOfBirth);
                entity.Attributes.Add(Individual.Fields.Gender,
                      new OptionSetValue(Convert.ToInt32(model.Gender)));
             
               entity.Attributes.Add(Individual.Fields.Nationality,
              new EntityReference(Individual.EntityLogicalName, model.Nationality));
                entity.Attributes.Add(Individual.Fields.CountryofResidence,
               new EntityReference(Individual.EntityLogicalName, model.CountryOfResidence));

                entity.Attributes.Add(Individual.Fields.IDType,
                 new OptionSetValue(Convert.ToInt32(model.IdType)));

                if (model.IdType == IdTypeEnum.NationalIdentity)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        
                        throw new BadRequestException((_localizer[ErrorMessageCodes.NationalIdentityWithidnumber]));
                       
                    }

                    var IsIdnumberExist = await _commonmethod.CheckIDNumberIsExsting(model.IdNumber);
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
                    var IsIdnumberExist = await _commonmethod.CheckIDNumberIsExsting(model.IdNumber);
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

                    var IsIdnumberExist = await _commonmethod.CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdNumberisexistingBefore]));
                    }
                    var IsPassportExsting = await _commonmethod.CheckPassportNumberIsExsting(model.PassportNumber);
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
                    var IsPassportExsting = await _commonmethod.CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {
                        throw new BadRequestException((_localizer[ErrorMessageCodes.IdtypeWithPassportNumber]));
                      
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


       

      
       
        






        }

        public async Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber)
        {
            var query = new QueryExpression(Contact.EntityLogicalName)
            {
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Contact.Fields.MobilePhone, ConditionOperator.Equal, mobileNumber));
            query.Criteria.AddFilter(filter);

            var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(query);

            if (result.Entities.Any())
                return new LookupDto { Id = result.Entities.FirstOrDefault().Id, EntityLogicalName = result.Entities.FirstOrDefault().LogicalName };

            return null;
        }

        public async Task<LookupDto> CreateIndividualAsync(string mobileNumber)
        {
            var entity = new Entity(Contact.EntityLogicalName);
            entity.Attributes.Add(Contact.Fields.MobilePhone, mobileNumber);
            var individualId = await _crmContext.ServiceClient.CreateAsync(entity);
            return new LookupDto { Id = individualId, EntityLogicalName = entity.LogicalName };
        }
        private async Task<bool> CheckPassportNumberExist(string number)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.PassportNumber, ConditionOperator.Equal, number));
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




