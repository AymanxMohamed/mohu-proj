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
using Linkdev.CRM.DataContracts.Enums;
using LinkDev.Common.Crm.Utilities;
using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Workflow.Activities;
using System.Xml.Linq;

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
       
        Language language;
         
        NotificationConfigurations NotificationConfigrecipients;
        #endregion

        #region Constructor
        public StageConfigurationsNotificationsBLL(IOrganizationService Service, ITracingService tracing, EntityReference regardingObject)
        {

            OrganizationService = Service;
            CRMAccessLayer = new CRMAccessLayer(OrganizationService, tracing);
             RegardingObject = regardingObject;
            tracingService = tracing;
            tracingService.Trace($" in StageConfigurationsNotificationsBLL constractor ");
            language = Language.Arabic;
          //  commonBLL = new CommonBLL(Service, tracing);
       
        }

        #endregion
        #region Methods
        /// <summary>
        /// get all info about configuration notification by stage config id
        /// </summary>
        /// <param name="stageConfigurationId"></param> syage config id
        public void SendStageNotification(Guid stageConfigurationId, EntityReference regardingObject) 
        {
            RegardingObject = regardingObject;
            tracingService.Trace($" in SendStageNotification 1 ");
            try
            {
                List<NotificationConfigurations> NotificationConfigurationsLst = new List<NotificationConfigurations>();
                // retrive all notification configuration (to role/field value , cc role/field value , conditions , notification template list)
                RetrieveValidNotificationConfigByStageConfig(stageConfigurationId);
                tracingService.Trace($" in SendStageNotification 2 ");
                if (NotificationConfigtLst != null && NotificationConfigtLst.Count > 0)
                {
                    tracingService.Trace($" NotificationConfigtLst.Count {NotificationConfigtLst.Count} ");
              

                  //  SendNotificationCommonBLL SendNotificationCommonBLL = new SendNotificationCommonBLL(OrganizationService, tracingService );
                tracingService.Trace($" before SendNotificationTemplate 3 ");

                     SendNotificationTemplate(NotificationConfigtLst, new EntityReference(RegardingObject.LogicalName, RegardingObject.Id));
                  tracingService.Trace($" after SendNotificationTemplate 4 ");

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
            tracingService.Trace($" in RetrieveValidNotificationConfigByStageConfig  ");

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
                tracingService.Trace($" stageNotifications.Entities.Count {stageNotifications.Entities.Count} ");

                foreach (var item in stageNotifications.Entities)
                {
                    tracingService.Trace($" item id: {item.Id} ");

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
            tracingService.Trace($" in ParseNotificationConfigValues ");

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
                    tracingService.Trace($"   NotificationTemplateId {NotificationTemplateId} ");

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
                    tracingService.Trace($"   isConditionMet {isConditionMet} ");

                    if (isConditionMet == false) return null;
                }

                // add to Role to the to party:
                if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_toroleconfigurationid"))
                {
                    var toRole = ((EntityReference)((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_toroleconfigurationid"]).Value).Id;
                    var toRoleLookup = RetrieveRoleConfiguration(toRole);
                    tracingService.Trace($"   toRoleLookup LogicalName {toRoleLookup.LogicalName} ");

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
                    tracingService.Trace($"   fieldValueLookup {fieldValueLookup} ");

                    if (fieldValueLookup != null || fieldValueLookup.Id != Guid.Empty)
                    {
                        NotificationConfigurations.toParty.Add(fieldValueLookup);
                        tracingService.Trace($"   fieldValueLookup LogicalName {fieldValueLookup.LogicalName} ");

                    }
                }
                //add cc field value to the cc party:
                //if (stageNotificationConfig.Attributes.Contains("ldv_notificationconfiguration1.ldv_ccfieldvalue"))
                //{
                //    tracingService.Trace($" Contains( ldv_notificationconfiguration1.ldv_ccfieldvalue) ");

                //    var CcFieldValue = ((AliasedValue)stageNotificationConfig.Attributes["ldv_notificationconfiguration1.ldv_ccfieldvalue"]).Value.ToString();
                //    var CCFieldValueLookup = RetrieveFieldValues(CcFieldValue);
                //    if (CCFieldValueLookup != null && CCFieldValueLookup.Id != Guid.Empty)
                //        NotificationConfigurations.ccParty.Add(CCFieldValueLookup);
                //}
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
            tracingService.Trace($" in RetrieveRoleConfiguration ");

            EntityReference To = new EntityReference();
            try
            {
                // retrieve  from role configuration
                #region retrieve user, team, queue from role cofiguration:
                var roleConfiguration = CRMAccessLayer.RetrieveEntity(roleId.ToString(), "ldv_roleconfiguration", new string[] { "ldv_queue", "ldv_user", "ldv_type", "ldv_team" });
                #endregion
                if (roleConfiguration == null) return null;
                if (roleConfiguration.Attributes.Contains("ldv_type"))
                {
                    var type = ((OptionSetValue)roleConfiguration.Attributes["ldv_type"]).Value;
                    if (type == 1 && roleConfiguration.Attributes.Contains("ldv_user"))
                        To = (EntityReference)(roleConfiguration.Attributes["ldv_user"]);
                    else if (type == 3 && roleConfiguration.Attributes.Contains("ldv_queue"))
                        To = (EntityReference)(roleConfiguration.Attributes["ldv_queue"]);
                    else if (type == 2 && roleConfiguration.Attributes.Contains("ldv_team"))
                        To = (EntityReference)(roleConfiguration.Attributes["ldv_team"]);
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
            tracingService.Trace($" in ValidateCondition ");

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


        #region send notification common bll methods:

        /// <summary>
        /// get the whole receiptients then send botifications to them
        /// </summary>
        /// <param name="NotificationConfigurationsLst"></param> 
        /// <param name="RegardingObject"></param>the context 
        public void SendNotificationTemplate(List<NotificationConfigurations> notificationConfigurationsList, EntityReference regardingObject)
        {
            #region variables
            List<NotificationConfigurations> actualNotificationConfigLst = new List<NotificationConfigurations>();
            #endregion
            tracingService.Trace($" in SendNotificationTemplate");

            try
            {
                
                tracingService.Trace($" before RetriveEmailFrom");

                // retrieve from attribute from moe configuration:
                EntityReference from =  RetriveEmailFrom();
                tracingService.Trace($"afetr from {from}");

                if (from == null) return;
                tracingService.Trace($" from LogicalName {from.LogicalName} , id {from.Id}");
                if (notificationConfigurationsList == null || notificationConfigurationsList.Count == 0) return;
                foreach (var item in notificationConfigurationsList)
                {
                    var notification = GetNotificationRecipientsList(item);
                    tracingService.Trace($" notification {notification}");
                    if (notification != null)
                    {
                        actualNotificationConfigLst.Add(notification);
                    }
                }
                if (actualNotificationConfigLst.Count > 0)
                {// to create and send notifications 
                    foreach (var notifctaionConfigurationTemplate in actualNotificationConfigLst)
                    {
                        // send eamil 
                        if (notifctaionConfigurationTemplate.notificationTemp.GetAttributeValue<bool>("ldv_useemail"))
                        {
                            tracingService.Trace($" before CreateAndSendEmail");
                             CreateAndSendEmail(from, notifctaionConfigurationTemplate.Language, notifctaionConfigurationTemplate.notificationTemp, regardingObject, notifctaionConfigurationTemplate.toParty, notifctaionConfigurationTemplate.ccParty, null, notifctaionConfigurationTemplate.NotificationConfiguration,null, null);
                            tracingService.Trace($" after CreateAndSendEmail");

                        }
                        // send portal notifications and sms:
                        if (notifctaionConfigurationTemplate.notificationTemp.GetAttributeValue<bool>("ldv_usesms"))
                        {
                            tracingService.Trace($" before CreateSMSAndPortalNotificationToContactList");
                             CreateSMSAndPortalNotificationToContactList(notifctaionConfigurationTemplate.contactLst, notifctaionConfigurationTemplate.notificationTemp, regardingObject, notifctaionConfigurationTemplate.NotificationConfiguration);
                            tracingService.Trace($" after CreateSMSAndPortalNotificationToContactList");

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                tracingService.Trace($"SendNotificationTemplate ex {ex}");

            }
        }
        /// <summary>
        /// get receiptients for each record
        /// </summary>
        /// <param name="notificationConfig"></param>
        /// <returns></returns>
        public NotificationConfigurations GetNotificationRecipientsList(NotificationConfigurations notificationConfig)
        {
            try
            {

                if (notificationConfig == null) return null;
                bool notifySendEmail = false;

                #region variables:
                NotificationConfigrecipients = new NotificationConfigurations();
                NotificationConfigrecipients.contactLst = new List<Entity>();
                NotificationConfigrecipients.notificationTemp = new Entity();
                NotificationConfigrecipients.ccParty = new List<EntityReference>();
                NotificationConfigrecipients.toParty = new List<EntityReference>();
                #endregion
                #region retrieve notification Template : 

                Entity notificationTemplate = CRMAccessLayer.RetrieveEntity(notificationConfig.notificationTempid.ToString(), "ldv_notificationtemplate", new string[] { });
                if (notificationTemplate == null) return null;
                notifySendEmail = notificationTemplate.Attributes.Contains("ldv_useemail") ? Convert.ToBoolean(notificationTemplate.Attributes["ldv_useemail"]) : false;
                NotificationConfigrecipients.Language = Language.Arabic;
                NotificationConfigrecipients.notificationTemp = notificationTemplate;
                NotificationConfigrecipients.NotificationConfiguration = notificationConfig.NotificationConfiguration;
                #endregion
                #region  email
                // get to receiptients for email and 
                var toParty = GetValidEmailToPartyAndSMSRecipient(notificationConfig, notifySendEmail);
                if (toParty != null)
                    NotificationConfigrecipients.toParty = toParty;
                // if notify send mail false , donot considder the cc 
                if (notifySendEmail)
                {
                    var ccParty = GetValidEmailCCParty(notificationConfig);
                    if (ccParty != null)
                        NotificationConfigrecipients.ccParty = ccParty;
                }

                #endregion
            }
            catch (Exception ex)
            {
                tracingService.Trace($"GetNotificationRecipientsList ex {ex}");

            }
            return NotificationConfigrecipients;
        }
        /// <summary>
        /// get valid email recipients
        /// </summary>
        /// <param name="notificationConfig"></param>
        /// <param name="notifySendEmail"></param>
        /// <returns></returns>
        List<EntityReference> GetValidEmailToPartyAndSMSRecipient(NotificationConfigurations notificationConfig, bool notifySendEmail)
        {
            List<EntityReference> toParty = new List<EntityReference>();
            try
            {
                if (notificationConfig == null) return null;
                foreach (var item in notificationConfig.toParty)
                {
                    switch (item.LogicalName)
                    {
                        case "contact":
                            // retrieve contact data 
                            var Contact = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "contact", new string[] { "ldv_preferredlanguagecode", "mobilephone", "emailaddress1" });
                            if (Contact == null) continue;
                            // if contact has email add the contact to the to list party
                            if (notifySendEmail && Contact.Attributes.Contains("emailaddress1"))
                                toParty.Add(item);
                            // add contact data to be used in create smsm and portal notifications
                            NotificationConfigrecipients.contactLst.Add(Contact);
                            // get contact preferrred language
                            if (Contact.Attributes.Contains("ldv_preferredlanguagecode"))
                                NotificationConfigrecipients.Language = (Language)(((OptionSetValue)(Contact.Attributes["ldv_preferredlanguagecode"])).Value);
                            break;
                        case "account":
                            if (!notifySendEmail) return null;
                            Entity Account = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "account", new string[] { "emailaddress1" });
                            if (Account != null && Account.Attributes.Contains("emailaddress1"))
                                toParty.Add(item);
                            break;
                        case "systemuser":
                            if (!notifySendEmail) return null;
                            Entity User = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "systemuser", new string[] { /*"preferredphonecode",*/ "internalemailaddress", "ldv_preferredlanguagecode" });
                            if (User != null)
                            {
                                if (User.Attributes.Contains("internalemailaddress"))
                                    toParty.Add(item);
                                if (User.Attributes.Contains("ldv_preferredlanguagecode"))
                                    NotificationConfigrecipients.Language = (Language)(((OptionSetValue)(User.Attributes["ldv_preferredlanguagecode"])).Value);
                            }
                            break;
                        case "queue":
                            if (!notifySendEmail) return null;
                            Entity Queue = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "queue", new string[] { "emailaddress", "ldv_preferredlanguagecode" });
                            if (Queue != null)
                            {
                                if (Queue.Attributes.Contains("emailaddress"))
                                    toParty.Add(item);
                                if (Queue.Attributes.Contains("ldv_preferredlanguagecode"))
                                    NotificationConfigrecipients.Language = (Language)(((OptionSetValue)(Queue.Attributes["ldv_preferredlanguagecode"])).Value);
                            }
                            break;
                        default:
                            if (!notifySendEmail) return null;
                            toParty.Add(item);
                            NotificationConfigrecipients.Language = Language.Arabic;
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"GetValidEmailToPartyAndSMSRecipient ex {ex}");

            }

            return toParty;

        }
        List<EntityReference> GetValidEmailCCParty(NotificationConfigurations notificationConfig)
        {
            List<EntityReference> ccParty = new List<EntityReference>();
            try
            {
                if (notificationConfig == null) return null;
                foreach (var item in notificationConfig.ccParty)
                {
                    switch (item.LogicalName)
                    {
                        case "contact":
                            var Contact = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "contact", new string[] { "emailaddress1" });
                            if (Contact != null && Contact.Attributes.Contains("emailaddress1"))
                                ccParty.Add(item);
                            break;
                        case "account":
                            Entity Account = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "account", new string[] { "emailaddress1" });
                            if (Account != null && Account.Attributes.Contains("emailaddress1"))
                                ccParty.Add(item);
                            break;
                        case "systemuser":
                            Entity User = CRMAccessLayer.RetrieveEntity(item.Id.ToString(), "systemuser", new string[] { "internalemailaddress" });
                            if (User != null)
                            {
                                if (User.Attributes.Contains("internalemailaddress"))
                                    ccParty.Add(item);
                            }
                            break;
                        case "queue":
                            if ( CheckTheQueueHasEmail(item.Id.ToString()))
                                ccParty.Add(item);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"GetValidEmailCCParty ex {ex}");

            }
            return ccParty;
        }

        #endregion

        #region  Notification Template Methods
        public void SendNotificationTemplate(EntityReference ToSystemUser, /*EntityReference ToTeam,*/ EntityReference ToAccount, EntityReference ToContact, EntityReference queues, string ccText, string bccText, string toText, EntityReference NotificationTemplates, EntityReference RegardingObject , EntityReference RegardingLookup,string toEmailAddress)
        {

            #region variables
            Entity Contact = new Entity();
            List<EntityReference> ccList = new List<EntityReference>();
            List<EntityReference> bccList = new List<EntityReference>();
            List<EntityReference> toList = new List<EntityReference>();
            bool notifySendEmail = false, notifySendSMS = false, notifySendPortal = false;
            #endregion

            try
            {
                #region retrieve notification Template : 

                Entity notificationTemplate = CRMAccessLayer.RetrieveEntity(NotificationTemplates.Id.ToString(), "ldv_notificationtemplate", new string[] { });
                if (notificationTemplate == null) return;
                notifySendEmail = notificationTemplate.Attributes.Contains("ldv_useemail") ? Convert.ToBoolean(notificationTemplate.Attributes["ldv_useemail"]) : false;

                notifySendSMS = notificationTemplate.Attributes.Contains("ldv_usesms") ? Convert.ToBoolean(notificationTemplate.Attributes["ldv_usesms"]) : false;
                notifySendPortal = notificationTemplate.Attributes.Contains("ldv_useportalnotification") ? Convert.ToBoolean(notificationTemplate.Attributes["ldv_useportalnotification"]) : false;

                #endregion

                if (notifySendEmail)
                {
                    #region determine to section is for contact or account or system user entity retrive user data : 
                    if (ToContact != null && ToContact.Id != Guid.Empty)
                    {
                        // retrieve contact data 
                        Contact = CRMAccessLayer.RetrieveEntity(ToContact.Id.ToString(), "contact", new string[] { "ldv_preferredlanguagecode", "mobilephone", "emailaddress1" });
                        if (Contact != null && Contact.Attributes.Contains("emailaddress1"))
                            toList.Add(new EntityReference("contact", ToContact.Id));
                        if (Contact.Attributes.Contains("ldv_preferredlanguagecode"))
                            language = (Language)(((OptionSetValue)(Contact.Attributes["ldv_preferredlanguagecode"])).Value);
                    }
                    else if (ToAccount != null && ToAccount.Id != Guid.Empty)
                    {
                        Entity Account = CRMAccessLayer.RetrieveEntity(ToAccount.Id.ToString(), "account", new string[] { "emailaddress1" });
                        if (Account != null && Account.Attributes.Contains("emailaddress1"))
                            toList.Add(new EntityReference("account", ToAccount.Id));

                    }
                    else if (ToSystemUser != null && ToSystemUser.Id != Guid.Empty)
                    {
                        if (ToSystemUser.LogicalName == "systemuser")
                        {
                            tracingService.Trace($"  in systemuser ");


                            Entity User = CRMAccessLayer.RetrieveEntity(ToSystemUser.Id.ToString(), "systemuser", new string[] { "ldv_preferredlanguagecode", "internalemailaddress" });
                            if (User != null)
                            {
                                if (User.Attributes.Contains("internalemailaddress"))
                                    toList.Add(new EntityReference("systemuser", ToSystemUser.Id));
                                if (User.Attributes.Contains("ldv_preferredlanguagecode"))
                                    language = (Language)(((OptionSetValue)(User.Attributes["ldv_preferredlanguagecode"])).Value);
                            }
                        }
                        else
                        {
                            if (ToSystemUser.LogicalName == "team")
                            {
                                tracingService.Trace($"  in team ");
                                var query = new QueryExpression("systemuser");
                                query.Distinct = true;
                                query.ColumnSet.AddColumns("fullname", "businessunitid", "title", "address1_telephone1", "positionid", "systemuserid");
                                query.AddOrder("fullname", OrderType.Ascending);
                                var query_teammembership = query.AddLink("teammembership", "systemuserid", "systemuserid");
                                var ab = query_teammembership.AddLink("team", "teamid", "teamid");
                                ab.EntityAlias = "ab";
                                ab.LinkCriteria.AddCondition("teamid", ConditionOperator.Equal, ToSystemUser.Id);
                                EntityCollection memebers = OrganizationService.RetrieveMultiple(query);
                                if (memebers.Entities.Count > 0)
                                {
                                    tracingService.Trace($"  # members = {memebers.Entities.Count}");
                                    foreach (var member in memebers.Entities)
                                    {
                                        Entity User = CRMAccessLayer.RetrieveEntity(member.Id.ToString(), "systemuser", new string[] { "ldv_preferredlanguagecode", "internalemailaddress" });
                                        if (User != null)
                                        {
                                            if (User.Attributes.Contains("internalemailaddress"))
                                                toList.Add(new EntityReference("systemuser", ToSystemUser.Id));
                                            if (User.Attributes.Contains("ldv_preferredlanguagecode"))
                                                language = (Language)(((OptionSetValue)(User.Attributes["ldv_preferredlanguagecode"])).Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                   
                   
                    #endregion

                    #region Send email to record Url Users:
                    else if (!string.IsNullOrEmpty(toText))
                    {
                        List<EntityReference> DynamicList = new List<EntityReference>();
                        DynamicList =  getEntityReferencesFromURLs(toText, OrganizationService);
                        if (DynamicList.Count > 0)
                        {
                            // check if all the list has emails 
                            var result =  FiltterTheListByEmail(DynamicList, true);
                            toList = result;
                        }

                    }
                    #endregion

                    #region To Queue: language default is english
                    else if (queues != null)
                    {
                        // check if the queue has email if yes so add the queue id to to list:
                        if ( CheckTheQueueHasEmail(queues.Id.ToString()))
                            toList.Add(queues);
                    }
                    #endregion
                    #region  ccs list emails:
                    /*To cc usrs*/
                    if (ccText != string.Empty && ccText != null)
                    {
                        var ccUsers =  getEntityReferencesFromURLs(ccText, OrganizationService);
                        // check if all the list has emails 
                        ccList =  FiltterTheListByEmail(ccUsers, false);
                    }

                    #endregion
                    #region  bccs list emails:
                    /*To bcc usrs*/
                    if (bccText != string.Empty && bccText != null)
                    {
                        var bccUsers =  getEntityReferencesFromURLs(bccText, OrganizationService);
                        // check if all the list has emails 
                        bccList =  FiltterTheListByEmail(bccUsers, false);
                    }

                    #endregion
                    // retrieve from:
                    EntityReference from =  RetriveEmailFrom();
                    // to create and send email 
                     CreateAndSendEmail(from, language, notificationTemplate, RegardingObject, toList, ccList, bccList, null, RegardingLookup  , toEmailAddress );
                }
                // to create sms and portal notifications to contact
                if (ToContact != null)
                {
                    // incase notify send email = false   so the contact entity will be null so we will retrieve the contact entity....
                    if (Contact == null)
                    {
                        Contact = CRMAccessLayer.RetrieveEntity(ToContact.Id.ToString(), "contact", new string[] { "ldv_preferredlanguagecode", "mobilephone", "emailaddress1" });
                    }
                    // to create sms + portal notifications to contact
                     CreateSMSAndPortalNotificationToContact(Contact, notificationTemplate, RegardingObject, null);
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"SendNotificationTemplate ex {ex}");
            }
        }
        public void SendNotificationToRepresintatives(EntityReference Notifications, EntityReference Account, EntityReference regardingObject, string startingPattern, string endingPattern)
        {
            tracingService.Trace($" in SendNotificationToRepresintatives ");

            if (Notifications == null && Notifications.Id == Guid.Empty)
                return;

            var englishMessage = string.Empty;
            var arabicMessage = string.Empty;
            var englishSms = string.Empty;
            var arabicSms = string.Empty;
            var from =  RetriveEmailFrom();
            var notificationTemplate = CRMAccessLayer.RetrieveEntity(Notifications.Id.ToString(), "ldv_notificationtemplate", new string[] { });
            var notifySendEmail = notificationTemplate.Attributes.Contains("ldv_useemail") ? Convert.ToBoolean(notificationTemplate.Attributes["ldv_useemail"]) : false;
            var notifySendSMS = notificationTemplate.Attributes.Contains("ldv_usesms") ? Convert.ToBoolean(notificationTemplate.Attributes["ldv_usesms"]) : false;

            var fetchxml =
                @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                  <entity name='contact'>
                    <attribute name='fullname' />
                    <attribute name='contactid' />
                    <attribute name='ldv_preferredlanguagecode' />
                    <attribute name='mobilephone' />
                    <attribute name='emailaddress1' />
                    <order attribute='fullname' descending='false' />
                    <filter type='and'>
                      <condition attribute='statecode' operator='eq' value='0' />
                    </filter>
                    <link-entity name='ldv_authenticationrole' from='ldv_contactid' to='contactid' link-type='inner' alias='af'>
                      <filter type='and'>
                        <condition attribute='ldv_accountid' operator='eq' uitype='account' value='{" + Account.Id + @"}' />
                        <condition attribute='ldv_roletypecodes' operator='contain-values'>
                            <value>1</value>
                        </condition>
                      </filter>                    
                    </link-entity>
                  </entity>
                </fetch>";

            var representatives = OrganizationService.RetrieveMultiple(new FetchExpression(fetchxml));

            tracingService.Trace($" after representatives");


            if (notificationTemplate.Contains("ldv_englishemailmessage"))
            {
                englishMessage = notificationTemplate.Attributes["ldv_englishemailmessage"].ToString();
            }
            if (notificationTemplate.Contains("ldv_arabicemailmessage"))
            {
                arabicMessage = notificationTemplate.Attributes["ldv_arabicemailmessage"].ToString();
            }
            if (notificationTemplate.Contains("ldv_englishsmsmessage"))
            {
                englishSms = notificationTemplate.Attributes["ldv_englishsmsmessage"].ToString();
            }
            if (notificationTemplate.Contains("ldv_arabicsmsmessage"))
            {
                arabicSms = notificationTemplate.Attributes["ldv_arabicsmsmessage"].ToString();
            }


            foreach (var item in representatives.Entities)
            {
                tracingService.Trace($" in representatives.Entities {representatives.Entities.Count} ");

                var notificationTemplateCopy = notificationTemplate;

                if (!string.IsNullOrEmpty(englishMessage))
                    notificationTemplateCopy["ldv_englishemailmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), englishMessage, startingPattern, endingPattern, OrganizationService);
                if (!string.IsNullOrEmpty(arabicMessage))
                    notificationTemplateCopy["ldv_arabicemailmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), arabicMessage, startingPattern, endingPattern, OrganizationService);
                if (!string.IsNullOrEmpty(englishSms))
                    notificationTemplateCopy["ldv_englishsmsmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), englishSms, startingPattern, endingPattern, OrganizationService);
                if (!string.IsNullOrEmpty(arabicSms))
                    notificationTemplateCopy["ldv_arabicsmsmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), arabicSms, startingPattern, endingPattern, OrganizationService);

                if (item.Contains("ldv_preferredlanguagecode"))
                    language = (Language)(item["ldv_preferredlanguagecode"] as OptionSetValue).Value;

                tracingService.Trace($" language {language} ");

                if (notifySendEmail)
                     CreateAndSendEmail(from, language, notificationTemplateCopy, regardingObject, new List<EntityReference>() { item.ToEntityReference() }, null, null, null,null,null);
                if (notifySendSMS)
                     CreateSMSAndPortalNotificationToContact(item, notificationTemplateCopy, regardingObject, null);
            }
        }
        #endregion

        #region Common Bll

 
        public void CreateAndSendEmail(EntityReference From, Language preferredLanguage, Entity notificationTemplate, EntityReference regardingObject, List<EntityReference> toParty, List<EntityReference> ccList, List<EntityReference> bccList, EntityReference notificationConfig,  EntityReference RegardingLookup,string toEmailAddress)
        {
            try
            {
                tracingService.Trace($" in CreateAndSendEmail ");

                string MailTitle = string.Empty, MailMessage = string.Empty, MailTitleWithFields = string.Empty, MailWithFields = string.Empty;

                if (preferredLanguage == Language.English)
                {
                    MailTitleWithFields = notificationTemplate.Attributes.Contains("ldv_englishemailtitle") ? notificationTemplate.Attributes["ldv_englishemailtitle"].ToString() : "";
                    MailWithFields = notificationTemplate.Attributes.Contains("ldv_englishemailmessage") ? notificationTemplate.Attributes["ldv_englishemailmessage"].ToString() : "";
                }
                else
                {
                    MailTitleWithFields = notificationTemplate.Attributes.Contains("ldv_arabicemailtitle") ? notificationTemplate.Attributes["ldv_arabicemailtitle"].ToString() : "";
                    MailWithFields = notificationTemplate.Attributes.Contains("ldv_arabicemailmessage") ? notificationTemplate.Attributes["ldv_arabicemailmessage"].ToString() : "";
                }

                tracingService.Trace($"   MailTitleWithFields {MailTitleWithFields}");
                //tracingService.Trace($"   MailWithFields {MailWithFields}");


                #region send and create email
                MailTitle = GetMessageWithValues(MailTitleWithFields, OrganizationService, regardingObject);
                tracingService.Trace($"   MailTitle {MailTitle}");

                MailMessage = GetMessageWithValues(MailWithFields, OrganizationService, regardingObject);
                //tracingService.Trace($"   MailMessage {MailMessage}");

                tracingService.Trace($" before CreateEmail ");


                if (RegardingLookup!=null)
                {
                    regardingObject = RegardingLookup;
                }
                Guid emailID = CRMAccessLayer.CreateEmail(From, toParty, regardingObject, MailTitle, MailMessage, ccList, bccList, notificationConfig);
                if (!string.IsNullOrEmpty(toEmailAddress))
                {

                    UpdateEmailWithExternalFromAddress(  emailID, MailTitle, MailMessage,   toEmailAddress );

                    
                }

                if (emailID != Guid.Empty)
                    CRMAccessLayer.SendEmail(emailID);
                tracingService.Trace($" after send email ");

                #endregion
            }
            catch (Exception ex)
            {
                tracingService.Trace($"CreateAndSendEmail ex {ex}");

            }
        }


        public void UpdateEmailWithExternalFromAddress(Guid emailID,  string subject, string emailBody, string recipientEmail)
        {
            // Create the email entity
            Entity email = new Entity("email", emailID);

            // Set subject and description
            email["subject"] = subject;
            email["description"] = emailBody;

            //// Set "from" using an external email address
            //Entity from = new Entity("activityparty");
            //from["addressused"] = senderEmail;  // Directly specify the external email address
            //email["from"] = new EntityCollection(new List<Entity> { from });

            // Set "to" to the recipient
            Entity to = new Entity("activityparty");
            to["addressused"] = recipientEmail;
            email["to"] = new EntityCollection(new List<Entity> { to });

            // Create the email in CRM
             CRMAccessLayer.UpdateEntity(email);


        }
        public void CreateSMS(Language preferredLanguage, Entity notificationTemplate, EntityReference regardingObject, string mobile, EntityReference stageNotificationConfig)
        {
            try
            {
                string SMSMessage = string.Empty, SMSWithFields = string.Empty;
                string subject = notificationTemplate.Attributes.Contains("ldv_name") ? (notificationTemplate.Attributes["ldv_name"]).ToString() : string.Empty;

                if (preferredLanguage == Language.English)
                    SMSWithFields = notificationTemplate.Attributes.Contains("ldv_englishsmsmessage") ? notificationTemplate.Attributes["ldv_englishsmsmessage"].ToString() : "";
                else
                    SMSWithFields = notificationTemplate.Attributes.Contains("ldv_arabicsmsmessage") ? notificationTemplate.Attributes["ldv_arabicsmsmessage"].ToString() : "";

                SMSMessage = GetMessageWithValues(SMSWithFields, OrganizationService, regardingObject);
                Guid activityId = CreateSms(regardingObject, mobile, SMSMessage, subject, stageNotificationConfig, preferredLanguage);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"CreateSMS ex {ex}");

            }

        }

        public void CreatePortalNotifications(Guid userid, Entity notificationTemplate, EntityReference regardingObject, EntityReference stageNotificationConfig)
        {
            try
            {
                string arabicPortalMessage = string.Empty, englishPortalMessage = string.Empty, englishPortalWithFields = string.Empty, arabicPortalWithFields = string.Empty;
                string subject = notificationTemplate.Attributes.Contains("ldv_name") ? (notificationTemplate.Attributes["ldv_name"]).ToString() : string.Empty;
                arabicPortalWithFields = notificationTemplate.Attributes.Contains("ldv_arabicportalmessage") ? notificationTemplate.Attributes["ldv_arabicportalmessage"].ToString() : "";
                englishPortalWithFields = notificationTemplate.Attributes.Contains("ldv_englishportalmessage") ? notificationTemplate.Attributes["ldv_englishportalmessage"].ToString() : "";

                arabicPortalMessage = GetMessageWithValues(arabicPortalWithFields, OrganizationService, regardingObject);
                englishPortalMessage = GetMessageWithValues(englishPortalWithFields, OrganizationService, regardingObject);
                Guid arabicActivityId = CreatePortalNotification(userid, regardingObject, englishPortalMessage, arabicPortalMessage, subject, stageNotificationConfig);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"CreatePortalNotifications ex {ex}");

            }

        }

        #region Helpers:

        public List<EntityReference> FiltterTheListByEmail(List<EntityReference> ToList, bool iscontact)
        {
            #region variables:
            int contactNumber = 0;
            List<EntityReference> contactList = ToList.Where(x => x.LogicalName == "contact").ToList();
            List<EntityReference> accountList = ToList.Where(x => x.LogicalName == "account").ToList();
            List<EntityReference> systemUserList = ToList.Where(x => x.LogicalName == "systemuser").ToList();
            List<EntityReference> toList = new List<EntityReference>();
            #endregion

            if (contactList != null && contactList.Count > 0)
            {
                #region get the contacts who has emails 
                ConditionExpression conditionIds = new ConditionExpression();
                conditionIds.AttributeName = "contactid";
                conditionIds.Operator = ConditionOperator.In;
                foreach (var item in contactList)
                    conditionIds.Values.Add(item.Id);


                var QEcontact = new QueryExpression("contact");
                QEcontact.ColumnSet.AddColumns("emailaddress1", "contactid", "ldv_preferredlanguagecode");
                // Define filter QEcontact.Criteria
                QEcontact.Criteria.AddCondition("emailaddress1", ConditionOperator.NotNull);
                QEcontact.Criteria.AddCondition(conditionIds);
                // retrieve contacts that has emails
                var contacts = CRMAccessLayer.RetrieveMultipleRequest(QEcontact);

                // get the contact list:
                if (contacts != null && contacts.Entities.Count > 0)
                {
                    foreach (var item in contacts.Entities)
                    {
                        // get the first contact language and it will be tha default language to all the contact in the list:
                        // the counter "contact number" = 0n till i get the first contact who has preffered language value then the counter will be = 1 and no need to get the preferred language value
                        if (item.Attributes.Contains("ldv_preferredlanguagecode") && contactNumber == 0 && iscontact)
                        {
                            language = (Language)(((OptionSetValue)(item.Attributes["ldv_preferredlanguagecode"])).Value);
                            contactNumber++;
                        }
                        toList.Add(new EntityReference(item.LogicalName, item.Id));
                    }
                }
                #endregion
            }
            if (accountList != null && accountList.Count > 0)
            {
                #region get the accounts who has emails 

                ConditionExpression conditionIds = new ConditionExpression();
                conditionIds.AttributeName = "accountid";
                conditionIds.Operator = ConditionOperator.In;
                foreach (var item in accountList)
                    conditionIds.Values.Add(item.Id);

                var QEaccountt = new QueryExpression("account");
                QEaccountt.ColumnSet.AddColumns("accountid");
                QEaccountt.Criteria.AddCondition("emailaddress1", ConditionOperator.NotNull);
                QEaccountt.Criteria.AddCondition(conditionIds);

                var accounts = CRMAccessLayer.RetrieveMultipleRequest(QEaccountt);

                // get the account list:
                if (accounts != null && accounts.Entities.Count > 0)
                {
                    foreach (var item in accounts.Entities)
                        toList.Add(new EntityReference(item.LogicalName, item.Id));
                }
                #endregion
            }
            if (systemUserList != null && systemUserList.Count > 0)
            {
                #region get the crm users who has emails 
                ConditionExpression conditionIds = new ConditionExpression();
                conditionIds.AttributeName = "systemuserid";
                conditionIds.Operator = ConditionOperator.In;
                foreach (var item in systemUserList)
                    conditionIds.Values.Add(item.Id);

                var QEsystemuser = new QueryExpression("systemuser");
                QEsystemuser.ColumnSet.AddColumns("systemuserid");
                QEsystemuser.Criteria.AddCondition("internalemailaddress", ConditionOperator.NotNull);
                QEsystemuser.Criteria.AddCondition(conditionIds);

                var systemusers = CRMAccessLayer.RetrieveMultipleRequest(QEsystemuser);

                // get the system list:
                if (systemusers != null && systemusers.Entities.Count > 0)
                {
                    foreach (var item in systemusers.Entities)
                        toList.Add(new EntityReference(item.LogicalName, item.Id));
                }
                #endregion
            }
            return toList;
        }
        public bool CheckTheQueueHasEmail(string id)
        {
            bool isValid = false;

            var QEqueue = new QueryExpression("queue");
            QEqueue.ColumnSet.AddColumns("queueid");
            QEqueue.Criteria.AddCondition("emailaddress", ConditionOperator.NotNull);
            QEqueue.Criteria.AddCondition("queueid", ConditionOperator.Equal, id);


            var result = CRMAccessLayer.RetrieveMultipleRequest(QEqueue);
            if (result != null && result.Entities.Count > 0)
                isValid = true;

            return isValid;
        }
        public void CreateSMSAndPortalNotificationToContactList(List<Entity> contactLst, Entity notificationTemplate, EntityReference RegardingObject, EntityReference notificationConfig)
        {
            if (contactLst == null && contactLst.Count == 0) return;
            foreach (var contact in contactLst)
            {
                CreateSMSAndPortalNotificationToContact(contact, notificationTemplate, RegardingObject, notificationConfig);
            }
        }
        public void CreateSMSAndPortalNotificationToContact(Entity contact, Entity notificationTemplate, EntityReference RegardingObject, EntityReference notificationConfig)
        {
            if (contact == null) return;
            bool notifySendSMS = (notificationTemplate.Attributes.Contains("ldv_usesms") ? (bool)(notificationTemplate.Attributes["ldv_usesms"]) : false);
            bool notifySendPortal = (notificationTemplate.Attributes.Contains("ldv_useportalnotification") ? (bool)(notificationTemplate.Attributes["ldv_useportalnotification"]) : false);
            if (contact.Attributes.Contains("ldv_preferredlanguagecode"))
            {
                int languageValue = ((OptionSetValue)contact.Attributes["ldv_preferredlanguagecode"]).Value;
                language = (Language)languageValue;
            }
            if (notifySendSMS && contact.Attributes.Contains("mobilephone"))
            {
                string mobile = contact.Attributes["mobilephone"].ToString();
                CreateSMS(language, notificationTemplate, RegardingObject, mobile, notificationConfig);
            }
            if (notifySendPortal && contact.Id != Guid.Empty)
                CreatePortalNotifications(contact.Id, notificationTemplate, RegardingObject, notificationConfig);

        }
        public string GetMessageWithValues(string Message, IOrganizationService crmService, EntityReference regardingObject)
        {
            tracingService.Trace($" in GetMessageWithValues ");
            CrmStringHandler crmStringHandler = new CrmStringHandler(regardingObject, crmService);
            var substitutedString = CrmStringHandler.Substitute(regardingObject, Message, crmService );
            return substitutedString;
        }

        public string GetFieldValue(string lookupName, string fieldName, IOrganizationService crmService, bool isLookup, EntityReference regardingObject)
        {
            Entity currEntity = new Entity();
            string fieldValue = string.Empty;

            if (isLookup)
            {
                var retrievedCols = new string[] { lookupName };
                currEntity = CRMAccessLayer.RetrieveEntity(regardingObject.Id.ToString(), regardingObject.LogicalName, retrievedCols);

                if (currEntity.Attributes.Contains(lookupName))
                {
                    string entityType = ((EntityReference)currEntity.Attributes[lookupName]).LogicalName;
                    Guid entityId = ((EntityReference)currEntity.Attributes[lookupName]).Id;

                    var retrievedLookupCols = new string[] { fieldName };
                    Entity lookupEntity = CRMAccessLayer.RetrieveEntity(entityId.ToString(), entityType, retrievedLookupCols);

                    fieldValue = GetFieldValueToString(lookupEntity, fieldName);
                }

            }
            else
            {
                var retrievedCols = new string[] { fieldName };
                currEntity = CRMAccessLayer.RetrieveEntity(regardingObject.Id.ToString(), regardingObject.LogicalName, retrievedCols);

                if (currEntity.Attributes.Contains(fieldName))
                    fieldValue = GetFieldValueToString(currEntity, fieldName);
            }

            return fieldValue;
        }
        public string GetFieldValueToString(Entity entity, string fieldName)
        {
            if (entity.Attributes.Contains(fieldName))
            {
                if (entity.Attributes[fieldName] is String) //String
                    return ((String)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is OptionSetValue) //OptionSet
                {
                    if (entity.FormattedValues.Contains(fieldName))
                        return (string)entity.FormattedValues[fieldName];
                    else
                        return ((OptionSetValue)entity.Attributes[fieldName]).Value.ToString();
                }

                else if (entity.Attributes[fieldName] is DateTime) //DateTime
                    return ((DateTime)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is BooleanManagedProperty) //Boolean
                    return ((BooleanManagedProperty)entity.Attributes[fieldName]).Value.ToString();

                else if (entity.Attributes[fieldName] is float) //float
                    return ((float)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is int) //Integer
                    return ((int)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is EntityReference) //Lookup
                    return ((EntityReference)entity.Attributes[fieldName]).Name;

                else if (entity.Attributes[fieldName] is Money) //Crm Money
                    return ((Money)entity.Attributes[fieldName]).Value.ToString();

                else if (entity.Attributes[fieldName] is decimal) //Decimal
                    return ((decimal)entity.Attributes[fieldName]).ToString();

                else
                    return "";
            }
            else
                return "";
        }
        public List<EntityReference> getEntityReferencesFromURLs(string _dynamicURLParams, IOrganizationService service)
        {
            try
            {

                if (_dynamicURLParams == null || _dynamicURLParams == string.Empty)
                {
                    return new List<EntityReference>();
                }
                //split parameter if multiple dynamic urls
                var dynamicUrlsArray = _dynamicURLParams.Replace(" ", "").Split(',');

                List<EntityReference> Results = new List<EntityReference>();
                //loop on dynamic urls to retrieve entity reference 
                for (int i = 0; i < dynamicUrlsArray.Length; i++)
                {
                    if (dynamicUrlsArray[i].Replace(" ", "") == string.Empty)
                        continue;
                    string[] urlParts = dynamicUrlsArray[i].Trim().Split("?".ToArray());
                    string[] urlParams = urlParts[1].Split("&".ToCharArray());
                    string objectTypeCode = urlParams[0].Replace("etn=", "");
                    string objectId = urlParams[1].Replace("id=", "").Replace("{", "").Replace("}", "");
                    //MetadataFilterExpression entityFilter = new MetadataFilterExpression(LogicalOperator.And);
                    //entityFilter.Conditions.Add(new MetadataConditionExpression("ObjectTypeCode", MetadataConditionOperator.Equals, Convert.ToInt32(objectTypeCode)));
                    //EntityQueryExpression entityQueryExpression = new EntityQueryExpression()
                    //{
                    //    Criteria = entityFilter
                    //};
                    //RetrieveMetadataChangesRequest retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest()
                    //{
                    //    Query = entityQueryExpression,
                    //    ClientVersionStamp = null
                    //};
                    //RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)service.Execute(retrieveMetadataChangesRequest);

                    //EntityMetadata entityMetadata = (EntityMetadata)response.EntityMetadata[0];


                    //string entityName = entityMetadata.SchemaName.ToLower();

                    string entityName = urlParams.Where(w => w.Contains("etn")).FirstOrDefault().Replace("etn=", "");
                    Results.Add(new EntityReference(entityName, Guid.Parse(objectId)));
                }
                return Results;
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("#" + _dynamicURLParams + "#" + ex.Message);
            }
        }
        /// <summary>
        /// get email from field from moe configuration entity
        /// </summary>
        /// <returns></returns>
        public EntityReference RetriveEmailFrom()
        {
            tracingService.Trace($" in RetriveEmailFrom");

            EntityReference from = new EntityReference();

            var configuration = new QueryExpression("ldv_configuration");
            configuration.ColumnSet.AddColumns("ldv_value");
            configuration.Criteria.AddCondition("ldv_name", ConditionOperator.Equal, "AdminId");
          

            var result = CRMAccessLayer.RetrieveMultiple(configuration);
            if (result != null && result.Count > 0)
            {
                string fromId = result[0].Attributes.Contains("ldv_value") ? (string)(result[0].Attributes["ldv_value"]) : string.Empty;
                if (fromId != string.Empty)
                {
                    from = new EntityReference("systemuser", new Guid(fromId));
                }
            }
            else
                return null;
            return from;
        }
        public EntityReference RetrieveRelatedApplicationByApplicationHeader(EntityReference applicationHeader)
        {
            if (applicationHeader?.Id == Guid.Empty) return null;

            EntityReference relatedApplicationlookup = new EntityReference();
            var relatedApplicationEntity = CRMAccessLayer.RetrieveEntity(applicationHeader.Id, "ldv_applicationheader", new string[] { ApplicationHeaderEntity.Regarding });
            if (relatedApplicationEntity != null)
            {
                if (relatedApplicationEntity.Attributes.Contains(ApplicationHeaderEntity.Regarding))
                {
                    relatedApplicationlookup = relatedApplicationEntity.GetAttributeValue<EntityReference>(ApplicationHeaderEntity.Regarding);
                }
            }
            return relatedApplicationlookup;
        }

        //Commented by Marina to remove Langauage Reference to be updated by Suzan
        public Guid CreateSms(EntityReference regardingId, string mobileNumber, string message, string subject, EntityReference notificationConfig, Language lang = Language.Arabic)
        {


            Guid activityId = Guid.Empty;
            try
            {
                //TODO : Check SMS entity in CRM

                Entity SMS = new Entity("ldv_sms");
                SMS.Attributes.Add("ldv_mobilenumber", mobileNumber);
                SMS.Attributes.Add("description", message);
                SMS.Attributes.Add("regardingobjectid", regardingId);
                SMS.Attributes.Add("ldv_sendsms", true);
                SMS.Attributes.Add("subject", subject);
                SMS.Attributes.Add("ldv_notificationconfigurationid", notificationConfig);


                int smsLanguage = (int)Language.Arabic; //Arabic

                if (lang == Language.English)
                {
                    smsLanguage = (int)Language.English;
                }
                SMS.Attributes.Add("ldv_language", new OptionSetValue(smsLanguage));

                activityId = OrganizationService.Create(SMS);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"CreateSms ex {ex}");

                throw;
            }

            return activityId;
        }


        /// <summary>
        /// create the portal notification template using the below parameters
        /// </summary>
        /// <param name="regardingId"></param>
        /// <param name="message"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public Guid CreatePortalNotification(Guid userid, EntityReference regardingId, string englishMessage, string arabicMessage, string subject, EntityReference notificationConfig)
        {


            Guid activityId = Guid.Empty;
            try
            {
                //TODO : Check SMS entity in CRM
                EntityReference user = new EntityReference("contact", userid);
                Entity PortalNotification = new Entity("ldv_portalnotification");
                PortalNotification.Attributes.Add("description", englishMessage);
                PortalNotification.Attributes.Add("ldv_arabicdescription", arabicMessage);
                PortalNotification.Attributes.Add("ldv_contact", user);
                PortalNotification.Attributes.Add("regardingobjectid", regardingId);
                PortalNotification.Attributes.Add("ldv_showportal", true);
                PortalNotification.Attributes.Add("subject", subject);
                PortalNotification.Attributes.Add("ldv_notificationconfigurationid", notificationConfig);

                activityId = OrganizationService.Create(PortalNotification);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"CreatePortalNotification ex {ex}");

                throw;
            }

            return activityId;
        }

        #endregion
        #endregion
    }
}
