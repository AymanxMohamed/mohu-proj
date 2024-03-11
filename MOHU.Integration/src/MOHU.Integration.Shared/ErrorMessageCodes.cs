using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOHU.Integration.Shared
{
    public  class ErrorMessageCodes

    {


        public static string FieldValueIsRequired = "30";

        public static string EmailValidator = "5"; // Please enter a valid email address.

     
        public static string FieldIsRequired = "100";

        public static string EnglishLettersValidator = "4";


        public static string ArabicLettersValidator = "3";

        //Exceedingcharacter
        public static string Exceedingcharacter = "101";

        //DateOfBirth
        public static string DateOfBirth = "104";

        //
        public static string MobilePhoneValidator = "2";

        //This Email is existing Before

        public static string EmailisexistingBefore = "105";

        //This PhoneisexistingBefore

        public static string PhoneisexistingBefore = "106";


        public static string NationalIdentityWithidnumber = "107";

        //This IdNumberisexistingBefore

        public static string IdNumberisexistingBefore = "108";

        //AccommodationWithIdNumber

        public static string AccommodationWithIdNumber = "110";

        //GulfcitizenWithIdNumber

        public static string GulfcitizenWithIdNumber = "111";

        //GulfcitizenWithPassportNumber

        public static string GulfcitizenWithPassportNumber = "112";


        //PassportNumberDuplication
        public static string PassportNumberDuplication = "113";

        //IdtypeWithPassportNumber

        public static string IdtypeWithPassportNumber = "114";


        //FirstnameFieldisRequired 

        public static string FirstnameFieldisRequired = "115";


        /// 

        public static string FirstnameExceedingcharacter = "116";

        //LastNameReuired

        public static string LastNameReuired = "117";

        //[individual] LastNameExceeding

        public static string LastNameExceeding = "118";


        //ArabicName

        public static string ArabicNameisRequired  = "119";


        // ArabicName

        public static string ArabicNameExceeding  = "120";

        //EmailRequired

        public static string EmailRequired = "121";

        //EmailExceeding

        public static string EmailExceeding = "122";




    }
}
