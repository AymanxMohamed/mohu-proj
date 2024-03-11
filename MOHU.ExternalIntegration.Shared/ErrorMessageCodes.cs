using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Shared
{
    public class ErrorMessageCodes
    {

        public static string FieldisRequired = "100";


        public static string EnglishLettersValidator = "4";


        public static string ArabicLettersValidator = "3";

        //EmailValidator

        public static string EmailValidator = "5";

        //field cannot exceed 75 characters // 101 

        public static string fieldcannotexceed75characters = "101";
        // Arabic name must contain only Arabic characters

        public static string ArabicnamemustcontainonlyArabiccharacters = "102";

        // Pleaseenteravalidemailaddress

        public static string Pleaseenteravalidemailaddress = "103";

        // Dateofbirthcannotbeinthefuture

        public static string Dateofbirthcannotbeinthefuture = "104";

        // Invalidphonenumberformat


        public static string Invalidphonenumberformat = "105";




    }
}
