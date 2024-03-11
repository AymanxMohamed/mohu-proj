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

        #region  original model 

        //[Required]
        //[MaxLength(50)]
        //public string FirstName { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public string LastName { get; set; }

        //[Required]
        //[EmailAddress]
        //[MaxLength(255)]
        //public string PrimaryEmail { get; set; }

        //[Required]
        //[MaxLength(60)]
        //public string Phone1 { get; set; }

        //// PreferredLanguage req to be string but on crm enum 
        ////   public PreferredLanguageEnum PreferredLanguage { get; set; }

        //[Required]
        //public Guid CountryOfResidence { get; set; }

        //[Required]
        //public Guid Nationality { get; set; }// req to be text on portal 

        //public string RecID { get; set; }

        //[Required]
        //public IdTypeEnum IdType { get; set; } // not exist on portal doc T24eer but re on crm 

        //[Required]
        //public string IdNumber { get; set; }// not req on portal doc 

        //[Required]
        //public string PassportNumber { get; set; }

        //// not req on crm 
        //[Required]
        //public DateTime DateOfBirth { get; set; }

        //[IgnoreDataMember]
        //[EnumDataType(typeof(OriginEnum))]
        //public int Origin { get; private set; } = (int)OriginEnum.Taasher;



        #endregion


        #region model with error Msg 

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string PrimaryEmail { get; set; }

        public string Phone1 { get; set; }

        // PreferredLanguage req to be string but on crm enum 
        //   public PreferredLanguageEnum PreferredLanguage { get; set; }

        //[Required]
        //public Guid CountryOfResidence { get; set; }


        [Required]
        public Guid Nationality { get; set; }   // req to be text on portal 

        public string RecID { get; set; }


        public IdTypeEnum IdType { get; set; } // not exist on portal doc T24eer but re on crm 


        public string IdNumber { get; set; } = string.Empty;   // not req on portal doc 


        public string PassportNumber { get; set; } = string.Empty;

        // not req on crm 
        //[Required]
        //public DateTime DateOfBirth { get; set; }

        [IgnoreDataMember]
        [EnumDataType(typeof(OriginEnum))]
        public int Origin { get; private set; } = (int)OriginEnum.Taasher;


        [IgnoreDataMember]
        [JsonIgnore]
        public string ErrorMessage { get; set; } = string.Empty;


        #endregion



    }


    public enum PreferredLanguageEnum
    {

        [Display(Name = "Arabic")]
        Arabic = 1,

        [Display(Name = "English")]
        English = 2,
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
        Passport = 4,
    }

    public enum OriginEnum
    {
        [Display(Name = "Taasher")]
        Taasher = 1

    }

}
