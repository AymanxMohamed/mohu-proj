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
    public class UpdateFieldsAndAssign : CodeActivity
    {
        #region "Input Parameters"
        [Input("Stage Configuration")]
        [RequiredArgument]
        [ReferenceTarget("ldv_ministageconfiguration")]
        public InArgument<EntityReference> StageConfiguration { get; set; }
        [Input("EntityLogicalName")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }
        [Input("EntityId")]
        [RequiredArgument]
        public InArgument<string> EntityId { get; set; }

        [Input("Is Send back ?")]
        [Default("False")]
        public InArgument<bool> IsSendback { get; set; }

        [Input("Is Update Fields? ")]
        [Default("False")]
        public InArgument<bool> IsUpdateFields { get; set; }

        [Input("Is Sendback Far Stage? ")]
        [Default("False")]
        public InArgument<bool> IsSendbackFarStage { get; set; }
        #endregion
        protected override void Execute(CodeActivityContext context)
        {
            new UpdateFieldsAndAssignLogic().Execute(this, context);
        }
    }
}
