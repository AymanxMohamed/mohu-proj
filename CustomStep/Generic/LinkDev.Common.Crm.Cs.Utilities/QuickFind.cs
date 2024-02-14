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
using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Utilities;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class QuickFind : CustomStepBase
    {
        #region "Input Parameters"


        [Input("Use Different Context")]
        [Default("false")]
        public InArgument<bool> useDifferentContext { get; set; }
        [Input("Different Context's Schema Name")]
        public InArgument<string> diffContextSchemaName { get; set; }
        [Input("Different Context's ID")]
        public InArgument<string> diffContextId { get; set; }
        [Input("Entity Logical Name To Search In")]
        public InArgument<string> entityLogicalNameToSearchIn { get; set; }

        [Input("Field Logical Name To Search On")]
        public InArgument<string> fieldLogicalNameToSearchOn { get; set; }


        [Input("Is the searchCriteria a Field Logical Name")]
        [Default("false")]
        public InArgument<bool> isTheSearchCriteriaaFieldLogicalName { get; set; }

        [Input("Search Criteria")]
        public InArgument<string> searchCriteria { get; set; }


        [Input("Field Logical Name That Will Contain Result")]
        public InArgument<string> fieldLogicalNameWillContainResult { get; set; }
        #endregion

        #region "output Parameters"
        [Output("foundSomething")]
        public OutArgument<bool> foundSomething { get; set; }

        #endregion
        public override void ExtendedExecute()
        {
            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"useDifferentContext '{useDifferentContext}', diffContextSchemaName '{diffContextSchemaName}', diffContextId '{diffContextId}', entityLogicalNameToSearchIn '{entityLogicalNameToSearchIn}', fieldLogicalNameToSearchOn '{fieldLogicalNameToSearchOn}', isTheSearchCriteriaaFieldLogicalName '{isTheSearchCriteriaaFieldLogicalName}', searchCriteria '{searchCriteria}', fieldLogicalNameWillContainResult '{fieldLogicalNameWillContainResult}' ", SeverityLevel.Info);

            foundSomething.Set(ExecutionContext, false);
            var entityLogicalName = string.Empty;
            var fieldLogicalName = string.Empty;
            var searchCriteriaIn = string.Empty;
            var DiffContextSchemaName = string.Empty;
            var DiffContextId = string.Empty;
            var resultfieldLogicalName = string.Empty;
            var isTheSearchCriteriaaFieldLogicalNameResult = isTheSearchCriteriaaFieldLogicalName.Get(ExecutionContext);
            var isUseDifferentContext = useDifferentContext.Get(ExecutionContext);
            EntityReference ContextEntity = null;

            if (diffContextSchemaName.Get(ExecutionContext) != null && isUseDifferentContext == true)
            {
                DiffContextSchemaName = diffContextSchemaName.Get(ExecutionContext).Trim();

            }
            else if(isUseDifferentContext == true)
            {
                throw new ArgumentNullException("diffContextSchemaName can't be null when you choose to use different context");
            }
            if (diffContextId.Get(ExecutionContext) != null && isUseDifferentContext == true)
            {
                DiffContextId = diffContextId.Get(ExecutionContext).Trim();
            }
            else if (isUseDifferentContext == true)
            {
                throw new ArgumentNullException("diffContextId can't be null when you choose to use different context");
            }

            if (!isUseDifferentContext)
            {
                 ContextEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            }
            else
            {
                ContextEntity = new EntityReference(DiffContextSchemaName,new Guid(DiffContextId));
            }



            if (entityLogicalNameToSearchIn.Get(ExecutionContext) != null)
            {
                entityLogicalName = entityLogicalNameToSearchIn.Get(ExecutionContext).Trim();
            }
            else
            {
                throw new ArgumentNullException("entityLogicalNameToSearchIn can't be null");
            }

            if (fieldLogicalNameToSearchOn.Get(ExecutionContext) != null)
            {
                fieldLogicalName = fieldLogicalNameToSearchOn.Get(ExecutionContext).Trim();
            }
            else
            {
                throw new ArgumentNullException("fieldLogicalNameToSearchOn can't be null");
            }

            if (searchCriteria.Get(ExecutionContext) != null)
            {
                searchCriteriaIn = searchCriteria.Get(ExecutionContext);
            }
            else
            {
                throw new ArgumentNullException("searchCriteria can't be null");
            }

            if (fieldLogicalNameWillContainResult.Get(ExecutionContext) != null)
            {
                resultfieldLogicalName = fieldLogicalNameWillContainResult.Get(ExecutionContext).Trim();
            }
            else
            {
                throw new ArgumentNullException("entityRefThatWillContainResult can't be null");
            }
            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"entityLogicalName '{entityLogicalName}' fieldLogicalName '{fieldLogicalName}' searchCriteriaIn '{searchCriteriaIn}' resultfieldLogicalName '{resultfieldLogicalName}' ", SeverityLevel.Info);


            bool Result = Tools.QuickFind(ContextEntity, entityLogicalName, fieldLogicalName, searchCriteriaIn, resultfieldLogicalName, OrganizationService, isTheSearchCriteriaaFieldLogicalNameResult);
            foundSomething.Set(ExecutionContext, Result);

            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"Result '{Result}'", SeverityLevel.Info);
        }
    }
}
