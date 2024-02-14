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
    public class FindEntityRefrenceField : CustomStepBase
    {

        #region "Input Parameters"



        [Input("entityLogicalNameToSearchIn")]
        public InArgument<string> entityLogicalNameToSearchIn { get; set; }

        [Input("fieldLogicalNameToSearchOn")]
        public InArgument<string> fieldLogicalNameToSearchOn { get; set; }

        [Input("fieldValue")]
        public InArgument<string> fieldValue { get; set; }

        [Input("searchCriteria")]
        public InArgument<string> searchCriteria { get; set; }

        [Input("entityRefThatWillContainResult")]
        public InArgument<string> entityRefThatWillContainResult { get; set; }
        #endregion

        #region "output Parameters"
        [Output("foundSomething")]
        public OutArgument<bool> foundSomething { get; set; }
        #endregion
        public override void ExtendedExecute()
        {
            foundSomething.Set(ExecutionContext, false);
            var entityLogicalName = string.Empty;
            var fieldLogicalName = string.Empty;
            var searchCriteriaIn = string.Empty;
            EntityReference entityRefResult = new EntityReference(entityRefThatWillContainResult.Get<EntityReference>(ExecutionContext).LogicalName, entityRefThatWillContainResult.Get<EntityReference>(ExecutionContext).Id);

            EntityReference ContextEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);



            if (entityLogicalNameToSearchIn.Get(ExecutionContext) != null)
            {
                entityLogicalName = entityLogicalNameToSearchIn.Get(ExecutionContext);
            }
            if (fieldLogicalNameToSearchOn.Get(ExecutionContext) != null)
            {
                fieldLogicalName = fieldLogicalNameToSearchOn.Get(ExecutionContext);
            }
            if (searchCriteria.Get(ExecutionContext) != null)
            {
                searchCriteriaIn = searchCriteria.Get(ExecutionContext);
            }

            bool Result =
                 Tools.QuickFind(ContextEntity, entityLogicalName, fieldLogicalName, searchCriteriaIn, entityRefResult.ToString(), OrganizationService);
            foundSomething.Set(ExecutionContext, Result);
        }
    }
}
