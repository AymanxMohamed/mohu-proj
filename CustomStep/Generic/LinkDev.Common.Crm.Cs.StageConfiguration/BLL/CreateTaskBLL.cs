
using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
using Linkdev.CRM.CS.s.StageConfiguration.Entities;
using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Logger;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
{
    public class CreateTaskBLL : BllBase
    {
        private CRMAccessLayer crmAccess;
        public CreateTaskBLL(IOrganizationService organizationService, ILogger logger, string languageCode)
            : base(organizationService, logger, languageCode)
        {
            crmAccess = new CRMAccessLayer(OrganizationService);
        }
        public EntityReference CreateTask(EntityReference stageConfiguration, string requestId, string requestLogicalName, EntityReference appHeader)
        {
            Guid guid = Guid.Empty;
            if (stageConfiguration?.Id != Guid.Empty && requestId != string.Empty && requestLogicalName != string.Empty)
            {
                //Get Stage configuration fields
                QueryExpression stageConfigurationQuery = new QueryExpression(StageConfigurationEntity.LogicalName);
                stageConfigurationQuery.ColumnSet.AddColumns(StageConfigurationEntity.TaskSubject,
                                                             StageConfigurationEntity.TaskType,
                                                             //StageConfigurationEntity.Reminder,
                                                             //StageConfigurationEntity.DueDate,
                                                             //StageConfigurationEntity.Service,
                                                             StageConfigurationEntity.CreateTask, StageConfigurationEntity.TaskCondition);
                stageConfigurationQuery.Criteria.AddCondition(StageConfigurationEntity.StageConfiguration, ConditionOperator.Equal, stageConfiguration.Id);
                if (stageConfigurationQuery != null)
                {
                    EntityCollection stageConfigurationEntity = crmAccess.RetrieveMultipleRequest(stageConfigurationQuery);

                    if (stageConfigurationEntity.Entities.Any())
                    {
                        Entity stage = stageConfigurationEntity.Entities[0];

                        if (stage?.Id != Guid.Empty)
                        {
                            if (stage.Contains(StageConfigurationEntity.CreateTask))
                            {
                                bool isCreateTask = (bool)stage.Attributes[StageConfigurationEntity.CreateTask];
                                if (isCreateTask)
                                {
                                    Entity task = new Entity(TaskEntity.LogicalName);
                                    bool isConditionMet = false;
                                    //check if there is a condition exist and check if it met
                                    if (stage.Contains(StageConfigurationEntity.TaskCondition))
                                    {
                                        EntityReference taskCondition = stage.GetAttributeValue<EntityReference>(StageConfigurationEntity.TaskCondition);
                                        if (taskCondition?.Id != null && taskCondition?.Id != Guid.Empty)
                                        {
                                            #region get condition
                                            var stageConditionQuery = new QueryExpression("ldv_stagecondition");
                                            stageConditionQuery.ColumnSet.AddColumns("ldv_entityschemaname", "ldv_condition");
                                            stageConditionQuery.Criteria.AddCondition("ldv_stageconditionid", ConditionOperator.Equal, taskCondition?.Id);

                                            //var QEldv_stageconfiguration = stageConditionQuery.AddLink("ldv_stageconfiguration", "ldv_stageconditionid", "ldv_taskconditionid");
                                            //QEldv_stageconfiguration.LinkCriteria.AddCondition("ldv_stageconfigurationid", ConditionOperator.Equal, taskCondition?.Id);


                                            #endregion

                                            EntityCollection condition = crmAccess.RetrieveMultipleRequest(stageConditionQuery);
                                            if (condition.Entities.Any())
                                            {
                                                string conditionFetch = condition[0].Contains("ldv_condition") ? condition[0].GetAttributeValue<string>("ldv_condition") : null;
                                                string entitySchemaName = condition[0].Contains("ldv_entityschemaname") ? condition[0].GetAttributeValue<string>("ldv_entityschemaname") : null;
                                                if (conditionFetch != null && entitySchemaName != null && entitySchemaName == requestLogicalName)
                                                {
                                                    isConditionMet = crmAccess.IsConditionMet(conditionFetch, new EntityReference(requestLogicalName, new Guid(requestId)));
                                                }
                                            }
                                        }
                                    }

                                    if ((stage.Contains(StageConfigurationEntity.TaskCondition) && isConditionMet) || !stage.Contains(StageConfigurationEntity.TaskCondition))
                                    {
                                        //Task subject will be dynamic
                                        #region PlaceHolder in task subject
                                        string taskSubjectPlaceHolder = stage.Contains(StageConfigurationEntity.TaskSubject) ? stage.GetAttributeValue<string>(StageConfigurationEntity.TaskSubject) : string.Empty;
                                        string taskSubjectPlaceHolderCompination = GetTaskSubjectPlaceHolder(taskSubjectPlaceHolder, requestId, requestLogicalName);
                                        task.Attributes.Add(TaskEntity.Subject, taskSubjectPlaceHolderCompination);
                                        //var ccc = stage.Attributes[ StageConfigurationEntity.DueDate];

                                        #endregion

                                        //task.Attributes.Add(TaskEntity.Duration, stage.Contains(StageConfigurationEntity.DueDate) ? stage.GetAttributeValue<int>(StageConfigurationEntity.DueDate) : 0);
                                        //task.Attributes.Add(TaskEntity.Reminder, stage.Contains(StageConfigurationEntity.Reminder) ? stage.GetAttributeValue<float>(StageConfigurationEntity.Reminder) : 0);
                                        task.Attributes.Add(TaskEntity.StageConfiguration, stageConfiguration);
                                        #region Get Service and sla from Request  
                                        var serviceQueryExpression = new QueryExpression(ServiceDefinitionEntity.LogicalName);
                                        serviceQueryExpression.ColumnSet.AddColumns(ServiceDefinitionEntity.Sla, ServiceDefinitionEntity.ServiceId);
                                        var requestLink = serviceQueryExpression.AddLink(requestLogicalName, ServiceDefinitionEntity.ServiceId, RequestEntity.Service);
                                        requestLink.EntityAlias = "Service";
                                        requestLink.LinkCriteria.AddCondition(requestLogicalName + "id", ConditionOperator.Equal, requestId);
                                        ////Get Sla from Service
                                        //QueryExpression serviceQueryExpression = new QueryExpression(ServiceDefinitionEntity.LogicalName);
                                        //serviceQueryExpression.ColumnSet.AddColumns(ServiceDefinitionEntity.Sla);
                                        //serviceQueryExpression.Criteria.AddCondition(ServiceDefinitionEntity.ServiceId, ConditionOperator.Equal, service.Id);
                                        #endregion

                                        EntityCollection serviceDefination = crmAccess.RetrieveMultipleRequest(serviceQueryExpression);
                                        if (serviceDefination.Entities.Any())
                                        {
                                            Guid serviceId = serviceDefination[0].Contains(ServiceDefinitionEntity.ServiceId) ? serviceDefination[0].GetAttributeValue<Guid>(ServiceDefinitionEntity.ServiceId) : Guid.Empty;
                                            if (serviceId != Guid.Empty)
                                            {
                                                task.Attributes.Add(TaskEntity.Service, new EntityReference(ServiceDefinitionEntity.LogicalName, serviceId));
                                                EntityReference sla = serviceDefination[0].Contains(ServiceDefinitionEntity.Sla) ? serviceDefination[0].GetAttributeValue<EntityReference>(ServiceDefinitionEntity.Sla) : null;
                                                if (sla?.Id != Guid.Empty && sla != null)
                                                {
                                                    task.Attributes.Add(TaskEntity.SLA, sla);
                                                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Sla Id {sla.Id} ", SeverityLevel.Info);
                                                }
                                            }
                                        }
                                        if (stage.Contains(StageConfigurationEntity.TaskType))
                                        {
                                            OptionSetValue taskType = stage.GetAttributeValue<OptionSetValue>(StageConfigurationEntity.TaskType);
                                            task.Attributes.Add(TaskEntity.TaskType, new OptionSetValue(taskType.Value));
                                        }

                                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Request Logical Name {requestLogicalName} ", SeverityLevel.Info);
                                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Request Id {requestId} ", SeverityLevel.Info);

                                        task.Attributes.Add(TaskEntity.Regarding, new EntityReference(requestLogicalName, new Guid(requestId)));
                                        if (appHeader?.Id != Guid.Empty)
                                        {
                                            task.Attributes.Add(TaskEntity.ApplicationHeader, new EntityReference(appHeader.LogicalName, appHeader.Id));
                                        }
                                        guid = crmAccess.CreateEntity(task);
                                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"task  Id {guid} Created ", SeverityLevel.Info);

                                        if (guid != Guid.Empty)
                                        {
                                            Entity target = new Entity(requestLogicalName, new Guid(requestId));
                                            target.Attributes.Add(RequestEntity.CurrentTask, new EntityReference(TaskEntity.LogicalName, guid));
                                            crmAccess.UpdateEntity(target);
                                            Logger.LogComment(LoggerHandler.GetMethodFullName(), $" Request has task now ", SeverityLevel.Info);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return new EntityReference("task", guid);
        }

        public string GetTaskSubjectPlaceHolder(string taskSubjectPlaceHolder, string requestId, string requestLogicalName)
        {
            string taskSubjectPlaceHolderCompination = string.Empty;
            if (requestLogicalName != string.Empty && requestId != string.Empty && taskSubjectPlaceHolder != string.Empty)
            {
                taskSubjectPlaceHolderCompination = crmAccess.GetMessageWithValues(taskSubjectPlaceHolder, new EntityReference(requestLogicalName, new Guid(requestId)));
            }
            return taskSubjectPlaceHolderCompination;
        }
    }
}