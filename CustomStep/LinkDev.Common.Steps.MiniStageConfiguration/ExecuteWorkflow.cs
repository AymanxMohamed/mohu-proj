using LinkDev.Common.Steps.MiniStageConfiguration.Logic;
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
    public class ExecuteWorkflow : CodeActivity
    {
       
        #region "Input Parameters"
        [RequiredArgument]
        [Input("Ids of Workflows (Comma separate)")]
        public InArgument<string> IdsOfWorkflows { get; set; }


        [Input("EntityLogicalName")]
        public InArgument<string> EntityLogicalName { get; set; }
        [Input("EntityId")]
        public InArgument<string> EntityId { get; set; }

        
        #endregion


    
        #region "Output Parameters"
        #endregion

        protected override void Execute(CodeActivityContext context)
        {
            new ExecuteWorkflowLogic().Execute(this, context);
        }
    }
}
