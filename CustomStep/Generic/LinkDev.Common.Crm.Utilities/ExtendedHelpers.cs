using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Utilities
{
    public static class ExtendedHelpers
    {
        private readonly static Dictionary<string, string> _stringReplacementDictionary;

        static ExtendedHelpers()
        {
            _stringReplacementDictionary = new Dictionary<string, string>
            {
                {"/","1" },
                {"+","2" },
                {" ","3" }
            };
        }
        public static string CalibrateStringAsToken(this string s) 
        {
            string result = s;

            foreach (var item in _stringReplacementDictionary)
                result = result.Replace(item.Key, item.Value);

            return result;
        }
    }
}
