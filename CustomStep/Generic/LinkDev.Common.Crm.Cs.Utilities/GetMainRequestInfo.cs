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
    public class GetMainRequestInfo : CustomStepBase
    {

        #region Input Paremeters
        [Input("Service Definition")]
        [ReferenceTarget("ldv_service")]
        public InArgument<EntityReference> serviceSetting { get; set; }
        #endregion

        #region "Output Parameters"
        [Output("logicalName")]
        public OutArgument<string> EntityLogicalName { get; set; }

        [Output("Id")]
        public new OutArgument<string> EntityId { get; set; }
        #endregion

        public override void ExtendedExecute()
        {
            EntityReference ServiceDefinition = serviceSetting.Get(ExecutionContext);


        }
    }
}
