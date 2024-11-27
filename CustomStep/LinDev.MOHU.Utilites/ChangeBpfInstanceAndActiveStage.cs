//using Microsoft.Xrm.Sdk.Workflow;
//using Microsoft.Xrm.Sdk;
//using System;
//using System.Activities;
//using System.IdentityModel.Metadata;
//using Microsoft.Xrm.Sdk.Query;
//using LinDev.MOHU.Utilites.Model;
//using System.Windows.Documents;
//using System.Collections.Generic;
//using System.Linq;
//namespace LinDev.MOHU.Utilites
//{
//    class ChangeBpfInstanceAndActiveStage : CodeActivity
//    {

//        #region Input
//        [Input("Process")]
//        [ReferenceTarget("workflow")]
//        public InArgument<EntityReference> Process { get; set; }

//        [Input("Process Stage")]
//        [ReferenceTarget("processstage")]
//        public InArgument<EntityReference> ProcessStage { get; set; }


//        [Input("Instance Logical Name")]
//        public InArgument<string> InstanceLogicalName { get; set; }


//        [Input("Target Entity Id")]
//        [RequiredArgument]
//        public InArgument<string> TargetEntityId { get; set; }

//        [Input("Target Entit SchemaName")]
//        [RequiredArgument]
//        public InArgument<string> TargetEntitSchemaName { get; set; }
//        #endregion



//        // Variables to hold services and context
//        protected CodeActivityContext executionContext;
//        protected IWorkflowContext wfContext;
//        protected IOrganizationServiceFactory serviceFactory;
//        protected ITracingService tracingService;
//        protected IOrganizationService service;

//        protected override void Execute(CodeActivityContext executionContext)
//        {
//            this.executionContext = executionContext;
//            // Initialize the tracing service for logging
//            tracingService = executionContext.GetExtension<ITracingService>();
//            tracingService.Trace("Custom workflow activity execution started.");

//            // Initialize the workflow context to access CRM data
//            wfContext = executionContext.GetExtension<IWorkflowContext>();

//            // Initialize the service factory to create an IOrganizationService instance
//            serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();

//            // Create the Organization Service to interact with CRM data
//            service = serviceFactory.CreateOrganizationService(wfContext.UserId);

          

//            EntityReference processStage = ProcessStage.Get(executionContext);
//            EntityReference process = Process.Get(executionContext);
//            EntityReference targetEntity = TargetEntity.Get(executionContext);

//            string instanceLogicalName = InstanceLogicalName.Get(executionContext);

//            ChangeBPFAndProcessStage()
//        }
//        public void ChangeBPFAndProcessStage(string InstanceLogicalName, Guid entityId, string entityLogicalName, EntityReference targetProcess, EntityReference targetActiveStage)
//        {
//            if (entityLogicalName != null && entityLogicalName != string.Empty &&
//                entityId != null && entityId != Guid.Empty &&
//                targetProcess != null && targetProcess?.Id != Guid.Empty &&
//                targetActiveStage != null && targetActiveStage?.Id != Guid.Empty)
//            {
//                tracingService.Trace($"in ChangeBPFAndProcessStage method");

//                #region Query to Retrieve Instance id
//                var requestInstanceQuerey = new QueryExpression(InstanceLogicalName);
//                requestInstanceQuerey.Distinct = true;
//                //requestInstanceQuerey.ColumnSet.AddColumns( "activestageid", "processid", "traversedpath");
//                requestInstanceQuerey.Criteria.AddCondition("bpf_" + entityLogicalName + "id", ConditionOperator.Equal, entityId);
//                requestInstanceQuerey.Criteria.AddCondition("processid", ConditionOperator.Equal, targetProcess.Id);
//                #endregion

//                EntityCollection retrieved = service.RetrieveMultiple(requestInstanceQuerey);

//                if (!retrieved.Entities.Any() || retrieved.Entities[0] == null || retrieved.Entities[0]?.Id == null)
//                {
//                    // throw new InvalidPluginExecutionException(String.Format($"Process '{targetProcess.Id}' not found"));
//                    tracingService.Trace($"Process '{targetProcess.Id}' not found");
//                }
//                else
//                {
//                    tracingService.Trace($"instance logical anme '{retrieved.Entities[0].LogicalName}', Id : '{ retrieved.Entities[0].Id}'");
//                    //// Change the stage
//                    Entity instanceEntity = new Entity(retrieved.Entities[0].LogicalName, retrieved.Entities[0].Id);
//                    instanceEntity.Attributes.Add("activestageid", targetActiveStage);
//                    service.Update(instanceEntity);
//                    tracingService.Trace($"updated Process '{targetProcess.Id}' ");
//                }
//            }
//        }
//    }
//}