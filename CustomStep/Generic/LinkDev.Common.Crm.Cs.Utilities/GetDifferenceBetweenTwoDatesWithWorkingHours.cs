using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Cs.Utilities.Utilities;
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
    public class GetDifferenceBetweenTwoDatesWithWorkingHours : CustomStepBase
    {
        [Input("Start Date")]
        [RequiredArgument]
        public InArgument<DateTime> StartDateValue { get; set; }

        [Input("End Date")]
        [RequiredArgument]
        public InArgument<DateTime> EndDateValue { get; set; }

        [Input("SLA Entity")]
        [RequiredArgument]
        [AttributeTarget("sla", "objecttypecode")]
        public InArgument<OptionSetValue> SLAEntity { get; set; }

        [Output("Difference Between Dates in Minutes")]
        [RequiredArgument]
        public OutArgument<double> Difference { get; set; }

        public override void ExtendedExecute()
        {
            GetDifferenceBetweenTwoDatesWithWorkingHoursBLL getDifferenceBetweenTwoDatesWithWorkingHoursBLL = new GetDifferenceBetweenTwoDatesWithWorkingHoursBLL(OrganizationService);
            DateTime StartDate = StartDateValue.Get<DateTime>(ExecutionContext);
            DateTime EndDate = EndDateValue.Get<DateTime>(ExecutionContext);
            int SLAEntityValue = SLAEntity.Get(ExecutionContext).Value;
            double WorkingHoursPerDay;
            double DifferenceBetweenDates = getDifferenceBetweenTwoDatesWithWorkingHoursBLL.GetDifferendeBetweenTwoDatesInWorkingHours(StartDate, EndDate, SLAEntityValue, out WorkingHoursPerDay);
            if (DifferenceBetweenDates > WorkingHoursPerDay)
            {
                DifferenceBetweenDates = (DifferenceBetweenDates / WorkingHoursPerDay) * 24 * 60;
            }
            else
            {
                DifferenceBetweenDates = DifferenceBetweenDates * 60;
            }
            if (DifferenceBetweenDates < 0)
            {
                DifferenceBetweenDates = 0;
            }
            Difference.Set(ExecutionContext, DifferenceBetweenDates);

        }
    }
}
