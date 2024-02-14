using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using Microsoft.Xrm.Sdk.Query;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Utilities;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetAccountEntity : CustomStepBase
    {

        #region "Input Parameters"
        [RequiredArgument]
        [Input("Target Field Schema Name")]       
        public InArgument<string> targetFieldSchemaName { get; set; }

        #endregion

        #region "Output Parameters"
        [Output("Account")]
        [ReferenceTarget("account")]
        public OutArgument<EntityReference> foundSomething { get; set; }
        #endregion

        public override void ExtendedExecute()
        {
            foundSomething.Set(ExecutionContext, null);
            var ContextEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            var result = (EntityReference) CrmStringHandler.SubstituteToAttribute(ContextEntity, $"${targetFieldSchemaName.Get(ExecutionContext)}$", OrganizationService);
            if (result != null)
                foundSomething.Set(ExecutionContext, result);
        }
    }
}
