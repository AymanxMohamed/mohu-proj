using LinkDev.Common.Steps.MiniStageConfiguration.Model;
using LinkDev.MAAN.Common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
{
    public class UpdateFieldsAndAssignLogic : StepLogic<UpdateFieldsAndAssign>
    {
        public MOHUStageConfiguration maanStageConfiguration = new MOHUStageConfiguration();
        public AssignType AssignType = new AssignType();
        protected override void ExecuteLogic()
        {
            tracingService.Trace($"UpdateFieldsAndAssignLogic");
            log.LogInfo($"UpdateFieldsAndAssignLogic");

            #region map input paramaters 
            EntityReference stageConfiguration = codeActivity.StageConfiguration.Get(executionContext);
            string entityLogicalName = codeActivity.EntityLogicalName.Get(executionContext);
            string entityId = codeActivity.EntityId.Get(executionContext);
            bool isSendback = codeActivity.IsSendback.Get(executionContext);
            bool isUpdateFields = codeActivity.IsUpdateFields.Get(executionContext);
            bool isSendbackFarStage = codeActivity.IsSendbackFarStage.Get(executionContext);

            #endregion

            #region Logic
            log.LogInfo($"stageConfiguration.Id {stageConfiguration.Id}");

            Entity stageConfigurationEnity = GetStageConfigurationFields(stageConfiguration.Id);
            log.LogInfo($"isUpdateFields {isUpdateFields}");
            log.LogInfo($"stageConfigurationEnity {stageConfigurationEnity}");
            log.LogInfo($"entityLogicalName {entityLogicalName}");
            log.LogInfo($"entityId {entityId}");
       
            if (isSendback)
            {
                tracingService.Trace($"isSendback");
                log.LogInfo($"isSendback");

                //update Fields
               // if (isUpdateFields)
                {
                log.LogInfo($"isUpdateFields {isUpdateFields}");

                    if (isSendbackFarStage)
                    {
                        log.LogInfo($"isSendbackFarStage {isSendbackFarStage}");

                        UpdateFieldsInTarget(  stageConfigurationEnity, entityLogicalName, new Guid(entityId),
                            maanStageConfiguration.SendbackFarStatus,
                            maanStageConfiguration.SendbackFarStatusReason,
                            maanStageConfiguration.SendbackFarPortalStatus);

                    }
                    else
                    {
                        UpdateFieldsInTarget(stageConfigurationEnity, entityLogicalName, new Guid(entityId), 
                            maanStageConfiguration.SendbackStatus,
                            maanStageConfiguration.SendbackStatusReason, 
                            maanStageConfiguration.SendbackPortalStatus);

                    }

                }
                    UpdateCurrentEntityWithCurrentStage(stageConfigurationEnity, entityLogicalName, new Guid(entityId), maanStageConfiguration.SendbackUser, maanStageConfiguration.SendbackTeam, maanStageConfiguration.SendbackAssigningFieldLogicalName, maanStageConfiguration.SendbackQueue, maanStageConfiguration.SendbackAssigningType);
                
            }
            else
            {
                tracingService.Trace($"Next");
                log.LogInfo($"Next");
                
             //   if (isUpdateFields)
                {
                    //update Fields
                    UpdateFieldsInTarget(  stageConfigurationEnity, entityLogicalName, new Guid(entityId), 
                        maanStageConfiguration.Status,
                        maanStageConfiguration.StatusReason,
                        maanStageConfiguration.PortalStatus);
                }

                UpdateCurrentEntityWithCurrentStage(stageConfigurationEnity, entityLogicalName, new Guid(entityId),
                    maanStageConfiguration.User,
                    maanStageConfiguration.Team, 
                    maanStageConfiguration.AssigningFieldLogicalName,
                    maanStageConfiguration.Queue, 
                    maanStageConfiguration.AssigningType);
                
            }
            #endregion
        }
        void UpdateCurrentEntityWithCurrentStage(Entity stageConfigurationEnity, string entityLogicalName, Guid entityId,string userSchemaName ,string teamSchemaName  ,string assigningFieldSchemaName,string queueSchemaName,string assignTypeSchemaName)
        {
            log.LogInfo($"UpdateCurrentEntityWithCurrentStage  ");

            EntityReference targetAssigningOwning = null;
            //Assign Record
            if (stageConfigurationEnity.Contains(assignTypeSchemaName))
            {
                tracingService.Trace($"assignTypeSchemaName : {assignTypeSchemaName}");
                log.LogInfo($"assignTypeSchemaName : {assignTypeSchemaName}");
                int assignType = stageConfigurationEnity.GetAttributeValue<OptionSetValue>(assignTypeSchemaName).Value;
                if (assignType == (int)AssignType.User)
                {
                    tracingService.Trace($"User");
                    log.LogInfo($"User");
                    EntityReference user = stageConfigurationEnity.Contains(userSchemaName) ? stageConfigurationEnity.GetAttributeValue<EntityReference>(userSchemaName) : null;
                    targetAssigningOwning = user;
                    tracingService.Trace($"User id {user.Id}");
                    log.LogInfo($"User id {user.Id}");
                }
                else if (assignType == (int)AssignType.Team)
                {
                    EntityReference team = stageConfigurationEnity.Contains(teamSchemaName) ? stageConfigurationEnity.GetAttributeValue<EntityReference>(teamSchemaName) : null;
                    targetAssigningOwning = team;
                    tracingService.Trace($"team id {team.Id}");
                    log.LogInfo($"team id {team.Id}");
                }
                else if(assignType == (int)AssignType.Field)
                {
                    tracingService.Trace($"AssignType.Field");
                    log.LogInfo($"AssignType.Field");
                    string assigningFieldLogicalName = stageConfigurationEnity.Contains(assigningFieldSchemaName) ? stageConfigurationEnity.GetAttributeValue<string>(assigningFieldSchemaName) : null;
                    if (!string.IsNullOrEmpty(assigningFieldLogicalName))
                    {
                        tracingService.Trace($"AssignType {assigningFieldLogicalName}");
                        log.LogInfo($"AssignType  {assigningFieldLogicalName}");
                        EntityReference user = GetAssignUser(assigningFieldLogicalName, entityLogicalName, entityId);
                        targetAssigningOwning = user;
                        tracingService.Trace($"User id {user.Id}");
                        log.LogInfo($"User id {user.Id}");
                    }
                }
                log.LogInfo($"queueSchemaName : {queueSchemaName}");

                if (stageConfigurationEnity.Contains(queueSchemaName) )
                {
                    log.LogInfo($"Contains queueSchemaName: {queueSchemaName}");

                    EntityReference queue = stageConfigurationEnity.Contains(queueSchemaName) ? stageConfigurationEnity.GetAttributeValue<EntityReference>(queueSchemaName) : null;
                    if (queue!=null&& queue?.Id!=null&& queue?.Id!=Guid.Empty)
                    {
                        log.LogInfo($"  queue?.Id: {queue?.Id}");

                        AssignRequestToQueue(new EntityReference(entityLogicalName, entityId),   queue);
                    }
                }
                if (targetAssigningOwning!=null)
                {
                    AssignRequest(new EntityReference(entityLogicalName, entityId), targetAssigningOwning);
                }
            }
        }
        EntityReference GetAssignUser(string assigningFieldLogicalName, string entityLogicalName, Guid entityId)
            {
                ColumnSet ColumnSet = new ColumnSet();
            EntityReference user = null;
                ColumnSet.AddColumn(assigningFieldLogicalName);
                Entity requestEntity = service.Retrieve(entityLogicalName, entityId , ColumnSet);
                if (requestEntity != null && requestEntity?.Id != null && requestEntity.Contains(assigningFieldLogicalName))
                {
                    log.LogInfo($" requestEntity contain assignField");

                   user = requestEntity.GetAttributeValue<EntityReference>(assigningFieldLogicalName);
                }
            return user;
            }
        private void UpdateFieldsInTarget(  Entity stageConfigurationEnity, string entityLogicalName, Guid entityId ,string status, string statusReason,string portalStatus   )
        {
            log.LogInfo($"  UpdateFieldsInTarget");
            TargetEntity targetEntity = new TargetEntity();
            Entity currentEntity = new Entity(entityLogicalName, entityId);
            if (stageConfigurationEnity.Contains(status))
            {
                currentEntity.Attributes.Add(targetEntity.Status, stageConfigurationEnity.GetAttributeValue<EntityReference>(status));
                log.LogInfo($"  Status { stageConfigurationEnity.GetAttributeValue<EntityReference>(status).Id}  ");
            }
            if (stageConfigurationEnity.Contains(statusReason))
            {
                currentEntity.Attributes.Add(targetEntity.StatusReason, stageConfigurationEnity.GetAttributeValue<EntityReference>(statusReason));
                log.LogInfo($"  statusReason { stageConfigurationEnity.GetAttributeValue<EntityReference>(statusReason).Id}");
            }
            if (stageConfigurationEnity.Contains(portalStatus))
            {
                currentEntity.Attributes.Add(targetEntity.PortalStatus, stageConfigurationEnity.GetAttributeValue<EntityReference>(portalStatus));
                log.LogInfo($"  portalStatus { stageConfigurationEnity.GetAttributeValue<EntityReference>(portalStatus).Id}");
            }
            service.Update(currentEntity);
            tracingService.Trace($"Updated statusess to {entityLogicalName}");
            log.LogInfo($"Updated statusess  to {entityLogicalName}");
        }
        private void AssignRequest ( EntityReference currentEntity, EntityReference assignedRecord )
        {
            log.LogInfo($"  AssignRequest");

            AssignRequest assignRequest = new AssignRequest()
            {
                Assignee = new EntityReference
                {
                    LogicalName = assignedRecord.LogicalName, //User or Team logical name
                    Id = assignedRecord.Id // User Id or Team Id
                },
                Target = currentEntity 
            };
            service.Execute(assignRequest);
            tracingService.Trace($"  {currentEntity.LogicalName} has been assigned to  {assignedRecord.LogicalName} {assignedRecord.Id} sucessfully ");
            log.LogInfo($"{currentEntity.LogicalName}  has been assigned to  {assignedRecord.LogicalName} {assignedRecord.Id} sucessfully ");

        }
        private void AssignRequestToQueue(EntityReference targetEntity, EntityReference queue )
        {
            //check if there is an active queue item
            // Define Condition Values
            var query_statecode = 0;
            var query = new QueryExpression("queueitem");
            query.ColumnSet.AddColumns(  "objectid", "queueid", "workerid");
            query.AddOrder("enteredon", OrderType.Descending);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, query_statecode);
            query.Criteria.AddCondition("objectid", ConditionOperator.Equal, targetEntity.Id);

            EntityCollection relatedEntities = service.RetrieveMultiple(query);
            log.LogInfo($"  relatedEntities Count {relatedEntities.Entities.Count}");
            tracingService.Trace($"  relatedEntities Count {relatedEntities.Entities.Count}");

            if ( relatedEntities.Entities.Any()&& relatedEntities.Entities.Count>0)

            {
                log.LogInfo($"  relatedEntities.Entities.Count {  relatedEntities.Entities.Count}");

                Entity queueItemRelated = relatedEntities.Entities[0];
                //log.LogInfo($" will update the existence queue item to the new queue");

                //Entity updateQeueuItem = new Entity(queueItemRelated.LogicalName, queueItemRelated.Id);
                //log.LogInfo($"queueItemRelated.LogicalName  {queueItemRelated.LogicalName} with queueItemRelated.Id{queueItemRelated.Id}");

                //updateQeueuItem.Attributes.Add("queueid", queue);

                service.Delete(queueItemRelated.LogicalName, queueItemRelated.Id);
                log.LogInfo($"queueitem has been deleted for {targetEntity.LogicalName} with queue id {queue.Id}");

            }

            //if (!relatedEntities.Entities.Any())
            {
                //log.LogInfo($" There are no related queue item for  targetEntity.Id { targetEntity.Id}");
                //tracingService.Trace($" There are no related queue item targetEntity.Id { targetEntity.Id}");

                log.LogInfo($"  will create new queue item ");

                Entity queueItem = new Entity("queueitem");
                queueItem.Attributes.Add("queueid", queue);
                queueItem.Attributes.Add("objectid", targetEntity);
                service.Create(queueItem);
                tracingService.Trace($" queueitem has been created to  {targetEntity.LogicalName} with queue id {queue.Id}");
                log.LogInfo($"queueitem has been created to  {targetEntity.LogicalName} with queue id {queue.Id}");
            }
           // else
          


        }
        private Entity GetStageConfigurationFields(Guid stageConfigurationId)
        {
            log.LogInfo($"  stageConfigurationId");

            log.LogInfo($"  GetStageConfigurationFields");

          
            // Instantiate QueryExpression query
            var query = new QueryExpression("ldv_ministageconfiguration");
            // Add all columns to query.ColumnSet
            query.ColumnSet.AllColumns = true;
            // Define filter query.Criteria
            query.Criteria.AddCondition("ldv_ministageconfigurationid", ConditionOperator.Equal, stageConfigurationId);

            EntityCollection stageEntity = service.RetrieveMultiple(query);
            Entity stageConfiguration = null;


            if (stageEntity.Entities.Count > 0   )
                stageConfiguration = stageEntity[0];

            log.LogInfo($"  stageConfiguration {stageConfiguration}");

            return stageConfiguration;
        }
        ///////////////////////////// unused
        private void UpdateCurrentEntityWithSendbackStage(Entity stageConfigurationEnity, string entityLogicalName, Guid entityId)
        {

            //Assign Record
        }
        private void UpdateRecords(Entity stageConfigurationEnity, string entityLogicalName, Guid entityId)
        {
            TargetEntity targetEntity = new TargetEntity();
            Entity currentEntity = new Entity(entityLogicalName, entityId);
            if (stageConfigurationEnity.Contains(maanStageConfiguration.Status))
            {
                currentEntity.Attributes.Add(targetEntity.Status, stageConfigurationEnity.GetAttributeValue<EntityReference>(maanStageConfiguration.Status));
            }
            if (stageConfigurationEnity.Contains(maanStageConfiguration.StatusReason))
            {
                currentEntity.Attributes.Add(targetEntity.StatusReason, stageConfigurationEnity.GetAttributeValue<EntityReference>(maanStageConfiguration.StatusReason));
            }
            if (stageConfigurationEnity.Contains(maanStageConfiguration.PortalStatus))
            {
                currentEntity.Attributes.Add(targetEntity.PortalStatus, stageConfigurationEnity.GetAttributeValue<EntityReference>(maanStageConfiguration.PortalStatus));
            }
            service.Update(currentEntity);
        }
        private void UpdateSenbackRecords(Entity stageConfigurationEnity, string entityLogicalName, Guid entityId)
        {
            TargetEntity targetEntity = new TargetEntity();
            Entity currentEntity = new Entity(entityLogicalName, entityId);
            if (stageConfigurationEnity.Contains(maanStageConfiguration.SendbackStatus))
            {
                currentEntity.Attributes.Add(targetEntity.Status, stageConfigurationEnity.GetAttributeValue<EntityReference>(maanStageConfiguration.SendbackStatus));
            }
            if (stageConfigurationEnity.Contains(maanStageConfiguration.SendbackStatusReason))
            {
                currentEntity.Attributes.Add(targetEntity.StatusReason, stageConfigurationEnity.GetAttributeValue<EntityReference>(maanStageConfiguration.SendbackStatusReason));
            }
            if (stageConfigurationEnity.Contains(maanStageConfiguration.SendbackPortalStatus))
            {
                currentEntity.Attributes.Add(targetEntity.PortalStatus, stageConfigurationEnity.GetAttributeValue<EntityReference>(maanStageConfiguration.SendbackPortalStatus));
            }
            service.Update(currentEntity);
        }
    }
}
