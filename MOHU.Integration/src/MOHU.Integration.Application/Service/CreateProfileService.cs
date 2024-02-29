using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.CreateProfile;
using MOHU.Integration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Xml.Linq;
using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Contracts.Dto.CaseTypes;
using Microsoft.Xrm.Sdk.Messages;
using MOHU.Integration.Contracts.Interface.Cache;
using static Azure.Core.HttpHeader;
using System.Collections;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using Azure;
using MOHU.Integration.Infrastructure.Service;
using MOHU.Integration.Shared;
using System.Linq.Expressions;

namespace MOHU.Integration.Application.Service
{

    /// <summary>
    ///  implementation here 
    /// </summary>
    public class CreateProfileService : ICreateProfileService
    {

        private readonly ICrmContext _crmContext;
        

        public CreateProfileService(ICrmContext crmContext)
        {
            _crmContext = crmContext;
            
        }

        public async Task<bool> CreateProfile(CreateProfileResponse model)
        {



            var isExist = await CheckPassportNumberExist(model.PassportNumber);


            var entity = new Entity(Individual.EntityLogicalName);

            if (isExist == false)
            {

                entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
                entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
                entity.Attributes.Add(Individual.Fields.Age, model.Age);
                entity.Attributes.Add(Individual.Fields.ArabicName, model.ArabicName);
                entity.Attributes.Add(Individual.Fields.Email, model.Email);
                entity.Attributes.Add(Individual.Fields.MobileNumber, model.MobileNumber);
                entity.Attributes.Add(Individual.Fields.BirthDate, model.DateOfBirth);
                entity.Attributes.Add(Individual.Fields.HijriBirthDate, model.HijriDateofBirth);
                entity.Attributes.Add(Individual.Fields.MobileCountryCode, model.MobileCountryCode);
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
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);

                }
                else if (model.IdType == IdTypeEnum.Accommodation)
                {
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                }
                else if (model.IdType == IdTypeEnum.Gulfcitizen)
                {
                    entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }
                else if (model.IdType == IdTypeEnum.Passport)
                {
                    entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
                }


                _crmContext.ServiceClient.Create(entity);
                return true;

            }
            return false;

        }
        public async Task<bool> CheckPassportNumberExist(string number)
        {
            var queryContact = new QueryExpression
            {
                EntityName = Individual.EntityLogicalName,
                NoLock = true
            };
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(new ConditionExpression(Individual.Fields.PassportNumber, ConditionOperator.Equal, number));
            queryContact.Criteria.AddFilter(filter);
            var response = _crmContext.ServiceClient.RetrieveMultiple(queryContact).Entities?.FirstOrDefault()?.ToString();
            if (response != null)
            {
                return true;
            }
            return false;
        }



      
       




        








    }








}




