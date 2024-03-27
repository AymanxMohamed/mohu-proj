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
    public class GetEntityRefrencePrimitivesForActions : CustomStepBase
    {
        #region "Input Parameters"


        [Input("Entity Logical Name")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }

        [Input("Entity Logical Id")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalId { get; set; }

        [Input("Lookup logical name")]
        [RequiredArgument]
        public InArgument<string> EntityRefrenceFieldLogicalName { get; set; }
        #endregion

        #region "Output Parameters"

        [Output("logicalName")]
        public OutArgument<string> OutEntityLogicalName { get; set; }

        [Output("Id")]
        public new OutArgument<string> OutEntityId { get; set; }

        #endregion

        public override void ExtendedExecute()
        {
            var lookup =
                CrmStringHandler.SubstituteToAttribute(
                    new EntityReference(EntityLogicalName.Get(ExecutionContext), new Guid (EntityLogicalId.Get(ExecutionContext))),
                    EntityRefrenceFieldLogicalName.Get(ExecutionContext),
                    OrganizationService);

            if (lookup != null)
            {
                if (lookup is EntityReference)
                {
                    var tmp = lookup as EntityReference;
                    OutEntityLogicalName.Set(ExecutionContext, tmp.LogicalName);
                    OutEntityId.Set(ExecutionContext, tmp.Id.ToString());
                }
                else
                {
                    throw new Exception($"Given Lookup logical name is not EntityReference, it's of type {lookup.ToString()}");
                }
            }
            else
            {
                throw new Exception($"Failed to retrieve'{EntityRefrenceFieldLogicalName.Get(ExecutionContext)}' on entity '{EntityLogicalName.Get(ExecutionContext)}' with id '{EntityLogicalId.Get(ExecutionContext)}'");
            }


        }
    }
}
