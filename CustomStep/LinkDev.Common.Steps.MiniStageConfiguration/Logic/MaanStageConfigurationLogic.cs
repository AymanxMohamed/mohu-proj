using LinkDev.MAAN.Common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Activities;
using System.Collections.Generic;
//using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Model
{
    public class MaanStageConfigurationLogic : StepLogic<GetMaanStageConfiguration>
    {
        protected override void ExecuteLogic()
        {
            tracingService.Trace($"  MaanStageConfigurationLogic");
            log.LogInfo($" MaanStageConfigurationLogic");
            #region check if input paramaters are null
            if (!codeActivity.IsContextIsTargetEntity.Get(executionContext) &&  codeActivity.StageId.Get(executionContext)==null  )
                throw new Exception(string.Format($"StageId or StageIdAsString must have value  "));

            else if (codeActivity.IsContextIsTargetEntity.Get(executionContext) &&
                (codeActivity.SchemaNameOfTargetEntityInBPF.Get(executionContext) == null
                || codeActivity.EntityId.Get(executionContext) == null
                || codeActivity.EntitySchemaName.Get(executionContext) == null
                || codeActivity.BPFSchemaName.Get(executionContext) == null
                ))
                throw new Exception(string.Format($"IsContextIsTargetEntity&EntityId&EntitySchemaName&BPFSchemaName&SchemaNameOfTargetEntityInBPF must have value as context is target "));

            #endregion 
            #region map input paramaters 
            //EntityReference processStage = codeActivity.ProcessStage.Get(executionContext);
            string stageId = codeActivity.StageId.Get(executionContext);
            string entityId = codeActivity.EntityId.Get(executionContext);
            string schemaNameOfTargetEntityInBPF = codeActivity.SchemaNameOfTargetEntityInBPF.Get(executionContext);
            string entitySchemaName = codeActivity.EntitySchemaName.Get(executionContext);
            string bPFSchemaName = codeActivity.BPFSchemaName.Get(executionContext);
             
            //string entityReferenceName = codeActivity.EntityReferenceName.Get(executionContext);
            #endregion
            codeActivity.StageConfiguration.Set(executionContext, null);
            #region Logic
            Microsoft.Xrm.Sdk.EntityReference stageConfiguration = null;
            if (!string.IsNullOrEmpty( stageId))
            {
                stageConfiguration = RetriveStageConfigurarionByProcessStage(stageId);
            }
            else
            {
              stageConfiguration = RetriveStageConfigurarionTargetEntity(schemaNameOfTargetEntityInBPF, entityId, entitySchemaName, bPFSchemaName);
            }
            #endregion

            #region map output paramaters 
            codeActivity.StageConfiguration.Set(executionContext, stageConfiguration);

            #endregion
        }

        public EntityReference RetriveStageConfigurarionTargetEntity(string schemaNameOfTargetEntityInBPF, string entityId, string entitySchemaName, string bPFSchemaName)
        {
            try
            {
                log.LogInfo($"  schemaNameOfTargetEntityInBPF {schemaNameOfTargetEntityInBPF}  ");
                log.LogInfo($"  entityId {entityId}  ");
                log.LogInfo($"  entitySchemaName {schemaNameOfTargetEntityInBPF}  ");
                log.LogInfo($"  bPFSchemaName {bPFSchemaName}  ");

                EntityReference stageConfiguration = null;
                var QEldv_bpfQuery = new QueryExpression(bPFSchemaName);
                QEldv_bpfQuery.ColumnSet.AddColumns(  "activestageid");
                QEldv_bpfQuery.Criteria.AddCondition(schemaNameOfTargetEntityInBPF, ConditionOperator.Equal, entityId);
                EntityCollection instance = service.RetrieveMultiple(QEldv_bpfQuery);
                if (!instance.Entities.Any()) return null;

                EntityReference activeStage = instance.Entities[0].GetAttributeValue<EntityReference>("activestageid");

                if (activeStage.Id != null && activeStage.Id !=Guid.Empty)
                {
                    log.LogInfo($"  activeStageId : {activeStage.Id }  ");

                    var query = new QueryExpression("ldv_ministageconfiguration");
                    query.ColumnSet.AddColumns("ldv_ministageconfigurationid", "ldv_stagenameid");
                    query.Criteria.AddCondition("ldv_stagenameid", ConditionOperator.Equal, activeStage.Id  );
                    EntityCollection stageEntity = service.RetrieveMultiple(query);

                    if (!stageEntity.Entities.Any()) return null;
                    
                    log.LogInfo($"  stageEntity {stageEntity.Entities.Count}");
                    stageConfiguration =
                        stageEntity[0].Attributes.Contains("ldv_ministageconfigurationid")
                            ? stageEntity[0].ToEntityReference()
                            : null;
                    tracingService.Trace($"  ldv_ministageconfigurationid {stageConfiguration?.Id}");
                    log.LogInfo($"  ldv_ministageconfigurationid {stageConfiguration?.Id}");
                }
                return stageConfiguration;
            }
            catch (Exception ex)
            {
                tracingService.Trace($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
                log.LogInfo($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
        }
        public EntityReference RetriveStageConfigurarionByProcessStage( string stageId)
        {
            try
            {
                if (stageId == string.Empty) return null;
 
                var query = new QueryExpression("ldv_ministageconfiguration");
                query.ColumnSet.AddColumns("ldv_ministageconfigurationid", "ldv_stagenameid" );
                query.Criteria.AddCondition("ldv_stagenameid", ConditionOperator.Equal, stageId);


                EntityCollection stageEntity = service.RetrieveMultiple(query);

                if (!stageEntity.Entities.Any()) return null;
                EntityReference stageConfiguration =
                    stageEntity[0].Attributes.Contains("ldv_ministageconfigurationid")
                        ? stageEntity[0].ToEntityReference()
                        : null;
                tracingService.Trace($"  ldv_ministageconfigurationid {stageConfiguration?.Id}");
                log.LogInfo($"  ldv_ministageconfigurationid {stageConfiguration?.Id}");

                return stageConfiguration;
            }
            catch (Exception ex)
            {
                // log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
              
                tracingService.Trace($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
                log.LogInfo($"ExecuteLogic hass been finished with Error:'{ex.Message}'");

                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
        }

     
    }

   
}
