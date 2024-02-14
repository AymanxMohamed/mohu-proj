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
    public class SubstituteUsingCrmStringHandler : CustomStepBase
    {
        #region Input Parameters
        [RequiredArgument]
        [Input("String To Evaluate")]
        public InArgument<string> StringToEvaluate { get; set; }

        [Output("Evaluated String")]
        public OutArgument<string> EvaluatedString { get; set; }
        #endregion
        public override void ExtendedExecute()
        {
            

            var result = Crm.Utilities.CrmStringHandler.Substitute(new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId),StringToEvaluate.Get<string>(ExecutionContext), OrganizationService);
            EvaluatedString.Set(ExecutionContext, result);
        }
    }
}
