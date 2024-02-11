using LinkDev.Common.Steps.MiniStageConfiguration.Logic;
using LinkDev.Common.Steps.MiniStageConfiguration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration
{
    public class GetAssigningUser : CodeActivity
    {
        #region "Input Parameters"
       
        // [RequiredArgument]
        [Input("AssignField")]
        [RequiredArgument]
        public InArgument<string> AssignUserLogicalName { get; set; }

        [Input("EntityLogicalName")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }
        [Input("EntityId")]
        [RequiredArgument]
        public InArgument<string> EntityId { get; set; }
        #endregion

        #region "Output Parameters"
        [Output("Assign User")]
        [ReferenceTarget("systemuser")]
        public OutArgument<EntityReference> AssignUser { get; set; }

        #endregion
        protected override void Execute(CodeActivityContext context)
        {
            new GetAssigningUserLogic().Execute(this, context);

        }
    }
}
