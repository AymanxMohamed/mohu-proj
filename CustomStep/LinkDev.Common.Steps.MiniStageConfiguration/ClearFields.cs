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
    public class ClearFields : CodeActivity
    {
        #region "Input Parameters"
        [RequiredArgument]
        [Input("Schema name of Fields To Be Cleared (Comma separate)")]
        public InArgument<string> FieldsToBeCleared { get; set; }


        

        [Input("EntityLogicalName")]
        public InArgument<string> EntityLogicalName { get; set; }
        [Input("EntityId")]
        public InArgument<string> EntityId { get; set; }

        #endregion
        #region "Output Parameters"
        #endregion

        protected override void Execute(CodeActivityContext context)
        {
            new ClearFieldsLogic().Execute(this, context);
        }
    }
}
