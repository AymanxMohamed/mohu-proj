//using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Workflow;
//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LinkDev.Common.Crm.Cs.StageConfiguration
//{
//     class PerformAction : CodeActivity
//    {
//        //[Input("Process Stage")]
//        //[ReferenceTarget("processstage")]
//        //public InArgument<EntityReference> ProcessStage { get; set; }
//        [Input("EntityReferenceId")]
//        public InArgument<string> EntityReferenceId { get; set; }

//        [Input("EntityReferenceSchemaName")]
//        public InArgument<string> EntityReferenceName { get; set; }


//        protected override void Execute(CodeActivityContext context)
//        {
//            PerformActionLogic BL = new PerformActionLogic(/*ProcessStage,*/ EntityReferenceId, EntityReferenceName);
//            BL.ExecuteLogic(context);
//        }
//    }
//    public class PerformActionLogic
//    {
//       // public InArgument<EntityReference> ProcessStage { get; set; }
//        public InArgument<string> EntityReferenceId { get; set; }
//        public InArgument<string> EntityReferenceName { get; set; }

//        //PerformActionBLL logicLayer;
//        public PerformActionLogic(/*InArgument<EntityReference> processStage,*/ InArgument<string> entityReferenceId, InArgument<string> entityReferenceName)
//        {
//            //ProcessStage = processStage;
//            EntityReferenceId = entityReferenceId;
//            EntityReferenceName = entityReferenceName;
//        }
//        public void ExecuteLogic(CodeActivityContext executionContext)
//        {
//            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
//            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
//            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
//           // logicLayer = new PerformActionBLL(service);
//            try
//            {
//                string entityReferenceId = EntityReferenceId.Get(executionContext);
//                string entityReferenceName = EntityReferenceName.Get(executionContext);
//                OrganizationRequest action = new OrganizationRequest("ldv_StageWorkflowGlobalActionPerformStageConfigurationLogic");
//                action["BpfInstanceId"] = entityReferenceId;
//                action["BpfInstanceLogicalName"] = entityReferenceName;               
//                OrganizationResponse response = service.Execute(action);
                 
//                //bool boolvalue = (bool)response.Results["BoolOutArgument"];
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//            finally
//            {
//                //log.LogExecutionEnd();
//            }

//        }
//    }
//}
