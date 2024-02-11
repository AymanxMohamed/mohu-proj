using LinkDev.MAAN.Common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
{
    public class ExecuteWorkflowLogic : StepLogic<ExecuteWorkflow>
    {
        protected override void ExecuteLogic()
        {
            tracingService.Trace($"  ExecuteWorkflowLogic");
            log.LogInfo($" ExecuteWorkflowLogic");
            string IdsOfWorkflows = codeActivity.IdsOfWorkflows.Get(executionContext);
            EntityReference entity =   entity = new EntityReference(codeActivity.EntityLogicalName.Get(executionContext),new Guid( codeActivity.EntityId.Get(executionContext)));
            
            Guid id = Guid.Empty;
            string entitySchemaName = string.Empty;
            if (entity != null && entity?.Id != null && entity?.Id != Guid.Empty)
            {
                id = entity.Id;
                entitySchemaName = entity.LogicalName;
            }
            else
            {
                id = context.PrimaryEntityId;
                entitySchemaName = context.PrimaryEntityName;
            }
            tracingService.Trace($"target Entity id : {id} , schemname {entitySchemaName}");
            log.LogInfo($"target Entity id : {id} , schemname {entitySchemaName}");
            EntityReference targetEntity = new EntityReference(entitySchemaName, id);
            ExecutrWorkflow(IdsOfWorkflows, targetEntity);
        }

        public void ExecutrWorkflow(string IdsOfWorkflows, EntityReference targetEntity)
        {
            tracingService.Trace($" ExecutrWorkflow  ");
            log.LogInfo($" ExecutrWorkflow ");
            string[] IdsOfWorkflowsList = IdsOfWorkflows.Split(',');
            log.LogInfo($"{IdsOfWorkflowsList }");
            string columns = string.Empty;

            foreach (string workflowId in IdsOfWorkflowsList) { 
                if (workflowId != null && workflowId != String.Empty)
                {
                    tracingService.Trace($"workflow id:" + workflowId);
                    log.LogInfo($"workflow id:" + workflowId);
                    ExecuteWorkflowRequest request = new ExecuteWorkflowRequest()
                    {
                        WorkflowId = new Guid(workflowId),
                        EntityId = targetEntity.Id,
                    };
                    ExecuteWorkflowResponse response =
                    (ExecuteWorkflowResponse)service.Execute(request);
                }
                else
                {
                    tracingService.Trace($"workflowsid is null  ");
                    log.LogInfo($"workflowsid Number  is null ");
                }
            }
        }
    }
}
