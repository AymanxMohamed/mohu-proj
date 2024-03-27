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
    public class BuildEntityRecordUrl : CustomStepBase
    {
        [RequiredArgument]
        [Input("Entity Logical Name")]
        public InArgument<string> EntityLogicalName { get; set; }

        [RequiredArgument]
        [Input("Entity Id")]
        public InArgument<string> EntityId { get; set; }

        [RequiredArgument]
        [Input("Crm Domain")]
        public InArgument<string> CrmDomain { get; set; }

        [Output("Entity Record Url")]
        public OutArgument<string> EntityRecordUrl { get; set; }

        public override void ExtendedExecute()
        {
            var recordUrl = string.Empty;

            var uriBuilder = new UriBuilder(CrmDomain.Get(ExecutionContext).ToString());
            uriBuilder.Path = "main.aspx";
            uriBuilder.Query = 
                $"etn={EntityLogicalName.Get(ExecutionContext)}&id={new Guid(EntityId.Get(ExecutionContext)).ToString()}&pagetype=entityrecord";

            recordUrl = uriBuilder.Uri.ToString();

            EntityRecordUrl.Set(ExecutionContext, recordUrl);
        }
    }
}
