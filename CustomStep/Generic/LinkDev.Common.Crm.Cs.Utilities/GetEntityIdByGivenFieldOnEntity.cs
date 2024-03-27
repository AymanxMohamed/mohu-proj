using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Logger;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetEntityIdByGivenFieldOnEntity : CustomStepBase
    {
        #region "Input Parameters"

        [Input("Source logicalname")]
        public InArgument<string> SourceLogicalName { get; set; }

        [Input("Unique Field logicalname")]
        public InArgument<string> SourceUniqueFieldLogicalName { get; set; }

        [Input("Field value")]
        public InArgument<string> FieldValue { get; set; }
        #endregion

        #region "Output Parameters"
        [Output("Source Id")]
        public OutArgument<string> sourceId { get; set; }
        #endregion

        public override void ExtendedExecute()
        {
            // caller entity
            var primaryEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);

            var query = new QueryExpression(SourceLogicalName.Get(ExecutionContext));
            query.Criteria.AddCondition(SourceUniqueFieldLogicalName.Get(ExecutionContext), ConditionOperator.Equal, FieldValue.Get(ExecutionContext));

            var retEntity =
                 OrganizationService.RetrieveMultiple(query);

            if (retEntity != null && retEntity.Entities != null && retEntity.Entities.Count > 0)
                sourceId.Set(ExecutionContext, retEntity.Entities[0].Id.ToString());
            else
            {
                Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"No records found in entity '{SourceLogicalName.Get(ExecutionContext)}' with '{SourceUniqueFieldLogicalName.Get(ExecutionContext)}' equal to '{FieldValue.Get(ExecutionContext)}'", SeverityLevel.Warning);
            }
        }
    }
}
