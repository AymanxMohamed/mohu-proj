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
    public class IncrementDate : CustomStepBase
    {
        #region "Input Parameters"
        [RequiredArgument]
        [Input("Date")]
        public InArgument<DateTime> DateValue { get; set; }

        [Input("Increment Min.")]
        public InArgument<double> IncrementMin { get; set; }

        [Input("Increment Days")]
        public InArgument<double> IncrementDays { get; set; }

        [Input("Increment Years")]
        public InArgument<int> IncrementYears { get; set; }

        [Output("Incremented Date")]
        public OutArgument<DateTime> IncrementedDate { get; set; }
        #endregion
        public override void ExtendedExecute()
        {
            #region check if input paramaters are null
            DateTime dateTime = DateValue.Get<DateTime>(ExecutionContext);
            double? incrementMin=null;
            double? incrementDays = null;
            int? incrementYears = null;

            if (IncrementMin.Get<double>(ExecutionContext) != 0)
                incrementMin = IncrementMin.Get<double>(ExecutionContext);

            if (IncrementDays.Get<double>(ExecutionContext) != 0)
                incrementDays = IncrementDays.Get<double>(ExecutionContext);

            if (IncrementYears.Get<double>(ExecutionContext) != 0)
                incrementYears = IncrementYears.Get<int>(ExecutionContext);
            // DateTime incrementedDate = IncrementedDate.Get<DateTime>(ExecutionContext);

            if (DateValue.Get<DateTime>(ExecutionContext) == null)
                throw new Exception(string.Format($"DateValue is null "));

            if (!incrementMin.HasValue  && !incrementDays.HasValue && !incrementYears.HasValue )
                throw new Exception(string.Format($"Increments is null "));

            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"DateValue {DateValue.Get<DateTime>(ExecutionContext)} ,  IncrementMin : {incrementMin} , IncrementDays : {incrementDays}  , IncrementYears : {incrementYears }"
               , Logger.SeverityLevel.Info);

            DateTime newDateTime = dateTime;
            if (incrementMin.HasValue)
            {
                newDateTime=dateTime.AddMinutes(incrementMin.Value);
            }
            if (incrementDays.HasValue)
            {
                  newDateTime = dateTime.AddDays(incrementDays.Value);
            }
            if (incrementYears.HasValue)
            {
                newDateTime=dateTime.AddYears(incrementYears.Value);
            }

            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"newDateTime {newDateTime}", Logger.SeverityLevel.Info);
            #endregion
            IncrementedDate.Set(ExecutionContext, newDateTime);
        }
    }
}
