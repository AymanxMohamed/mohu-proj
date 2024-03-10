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

namespace MOHU.Integration.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICrmContext _crmContext;
        private readonly IHttpExceptionService _httpExceptionService;
        private readonly IMessageService _messageService;

        public CustomerService(ICrmContext crmContext , 
            IHttpExceptionService httpExceptionService
            
            )
        {
            _crmContext = crmContext;
            _httpExceptionService = httpExceptionService;
            
        }


      
        #region enhacement code 

        public async Task<Guid> CreateProfile(CreateProfileResponse model)
        {


            var results = await _httpExceptionService.ValidateModelAsync<CreateProfileResponse, CreateProfileValidator>(model);

            if (results?.IsValid == false)
            {
                _httpExceptionService.ThrowBadRequestError(results.ToString(", "));
            }

            var entity = new Entity(Individual.EntityLogicalName);


            var isEmailExist = await CheckEmailAddressExist(model.Email);
            if (isEmailExist == true)
            {
                
                _httpExceptionService.ThrowBadRequestError("This Email is existing Before ");
            }
            var IsMobileExist = await CheckMobileNumberDuplication(model.MobileNumber);
            if (IsMobileExist == true)
            {
               
                _httpExceptionService.ThrowBadRequestError("This Phone is already registered ");

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
                        _httpExceptionService.ThrowBadRequestError("National identity need " +
                            "to add with it id number only with optional passport no.");
                    }

                    var IsIdnumberExist = await CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        _httpExceptionService.ThrowBadRequestError("This Id Number is existing Before ");
                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Accommodation)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                       
                        _httpExceptionService.ThrowBadRequestError("Accommodation need to add with it" +
                            " id number only with optional passport no. ");
                    }
                    var IsIdnumberExist = await CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        _httpExceptionService.ThrowBadRequestError("This Id Number is existing Before ");
                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Gulfcitizen)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {

                        _httpExceptionService.ThrowBadRequestError("IdNumber is Required with IdType of Gulfcitizen "); 
                          
                    }
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        _httpExceptionService.ThrowBadRequestError("PassportNumber is Required with IdType of Gulfcitizen ");
                    }

                    var IsIdnumberExist = await CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        _httpExceptionService.ThrowBadRequestError("This Id Number is existing Before ");
                    }
                    var IsPassportExsting = await CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {
                        _httpExceptionService.ThrowBadRequestError("This Passport Number is existing Before ");
                    }

                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }
                else if (model.IdType == IdTypeEnum.Passport)
                {
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        _httpExceptionService.ThrowBadRequestError("Id Type of passport need to enter with it passport number ");
                    }
                    var IsPassportExsting = await CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {
                        _httpExceptionService.ThrowBadRequestError("This Passport Number is existing Before ");
                    }
                    else
                    {
                        entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                    }
                }

                var customerId = await _crmContext.ServiceClient.CreateAsync(entity);
                return customerId;
            }


            throw new BadRequestException("Customer already exist");

        }


        #endregion

      
        public async Task<bool> CheckEmailAddressExist(string Email)
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
        public async Task<bool> CheckIDNumberIsExsting(string IDNumber)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.IDNumber, ConditionOperator.Equal, IDNumber));
            queryContact.Criteria.AddFilter(filter);
            var response =(await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckPassportNumberIsExsting(string PassportNo)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.PassportNumber, ConditionOperator.Equal, PassportNo));
            queryContact.Criteria.AddFilter(filter);
            var response =(await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckMobileNumberDuplication(string MobileNo)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.MobileNumber, ConditionOperator.Equal, MobileNo));
            queryContact.Criteria.AddFilter(filter);
            var response =(await _crmContext.ServiceClient.RetrieveMultipleAsync(queryContact)).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }









    }
}




