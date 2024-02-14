using Linkdev.MOE.CRM.DAL;
using Linkdev.CRM.DataContracts.Enums;
using LinkDev.Common.Crm.Cs.NotificationTemplates.Entities;
using LinkDev.Libraries.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Logger;

namespace LinkDev.Common.Crm.Cs.NotificationTemplates.Helper
{
    public class SendNotificationCommonBLL : BllBase
    {
        #region Variables
        CRMAccessLayer CRMAccessLayer;
        CommonBLL CommonBLL;
        NotificationConfigurations NotificationConfigrecipients;
        #endregion

        #region constructor
        public SendNotificationCommonBLL(IOrganizationService service, ILogger logger,string languageCode)
            :base(service,logger, languageCode)
        {
            CRMAccessLayer = new CRMAccessLayer(service);
            CommonBLL = new CommonBLL(service, logger, languageCode);
        }
        #endregion

        #region methods:

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

            try
            {
                // retrieve from attribute from moe configuration:
                EntityReference from = CommonBLL.RetriveEmailFrom();
                if (from == null) return;
                if (notificationConfigurationsList == null || notificationConfigurationsList.Count == 0) return; 
                foreach (var item in notificationConfigurationsList)
                {
                    var notification = GetNotificationRecipientsList(item);
                    if (notification != null)
                        actualNotificationConfigLst.Add(notification);
                }
                if (actualNotificationConfigLst.Count > 0)
                {// to create and send notifications 
                    foreach (var notifctaionConfigurationTemplate in actualNotificationConfigLst) 
                    {
                        // send eamil 
                        if (notifctaionConfigurationTemplate.notificationTemp.GetAttributeValue<bool>("ldv_useemail"))
                            CommonBLL.CreateAndSendEmail(from, notifctaionConfigurationTemplate.Language, notifctaionConfigurationTemplate.notificationTemp, regardingObject, notifctaionConfigurationTemplate.toParty, notifctaionConfigurationTemplate.ccParty, null, notifctaionConfigurationTemplate.NotificationConfiguration);
                        // send portal notifications and sms:
                        if (notifctaionConfigurationTemplate.notificationTemp.GetAttributeValue<bool>("ldv_usesms"))
                            CommonBLL.CreateSMSAndPortalNotificationToContactList(notifctaionConfigurationTemplate.contactLst, notifctaionConfigurationTemplate.notificationTemp, regardingObject, notifctaionConfigurationTemplate.NotificationConfiguration);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(LoggerHandler.GetMethodFullName(), ex);
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
                Logger.LogException(LoggerHandler.GetMethodFullName(), ex);
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
                Logger.LogException(LoggerHandler.GetMethodFullName(), ex);
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
                            if (CommonBLL.CheckTheQueueHasEmail(item.Id.ToString()))
                                ccParty.Add(item);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(LoggerHandler.GetMethodFullName(), ex);
            }
            return ccParty;
        }

        #endregion

    }
}
