using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class ExtractTextFromHtml : CustomStepBase
    {
        [Input("Html Message")]
        public InArgument<string> HtmlMessage { get; set; }
        [Output("Plain Message")]
        [Default("No Email Body")]
        public OutArgument<string> PlainMessage { get; set; }

        public override void ExtendedExecute()
        {
            string HtmlMessageString = HtmlMessage.Get<string>(ExecutionContext);
            PlainMessage.Set(ExecutionContext, "No Email Body");

            if (HtmlMessageString != null && HtmlMessageString != string.Empty)
            {
                string PlainMessageString = System.Text.RegularExpressions.Regex.Replace(HtmlMessageString, "<[^>]*>", string.Empty);
                
                PlainMessageString.Replace("&amp;", "&")
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .Replace("&quot;", "\"")
                    .Replace("&nbsp;", " ")
                    .Replace("&apos;", "'");

                PlainMessageString = PlainMessageString.Trim();

                if (PlainMessageString.Length <= 1048576)
                    PlainMessage.Set(ExecutionContext,
                        PlainMessageString);
                else
                    PlainMessage.Set(ExecutionContext,
                        PlainMessageString.Substring(0, 1048575));
            }
        }

    }
}
