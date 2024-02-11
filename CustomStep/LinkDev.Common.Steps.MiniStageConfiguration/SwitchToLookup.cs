//using LinkDev.Common.Steps.MiniStageConfiguration.Logic;
//using LinkDev.Common.Steps.MiniStageConfiguration.Model;
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
//    public class SwitchToLookup : CodeActivity
//    {
//        #region "Input Parameters"
//        [RequiredArgument]
//        [Input("RecordSchemaName")]
//        public InArgument<string> RecordSchemaName { get; set; }
//        [RequiredArgument]
//        [Input("RecordId")]
//        public InArgument<string> RecordId { get; set; }
//        #endregion

//        #region "Output Parameters"
//        [Output("Incubation Application")]
//        [ReferenceTarget("ldv_incubationapplication")]
//        public OutArgument<EntityReference> IncubationApplication { get; set; }

//        [Output("Allocation Application")]
//        [ReferenceTarget("ldv_allocationapplication")]
//        public OutArgument<EntityReference> AllocationApplication { get; set; }

//        [Output("Social Certification Application")]
//        [ReferenceTarget("ldv_socialcertificationapplication")]
//        public OutArgument<EntityReference> SocialCertificationApplication { get; set; }


//        [Output("User")]
//        [ReferenceTarget("systemuser")]
//        public OutArgument<EntityReference> User { get; set; }

//        #endregion

//        protected override void Execute(CodeActivityContext context)
//        {
//            new SwitchToLookupLogic().Execute(this, context);
//        }
//    }
//}
