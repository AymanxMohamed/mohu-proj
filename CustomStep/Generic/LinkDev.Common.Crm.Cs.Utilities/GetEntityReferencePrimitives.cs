using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Utilities;
using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetEntityReferencePrimitives : CustomStepBase
    {
        #region "Input Parameters"

        [Input("Use the current entity as entity refrence")]
        [Default("true")]
        public InArgument<bool> UseWFContext { get; set; }

        [Input("Lookup logical name (useWFContext is false)")]
        public InArgument<string> EntityRefrenceFieldLogicalName { get; set; }

        #endregion

        #region "Output Parameters"

        [Output("logicalName")]
        public OutArgument<string> EntityLogicalName { get; set; }

        [Output("Id")]
        public new OutArgument<string> EntityId { get; set; }

        #endregion

        public override void ExtendedExecute()
        {

            if (UseWFContext.Get(ExecutionContext))
            {
                EntityLogicalName.Set(ExecutionContext, Context.PrimaryEntityName);
                EntityId.Set(ExecutionContext, Context.PrimaryEntityId.ToString());
            }
            else
            {
                var lookup =
                    CrmStringHandler.SubstituteToAttribute(
                        new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId),
                        EntityRefrenceFieldLogicalName.Get(ExecutionContext),
                        OrganizationService);

                if (lookup != null)
                {
                    if (lookup is EntityReference)
                    {
                        var tmp = lookup as EntityReference;
                        EntityLogicalName.Set(ExecutionContext, tmp.LogicalName);
                        EntityId.Set(ExecutionContext, tmp.Id.ToString());
                    }
                    else
                    {
                        throw new Exception($"Given Lookup logical name is not EntityReference, it's of type {lookup.ToString()}");
                    }
                }

            }
        }
    }
}
