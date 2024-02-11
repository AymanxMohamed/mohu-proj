//using LinkDev.Common.Steps.MiniStageConfiguration.Logic;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Workflow;
//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LinkDev.Common.Steps.MiniStageConfiguration
//{
//     class GetStageConfigurationOwner : CodeActivity
//    {
//        #region "Input Parameters"
//        [RequiredArgument]
//        [Input("Program Type")]
//        [ReferenceTarget("ldv_programtype")]
//        public InArgument<EntityReference> ProgramType { get; set; }

//        [RequiredArgument]
//        [Input("Maan Stage Configuration")]
//        [ReferenceTarget("ldv_ministageconfiguration")]
//        public InArgument<EntityReference> MaanStageConfiguration { get; set; }
     
//        #endregion
//        #region "Output Parameters"

//        [Output("Stage Configuration Owner")]
//        [ReferenceTarget("ldv_stageconfigurationowner")]
//        public OutArgument<EntityReference> StageConfigurationOwner { get; set; }

//        #endregion

//        protected override void Execute(CodeActivityContext context)
//        {
//            new StageConfigurationOwnerLogic().Execute(this, context);
//        }
//    }
//}
