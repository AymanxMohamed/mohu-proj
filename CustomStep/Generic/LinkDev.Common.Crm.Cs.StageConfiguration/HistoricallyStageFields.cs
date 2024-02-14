using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration
{
    public class HistoricallyStageFields : CustomStepBase
    {
        [RequiredArgument]
        [Input("Stages Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        public InArgument<EntityReference> StageConfiguration { get; set; }

        [RequiredArgument]
        [Input("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public InArgument<EntityReference> ApplicationHeader { get; set; }

        public override void ExtendedExecute()
        {
            var bll = new HistoricallyStageFieldsBLL(OrganizationService, Tracer, LanguageCode, tracingService);
            if (StageConfiguration.Get(ExecutionContext) != null && ApplicationHeader.Get(ExecutionContext) != null)
            {
                bll.FieldsToBeHistorically(StageConfiguration.Get(ExecutionContext), ApplicationHeader.Get(ExecutionContext));
            }

        }
    }
}