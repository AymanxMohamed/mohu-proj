//using LinkDev.MAAN.Common;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
//{
//    public class StageConfigurationOwnerLogic : StepLogic<GetStageConfigurationOwner>
//    {
//        protected override void ExecuteLogic()
//        {
//            tracingService.Trace($"StageConfigurationOwnerLogic");
//            log.LogInfo($"StageConfigurationOwnerLogic");
//            #region check if input paramaters are null
//            if ( codeActivity.ProgramType.Get(executionContext) == null && codeActivity.MaanStageConfiguration.Get(executionContext) == null)
//                throw new Exception(string.Format($"Program Type or Maan Stage Configuration must have value  "));
//            #endregion 
//            #region map input paramaters 
//            EntityReference programTypeRef = codeActivity.ProgramType.Get<EntityReference>(executionContext);
                 
//            EntityReference maanStageConfiguration = codeActivity.MaanStageConfiguration.Get<EntityReference>(executionContext);
//            #endregion
//            codeActivity.StageConfigurationOwner.Set(executionContext, null);
//            #region Logic
//            Microsoft.Xrm.Sdk.EntityReference stageConfigurationOwner = null;
//            if (programTypeRef != null && maanStageConfiguration != null)
//            {
//                stageConfigurationOwner = RetriveStageConfigurarionOwnerEntity(programTypeRef,maanStageConfiguration);
//            }
           
//            #endregion

//            #region map output paramaters 
//            codeActivity.StageConfigurationOwner.Set(executionContext, stageConfigurationOwner);

//            #endregion
//        }

//        public EntityReference RetriveStageConfigurarionOwnerEntity(EntityReference programTypeRef, EntityReference maanStageConfigurationRef)
//        {
//            try
//            {
//                log.LogInfo($"  Program Type Name{programTypeRef.Name}  ");
//                log.LogInfo($"  Program Type ID {programTypeRef.Id}  ");
//                log.LogInfo($"  Maan Stage Configuration Name {maanStageConfigurationRef.Name}  ");
//                log.LogInfo($"  Maan Stage Configuration ID {maanStageConfigurationRef.Id}  ");

//                EntityReference stageConfigurationOwner = null;
//                // Define Condition Values
//                var query_ldv_programtypeid = programTypeRef.Id;
//                var query_ldv_maanstageconfigurationid = maanStageConfigurationRef.Id;

//                // Instantiate QueryExpression query
//                var query = new QueryExpression("ldv_stageconfigurationowner");

//                // Add all columns to query.ColumnSet
//                query.ColumnSet.AllColumns = true;

//                // Define filter query.Criteria
//                query.Criteria.AddCondition("ldv_programtypeid", ConditionOperator.Equal, query_ldv_programtypeid);
//                query.Criteria.AddCondition("ldv_maanstageconfigurationid", ConditionOperator.Equal, query_ldv_maanstageconfigurationid);

//                EntityCollection instance = service.RetrieveMultiple(query);
//                if (!instance.Entities.Any()) return null;

//                log.LogInfo($"  stageConfigurationOwnerEntity Count : {instance.Entities.Count}");



//                stageConfigurationOwner = instance[0].Attributes.Contains("ldv_stageconfigurationownerid") ? instance[0].ToEntityReference(): null;
//                    tracingService.Trace($"  ldv_stageconfigurationownerid {stageConfigurationOwner?.Id}");
//                    log.LogInfo($"  ldv_ministageconfigurationid {stageConfigurationOwner?.Id}");
                
//                return stageConfigurationOwner;
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
//                log.LogInfo($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
//                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
//            }


//        }


//    }
//}
