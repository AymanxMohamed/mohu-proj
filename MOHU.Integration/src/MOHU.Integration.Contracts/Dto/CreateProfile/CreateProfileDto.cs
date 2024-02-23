using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOHU.Integration.Contracts.Dto.CreateProfile
{
    public class CreateProfileDto
    {
        //[Required]
        //[StringLength(150, ErrorMessage = "The {0} field must be a string with a maximum length of {150}.")]
        //public string EnglishName { get; set; } = string.Empty; // not exist on crm 


      //  [Required]
       // [StringLength(150, ErrorMessage = "The {0} field must be a string with a maximum length of {150}.")]
        public string ArabicName { get; set; } 

      //  [Required]
       // [MaxLength(100)]
      //  [EmailAddress(ErrorMessage = "The {0} field must be an email address with a maximum length of {100}.")]
        public string Email { get; set; } 

        //[Required]
        //public string MobileCountryCode { get; set; } = string.Empty; /// not exist on crm 

       // [Required]
      //  [StringLength(20, ErrorMessage = "The {0} field must be a Mobile Number with a maximum length of {20} Number by Max.")]
        public string MobileNumber  { get; set; }


      //  [Required]
        public string DateOfBirth { get; set; }  ///


        public string HijriDateofBirth { get; set; }


        public GenderEnum? Gender { get; set; } 

        //public int Age { get; set; } // not exisy on portal 

        public Guid Nationality { get; set; }

      //  [Required]
        public Guid CountryOfResidence { get; set; }

      //  [Required]
        public IdTypeEnum IdType { get; set; }

        // [Required]
        /*public string IdNumberOrPassportNumber { get; set; } = string.Empty;*/ // n the write way of the property 
        public string? IdNumber { get; set; } 

        //PassportNumber
        public string? PassportNumber { get; set; }


        //public SeasonEnum Season { get; set; }   ///optional not exist on crm 
        //public LocationEnum Location { get; set; } // optional not exist on crm 

    }



    public enum GenderEnum
    {
        [Display(Name = "Male")]
        Male = 1,

        [Display(Name = "Female")]
        Female = 2,
    }

    
   
    public enum IdTypeEnum
    {
        [Display(Name = "National Identity")]
        NationalIdentity = 1,
        [Display(Name = "Accommodation")]
        Accommodation = 2,
        [Display(Name = "Gulf citizen")]
        Gulfcitizen = 3,
        [Display(Name = "Passport (in the case of a non-citizen or resident)")]
        Passport = 4 ,
    }

    public enum SeasonEnum
    {
        [Display(Name = "Hajj")]
        Hajj = 1,

        [Display(Name = "Umrah")]
        Umrah = 2,
    }
    public enum LocationEnum
    {
        [Display(Name = "Mekkah")]
        Hajj = 1,
        [Display(Name = "Madina")]
        Umrah = 2,
        [Display(Name = "Border Crossings")]
        BorderCrossings = 3,
        [Display(Name = "Arrafat")]
        Arrafat = 4,
        [Display(Name = "Muzdalifa")]
        Muzdalifa = 5,
        [Display(Name = "Mina")]
        Mina = 6,
        [Display(Name = "Jada")]
        Jada = 7,
    }









}
