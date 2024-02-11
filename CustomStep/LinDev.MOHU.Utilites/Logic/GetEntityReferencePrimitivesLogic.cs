using Linkdev.Maan.Core.Helper;
using LinkDev.MAAN.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Logic
{
    public class GetEntityReferencePrimitivesLogic : StepLogic<LinDev.MOHU.Utilites.GetEntityReferencePrimitives>
    {
        protected override void ExecuteLogic()
        {
            if (codeActivity.UseWFContext.Get(executionContext))
            {
                codeActivity.EntityLogicalName.Set(executionContext, context.PrimaryEntityName);
                codeActivity.EntityId.Set(executionContext, context.PrimaryEntityId.ToString());
            }
            else
            {
                var lookup =
                    CrmStringHandler.SubstituteToAttribute(
                        new EntityReference(context.PrimaryEntityName, context.PrimaryEntityId),
                        codeActivity.EntityRefrenceFieldLogicalName.Get(executionContext),
                        service);

                if (lookup != null)
                {
                    if (lookup is EntityReference)
                    {
                        var tmp = lookup as EntityReference;
                        codeActivity.EntityLogicalName.Set(executionContext, tmp.LogicalName);
                        codeActivity.EntityId.Set(executionContext, tmp.Id.ToString());
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
