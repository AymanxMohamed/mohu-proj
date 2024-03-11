using Microsoft.Extensions.Localization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
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
        private readonly IHttpExceptionService _httpExceptionService;
        private readonly IStringLocalizer _stringLocalizer;

        public CreateProfileService(ICrmContext crmContext,
            IHttpExceptionService httpExceptionService, IStringLocalizer stringLocalizer)
        {
            this.crmContext = crmContext;
            _httpExceptionService = httpExceptionService;

            _stringLocalizer = stringLocalizer;
        }




        #region Validation msg 

        public async Task<Guid> CreateProfile(CreateProfileResponse model)
        {


            var results = await _httpExceptionService.ValidateModelAsync<CreateProfileResponse, CreateProfileValidator>(model);

            if (results?.IsValid == false)
            {
                _httpExceptionService.ThrowBadRequestError(results.ToString(", "));
            }

            var entity = new Entity(Individual.EntityLogicalName);


            var isEmailExist = await CheckEmailAddressExist(model.PrimaryEmail);
            if (isEmailExist == true)
            {
                //model.ErrorMessage = "This Email is existing Before.";
                //throw new BadRequestException(model.ErrorMessage);

                //  _httpExceptionService.ThrowBadRequestError("This Email is existing Before");

                _httpExceptionService.ThrowBadRequestError(_stringLocalizer["EmailMsg"]);


            }
            var IsMobileExist = await CheckMobileNumberDuplication(model.Phone1);
            if (IsMobileExist == true)
            {
                model.ErrorMessage = "This Phone is already registered.";
                throw new BadRequestException(model.ErrorMessage);
            }

            if (isEmailExist == false && IsMobileExist == false)
            {
                entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
                entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
                entity.Attributes.Add(Individual.Fields.MobileNumber, model.Phone1);


                entity.Attributes.Add(Individual.Fields.Email, model.PrimaryEmail);
                // entity.Attributes.Add(Individual.Fields.PreferredLanguage,
                //new OptionSetValue(Convert.ToInt32(model.PreferredLanguage)));
                entity.Attributes.Add(Individual.Fields.TaasherRecID, model.RecID);
                entity.Attributes.Add(Individual.Fields.Origin, model.Origin);
                entity.Attributes.Add(Individual.Fields.Nationality,
              new EntityReference(Individual.EntityLogicalName, model.Nationality));

                // not required on rsd 
                // entity.Attributes.Add(Individual.Fields.CountryofResidence,
                //new EntityReference(Individual.EntityLogicalName, model.CountryOfResidence));

                entity.Attributes.Add(Individual.Fields.IDType,
                 new OptionSetValue(Convert.ToInt32(model.IdType)));

                if (model.IdType == IdTypeEnum.NationalIdentity)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        model.ErrorMessage = "National identity need to add with it id number only with optional passport no. ";
                        throw new BadRequestException(model.ErrorMessage);
                    }

                    var IsIdnumberExist = await CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        model.ErrorMessage = "This Id Number is existing Before.";
                        throw new BadRequestException(model.ErrorMessage);
                    }
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Accommodation)
                {
                    if (string.IsNullOrEmpty(model.IdNumber))
                    {
                        model.ErrorMessage = "Accommodation need to add with it id number only with optional passport no. ";
                        throw new BadRequestException(model.ErrorMessage);
                    }
                    var IsIdnumberExist = await CheckIDNumberIsExsting(model.IdNumber);
                    if (IsIdnumberExist == true)
                    {
                        model.ErrorMessage = "This Id Number is existing Before.";
                        throw new BadRequestException(model.ErrorMessage);
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
                        model.ErrorMessage = "This Id Number is existing Before.";
                        throw new BadRequestException(model.ErrorMessage);
                    }
                    var IsPassportExsting = await CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {
                        model.ErrorMessage = "This Passport Number is existing Before.";
                        throw new BadRequestException(model.ErrorMessage);
                    }

                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }
                else if (model.IdType == IdTypeEnum.Passport)
                {
                    if (string.IsNullOrEmpty(model.PassportNumber))
                    {
                        model.ErrorMessage = "Id Type of passport need to enter with it passport number.";
                        throw new BadRequestException(model.ErrorMessage);
                    }
                    var IsPassportExsting = await CheckPassportNumberIsExsting(model.PassportNumber);
                    if (IsPassportExsting == true)
                    {
                        model.ErrorMessage = "This Passport Number is existing Before.";
                        throw new BadRequestException(model.ErrorMessage);
                    }
                    else
                    {
                        entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                    }
                }

                var customerId = await crmContext.ServiceClient.CreateAsync(entity);
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
            var response = crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
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
            var response = crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
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
            var response = crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
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
            var response = crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }


    }
}
