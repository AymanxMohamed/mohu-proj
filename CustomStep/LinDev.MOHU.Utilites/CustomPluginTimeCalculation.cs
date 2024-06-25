using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinDev.MOHU.Utilites.Logic;
//using Linkdev.Maan.Steps.FundraisingCampaign.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace LinDev.MOHU.Utilites
{
    public class CustomPluginTimeCalculation : CodeActivity
    {
        #region "Input Parameters"
        [RequiredArgument]
        [Input("calendarId")]
        public InArgument<string> calendarId { get; set; }

        [RequiredArgument]
        [Input("regardingId")]
        public InArgument<string> regardingId { get; set; }

        [RequiredArgument]
        [Input("slaItemId")]
        public InArgument<string> slaItemId { get; set; }

        [RequiredArgument]
        [Input("entityName")]
        public InArgument<string> entityName { get; set; }

        [RequiredArgument]
        [Input("requestType")]
        public InArgument<string> requestType { get; set; } //requestType=getEndTime :If you don't have a pause or resume scenario, only the WarningTime and FailureTime are to be calculated.

        //[RequiredArgument]
        [Input("previousInstanceId")]
        public InArgument<string> previousInstanceId { get; set; }

        [RequiredArgument]
        [Input("firstInputDuration")]
        public InArgument<int> firstInputDuration { get; set; }//Duration (warning duration or failure duration in minutes).

        [RequiredArgument]
        [Input("secondInputDuration")]
        public InArgument<int> secondInputDuration { get; set; }

        [RequiredArgument]
        [Input("SlaLevel")]
        public InArgument<int> SlaLevel { get; set; }

        [RequiredArgument]
        [Input("firstInputDate")]
        public InArgument<DateTime> firstInputDate { get; set; }

        [RequiredArgument]
        [Input("secondInputDate")]
        public InArgument<DateTime> secondInputDate { get; set; }

        #endregion

        #region "Output Parameters"

        //[RequiredArgument]
        [Output("firstOutputValue")]
        public OutArgument<string> firstOutputValue { get; set; }

        [RequiredArgument]
        [Output("secondOutputValue")]
        public OutArgument<string> secondOutputValue { get; set; }

        [Output("returnCalendarId")]
        public OutArgument<string> returnCalendarId { get; set; }

        #endregion
        protected override void Execute(CodeActivityContext context)
        {
             new CustomPluginTimeCalculationLogic() .Execute(this, context);
        }
    }
}
