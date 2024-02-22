using Linkdev.MOE.CRM.DAL;
using Linkdev.CRM.DataContracts;
using LinkDev.Common.Crm.Cs.NotificationTemplates.Entities;
using LinkDev.Libraries.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Logger;

namespace LinkDev.Common.Crm.Cs.NotificationTemplates.Helper
{
    public class StageConfigurationsNotificationsBLL //: BllBase
    {

        #region Variables
        CRMAccessLayer CRMAccessLayer;
        EntityReference RegardingObject;
        List<NotificationConfigurations> NotificationConfigtLst;
        ITracingService tracingService;
        IOrganizationService OrganizationService;
        #endregion

        #region Constructor
        public StageConfigurationsNotificationsBLL(IOrganizationService Service, ITracingService tracing, EntityReference RegardingObject)
        {
            OrganizationService = Service;
            CRMAccessLayer = new CRMAccessLayer(OrganizationService);
            this.RegardingObject = RegardingObject;
        }

        #endregion
        #region Methods
        /// <summary>
        /// get all info about configuration notification by stage config id
        /// </summary>
        /// <param name="stageConfigurationId"></param> syage config id
        public void SendStageNotification(Guid stageConfigurationId) 
        {
            try
            {
                List<NotificationConfigurations> NotificationConfigurationsLst = new List<NotificationConfigurations>();
                // retrive all notification configuration (to role/field value , cc role/field value , conditions , notification template list)
                RetrieveValidNotificationConfigByStageConfig(stageConfigurationId);

                if (NotificationConfigtLst != null && NotificationConfigtLst.Count > 0)
                {
                    SendNotificationCommonBLL SendNotificationCommonBLL = new SendNotificationCommonBLL(OrganizationService, tracingService );
                    SendNotificationCommonBLL.SendNotificationTemplate(NotificationConfigtLst, new EntityReference(RegardingObject.LogicalName, RegardingObject.Id));
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"SendStageNotification ex {ex}");

               
            }
        }

        /// <summary>
        /// Retrieve all notification configs by stage config id
        /// </summary>
        /// <param name="stageConfigurationId"></param>stage config id
        /// <returns></returns>

        public List<NotificationConfigurations> RetrieveValidNotificationConfigByStageConfig(Guid stageConfigurationId) 
        {
            NotificationConfigtLst = new List<NotificationConfigurations>();
            try
            {
                #region retrieve notification configuration fields:
                var QEldv_stagenotificationconfiguration = new QueryExpression("ldv_stagenotificationconfiguration");
                QEldv_stagenotificationconfiguration.Criteria.AddCondition("ldv_stageconfigurationid", ConditionOperator.Equal, stageConfigurationId);

                var QEldv_stagenotificationconfiguration_ldv_notificationconfiguration = QEldv_stagenotificationconfiguration.AddLink("ldv_notificationconfiguration", "ldv_notificationconfigurationid", "ldv_notificationconfigurationid");

                QEldv_stagenotificationconfiguration_ldv_notificationconfiguration.Columns.AddColumns("ldv_notificationtemplateid", "ldv_stageconditionid", "ldv_ccroleconfigurationid", "ldv_ccfieldvalue", "ldv_toroleconfigurationid", "ldv_tofieldvalue", "ldv_notificationconfigurationid");
                var stageNotifications = OrganizationService.RetrieveMultiple(QEldv_stagenotificationconfiguration); 
                #endregion
                if (stageNotifications.Entities.Count == 0) return null;

                foreach (var item in stageNotifications.Entities)
                {
                    var notificationConfigEntity = ParseNotificationConfigValues(item);
                    if (notificationConfigEntity != null)
                        NotificationConfigtLst.Add(notificationConfigEntity);
                }
            }
            catch (Exception ex)
            { 
                tracingService.Trace($"RetrieveValidNotificationConfigByStageConfig ex {ex}");

            }


            return NotificationConfigtLst;
        }

        /// <summary>
        /// create class of notification contract (Parse Notification Config Values)
        /// </summary>
        /// <param name="stageNotificationConfig"></param> notification configuration record //to be renamed
        /// <returns></returns>
        public NotificationConfigurations ParseNotificationConfigValues(Entity stageNotificationConfig) 
        {
            #region Parse Notification Config Values 
            if (stageNotificationConfig == null) return null;
            NotificationConfigurations NotificationConfigurations = new NotificationConfigurations();
            try
            {
                NotificationConfigurations.toParty = new List<EntityReference>();
                NotificationConfigurations.ccParty = new List<EntityReference>();
                // to do 
                NotificationConfigurations.notificationTemp = new Entity();

                // notification template:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_notificationtemplateid"))
                {
                    var NotificationTemplateId = ((EntityReference)((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_notificationtemplateid"]).Value).Id;
                    NotificationConfigurations.notificationTempid = NotificationTemplateId;
                }
                else return null;
                // stgae notificaton configuration:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_notificationconfigurationid"))
                {
                    var notificationConfigurationId = ((Guid)((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_notificationconfigurationid"]).Value);
                    var notificationConfigurationEntity = ((AliasedValue)(stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_notificationconfigurationid"])).EntityLogicalName;
                    NotificationConfigurations.NotificationConfiguration = new EntityReference(notificationConfigurationEntity, notificationConfigurationId);
                }
                // retrieve condition and execute it 
                //condition
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_stageconditionid"))
                {
                    Guid stageConditionId = ((EntityReference)((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_stageconditionid"]).Value).Id;
                    //retrieve then execute the condition and check the returned value execute

                    var isConditionMet = ValidateCondition(stageConditionId);
                    if (isConditionMet == false) return null;
                }

                // add to Role to the to party:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_toroleconfigurationid"))
                {
                    var toRole = ((EntityReference)((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_toroleconfigurationid"]).Value).Id;
                    var toRoleLookup = RetrieveRoleConfiguration(toRole);
                    if (toRoleLookup != null)
                        NotificationConfigurations.toParty.Add(toRoleLookup);
                }

                // cc role to the cc party:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_ccroleconfigurationid"))
                {
                    var CCRole = ((EntityReference)((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_ccroleconfigurationid"]).Value).Id;
                    var ccRoleLookup = RetrieveRoleConfiguration(CCRole);
                    if (ccRoleLookup != null)
                        NotificationConfigurations.ccParty.Add(ccRoleLookup);
                }
                // add to field value to the to party:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_tofieldvalue"))
                {
                    var tofieldvalue = (((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_tofieldvalue"]).Value).ToString();
                    var fieldValueLookup = RetrieveFieldValues(tofieldvalue);
                    if (fieldValueLookup != null || fieldValueLookup.Id != Guid.Empty)
                        NotificationConfigurations.toParty.Add(fieldValueLookup);
                }
                //add cc field value to the cc party:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_ccfieldvalue"))
                {
                    var CcFieldValue = ((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_ccfieldvalue"]).Value.ToString();
                    var CCFieldValueLookup = RetrieveFieldValues(CcFieldValue);
                    if (CCFieldValueLookup != null && CCFieldValueLookup.Id != Guid.Empty)
                        NotificationConfigurations.ccParty.Add(CCFieldValueLookup);
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"ParseNotificationConfigValues ex {ex}");

            }

            // create own class :
            return NotificationConfigurations;
            #endregion
        }
      
        /// <summary>
        /// Validate Notification Configuration Record , to retreive to , cc , notification template , condition
        /// </summary>
        /// /// <param name="notificationConfigurationId"></param>
        /// notification Configuration Id
        /// <returns></returns>
        /// <summary>
        /// get to  from role configuration entity
        /// </summary>
        /// <param name="roleIdLst"></param>list of role configuration ids
        /// <returns></returns>
        public EntityReference RetrieveRoleConfiguration(Guid roleId)
        {
            EntityReference To = new EntityReference();
            try
            {
                // retrieve  from role configuration
                #region retrieve user, team, queue from role cofiguration:
                var roleConfiguration = CRMAccessLayer.RetrieveEntity(roleId.ToString(), "ldv_roleconfiguration", new string[] { "ldv_queue", "ldv_user", "ldv_type" });
                #endregion
                if (roleConfiguration == null) return null;
                if (roleConfiguration.Attributes.Contains("ldv_type"))
                {
                    var type = ((OptionSetValue)roleConfiguration.Attributes["ldv_type"]).Value;
                    if (type == 1 && roleConfiguration.Attributes.Contains("ldv_user"))
                        To = (EntityReference)(roleConfiguration.Attributes["ldv_user"]);
                    else if (type == 3 && roleConfiguration.Attributes.Contains("ldv_queue"))
                        To = (EntityReference)(roleConfiguration.Attributes["ldv_queue"]);
                }
               
            }
            catch (Exception ex)
            {
                tracingService.Trace($"RetrieveRoleConfiguration ex {ex}");

            }


            return To;
        }

        /// <summary>
        /// Retrive to of  field value from the RegardingObject entity
        /// </summary>
        /// <param name="fieldValueLst"></param> represent field values list 
        /// <returns></returns>
        public EntityReference RetrieveFieldValues(string fieldValue)
        {

            EntityReference lookupFieldValue = new EntityReference();
            try
            {
                if (fieldValue == null || fieldValue == string.Empty) return null;
                if (fieldValue.Contains(":"))
                {
                    string fieldName = string.Empty;
                    string lookupName = string.Empty;
                    string[] splitedDynamicValue = fieldValue.Split(':');

                    EntityReference regarding = new EntityReference(RegardingObject.LogicalName, RegardingObject.Id);
                    for (int i = 0; i < splitedDynamicValue.Length - 1; i++)
                    {
                        lookupName = splitedDynamicValue[i];
                        fieldName = splitedDynamicValue[i + 1];
                    var retrievedCols = new string[] { lookupName };
                        Entity currEntity = CRMAccessLayer.RetrieveEntity(regarding.Id.ToString(), regarding.LogicalName, retrievedCols);
                    if (currEntity == null) return null;
                    if (currEntity.Attributes.Contains(lookupName))
                    {
                        string lookupEntityName = ((EntityReference)currEntity.Attributes[lookupName]).LogicalName; 
                        Guid lookupEntityId = ((EntityReference)currEntity.Attributes[lookupName]).Id; 
                        var retrievedLookupCols = new string[] { fieldName };
                        Entity lookupEntity = CRMAccessLayer.RetrieveEntity(lookupEntityId.ToString(), lookupEntityName, retrievedLookupCols);
                        if (lookupEntity == null) return null;
                        if (lookupEntity.Attributes.Contains(fieldName))
                        {
                                if (i == splitedDynamicValue.Length - 2)
                                {
                            lookupFieldValue = (EntityReference)(lookupEntity.Attributes[fieldName]);
                        }
                                else
                                {
                                    regarding = lookupEntity.ToEntityReference();
                                }
                            }
                        }
                    }
                }
                else
                {
                    var retrievedCols = new string[] { fieldValue };
                    Entity currEntity = CRMAccessLayer.RetrieveEntity(RegardingObject.Id.ToString(), RegardingObject.LogicalName, retrievedCols);
                    if (currEntity == null) return null;
                    if (currEntity.Attributes.Contains(fieldValue))
                    {
                        lookupFieldValue = (EntityReference)(currEntity.Attributes[fieldValue]); 
                    }
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"RetrieveFieldValues ex {ex}");

            }


            return lookupFieldValue;
        }
        /// <summary>
        /// retrieve fetch xml conditions by condition id then validate if it return yes so the condition meet else the condition doesnot meet
        /// </summary>
        /// parameters: stageConditionId (stage condition id)
        /// <returns></returns>
        public bool ValidateCondition(Guid stageConditionId)
        {
            bool isConditionMet = true;
            try
            {
                if (stageConditionId == Guid.Empty && stageConditionId == null) return isConditionMet;
                #region retrieve fetch xml from stage condition by stage condition id
                var condition = CRMAccessLayer.RetrieveEntity(stageConditionId.ToString(), "ldv_stagecondition", new string[] { "ldv_condition" });
                #endregion
                // so if there was no condition record it will return true to retrieve the other satge notification value
                if (condition != null)
                {
                    if (condition.Contains("ldv_condition"))
                    {
                        string fetchXml = condition.Attributes["ldv_condition"].ToString();
                        // to check if it returns yes so the condition is vakidated on the record and so i will send the flage(isConditionMet) with yes vaue or with the output of the below function, if there is no recordes the falge will be set to no.
                        isConditionMet = CRMAccessLayer.IsConditionMet(OrganizationService, fetchXml, RegardingObject, false);
                    }
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"ValidateCondition ex {ex}");

            }

            return isConditionMet;
        }
        #endregion
    }
}
