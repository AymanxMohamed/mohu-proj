using Microsoft.Extensions.Localization;

using MOHU.Integration.Contracts.Enum;
using MOHU.Integration.Shared;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOHU.Integration.Contracts.Dto.CreateProfile
{
    public class CreateProfileRequest

    {
        
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
        
        public string ArabicName { get; set; }

        public string Email { get; set; }   

        public string MobileNumber  { get; set; }

        public string MobileCountryCode { get; set; }

       
        public DateTime DateOfBirth { get; set; }

        public GenderEnum? Gender { get; set; }

        public int? Age { get; set; } = null; 

        public Guid Nationality { get; set; }

        public Guid CountryOfResidence { get; set; } 

        public IdType IdType { get; set; }

        public string IdNumber { get; set; }
 



    }



  

}
