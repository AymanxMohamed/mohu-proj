using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using LinkDev.Common.Crm.Bll.ChangeBpfInstanceStage;
using LinkDev.Common.Crm.Cs.Base;

namespace LinkDev.Common.Crm.Cs.ChangeBpfInstanceStage
{
    public class ChangeBpfInstanceStageGivenPrimaryEntity : CustomStepBase
    {

        #region Input Paremeters


        [Input("Primary Entity Logical Name")]
        [RequiredArgument]
        public InArgument<string> PrimaryEntityLogicalName { get; set; }

        [Input("Primary Entity ID")]
        [RequiredArgument]
        public InArgument<string> PrimaryEntityId { get; set; }

        [RequiredArgument]
        [Input("Move To Next Stage?")]
        public InArgument<bool> MoveToNextStage { get; set; }

        [RequiredArgument]
        [Input("Back To Previous Stage?")]
        public InArgument<bool> BackToPreviousStage { get; set; }

        [RequiredArgument]
        [Input("Move To specific Stage?")]
        public InArgument<bool> MoveToSpecificStage { get; set; }

        [Input("Process Stage")]
        [ReferenceTarget("processstage")]
        public InArgument<EntityReference> ProcessStage { get; set; }


        #endregion


        public override void ExtendedExecute()
        {
            bool moveToNextStage = MoveToNextStage.Get(ExecutionContext);
            bool backToPreviousStage = BackToPreviousStage.Get(ExecutionContext);
            bool moveToSpecificStage = MoveToSpecificStage.Get(ExecutionContext);
            string PrimaryLogicalName = PrimaryEntityLogicalName.Get(ExecutionContext);
            string PrimaryId = PrimaryEntityId.Get(ExecutionContext);
            EntityReference processStage = ProcessStage.Get(ExecutionContext);

            var ChangeBpfInstanceBll = new ChangeBpfInstanceStageBll(OrganizationService, Tracer, LanguageCode);//, CrmLog);
            if ((moveToNextStage == true && backToPreviousStage == true)
                || (moveToNextStage == true && moveToSpecificStage == true)
                || (backToPreviousStage == true && moveToSpecificStage == true))
            {
                throw new InvalidPluginExecutionException("You choose two options for moving stage , please choose only one option");
            }
            else if (moveToNextStage == false && backToPreviousStage == false && moveToSpecificStage == false)
            {
                throw new InvalidPluginExecutionException("You Must Choose One Option For Moving Stage");
            }
            else
            {


                ChangeBpfInstanceBll.ChangeBPFProcessStage(new Guid(PrimaryId), PrimaryLogicalName, moveToNextStage, backToPreviousStage, moveToSpecificStage, processStage);


            }
        }
    }
}