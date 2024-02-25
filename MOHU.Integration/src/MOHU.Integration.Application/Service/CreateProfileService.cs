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

namespace MOHU.Integration.Application.Service
{

    /// <summary>
    ///  implementation here 
    /// </summary>
    public class CreateProfileService: ICreateProfileService
    {

        private readonly ICrmContext _crmContext;
       
        public CreateProfileService(ICrmContext crmContext)
        {
            _crmContext = crmContext;
        }

        public async Task<bool> CreateProfile(CreateProfileResponse model , string number )
        {

            var IsNumberExist = await CheckNumberExist(number);
            var entity = new Entity(Individual.EntityLogicalName);

            if (IsNumberExist)
            {

              
                entity.Attributes.Add(Individual.Fields.FirstName, model.FirstName);
                entity.Attributes.Add(Individual.Fields.LastName, model.LastName);
                entity.Attributes.Add(Individual.Fields.Age, model.Age);
                entity.Attributes.Add(Individual.Fields.ArabicName, model.ArabicName);
                entity.Attributes.Add(Individual.Fields.Email, model.Email);
                entity.Attributes.Add(Individual.Fields.MobileNumber, model.MobileNumber);
                entity.Attributes.Add(Individual.Fields.BirthDate, model.DateOfBirth);
                entity.Attributes.Add(Individual.Fields.HijriBirthDate, model.HijriDateofBirth);
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

                _crmContext.ServiceClient.CreateAsync(entity); 





            }


            return false ;





        }

      

        public async Task<bool> CheckNumberExist(string Number)
        {

            var result = await _crmContext.ServiceClient.RetrieveMultipleAsync(new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet(
                      Individual.Fields.IDNumber,
                      Individual.Fields.PassportNumber),
                Criteria = new FilterExpression  
                {
                    Filters =
                       {
                         
                          new FilterExpression(LogicalOperator.Or) 
                          {
                            Conditions =
                            {
                                new ConditionExpression(Number,ConditionOperator.Equal,Individual.Fields.IDNumber),
                               new ConditionExpression(Number,ConditionOperator.Equal, Individual.Fields.PassportNumber),
                                
                            }
                          }


                       }


                }
                
            });

            Entity? response = result.Entities.FirstOrDefault();

            return (response != null) ? true : false;

        }






     




    } 
}
