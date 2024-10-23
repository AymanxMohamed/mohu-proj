using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Libraries.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Runtime.InteropServices;
using System.Web.Services.Discovery;
using System.Windows.Documents;
using LinkDev.Common.Crm.Cs.StageConfiguration.Enum;
using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
using Linkdev.CRM.CS.s.StageConfiguration.Entities;
using Microsoft.Crm.Sdk.Messages;
using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Bll.Base;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

//using Linkdev.MOE.CRM.LoggerManagement;
//using Linkdev.CRM.CS.s.StageConfiguration.DAL;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
{
    public class StageConfigurationBLL
    {
        private ILogger Logger { get; set; }

        private IOrganizationService OrganizationService;

        //private CrmLog log;
        private CRMAccessLayer crmAccess;


        /// <summary>
        /// the constractor which have service and log as input parameter
        /// </summary>
        /// <param name="service"></param>
        /// <param name="log"></param>
        public StageConfigurationBLL(IOrganizationService service, ITracingService tracingService, CodeActivityContext executionContext)
             : this(service, null, "1025")
        {
        }

        public StageConfigurationBLL(IOrganizationService service)
            : this(service, null, "1025")
        {
        }

        public StageConfigurationBLL(IOrganizationService service, ILogger logger, string langaugeCode)
        {
            OrganizationService = service;
            crmAccess = new CRMAccessLayer(OrganizationService);
            if (logger == null)
                Logger = new LoggerHandler(false, false, false, OrganizationService);
        }

        #region Routing Assigning

        /// <summary>
        /// Check Condition (exist and valid ) or Condition (not exist and get the field and check if exist in target request ) then Get Valid Index from stage  Routing Configuration List
        /// </summary>
        /// <param name="stageRoutingConfigurationList"></param>
        /// <param name="targetRequest"></param>
        /// <returns></returns>
        public int CheckConditionAndGetValidIndex(List<Entity> stageRoutingConfigurationList, Entity targetRequest, ITracingService tracingService)
        {

            int routingConfigurationWithoutConditionCount = 0;
            int routingConfigurationWithConditionCount = 0;


            IDictionary<int, EntityReference> routingIndexWithCondition = new Dictionary<int, EntityReference>();
            IDictionary<int, EntityReference> routingIndexWithEmptyCondition = new Dictionary<int, EntityReference>();
            EntityReference conditionLookup;
            int index = -1;
            // Check if result == null or result Count == 0 then return index

            if (stageRoutingConfigurationList != null)
            {
                //check how many  condition and empty condition
                for (int i = 0; i < stageRoutingConfigurationList.Count; i++)
                {
                    bool stageConditionIdExist = stageRoutingConfigurationList[i].Attributes.Contains("routingconfiguration.ldv_stageconditionid"); //Contains("routingconfiguration.ldv_stageconditionid");
                    if (stageConditionIdExist)
                    {
                        // condition = ((Microsoft.Xrm.Sdk.AliasedValue) result[i].Attributes["stagecondition.ldv_condition"]).Value.ToString();
                        conditionLookup = (EntityReference)((Microsoft.Xrm.Sdk.AliasedValue)stageRoutingConfigurationList[i].Attributes["routingconfiguration.ldv_stageconditionid"]).Value;

                        if (conditionLookup != null)
                        {
                            routingConfigurationWithConditionCount++;
                            routingIndexWithCondition.Add(i, conditionLookup);
                        }

                    }
                    else
                    {
                        routingConfigurationWithoutConditionCount++;
                        routingIndexWithEmptyCondition.Add(i, null);
                    }
                }
                // log.Log("emptyCondition :  " + routingConfigurationWithoutConditionCount + ", withCondition : " + routingConfigurationWithConditionCount, LogLevel.Debug);              
                conditionLookup = null;
                object condition;
                EntityReference routingConfiguration = new EntityReference();

                #region //1- only one with condition that is valid then assign it
                if (routingConfigurationWithConditionCount == 1 && routingConfigurationWithoutConditionCount == 0)
                {
                    tracingService.Trace($"1- withCondition == 1 && emptyCondition == 0 ");
                    conditionLookup = routingIndexWithCondition.ElementAt(0).Value;
                    if (conditionLookup != null)
                    {
                        //get condition value and check if valid     
                        condition = crmAccess.RetriveColumnValueFromEntity(conditionLookup.Id, conditionLookup.LogicalName,
                          StageConditionEntity.Condition);
                        if (condition != null)
                        {
                            if (crmAccess.IsConditionMet(condition.ToString(), targetRequest.ToEntityReference()))
                            {
                                index = routingIndexWithCondition.ElementAt(0).Key;
                            }
                        }
                    }
                }
                #endregion

                #region //2-only one without  condition then assign it
                else if (routingConfigurationWithoutConditionCount == 1 && routingConfigurationWithConditionCount == 0)
                {
                    tracingService.Trace($"in ---->2-  emptyCondition == 1 && withCondition == 0  ");
                    index = routingIndexWithEmptyCondition.ElementAt(0).Key;
                }
                #endregion

                #region //3-if one without condition and one or more with condition
                else if (routingConfigurationWithoutConditionCount == 1 && routingConfigurationWithConditionCount >= 1)
                {
                    tracingService.Trace($"in ----->3- emptyCondition == 1 && withCondition >= 1  ");
                    int validConditionsCount = 0;
                    foreach (KeyValuePair<int, EntityReference> item in routingIndexWithCondition)
                    {
                        conditionLookup = item.Value;
                        // execute the validation of the condition   
                        condition = (string)crmAccess.RetriveColumnValueFromEntity(conditionLookup.Id, conditionLookup.LogicalName,
                           StageConditionEntity.Condition);
                        if (crmAccess.IsConditionMet(condition.ToString(), targetRequest.ToEntityReference()))
                        {
                            validConditionsCount++;
                        }
                    }
                    if (validConditionsCount == 0)
                    {
                        //assign to empty condition
                        index = routingIndexWithEmptyCondition.ElementAt(0).Key;
                        //log.Log("if executedCondation == 0 then index= " + index, LogLevel.Debug);
                    }
                }
                #endregion

                #region //4- if there are more than one condition


                else if (routingConfigurationWithoutConditionCount == 0 && routingConfigurationWithConditionCount > 0)
                {
                    tracingService.Trace($"in -----> emptyCondition == 0 && withCondition > 0 ");
                    int onlyOneValid = 0;
                    foreach (KeyValuePair<int, EntityReference> item in routingIndexWithCondition)
                    {
                        conditionLookup = item.Value;
                        if (conditionLookup == null)
                        {
                            break;
                        }
                        condition = (string)crmAccess.RetriveColumnValueFromEntity(conditionLookup.Id, conditionLookup.LogicalName,
                            StageConditionEntity.Condition);
                        if (crmAccess.IsConditionMet(condition.ToString(), targetRequest.ToEntityReference()))
                        {
                            // check  1 and 2 if both executed break ;  
                            onlyOneValid++;
                            // if only one that valid then assign to it
                            if (onlyOneValid == 1)
                            {
                                index = item.Key;
                            }
                            else if (onlyOneValid >= 2)
                            {
                                tracingService.Trace($"more than onlyOneValid ");

                                index = -1;
                                break;
                            }
                        }
                    }
                }

                #endregion

                else
                {
                    index = -1;
                }
            }
            tracingService.Trace($"index {index}");

            return index;
        }

        /// <summary>
        /// Get Role Configuration Fields (type and its role = user or team or queue)
        /// </summary>
        /// <param name="roleConfigurationLookup"></param>
        /// <returns></returns>
        public EntityReference GetRoleConfigurationFields(Entity roleConfigurationLookup)
        {
            try
            {
                //log.Log("in fn. GetRoleConfigurationFields", LogLevel.Debug);
                EntityReference assigningRouting = new EntityReference();
                Entity roleConfigurationEntity = crmAccess.RetrieveEntityWithColumns(
                    roleConfigurationLookup.ToEntityReference(), new string[]
                    {
                        RoleConfigurationField.Queue, RoleConfigurationField.Team,
                        RoleConfigurationField.Type, RoleConfigurationField.User
                    });

                if (roleConfigurationEntity != null)
                {
                    OptionSetValue type =
                        roleConfigurationEntity.Contains(RoleConfigurationField.Type)
                            ? roleConfigurationEntity.GetAttributeValue<OptionSetValue>(
                                RoleConfigurationField.Type)
                            : null;
                    if (type != null)
                    {
                        // make sure if one have value other shouldn't have values
                        if (type.Value == (int)RoleConfigurationType.Queue)
                        {
                            assigningRouting =
                                roleConfigurationEntity.Contains(RoleConfigurationField.Queue)
                                    ? roleConfigurationEntity
                                        .GetAttributeValue<EntityReference>(
                                            RoleConfigurationField.Queue)
                                    : null;
                        }
                        else if (type.Value == (int)RoleConfigurationType.User)
                        {
                            assigningRouting =
                                roleConfigurationEntity.Contains(RoleConfigurationField.User)
                                    ? roleConfigurationEntity
                                        .GetAttributeValue<EntityReference>(
                                            RoleConfigurationField.User)
                                    : null;
                        }
                        else if (type.Value == (int)RoleConfigurationType.Team)
                        {
                            assigningRouting =
                                roleConfigurationEntity.Contains(RoleConfigurationField.Team)
                                    ? roleConfigurationEntity
                                        .GetAttributeValue<EntityReference>(
                                            RoleConfigurationField.Team)
                                    : null;
                        }
                    }
                    else
                    {
                        // log.Log("type = null", LogLevel.Warning);
                    }

                }
                else
                {
                    //  log.Log("roleConfigurationEntity is null", LogLevel.Warning);
                }
                return assigningRouting;
            }
            catch (Exception ex)
            {
                //log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                // log.LogFunctionEnd();
            }
        }

        /// <summary>
        /// Get Assinging routing record  From Spasific Field which exist in target request
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="targetRequest"></param>
        /// <returns></returns>
        public EntityReference GetAssingingFromSpasificField(string fieldValue, Entity targetRequest)
        {
            try
            {
                //log.Log("in fn. GetAssingingFromSpasificField", LogLevel.Debug);
                EntityReference conditionOfSpacificField = new EntityReference();
                if (fieldValue == string.Empty)
                {
                    return null;
                }
                //check if field exist in the entity
                bool doesFieldExistInTargetEntity = crmAccess.DoesFieldExist(targetRequest.LogicalName,
                    fieldValue);
                //log.Log("doesFieldExistInTargetEntity: " + doesFieldExistInTargetEntity, LogLevel.Debug);
                if (doesFieldExistInTargetEntity)
                {



                    // log.Log(" Field Exists in target entity ", LogLevel.Debug);
                    //get field value from target entity
                    //EntityCollection TargetEntity =
                    //    crmAccess.EntityReferenceCollectionByQueryExpression(targetRequest, fieldValue);


                    Entity TargetEntity = crmAccess.RetrieveEntityWithColumns(targetRequest.ToEntityReference(), new[] { fieldValue });
                    if (TargetEntity != null)
                    {
                        if (TargetEntity.Id != Guid.Empty)
                        {
                            EntityReference field = TargetEntity.Contains(fieldValue)
                                ? TargetEntity.GetAttributeValue<EntityReference>(fieldValue)
                                : null;
                            if (field != null)
                            {
                                //log.Log("field.id : " + field.Id + "field name : " + field.LogicalName);
                                conditionOfSpacificField = field;
                            }
                        }
                    }


                    //if (TargetEntity.Entities.Any())
                    //{
                    //    EntityReference field = new EntityReference(null);
                    //    log.Log("result have value ", LogLevel.Debug);
                    //    bool valueExist = TargetEntity.Entities[0].Contains(fieldValue);
                    //    if (valueExist)
                    //    {
                    //        field = TargetEntity.Entities[0].GetAttributeValue<EntityReference>(fieldValue);
                    //        log.Log("field : " + field, LogLevel.Debug);
                    //        if (field != null)
                    //        {
                    //            log.Log("field.id : " + field.Id + "field name : " + field.LogicalName);
                    //            conditionOfSpacificField = field;
                    //        }
                    //    }
                    //}
                }
                else
                {
                    //log.Log("field doesn't exist ", LogLevel.Warning);
                }
                return conditionOfSpacificField;
            }
            catch (Exception ex)
            {
                //log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //log.LogFunctionEnd();

            }
        }

        /// <summary>
        /// Retrieve Target Entity From Application Header which exist in  input parameter 
        /// </summary>
        /// <param name="appHeader"></param>
        /// <returns></returns>
        public Entity RetrieveTargetEntityFromApplicationHeader(EntityReference appHeader)
        {
            try
            {
                Entity targetEntity = new Entity();

                if (appHeader?.Id != Guid.Empty)
                {
                    Entity applicationHeader = crmAccess.RetrieveEntityWithColumns(appHeader,
                        new string[]
                        {
                            ApplicationHeaderEntity.Regarding
                        });


                    EntityReference Regarding = applicationHeader.Contains(ApplicationHeaderEntity.Regarding)
                        ? applicationHeader.GetAttributeValue<EntityReference>(ApplicationHeaderEntity.Regarding)
                        : null;

                    if (Regarding != null)
                    {

                        targetEntity = new Entity(Regarding.LogicalName, Regarding.Id);

                        return targetEntity;
                    }
                }
                return targetEntity;
            }
            catch (Exception ex)
            {
                //log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //log.LogFunctionEnd();
            }

        }


        /// <summary>
        /// retrieve all Stage routing configuration fields ,  routingconfiguration fields ,stage condition fields  and  role configuration fields by using link entities and alies
        /// </summary>
        /// <param name="service"></param>
        /// <param name="stageConfigurationId"></param>
        /// <param name="log"></param>
        /// <returns></returns>      
        public List<Entity> RetrieveStageRoutingConfigurationFields(Guid stageConfigurationId)
        {
            List<Entity> entities = new List<Entity>();
            string fetchXml = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>" +
                              "  <entity name='ldv_stageroutingconfiguration'>" +
                              "    <attribute name='ldv_stageroutingconfigurationid' />" +
                              "    <filter type='and'>" +
                              "      <condition attribute='ldv_stageconfigurationid' operator='eq'  uitype='ldv_stageconfiguration' value='{" + stageConfigurationId + "}' />" +
                              "    </filter>" +
                              "    <link-entity name='ldv_routingconfiguration' from='ldv_routingconfigurationid' to='ldv_routingconfigurationid' alias='routingconfiguration'>" +
                              "     <attribute name='ldv_routingconfigurationid' />" +
                              "      <attribute name='ldv_typecode' />" +
                              "      <attribute name='ldv_roleconfigurationid' />" +
                              "      <attribute name='ldv_name' />" +
                              "      <attribute name='ldv_fieldname' />" +
                              "      <attribute name='ldv_stageconditionid' />" +
                              //"      <link-entity name='ldv_stagecondition' from='ldv_stageconditionid' to='ldv_stageconditionid' alias='stagecondition'>" +
                              //" 	    <attribute name='ldv_stageconditionid' />" +
                              //"   	    <attribute name='ldv_name' />" +
                              //"    	    <attribute name='ldv_entityschemaname' />" +
                              //"         <attribute name='ldv_condition' />" +
                              //"	        <order attribute='ldv_name' descending='false' />" +
                              //"      </link-entity>" +
                              //"      <link-entity name='ldv_roleconfiguration' from='ldv_roleconfigurationid' to='ldv_roleconfigurationid' alias='roleconfiguration'>" +
                              //"        <attribute name='ldv_roleconfigurationid' />" +
                              //"    <attribute name='ldv_type' />" +
                              //"    <attribute name='ldv_user' />" +
                              //"    <attribute name='ldv_team' />" +
                              //"    <attribute name='ldv_queue' />" +
                              //"    <order attribute='ldv_name' descending='false' />" +
                              //"      </link-entity>" +
                              "    </link-entity>" +
                              "  </entity>" +
                              "</fetch>";

            EntityCollection retrieved = crmAccess.RetrieveMultipleByFetch(new FetchExpression(fetchXml));

            if (retrieved == null && retrieved.Entities == null)
                return null;
            else
            {
                //log.Log(" StageRoutingConfiguration count : " + retrieved.Entities.Count, LogLevel.Debug);
                return retrieved.Entities.ToList();
            }
        }

        #endregion

        #region StageConfiguration Part

        /// <summary>
        ///This method used for Retrive Stage Configurarion Entity By passing  Process Stage id
        /// </summary>
        /// <param name="processStageId">the input parameter Process Stage id</param>
        /// <param name="service">Organization Service</param>
        /// <param name="log">the logger </param>
        /// <returns></returns>
        public EntityReference RetriveStageConfigurarionByProcessStage(Guid processStageId)
        {
            try
            {
                // log.Log("in fn. RetriveStageConfigurarionByProcessStage    ", LogLevel.Debug);

                string fetchXml =
                    "<fetch version='1.0' output-format='xml-platform' mapping='logical' no-lock='true' distinct='true'>" +
                    "  <entity name='ldv_stageconfiguration'>" +
                    "    <attribute name='ldv_stageconfigurationid' />" +
                    "    <filter type='and'>" +
                    "      <condition attribute='ldv_processstageid' operator='eq'  uitype='processstage' value='{" +
                    processStageId + "}' />" +
                    "    </filter>" +
                    "  </entity>" +
                    "</fetch>";
                var retrieved = crmAccess.RetrieveMultiple(new FetchExpression(fetchXml));
                // log.Log("retrive count= " + retrieved.Count, LogLevel.Debug); 

                if (!retrieved.Any()) return null;
                //log.Log("retrieved= " + retrieved + ", retrieved.Entities.Count " + retrieved.Count, LogLevel.Debug);
                EntityReference stageConfiguration =
                    retrieved[0].Attributes.Contains(StageConfigurationEntity.StageConfiguration)
                        ? retrieved[0].ToEntityReference()
                        : null;
                //log.Log("guid :" + retrieved[0].Id, LogLevel.Debug);
                return stageConfiguration;
            }
            catch (Exception ex)
            {
                // log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                // log.LogFunctionEnd();
            }
        }

        public EntityReference RetriveStageConfigurarionByProcessStage(Guid bpfInstanceId, string bpfInstanceSchemaName, Guid serviceId, ITracingService tracingService)
        {
            try
            {
                tracingService.Trace($" In method RetriveStageConfigurarionByProcessStage {bpfInstanceId} {bpfInstanceSchemaName}");
                if (bpfInstanceId == Guid.Empty && bpfInstanceSchemaName == string.Empty) return null;
                tracingService.Trace($"4");
                var bpfOfRequest = new QueryExpression(bpfInstanceSchemaName);
                bpfOfRequest.Distinct = true;
                bpfOfRequest.ColumnSet.AddColumns("activestageid");
                bpfOfRequest.Criteria.AddCondition("businessprocessflowinstanceid", ConditionOperator.Equal, bpfInstanceId);
                EntityCollection instance = crmAccess.RetrieveMultipleRequest(bpfOfRequest);
                if (!instance.Entities.Any()) return null;
                EntityReference activeStage = instance.Entities[0].GetAttributeValue<EntityReference>("activestageid");
                if (activeStage?.Id == Guid.Empty) return null;
                tracingService.Trace($"stage id { activeStage.Id}");
                string serviceIdFetch = "";
                if (serviceId!=Guid.Empty)
                {
                      serviceIdFetch="<condition attribute='ldv_serviceid' operator='eq'  uitype='ldv_service' value='"+serviceId+"' />";
                }
                string fetchXml = "<fetch version='1.0' output-format='xml-platform' mapping='logical' no-lock='true' distinct='true'>" +
                                         "  <entity name='ldv_stageconfiguration'>" +
                                         "    <attribute name='ldv_stageconfigurationid' />" +
                                         "    <filter type='and'>" +
                                         "      <condition attribute='ldv_processstageid' operator='eq'  uitype='processstage' value='{" +
                                         activeStage.Id + "}' />" +
                                         serviceIdFetch +
                                         "    </filter>" +
                                         "  </entity>" +
                                         "</fetch>";
                tracingService.Trace($"fetchXml {fetchXml}");

                var retrieved = crmAccess.RetrieveMultiple(new FetchExpression(fetchXml));

                //tracingService.Trace($" { fetchXml}");
                // log.Log("retrive count= " + retrieved.Count, LogLevel.Debug); 

                tracingService.Trace($"5");

                if (retrieved.Count<1) return null;
                //log.Log("retrieved= " + retrieved + ", retrieved.Entities.Count " + retrieved.Count, LogLevel.Debug);
                EntityReference stageConfiguration =
                    retrieved[0].Attributes.Contains(StageConfigurationEntity.StageConfiguration)
                        ? retrieved[0].ToEntityReference()
                        : null;

                tracingService.Trace($"6");

                return stageConfiguration;
            }
            catch (Exception ex)
            {
                // log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                // log.LogFunctionEnd();
            }
        }

        #endregion

        #region StageStatuses part

        /// <summary>
        ///   Retrive Stage Status and sub status
        /// </summary>
        /// <param name="stageConfiguration"></param>
        /// <returns></returns>

        public Entity GetStageStatuses(EntityReference stageConfiguration)
        {
            try
            {
                if (stageConfiguration != null)
                {
                    // log.Log("in fn. RetriveStageStatus", LogLevel.Debug);

                    Entity result = crmAccess.RetrieveEntityWithColumns(stageConfiguration,
                        new string[] { StageConfigurationEntity.ServiceStatus, StageConfigurationEntity.ServiceSubStatus }
                       );

                    if (result != null)
                    {
                        if (result.Id == Guid.Empty)
                        {
                            return null;
                        }
                        return result;

                    }
                }
                return new Entity(null);
            }
            catch (Exception ex)
            {
                // log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //log.LogFunctionEnd();
            }
        }


        #endregion

        #region Task

        public List<Entity> RetrieveStageTaskRelatedToTargetFetch(Guid stageConfigurationId, Guid RegardingEntityId)
        {
            List<Entity> entities = new List<Entity>();
            string fetchXml = @"< fetch version = '1.0' output - format = 'xml-platform' mapping = 'logical' distinct = 'true' >         
           <entity name = 'task' >                                        
                      <attribute name = 'ldv_decisionmadebysystemuserid' />                   
                       <order attribute = 'createdon' descending = 'true' />                      
                          <filter type = 'and' >                      
                             <filter type = 'and' >
                        <condition attribute = 'ldv_stageconfigurationid' operator= 'eq' value = '" + stageConfigurationId + @"' />
                        <condition attribute = 'regardingobjectid' operator= 'eq' value = '" + RegardingEntityId + @"' /> 
                                              </ filter >
                                            </ filter >
                                          </ entity >
                                        </ fetch >";

            EntityCollection retrieved = crmAccess.RetrieveMultipleByFetch(new FetchExpression(fetchXml));

            if (retrieved == null && retrieved.Entities == null)
                return null;
            else
            {
                //log.Log(" StageRoutingConfiguration count : " + retrieved.Entities.Count, LogLevel.Debug);
                return retrieved.Entities.ToList();
            }
        }
        //public Task RetrieveStageTaskRelatedToTarget(Guid stageConfigurationId, Guid RegardingEntityId)
        //{
        //    ////i don't know what is that 
        //    //XrmServiceContext context = new XrmServiceContext(OrganizationService);
        //    //var task = context.TaskSet.Where(a => a.Regarding.Id == RegardingEntityId && a.StageConfiguration == stageConfigurationId).OrderByDescending(a => a.CreatedOn).FirstOrDefault();

        //    //return task;

        //}

        #endregion
        #region Stage Change Fields and History
        public List<StageFields> RetrieveStageFields(Guid stageConfigurationId, GridType gridType, Entity request)
        {
            List<StageFields> stageFieldsList = new List<StageFields>();
            if (stageConfigurationId != Guid.Empty)
            {
                //Get Changed fields from stage configuration  
                #region stagefieldsQueryExpression
                var stagefieldsQueryExpression = new QueryExpression(StageFieldsEntity.LogicalName);
                stagefieldsQueryExpression.Distinct = true;
                stagefieldsQueryExpression.ColumnSet.AddColumns(StageFieldsEntity.StageFields, StageFieldsEntity.EntitySchemaName, StageFieldsEntity.FieldSchemaName,
                    StageFieldsEntity.FieldType, StageFieldsEntity.LookupEntitySchemaName, StageFieldsEntity.Name, StageFieldsEntity.StageFields);
                //stagefieldsQueryExpression.AddOrder("ldv_name", OrderType.Ascending);
                var stageconfigurationchangedfieldsLink = stagefieldsQueryExpression.AddLink(SC_ChangedFields.LogicalName, StageFieldsEntity.StageFields, StageFieldsEntity.StageFields);
                stageconfigurationchangedfieldsLink.EntityAlias = "SC_ChangField";
                stageconfigurationchangedfieldsLink.Columns.AddColumns(SC_ChangedFields.IsNullable
                    , SC_ChangedFields.CodeValue, SC_ChangedFields.TaskFieldSchemaName, SC_ChangedFields.RelationNameCode);//, SC_ChangedFields.Condition);
                stageconfigurationchangedfieldsLink.LinkCriteria.AddCondition(SC_ChangedFields.StageConfiguration, ConditionOperator.Equal, stageConfigurationId);
                stageconfigurationchangedfieldsLink.LinkCriteria.AddCondition(SC_ChangedFields.RelationNameCode, ConditionOperator.Equal, (int)gridType);

                //var  stageCondition = stageconfigurationchangedfieldsLink.AddLink(StageConditionEntity.LogicalName  , SC_ChangedFields.Condition , StageConditionEntity.StageCondition , JoinOperator.LeftOuter);
                //stageCondition.EntityAlias = "Condition";
                //stageCondition.Columns.AddColumns(StageConditionEntity.Condition , StageConditionEntity.EntitySchemaName);

                #endregion


                EntityCollection stageFieldsCollection = crmAccess.RetrieveMultipleRequest(stagefieldsQueryExpression);
                if (stageFieldsCollection.Entities.Any())
                {
                    // create object of the above columns
                    foreach (Entity stageFieldsEntity in stageFieldsCollection.Entities)
                    {

                        StageFields stageFields = new StageFields();
                        stageFields.CodeValue = stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.CodeValue) ? (string)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + SC_ChangedFields.CodeValue).Value : null;
                        //OptionSetItem relationNameOptionSetItem = new OptionSetItem();
                        //relationNameOptionSetItem.Id = stageFieldsEntity.Contains("SC_ChangField." + "ldv_relationnamecode") ? ((OptionSetValue)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + "ldv_relationnamecode").Value).Value : -1;
                        //stageFields.RelationShipName = relationNameOptionSetItem;
                        stageFields.TaskFieldSchemaName = stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.TaskFieldSchemaName) ? (string)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + SC_ChangedFields.TaskFieldSchemaName).Value : null;
                        bool isNull = stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.IsNullable) ? (bool)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + SC_ChangedFields.IsNullable).Value : false;
                        stageFields.IsNullable = isNull;
                        stageFields.EntitySchemaName = stageFieldsEntity.Contains(StageFieldsEntity.EntitySchemaName) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.EntitySchemaName) : null;
                        stageFields.FieldSchemaName = stageFieldsEntity.Contains(StageFieldsEntity.FieldSchemaName) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.FieldSchemaName) : null;
                        stageFields.LookupEntitySchemaName = stageFieldsEntity.Contains(StageFieldsEntity.LookupEntitySchemaName) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.LookupEntitySchemaName) : null;
                        stageFields.Name = stageFieldsEntity.Contains(StageFieldsEntity.Name) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.Name) : null;
                        GuidItem stageFieldsGuidItem = new GuidItem();
                        stageFieldsGuidItem.Id = stageFieldsEntity.Contains(StageFieldsEntity.StageFields) ? stageFieldsEntity.GetAttributeValue<Guid>(StageFieldsEntity.StageFields) : Guid.Empty;
                        stageFields.StageFieldsId = stageFieldsGuidItem;
                        OptionSetItem fieldTypeOptionSetItem = new OptionSetItem();
                        //var xx = stageFieldsEntity.GetAttributeValue<OptionSetValue>(StageFieldsEntity.FieldType);
                        fieldTypeOptionSetItem.Id = stageFieldsEntity.Contains(StageFieldsEntity.FieldType) ? (stageFieldsEntity.GetAttributeValue<OptionSetValue>(StageFieldsEntity.FieldType)).Value : -1;
                        stageFields.FieldType = fieldTypeOptionSetItem;
                        stageFieldsList.Add(stageFields);
                    }
                }
            }
            return stageFieldsList;
        }

        public List<ChangedFieldTriggers> RetrieveChangedStageFields(Guid stageConfigurationId, GridType gridType, Entity request, ITracingService tracingService)
        {
            tracingService.Trace($" in RetrieveChangedStageFields methods");
            List<ChangedFieldTriggers> changedFieldTriggersList = new List<ChangedFieldTriggers>();

            if (stageConfigurationId != Guid.Empty)
            {
                //Get Changed fields from stage configuration  
                #region stagefieldsQueryExpression
                var stagefieldsQueryExpression = new QueryExpression(StageFieldsEntity.LogicalName);
                stagefieldsQueryExpression.Distinct = true;
                stagefieldsQueryExpression.ColumnSet.AddColumns(StageFieldsEntity.StageFields, StageFieldsEntity.EntitySchemaName, StageFieldsEntity.FieldSchemaName, StageFieldsEntity.FieldType, StageFieldsEntity.LookupEntitySchemaName, StageFieldsEntity.Name, StageFieldsEntity.StageFields);
                //stagefieldsQueryExpression.AddOrder("ldv_name", OrderType.Ascending);
                var stageconfigurationchangedfieldsLink = stagefieldsQueryExpression.AddLink(SC_ChangedFields.LogicalName, StageFieldsEntity.StageFields, StageFieldsEntity.StageFields);
                stageconfigurationchangedfieldsLink.EntityAlias = "SC_ChangField";
                stageconfigurationchangedfieldsLink.Columns.AddColumns(SC_ChangedFields.IsNullable, SC_ChangedFields.CodeValue, SC_ChangedFields.TaskFieldSchemaName, SC_ChangedFields.RelationNameCode, SC_ChangedFields.Condition);
                stageconfigurationchangedfieldsLink.LinkCriteria.AddCondition(SC_ChangedFields.StageConfiguration, ConditionOperator.Equal, stageConfigurationId);
                stageconfigurationchangedfieldsLink.LinkCriteria.AddCondition(SC_ChangedFields.RelationNameCode, ConditionOperator.Equal, (int)gridType);
                var stageCondition = stageconfigurationchangedfieldsLink.AddLink(StageConditionEntity.LogicalName, SC_ChangedFields.Condition, StageConditionEntity.StageCondition, JoinOperator.LeftOuter);
                stageCondition.EntityAlias = "Condition";
                stageCondition.Columns.AddColumns(StageConditionEntity.Condition, StageConditionEntity.EntitySchemaName);
                #endregion

                EntityCollection stageFieldsCollection = crmAccess.RetrieveMultipleRequest(stagefieldsQueryExpression);
                if (stageFieldsCollection.Entities.Any())
                {

                    string[] SchemaNames = new string[stageFieldsCollection.Entities.Count];
                    int SchemaNamesCount = 0;
                    // create object of the above columns
                    foreach (Entity stageFieldsEntity in stageFieldsCollection.Entities)
                    {
                        StageFields stageFields = new StageFields();
                        stageFields.CodeValue = stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.CodeValue) ? (string)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + SC_ChangedFields.CodeValue).Value : null;
                        //OptionSetItem relationNameOptionSetItem = new OptionSetItem();
                        //relationNameOptionSetItem.Id = stageFieldsEntity.Contains("SC_ChangField." + "ldv_relationnamecode") ? ((OptionSetValue)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + "ldv_relationnamecode").Value).Value : -1;
                        //stageFields.RelationShipName = relationNameOptionSetItem;
                        stageFields.TaskFieldSchemaName = stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.TaskFieldSchemaName) ? (string)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + SC_ChangedFields.TaskFieldSchemaName).Value : null;
                        bool isNull = stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.IsNullable) ? (bool)stageFieldsEntity.GetAttributeValue<AliasedValue>("SC_ChangField." + SC_ChangedFields.IsNullable).Value : false;
                        stageFields.IsNullable = isNull;
                        stageFields.EntitySchemaName = stageFieldsEntity.Contains(StageFieldsEntity.EntitySchemaName) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.EntitySchemaName) : null;
                        stageFields.FieldSchemaName = stageFieldsEntity.Contains(StageFieldsEntity.FieldSchemaName) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.FieldSchemaName) : null;

                        stageFields.LookupEntitySchemaName = stageFieldsEntity.Contains(StageFieldsEntity.LookupEntitySchemaName) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.LookupEntitySchemaName) : null;
                        stageFields.Name = stageFieldsEntity.Contains(StageFieldsEntity.Name) ? stageFieldsEntity.GetAttributeValue<string>(StageFieldsEntity.Name) : null;
                        GuidItem stageFieldsGuidItem = new GuidItem();
                        stageFieldsGuidItem.Id = stageFieldsEntity.Contains(StageFieldsEntity.StageFields) ? stageFieldsEntity.GetAttributeValue<Guid>(StageFieldsEntity.StageFields) : Guid.Empty;
                        stageFields.StageFieldsId = stageFieldsGuidItem;
                        OptionSetItem fieldTypeOptionSetItem = new OptionSetItem();
                        fieldTypeOptionSetItem.Id = stageFieldsEntity.Contains(StageFieldsEntity.FieldType) ? (stageFieldsEntity.GetAttributeValue<OptionSetValue>(StageFieldsEntity.FieldType)).Value : -1;
                        stageFields.FieldType = fieldTypeOptionSetItem;
                        ///condition part
                        if (stageFieldsEntity.Contains("SC_ChangField." + SC_ChangedFields.Condition))
                        {
                            tracingService.Trace($"Field {stageFields.FieldSchemaName} Has condition");
                            stageFields.HasCondition = true;
                            ConditionFields conditionFields = new ConditionFields();
                            conditionFields.FetchCondition = stageFieldsEntity.Contains("Condition." + StageConditionEntity.Condition) ? (string)stageFieldsEntity.GetAttributeValue<AliasedValue>("Condition." + StageConditionEntity.Condition).Value : null;
                            stageFields.ConditionField = conditionFields;
                        }
                        else
                        {
                            tracingService.Trace($"Field {stageFields.FieldSchemaName} Has No condition");
                            stageFields.HasCondition = false;
                            //stageFields.IsValidToChange = true;
                        }
                        //stageFieldsList.Add(stageFields);
                        //if schemaName exist then loop and get the corresponding ChangedFieldTriggers to add in it
                        if (SchemaNames.Contains(stageFields.FieldSchemaName))
                        {
                            foreach (ChangedFieldTriggers changedFieldTrigger in changedFieldTriggersList)
                            {
                                if (changedFieldTrigger.FieldSchemaName == stageFields.FieldSchemaName)
                                {
                                    List<StageFields> existStageField = changedFieldTrigger.StageFields;
                                    existStageField.Add(stageFields);
                                    changedFieldTrigger.StageFields = existStageField;
                                    if (stageFields.HasCondition)
                                    {
                                        changedFieldTrigger.WithConditionCount = changedFieldTrigger.WithConditionCount + 1;
                                    }
                                    else
                                    {
                                        changedFieldTrigger.WithOutConditionCount = changedFieldTrigger.WithOutConditionCount + 1;
                                    }
                                }
                            }
                        }
                        else //new changed changedFieldTriggers record
                        {
                            //add in list
                            SchemaNames[SchemaNamesCount] = stageFields.FieldSchemaName;
                            SchemaNamesCount++;
                            ChangedFieldTriggers changedFieldTrigger = new ChangedFieldTriggers();
                            List<StageFields> newStageField = new List<StageFields>();
                            newStageField.Add(stageFields);
                            changedFieldTrigger.StageFields = newStageField;
                            changedFieldTrigger.FieldSchemaName = stageFields.FieldSchemaName;
                            changedFieldTriggersList.Add(changedFieldTrigger);
                            if (stageFields.HasCondition)
                            {
                                changedFieldTrigger.WithConditionCount = changedFieldTrigger.WithConditionCount + 1;
                            }
                            else
                            {
                                changedFieldTrigger.WithOutConditionCount = changedFieldTrigger.WithOutConditionCount + 1;
                            }
                        }
                    }
                }
            }
            return changedFieldTriggersList;
        }
        public Guid GetEntityByCode(string code, string entitySchemaName, ITracingService tracingService)
        {
            tracingService.Trace($"in GetEntityByCode");

            Guid id = Guid.Empty;
            if (string.IsNullOrEmpty( code)&& entitySchemaName == string.Empty) return Guid.Empty;
            //var serviceSubStatusQuery = new QueryExpression(entitySchemaName);
            //serviceSubStatusQuery.Distinct = true;
            //serviceSubStatusQuery.ColumnSet.AddColumns(entitySchemaName + "id");
            //serviceSubStatusQuery.Criteria.AddCondition("ldv_code", ConditionOperator.Equal, code.ToString() );
            //EntityCollection entityCollection = crmAccess.RetrieveMultiple(serviceSubStatusQuery);
            //tracingService.Trace($"entityCollection.Entities.Count {entityCollection.Entities.Count} ");

            //if (entityCollection.Entities.Count > 0)
            //    if (entityCollection.Entities[0].Contains(entitySchemaName + "id"))
            //    {
            //        id = entityCollection.Entities[0].GetAttributeValue<Guid>(entitySchemaName + "id");
            //    }
            //return id;
            tracingService.Trace($"in entitySchemaName {entitySchemaName}, code {code}");

            var query = new QueryExpression(entitySchemaName);
            query.ColumnSet.AddColumns(entitySchemaName + "id", "createdon");
            query.AddOrder("ldv_code", OrderType.Descending);
            query.Criteria.AddCondition("ldv_code", ConditionOperator.Equal, code.ToString());
            EntityCollection entityCollection = OrganizationService.RetrieveMultiple(query);
            tracingService.Trace($"entityCollection.Entities.Count {entityCollection.Entities.Count} ");

            if (entityCollection.Entities.Count > 0)
                if (entityCollection.Entities[0].Contains(entitySchemaName + "id"))
                {
                    id = entityCollection.Entities[0].GetAttributeValue<Guid>(entitySchemaName + "id");
                }
            return id;





        }

        #endregion

        #region Active , Deactive , finish and close task
        public void CompleteTask(string taskLogicalName, Guid taskId)
        {
            SetStateRequest setStateRequest = new SetStateRequest();

            setStateRequest.EntityMoniker = new EntityReference(taskLogicalName, taskId);

            setStateRequest.State = new OptionSetValue(1);
            setStateRequest.Status = new OptionSetValue(5);

            // Execute the Response
            SetStateResponse setStateResponse = (SetStateResponse)crmAccess.Execute(setStateRequest);
        }
        public void FinalizeInstance(string processFlowSchemaName, Guid processId, ILogger Tracer)
        {
            if (processFlowSchemaName == "phonetocaseprocess") { return; }
            if (processFlowSchemaName == "ldv_bpfcasemanagementfeedback") { return; }
            var stateRequest = new SetStateRequest
            {
                EntityMoniker = new EntityReference(processFlowSchemaName, processId),
                State = new OptionSetValue(1), // Inactive.
                Status = new OptionSetValue(2) // Finished.
            };
            crmAccess.Execute(stateRequest);
            Tracer.LogComment(this.GetType().FullName, $"Instance  of logical name {processFlowSchemaName} and id {processId} is finalized", SeverityLevel.Info);

        }
        public void DeactivateRecord(string entityShemaName, Guid recordId, ILogger Tracer)//, IOrganizationService organizationService)
        {
            var cols = new string[] { RequestEntity.statecode, RequestEntity.statuscode };// new ColumnSet(new[] { "statecode", "statuscode" });

            //Check if it is Active or not
            var entity = crmAccess.RetrieveEntityWithColumns(new EntityReference(entityShemaName, recordId), cols);//.Retrieve(entityName, recordId, cols);

            if (entity != null && entity.GetAttributeValue<OptionSetValue>(RequestEntity.statecode).Value == 0)
            {
                Tracer.LogComment(this.GetType().FullName, $"Entity statecode = 0 -- active", SeverityLevel.Info);

                int status = 2;
                if (entity.LogicalName != "incident")
                {
                    //status = 5;
                    //CloseIncident(new EntityReference(entityShemaName, recordId));

                    //StateCode = 1 and StatusCode = 2 for deactivating 
                    SetStateRequest setStateRequest = new SetStateRequest()
                    {
                        EntityMoniker = new EntityReference
                        {
                            Id = recordId,
                            LogicalName = entityShemaName,
                        },
                        State = new OptionSetValue(1),// Inactive.
                        Status = new OptionSetValue(status)// deactive.
                    };
                    crmAccess.Execute(setStateRequest);
                    Tracer.LogComment(this.GetType().FullName, $"Entity deactivated ", SeverityLevel.Info);

                }
            }
        }

        private void SetState(EntityReference caseReference)
        {
            // Open the incident

            // Create the Request Object
            SetStateRequest state = new SetStateRequest();

            // Set the Request Object's Properties
            state.State = new OptionSetValue(1);// (int)IncidentState.Active);
            state.Status = new OptionSetValue(5);// (int)incident_statuscode.WaitingforDetails);

            // Point the Request to the case whose state is being changed
            state.EntityMoniker = caseReference;

            // Execute the Request
            SetStateResponse stateSet = (SetStateResponse)crmAccess.Execute(state);
        }


        private void CloseIncident(EntityReference caseReference)
        {
            // Close the Incident

            // Create resolution for the closing incident
            Entity resolution = new Entity(caseReference.LogicalName, caseReference.Id);
            // Create the request to close the incident, and set its resolution to the 
            // resolution created above
            CloseIncidentRequest closeRequest = new CloseIncidentRequest();
            closeRequest.IncidentResolution = resolution;

            // Set the requested new status for the closed Incident
            closeRequest.Status = new OptionSetValue((int)Incident_statuscode.ProblemSolved);

            // Execute the close request
            CloseIncidentResponse closeResponse = (CloseIncidentResponse)crmAccess.Execute(closeRequest);
        }
        //Activate a record
        public static void ActivateRecord(string entityName, Guid recordId, IOrganizationService organizationService)
        {
            var cols = new ColumnSet(new[] { "statecode", "statuscode" });

            //Check if it is Inactive or not
            var entity = organizationService.Retrieve(entityName, recordId, cols);

            if (entity != null && entity.GetAttributeValue<OptionSetValue>("statecode").Value == 1)
            {
                //StateCode = 0 and StatusCode = 1 for activating Account or Contact
                SetStateRequest setStateRequest = new SetStateRequest()
                {
                    EntityMoniker = new EntityReference
                    {
                        Id = recordId,
                        LogicalName = entityName,
                    },
                    State = new OptionSetValue(0),
                    Status = new OptionSetValue(1)
                };
                organizationService.Execute(setStateRequest);
            }
        }
        #endregion

        #region Add these methods in  Dal after merge

        /// <summary>
        /// This method used to get the index of which routing configuration valid then get its assigning ( from role configuration or from spacific field)
        /// </summary>
        /// <param name="stageRoutingConfigurationRecords"></param>
        /// <param name="targetRequest"></param>
        /// <returns></returns>
        public EntityReference RetrieveRoutingAssigningRecord(List<Entity> stageRoutingConfigurationRecords
             , Entity targetRequest, Guid stageConfigurationId, ITracingService tracingService)
        {
            tracingService.Trace($"in   RetrieveRoutingAssigningRecord  ");
            int index = CheckConditionAndGetValidIndex(stageRoutingConfigurationRecords, targetRequest, tracingService);
            EntityReference assingingLookup = new EntityReference(null);
            if (index != -1)
            {
                /////Get type 
                if (stageRoutingConfigurationRecords[index].Contains("routingconfiguration.ldv_typecode"))
                {
                    OptionSetValue type
                        = (OptionSetValue)((Microsoft.Xrm.Sdk.AliasedValue)stageRoutingConfigurationRecords[index].Attributes["routingconfiguration.ldv_typecode"]).Value;
                    int typeValue = type.Value;
                    object filedName = null;
                    if (typeValue == (int)RoutingConfigurationType.RoleConfiguration)
                    {
                        tracingService.Trace($"in   RoleConfiguration  ");

                        EntityReference roleConfigurationLookup = stageRoutingConfigurationRecords[index].Contains("routingconfiguration.ldv_roleconfigurationid") ?
                            (EntityReference)((Microsoft.Xrm.Sdk.AliasedValue)stageRoutingConfigurationRecords[index].Attributes["routingconfiguration.ldv_roleconfigurationid"]).Value : null;
                        if (roleConfigurationLookup != null)
                        {
                            ////method to get team or user or queue from role configuration
                            assingingLookup = GetRoleConfigurationFields(new Entity(roleConfigurationLookup.LogicalName, roleConfigurationLookup.Id));
                        }
                    }
                    else if (typeValue == (int)RoutingConfigurationType.FieldName)
                    {
                        tracingService.Trace($"in   Type.FieldName  ");

                        filedName = stageRoutingConfigurationRecords[index].Contains("routingconfiguration.ldv_fieldname") ?
                            ((Microsoft.Xrm.Sdk.AliasedValue)stageRoutingConfigurationRecords[index].Attributes["routingconfiguration.ldv_fieldname"]).Value : null;
                        ////   get fieldschema Name and check if it exist in entity
                        if (filedName != null)
                        {
                            var substituted = Utilities.CrmStringHandler.SubstituteToAttribute(targetRequest.ToEntityReference(), filedName.ToString(), OrganizationService);
                        tracingService.Trace($"in  substituted {substituted}  ");

                            if (substituted != null)
                                assingingLookup = (EntityReference)substituted;
                        }
                    }
                    else if (typeValue == (int)RoutingConfigurationType.CurrentStageOwner)
                    {
                        tracingService.Trace($"in   Type.CurrentStageOwner  ");

                        List<Entity> task = RetrieveStageTaskRelatedToTargetFetch(stageConfigurationId, targetRequest.Id);
                        if (task.Count > 0)
                        {
                            if (task[0].Attributes.Contains(TaskEntity.DecisionMadeby))
                            {
                                if (task[0].GetAttributeValue<EntityReference>(TaskEntity.DecisionMadeby)?.Id != Guid.Empty)
                                {
                                    assingingLookup.Id = task[0].GetAttributeValue<EntityReference>(TaskEntity.DecisionMadeby).Id;
                                    assingingLookup.LogicalName = "systemuser";
                                }
                            }
                        }
                    }
                }
                else
                {
                    tracingService.Trace($"type code empty");
                }
            }
            return assingingLookup;
        }

        /// <summary>
        /// This method used to Discover Field Type (team or user or queue)
        /// </summary>
        /// <param name="fieldValueLogicalName"></param>
        /// <returns></returns>
        public string DiscoverFieldType(string fieldValueLogicalName)
        {
            try
            {

                // log.Log("fieldValueLogicalName: " + fieldValueLogicalName, LogLevel.Debug);
                if (fieldValueLogicalName == AssigningRouting.User)
                {
                    return AssigningRouting.User;
                }
                else if (fieldValueLogicalName == AssigningRouting.Team)
                {
                    return AssigningRouting.Team;
                }
                else if (fieldValueLogicalName == AssigningRouting.Queue)
                {
                    return AssigningRouting.Queue;
                }
                else if (fieldValueLogicalName == AssigningRouting.RoleConfiguration)
                {
                    return AssigningRouting.RoleConfiguration;
                }
                else
                {
                    //log.Log("Unknown Field Type", LogLevel.Debug);
                    return null;

                }
            }
            catch (Exception ex)
            {
                //log.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //  log.LogFunctionEnd();
            }

        }

        #endregion

        #region IsValidCurrentProcess
        public bool IsValidCurrentProcess(string entityReferenceId, string entityReferenceName,Guid  serviceId , ITracingService tracingService)
        {
            tracingService.Trace($"in IsValidCurrentProcess");

            bool isBpfValid = false;
            if (entityReferenceId != null && entityReferenceName != null && entityReferenceId != string.Empty && entityReferenceName != string.Empty)
            {
                tracingService.Trace($"1");
                Entity target = crmAccess.RetrivePrimaryEntityOfBpf(entityReferenceName, new Guid(entityReferenceId));
                tracingService.Trace($"2");
                EntityReference stageConf = RetriveStageConfigurarionByProcessStage(new Guid(entityReferenceId), entityReferenceName, serviceId, tracingService);
                tracingService.Trace($"3");

                if (target?.Id == Guid.Empty) return false;
                if (stageConf?.Id == Guid.Empty) return false;
                tracingService.Trace($"Request id : {target.Id} - Request schemaName : {target.LogicalName}");
                //tracingService.Trace($"stageConf id : {stageConf.Id} - stageConf schemaName : {stageConf.LogicalName}");

                isBpfValid = RetrieveCurrentProcessFromRequest(target, stageConf, tracingService);
            }
            return isBpfValid;
        }
        public bool RetrieveCurrentProcessFromRequest(Entity target, EntityReference stageConf, ITracingService tracingService)
        {
            tracingService.Trace($"in RetrieveCurrentProcessFromRequest");
            bool isBpfValid = false;

            //get process from request
            var requestQuery = new QueryExpression(target.LogicalName);
            requestQuery.Distinct = true;
            requestQuery.ColumnSet.AddColumns(RequestEntity.Process);
            requestQuery.Criteria.AddCondition(target.LogicalName + "id", ConditionOperator.Equal, target.Id);
            EntityCollection requestEntities = OrganizationService.RetrieveMultiple(requestQuery);

            if (!requestEntities.Entities.Any()) return false;
            Entity requestEntity = requestEntities.Entities[0];
            EntityReference requestProcess = requestEntity.Contains(RequestEntity.Process) ? requestEntity.GetAttributeValue<EntityReference>(RequestEntity.Process) : null;

            if (requestProcess?.Id == Guid.Empty) return false;
            tracingService.Trace($"requestProcess id : {requestProcess.Id} - requestProcess schemaName : {requestProcess.LogicalName}");
            tracingService.Trace($"stage  : {stageConf.Id} - stageConf schemaName : {stageConf.LogicalName}");

            //get process from stage configuration             
            var stageConfiguration = new QueryExpression(StageConfigurationEntity.LogicalName);
            stageConfiguration.Distinct = true;
            stageConfiguration.ColumnSet.AddColumns(StageConfigurationEntity.Process);
            stageConfiguration.Criteria.AddCondition(StageConfigurationEntity.StageConfiguration, ConditionOperator.Equal, stageConf.Id);
            EntityCollection stageConfigurationEntities = OrganizationService.RetrieveMultiple(stageConfiguration);
            if (!stageConfigurationEntities.Entities.Any()) return false;
            tracingService.Trace($"stageConfigurationEntities.count {stageConfigurationEntities.Entities.Count}");
            Entity stageConfigurationEntity = stageConfigurationEntities.Entities[0];
            //tracingService.Trace($"stageConfigurationEntity {stageConfigurationEntity.Id}");

            EntityReference stageConfigurationProcess = stageConfigurationEntity.Contains(StageConfigurationEntity.Process) ? stageConfigurationEntity.GetAttributeValue<EntityReference>(StageConfigurationEntity.Process) : null;

            if (stageConfigurationProcess?.Id == Guid.Empty) return false;
            tracingService.Trace($"stageConfigurationProcess id : {requestProcess.Id} - stageConfigurationProcess schemaName : {stageConfigurationProcess.LogicalName}");

            if (requestProcess?.Id == stageConfigurationProcess?.Id)
            {
                tracingService.Trace($" Both are equal");
                return true;
            }
            return isBpfValid;
        }


        #endregion
    }
}

