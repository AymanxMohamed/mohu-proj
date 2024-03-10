using MOHU.Integration.Contracts.Enum;
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
    public class CreateProfileResponse

    {

        public string FirstName { get; set; }

       
        public string LastName { get; set; }

       
        public string ArabicName { get; set; }

        public string Email { get; set; }

        public string MobileNumber  { get; set; }

        public string MobileCountryCode { get; set; }

       
        public DateTime DateOfBirth { get; set; }

        public GenderEnum? Gender { get; set; }


        public int? Age { get; set; } 

        public Guid Nationality { get; set; }

        public Guid CountryOfResidence { get; set; }

        public IdTypeEnum IdType { get; set; }

        public string IdNumber { get; set; }= string.Empty;

        public string PassportNumber { get; set; } = string.Empty;



    }



  

}
