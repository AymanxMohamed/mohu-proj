using MOHU.ExternalIntegration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Taasher
{
    public class CreateProfileResponse
    {

        
        public string FirstName { get; set; }


        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string PrimaryEmail { get; set; }

        public string Phone1 { get; set; }

      
       
        public Guid Nationality { get; set; }   


        public string? RecID { get; set; }


        public IdTypeEnum IdType { get; set; } 


        public string IdNumber { get; set; } = string.Empty;  


        public string PassportNumber { get; set; } = string.Empty;

       
        [IgnoreDataMember]
        [EnumDataType(typeof(OriginEnum))]
        public int Origin { get; private set; } = (int)OriginEnum.Taasher;


    }


   

   

   

}
