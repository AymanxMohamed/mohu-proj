using LinkDev.Common.Crm.Cs.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Utilities;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class Update1toMEntitiesWithGivenValue : CustomStepBase
    {
        #region "Input Parameters"
        [RequiredArgument]
        [Default("false")]
        [Input("is global action")]
        public InArgument<bool> IsGlobalAction { get; set; }
        [Input("Context Schema Name (In case IsGlobalAction is true)")]
        public InArgument<string> ContextSchemaName { get; set; }
        [Input("Context Id (In case IsGlobalAction is true)")]
        public InArgument<string> ContextId { get; set; }
        [RequiredArgument]
        [Default("true")]
        [Input("Use Context")]
        public InArgument<bool> UseContext { get; set; }

        [Input("Use another Entity as Context (In case use context is false)")]
        public InArgument<string> UseDifferentContext { get; set; }

        [RequiredArgument]
        [Input("Value to Update Related Entities")]
        public InArgument<string> ValueToUpdateRelatedEntities { get; set; }

        [RequiredArgument]
        [Input("Related Entities Logical Name")]
        public InArgument<string> RelatedEntitiesLogicalName { get; set; }

        [RequiredArgument]
        [Input("Related Entities' lookup To Parent Entity")]
        public InArgument<string> RelatedEntitieslookupToParentEntity { get; set; }

        [RequiredArgument]
        [Input("Field Needed To Be Updated In Related Entities")]
        public InArgument<string> FieldNeededToBeUpdatedInRelatedEntities { get; set; }

        [RequiredArgument]
        [Default("true")]
        [Input("Override If Found a Value")]
        public InArgument<bool> OverrideIfFoundValue { get; set; }

        #endregion
        #region "Output Parameters"
        #endregion
        public override void ExtendedExecute()
        {

            #region check if input paramaters are null
            if (UseDifferentContext.Get<string>(ExecutionContext) == null && UseContext.Get<bool>(ExecutionContext) == false)
                throw new Exception(string.Format("{0} is null Althaugh you choose to use different context", "UseDifferentContext"));

            if (ValueToUpdateRelatedEntities.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} is null", "Value To Update Related Entities"));

            if (RelatedEntitiesLogicalName.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} is null", "Related Entities Logical Name"));

            if (RelatedEntitieslookupToParentEntity.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} is null", "Related Entities' lookup To Parent Entity"));

            if (FieldNeededToBeUpdatedInRelatedEntities.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} is null", "Field Needed To Be Updated In Related Entities"));

            if (ContextSchemaName.Get<string>(ExecutionContext) == null && IsGlobalAction.Get<bool>(ExecutionContext))
                throw new Exception(string.Format("{0} is null", "ContextSchemaName"));

            if (ContextId.Get<string>(ExecutionContext) == null && IsGlobalAction.Get<bool>(ExecutionContext))
                throw new Exception(string.Format("{0} is null", "ContextId"));
            #endregion

            #region map input paramaters to entity references

            bool useCurrentContext = UseContext.Get<bool>(ExecutionContext);

            bool useAsGlobalAction = IsGlobalAction.Get<bool>(ExecutionContext);

            string contextSchemaName = ContextSchemaName.Get<string>(ExecutionContext);
            string contextId = ContextId.Get<string>(ExecutionContext);

            EntityReference PrimaryEntity;
            if (!useAsGlobalAction)
            {
                PrimaryEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            }
            else
            {
                PrimaryEntity = new EntityReference(contextSchemaName,new Guid(contextId));
            }
            EntityReference EntityContext;
            if (useCurrentContext)
            {
                EntityContext = PrimaryEntity;
            }
            else
            {
                EntityContext = (EntityReference)CrmStringHandler.SubstituteToAttribute(PrimaryEntity, UseDifferentContext.Get<string>(ExecutionContext), OrganizationService);
            }

            string valueToUpdateRelatedEntities = ValueToUpdateRelatedEntities.Get<string>(ExecutionContext);
            string relatedEntitiesLogicalName = RelatedEntitiesLogicalName.Get<string>(ExecutionContext);
            string relatedEntitiesLookupToParentEntity = RelatedEntitieslookupToParentEntity.Get<string>(ExecutionContext);
            string fieldNeededToBeUpdatedInRelatedEntities = FieldNeededToBeUpdatedInRelatedEntities.Get<string>(ExecutionContext);
            bool overrideIfFoundValue = OverrideIfFoundValue.Get<bool>(ExecutionContext);

            #endregion

            #region call BLL method Update1toMEntitiesWithGivenValue
 
            Tools.Update1toMEntitiesWithGivenValue(OrganizationService, EntityContext, valueToUpdateRelatedEntities, relatedEntitiesLogicalName, relatedEntitiesLookupToParentEntity, fieldNeededToBeUpdatedInRelatedEntities, overrideIfFoundValue);
            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"EntityContext.LogicalName: '{EntityContext.LogicalName}', EntityContext.Id: '{EntityContext.Id}', valueToUpdateRelatedEntities: '{valueToUpdateRelatedEntities}', relatedEntitiesLogicalName: '{relatedEntitiesLogicalName}', relatedEntitiesLookupToParentEntity: '{relatedEntitiesLookupToParentEntity}', fieldNeededToBeUpdatedInRelatedEntities: '{fieldNeededToBeUpdatedInRelatedEntities}', overrideIfFoundValue: '{overrideIfFoundValue}',", Logger.SeverityLevel.Info);
            #endregion

        }
    }
}
