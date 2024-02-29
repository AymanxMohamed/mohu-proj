using MOHU.Integration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOHU.Integration.Contracts.Dto.CreateProfile
{
    public class CreateProfileResponse

    {

        [Required]
        [MaxLength(75)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(150)]
        public string ArabicName { get; set; }

        [Required]
        [EmailAddress] 
        [MaxLength(100)]
        public string Email { get; set; }

        

        [Required]
        [MaxLength(20)]
        public string MobileNumber  { get; set; }


        [Required]
        public string MobileCountryCode { get; set; }




        [Required]
        public DateTime DateOfBirth { get; set; }  


        public DateTime HijriDateofBirth { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }


        public int Age { get; set; } 

        [Required]
        public Guid Nationality { get; set; }

        [Required]
        public Guid CountryOfResidence { get; set; }

        [Required]
        public IdTypeEnum IdType { get; set; }


        [Required]
        public string IdNumber { get; set; }


        [Required]
        public string PassportNumber { get; set; }


       

    }



  

}
