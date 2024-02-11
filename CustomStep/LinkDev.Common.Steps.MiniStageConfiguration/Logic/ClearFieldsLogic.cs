using LinkDev.MAAN.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
{
    public class ClearFieldsLogic : StepLogic<ClearFields>
    {
        protected override void ExecuteLogic()
        {
            tracingService.Trace($"  ClearFieldsLogic");
            log.LogInfo($" ClearFieldsLogic");
            string fieldsToBeCleared = codeActivity.FieldsToBeCleared.Get(executionContext);
            tracingService.Trace($"  fieldsToBeCleared {fieldsToBeCleared}");
            log.LogInfo($" fieldsToBeCleared {fieldsToBeCleared}");

            EntityReference entity =   new EntityReference(codeActivity.EntityLogicalName.Get(executionContext), new Guid(codeActivity.EntityId.Get(executionContext)));
             
            Guid id = Guid.Empty;
            string entitySchemaName = string.Empty;
            if (entity!=null&& entity?.Id!=null&& entity?.Id!=Guid.Empty)
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
            ClearFields(targetEntity, fieldsToBeCleared);
        }
        public void ClearFields(EntityReference targetEntity, string FieldsToClear)
        {
            tracingService.Trace($"1 ClearFields function");
            log.LogInfo($"2 ClearFields function");

            log.LogInfo($"  FieldsToClear {FieldsToClear}");

            if (!string.IsNullOrEmpty(FieldsToClear))
            {
                Entity request = new Entity(targetEntity.LogicalName, targetEntity.Id);
                string[] FieldsToClearList = FieldsToClear.Split(',');
                foreach (string dynamicValue in FieldsToClearList)
                {
                    request.Attributes[dynamicValue] = null;
                    tracingService.Trace($"Added Field {dynamicValue} to clear");
                    log.LogInfo($"Added Field {dynamicValue} to clear");
                }
                service.Update(request);
                tracingService.Trace($"request cleared fields successfully");
                log.LogInfo($"request cleared fields successfully");
            }
        }
    }
}
