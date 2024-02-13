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
    public class LogFields : CodeActivity
    {
        #region "Input Parameters"

        [RequiredArgument]
        [Input("Schema name of Fields To Be Logged (Comma separate)")]
        public InArgument<string> FieldsToBeLogged { get; set; }

        
        [Input("EntityLogicalName")]
        public InArgument<string> EntityLogicalName { get; set; }
        [Input("EntityId")]
        public InArgument<string> EntityId { get; set; }

        [Input("Is Applicant Logged?")]
        [Default("False")]
        public InArgument<bool> IsApplicantLogged { get; set; }

        [Input("ActionOrDecisionType")]
        public InArgument<string> ActionOrDecisionType { get; set; }

        [Input("Is Two option?")]
        [Default("False")]
        public InArgument<bool> IsTwoOption  { get; set; }

        [Input("TwoOptionValue")]
        public InArgument<string> TwoOptionValue { get; set; }

        #endregion
        #region "Output Parameters"
        #endregion

        protected override void Execute(CodeActivityContext context)
        {
            new LogFieldsLogic().Execute(this, context);
        }
    }
}
