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
    public class ChangedStageFields : CodeActivity
    {
        [RequiredArgument]
        [Input("Stages Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        public InArgument<EntityReference> StageConfiguration { get; set; }

        [RequiredArgument]
        [Input("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public InArgument<EntityReference> ApplicationHeader { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            ChangeStageFieldsLogic BL = new ChangeStageFieldsLogic(StageConfiguration, ApplicationHeader);
            BL.ExecuteLogic(context);
        }
    }
    public class ChangeStageFieldsLogic
    {
        public InArgument<EntityReference> StageConfiguration { get; set; }
        public InArgument<EntityReference> ApplicationHeader { get; set; }
        ChangeStageFieldsBLL logicLayer;
        public ChangeStageFieldsLogic(InArgument<EntityReference> stageConfiguration, InArgument<EntityReference> applicationHeader)
        {
            StageConfiguration = stageConfiguration;
            ApplicationHeader = applicationHeader;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
            logicLayer = new ChangeStageFieldsBLL(service , tracingService,executionContext);

            try
            {
                EntityReference stageConfiguration = StageConfiguration.Get(executionContext);
                EntityReference applicationHeader = ApplicationHeader.Get(executionContext);
                if (stageConfiguration != null && applicationHeader != null)
                {
                    logicLayer.FieldsToBeChanged(stageConfiguration, applicationHeader, tracingService);
                }
            }
            catch (Exception e)
            {
                tracingService.Trace($"Error : {e.Message}");
                throw e;
            }
            finally
            {
                tracingService.Trace(" Final ");

                //log.LogExecutionEnd();
            }
        }
    }
}
