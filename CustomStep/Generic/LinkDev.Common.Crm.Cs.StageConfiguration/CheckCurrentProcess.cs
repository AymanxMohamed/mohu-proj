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
    public class CheckCurrentProcess : CodeActivity
    {
        [RequiredArgument]
        [Input("InstanceId")]
        public InArgument<string> EntityReferenceId { get; set; }
        [RequiredArgument]
        [Input("InstanceName")]
        public InArgument<string> EntityReferenceName { get; set; }
        [Input("UseService")]
        [Default("False")]
        public InArgument<bool> UseService { get; set; }

        [Input("ServiceID")]
        public InArgument<string> ServiceId { get; set; }
        [Output("IsBpfValid")]
        [Default("False")]
        public OutArgument<bool> IsBpfValid { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            CheckCurrentProcessLogic BL = new CheckCurrentProcessLogic(IsBpfValid,   EntityReferenceId, EntityReferenceName, ServiceId,UseService );
            BL.ExecuteLogic(context);
        }
    }
    public class CheckCurrentProcessLogic
    {
        public InArgument<string> EntityReferenceId { get; set; }
        public InArgument<string> EntityReferenceName { get; set; }
        public InArgument<bool> UseService { get; set; }
        public InArgument<string> ServiceId { get; set; }
        public OutArgument<bool> IsBpfValid { get; set; }
        protected StageConfigurationBLL logicLayer;
        public CheckCurrentProcessLogic(OutArgument<bool> isBpfValid, InArgument<string> entityReferenceId, InArgument<string> entityReferenceName, InArgument<string> serviceId, InArgument<bool> useService)
        {
            IsBpfValid = isBpfValid;
            EntityReferenceId = entityReferenceId;
            EntityReferenceName = entityReferenceName;
            ServiceId = serviceId;
            UseService = useService;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
            logicLayer = new StageConfigurationBLL(service, tracingService, executionContext);
            try
            {
                string entityReferenceId = EntityReferenceId.Get(executionContext);
                string entityReferenceName = EntityReferenceName.Get(executionContext);
                bool useService = UseService.Get(executionContext);
                string serviceId = ServiceId.Get(executionContext);
                tracingService.Trace($"Instance id {entityReferenceId}");
                tracingService.Trace($"Instance Schema Name {entityReferenceName}");

                //tracingService.Trace();

                //IsBpfValid.Set(executionContext, false);
                bool isBpfValid = false;
                if (useService)
                {
                      isBpfValid = logicLayer.IsValidCurrentProcess(entityReferenceId, entityReferenceName,new Guid( serviceId), tracingService);

                }
                else
                  isBpfValid = logicLayer.IsValidCurrentProcess(  entityReferenceId ,   entityReferenceName,Guid.Empty, tracingService);
                IsBpfValid.Set(executionContext, true);

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
