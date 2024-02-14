using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using LinkDev.Common.Crm.Logger;
using LinkDev.CRM.Library.DAL;
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
    public class DeactiveRequestAndFinalizeInstance : CustomStepBase
    {
        [RequiredArgument]
        [Input("DeactiveRequest")]
        public InArgument<string> DeactiveRequest { get; set; }

        [RequiredArgument]
        [Input("FinalizeInstance")]
        public InArgument<string> FinalizeInstance { get; set; }

        [RequiredArgument]
        [Input("RequestSchemaName")]
        public InArgument<string> InstanceSchemaName { get; set; }

        [RequiredArgument]
        [Input("RequestId")]
        public InArgument<string> InstanceId { get; set; }

        public override void ExtendedExecute()
        {
            DeactiveRequestAndFinalizeInstanceLogic BL = new DeactiveRequestAndFinalizeInstanceLogic(DeactiveRequest, FinalizeInstance, InstanceSchemaName, InstanceId,Tracer);
            BL.ExecuteLogic(ExecutionContext);
        }
    }

    public class DeactiveRequestAndFinalizeInstanceLogic
    {
        public InArgument<string> DeactiveRequest { get; set; }
        public InArgument<string> FinalizeInstance { get; set; }
        public InArgument<string> InstanceSchemaName { get; set; }
        public InArgument<string> InstanceId { get; set; }

        protected CRMAccessLayer DAL;
        protected StageConfigurationBLL logicLayer;
        ILogger Tracer { get; set; }
        public DeactiveRequestAndFinalizeInstanceLogic(InArgument<string> deactiveRequest, InArgument<string> finalizeInstance , InArgument<string> instanceSchemaName, InArgument<string> instanceId, ILogger tracer)
        {
            DeactiveRequest = deactiveRequest;
            FinalizeInstance = finalizeInstance;
            InstanceSchemaName = instanceSchemaName;
            InstanceId = instanceId;
            Tracer = tracer;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            DAL = new CRMAccessLayer(service);
            logicLayer = new StageConfigurationBLL(service, tracingService, executionContext);
            try
            {
                string deactiveRequest = DeactiveRequest.Get(executionContext);
                string finalizeInstance = FinalizeInstance.Get(executionContext);
                string  instanceName = InstanceSchemaName.Get(executionContext);
                string instanceId = InstanceId.Get(executionContext);

                
                if (instanceId != string.Empty && instanceName != string.Empty)
                {
                    Tracer.LogComment(this.GetType().FullName, $"instanceId : {instanceId} , instanceName : {instanceName} ", SeverityLevel.Info);

                    if (instanceName == "phonetocaseprocess" || instanceName == "ldv_bpfcasemanagementfeedback")
                    {
                        tracingService.Trace($"instanceName : "+ instanceName) ; 
                        return;
                    }
                    Entity target = DAL.RetrivePrimaryEntityOfBpf(instanceName, new Guid(instanceId));

                    if (target?.Id != Guid.Empty)
                    {
                        Tracer.LogComment(this.GetType().FullName, $"target.Id : {target.Id} , target.LogicalName : {target.LogicalName} ", SeverityLevel.Info);
                        if (deactiveRequest.ToLower() == "yes")
                        {
                            logicLayer.DeactivateRecord(target.LogicalName, target.Id,Tracer);
                        }
                        if (finalizeInstance.ToLower() == "yes")
                        {
                            logicLayer.FinalizeInstance(instanceName, new Guid(instanceId), Tracer);
                        }
                    }
                }                        
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
