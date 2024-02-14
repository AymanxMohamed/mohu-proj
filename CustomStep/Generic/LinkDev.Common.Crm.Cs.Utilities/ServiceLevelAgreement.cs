using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class ServiceLevelAgreement : CustomStepBase
    {
        #region Input Parameters
        [RequiredArgument]
        [Input("Date 1")]
        public InArgument<DateTime> Date1 { get; set; }

        [RequiredArgument]
        [Input("Date 2")]
        public InArgument<DateTime> Date2 { get; set; }

        [RequiredArgument]
        [Input("Calendar")]
        [ReferenceTarget("calendar")]
        public InArgument<EntityReference> Calendar { get; set; }
        #endregion

        #region Output Parameters
        [Output("Working Duration in Days")]
        public OutArgument<int> WorkingDurationInDays { get; set; }


        [Output("Working Duration in Minutes")]
        public OutArgument<int> WorkingDurationInMinutes { get; set; }
        #endregion
        public override void ExtendedExecute()
        {
            WorkingDurationInDays.Set(ExecutionContext, 0);
            WorkingDurationInMinutes.Set(ExecutionContext, 0);

            var bll = new LinkDev.Common.Crm.Utilities.ServiceLevelAgreement(OrganizationService, Calendar.Get<EntityReference>(ExecutionContext));
            var result = bll.CalculateActualWorkingDuration(Date1.Get<DateTime>(ExecutionContext).ToLocalTime(), Date2.Get<DateTime>(ExecutionContext).ToLocalTime());
            if (result != null)
            {
                WorkingDurationInDays.Set(ExecutionContext, result.WorkingDurationInDays);
                WorkingDurationInMinutes.Set(ExecutionContext, result.WorkingDurationInMinutes);
            }
        }
    }
}
