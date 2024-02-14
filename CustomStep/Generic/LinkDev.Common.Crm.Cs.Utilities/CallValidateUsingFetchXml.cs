using LinkDev.Common.Crm.Bll.ValidateUsingFetchXml;
using LinkDev.Common.Crm.Cs.Base;
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
    public class CallValidateUsingFetchXml : CustomStepBase
    {
        [RequiredArgument]
        [Input("Context Id")]
        public InArgument<string> ContextId { get; set; }

        [RequiredArgument]
        [Input("Context Logical Name")]
        public InArgument<string> ContextLogicalName { get; set; }

        [RequiredArgument]
        [Input("Group Alias")]
        public InArgument<string> GroupAlias { get; set; }
        public override void ExtendedExecute()
        {
            var bll = new  ValidateUsingFetchXmlServices(OrganizationService, Tracer, LanguageCode);
            bll.ValidateUsingFetchXml(
                new EntityReference(ContextLogicalName.Get(ExecutionContext), new Guid(ContextId.Get(ExecutionContext))), 
                GroupAlias.Get(ExecutionContext));
        }
    }
}
