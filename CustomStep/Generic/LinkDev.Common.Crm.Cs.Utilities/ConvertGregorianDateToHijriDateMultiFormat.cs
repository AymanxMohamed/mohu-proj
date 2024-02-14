using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class ConvertGregorianDateToHijriDateMultiFormat : CustomStepBase
    {
        #region "Input Parameters"
        

        [Input("Greg Date")]
        [RequiredArgument]
        public InArgument<DateTime> gergDate { get; set; }
        
        [Output("Hijri Date with dash")]
        public OutArgument<string> hijriDatewithdash { get; set; }

        [Output("Hijri Date with /")]
        public OutArgument<string> hijriDate { get; set; }
        #endregion


        public override void ExtendedExecute()
        {
            if (gergDate.Get<DateTime>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} are null", "gergDate"));

            var gregorianDate = gergDate.Get<DateTime>(ExecutionContext);
            //var hijriaDateConvert = Tools.ConvertGregDateToHijriDate(gregorianDate.ToString("yyyy/MM/dd"));
            //var hijriaDateConvert_dash = hijriaDateConvert.Replace('/', '-');
            //hijriDate.Set(ExecutionContext, Tools.ConvertGregDateToHijriDate(hijriaDateConvert_dash));
            // hijriDatewithdash.Set(ExecutionContext, Tools.ConvertGregDateToHijriDate(gregorianDate.ToString("dd-mm-yyyy")));
             hijriDate.Set(ExecutionContext, Tools.ConvertGregDateToHijriDate(gregorianDate.ToString("yyyy/MM/dd")));

            hijriDatewithdash.Set(ExecutionContext, Tools.ConvertGregDateToHijriDate(gregorianDate.ToString("yyyy/MM/dd")));
        }
    }
}
