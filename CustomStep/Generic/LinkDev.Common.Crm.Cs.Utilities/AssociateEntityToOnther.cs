using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class AssociateEntityToOnther : CustomStepBase
    {

        #region "Input Parameters"

        [Input("Use Context To Get Target 1 lookup")]
        [Default("true")]
        public InArgument<bool> target1UserContext { get; set; }

        [Input("Target 1 lookup (if use context is false)")]
        public InArgument<string> target1 { get; set; }

        [Input("Use Context To Get Target 2 lookup")]
        [Default("true")]
        public InArgument<bool> target2UserContext { get; set; }

        [Input("Target 2 lookup (if use context is false)")]
        public InArgument<string> target2 { get; set; }

        [Input("Destination Intersect Entity Name")]
        public InArgument<string> destinationIntersectEntityName { get; set; }

        [Input("Relationship Name")]
        public InArgument<string> relationshipName { get; set; }

        [Input("Clear Any Records In the Relationship While Associating")]
        [Default("false")]
        public InArgument<bool> clearAnyRecordsInDestination { get; set; }

        #endregion

        #region "output Parameters"

        #endregion

        public override void ExtendedExecute()
        {

            var primaryEntityRef = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);

            #region Inputs Validation
            if (!target1UserContext.Get(ExecutionContext) && string.IsNullOrEmpty(target1.Get(ExecutionContext)))
            {
                throw new Exception($"target1 is empty, kindly provide it");
            }

            if (!target2UserContext.Get(ExecutionContext) && string.IsNullOrEmpty(target2.Get(ExecutionContext)))
            {
                throw new Exception($"target2 is empty, kindly provide it");
            }
            #endregion

            var primaryEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            var target1Ref = new EntityReference();
            var target2Ref = new EntityReference();

            if (target1UserContext.Get(ExecutionContext))
            {
                target1Ref = primaryEntity;
            }
            else
            {
                target1Ref =
                    (EntityReference)CrmStringHandler.SubstituteToAttribute(primaryEntity, target1.Get(ExecutionContext).ToString(), OrganizationService);
            }

            if (target2UserContext.Get(ExecutionContext))
            {
                target2Ref = primaryEntity;
            }
            else
            {
                target2Ref =
                    (EntityReference)CrmStringHandler.SubstituteToAttribute(primaryEntity, target2.Get(ExecutionContext).ToString(), OrganizationService);
            }

            var relatedEntities = new EntityReferenceCollection();
            relatedEntities.Add(target2Ref);

            Tools.AssociateEntityToOnther(
                target2Ref.LogicalName,
                relationshipName.Get(ExecutionContext),
                destinationIntersectEntityName.Get(ExecutionContext),
                target1Ref,
                relatedEntities,
                clearAnyRecordsInDestination.Get(ExecutionContext),
                OrganizationService);
        }
    }
}
