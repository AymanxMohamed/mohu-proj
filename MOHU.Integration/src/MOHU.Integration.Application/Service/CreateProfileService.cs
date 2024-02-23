using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Guid> CreateProfile(CreateProfileDto model)
        {

            var entity = new Entity(Individual.EntityLogicalName);

            

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

            //IdNumberOrPassportNumber

            if (model.IdType== (IdTypeEnum)1)
            {
                entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
            }
            else if (model.IdType== (IdTypeEnum)2)
            {
                entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
            }
            else if(model.IdType == (IdTypeEnum)3)
            {
                entity.Attributes.Add(Individual.Fields.IDNumber, model.IdNumber);
                entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
            }
            else if (model.IdType == (IdTypeEnum)4)
            {
                entity.Attributes.Add(Individual.Fields.PassportNumber, model.PassportNumber);
            }



            
          //  _crmContext.ServiceClient.Create(entity);



            //var CaseSetFields = new Entity(CaseManagement.EntityLogicalName);

            //CaseSetFields.Attributes.Add(CaseManagement.Fields.Season,
            //  new OptionSetValue(Convert.ToInt32(model.Season)));

            //CaseSetFields.Attributes.Add(CaseManagement.Fields.Location,
            //  new OptionSetValue(Convert.ToInt32(model.Location)));


            //_crmContext.ServiceClient.Create(CaseSetFields);


            // return ;


            return await Task.FromResult(_crmContext.ServiceClient.Create(entity));

        }

    }
}
