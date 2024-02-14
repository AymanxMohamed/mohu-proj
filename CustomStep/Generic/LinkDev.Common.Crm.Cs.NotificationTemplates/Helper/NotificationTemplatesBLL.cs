using Linkdev.MOE.CRM.DAL;
using Linkdev.CRM.DataContracts.Enums;
using LinkDev.Common.Crm.Cs.NotificationTemplates.Helper;
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
using LinkDev.Common.Crm.Utilities;
using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Logger;

namespace LinkDev.Common.Crm.Cs.NotificationTemplates
{
    public class NotificationTemplatesBLL : BllBase
    {
        #region Variables
        CRMAccessLayer CRMAccessLayer;
        Language language ;
        CommonBLL commonBLL;
        #endregion

        #region Constructor

        public NotificationTemplatesBLL(IOrganizationService service, ILogger logger,string languageCode)
            :base(service,logger,languageCode)
        {           
            CRMAccessLayer = new CRMAccessLayer(service);
            commonBLL = new CommonBLL(service, logger,languageCode);
        }
        #endregion

        #region  Methods
        public void SendNotificationTemplate( EntityReference ToSystemUser,EntityReference ToAccount, EntityReference ToContact,EntityReference queues,string ccText, string bccText, string toText, EntityReference NotificationTemplates, EntityReference RegardingObject)
        {

            #region variables
            Entity Contact = new Entity();
            List<EntityReference> ccList = new List<EntityReference>();
            List<EntityReference> bccList = new List<EntityReference>();
            List<EntityReference> toList = new List<EntityReference>();
            bool notifySendEmail = false , notifySendSMS = false, notifySendPortal= false;
            #endregion

            try
            {
                #region retrieve notification Template : 

                Entity notificationTemplate = CRMAccessLayer.RetrieveEntity(NotificationTemplates.Id.ToString(), "ldv_notificationtemplate", new string[] { });
                if (notificationTemplate == null) return;
                notifySendEmail =  notificationTemplate.Attributes.Contains("ldv_useemail") ?  Convert.ToBoolean(notificationTemplate.Attributes["ldv_useemail"]) : false;
                
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
                        if (Contact !=null && Contact.Attributes.Contains("emailaddress1") )
                            toList.Add(new EntityReference("contact", ToContact.Id));
                        if (Contact.Attributes.Contains("ldv_preferredlanguagecode"))
                            language = (Language)(((OptionSetValue)(Contact.Attributes["ldv_preferredlanguagecode"])).Value);
                    }
                    else if (ToAccount != null && ToAccount.Id != Guid.Empty)
                    {
                        Entity Account = CRMAccessLayer.RetrieveEntity(ToAccount.Id.ToString(), "account", new string[] {  "emailaddress1" });
                        if (Account != null && Account.Attributes.Contains("emailaddress1"))
                            toList.Add(new EntityReference("account",ToAccount.Id));

                    }
                    else if (ToSystemUser != null && ToSystemUser.Id != Guid.Empty)
                    {
                        Entity User = CRMAccessLayer.RetrieveEntity(ToSystemUser.Id.ToString(), "systemuser", new string[] { "ldv_preferredlanguagecode", "internalemailaddress" });
                        if (User != null)
                        {
                            if (User.Attributes.Contains("internalemailaddress"))
                                toList.Add(new EntityReference("systemuser", ToSystemUser.Id));
                            if (User.Attributes.Contains("ldv_preferredlanguagecode"))
                                language = (Language)(((OptionSetValue)(User.Attributes["ldv_preferredlanguagecode"])).Value);
                        }
                    }

                    #endregion

                    #region Send email to record Url Users:
                    else if (!string.IsNullOrEmpty(toText))
                    {
                        List<EntityReference> DynamicList = new List<EntityReference>();
                        DynamicList = commonBLL.getEntityReferencesFromURLs(toText, OrganizationService);
                        if (DynamicList.Count > 0)
                        {
                            // check if all the list has emails 
                            var result = commonBLL.FiltterTheListByEmail(DynamicList,true);
                            toList = result;
                        }

                    }
                    #endregion

                    #region To Queue: language default is english
                    else if (queues != null)
                    {
                        // check if the queue has email if yes so add the queue id to to list:
                        if(commonBLL.CheckTheQueueHasEmail(queues.Id.ToString()))
                            toList.Add(queues);
                    }
                    #endregion
                    #region  ccs list emails:
                    /*To cc usrs*/
                    if (ccText != string.Empty && ccText!=null)
                    {
                        var ccUsers = commonBLL.getEntityReferencesFromURLs(ccText, OrganizationService);
                        // check if all the list has emails 
                        ccList = commonBLL.FiltterTheListByEmail(ccUsers,false);
                    }

                    #endregion
                    #region  bccs list emails:
                        /*To bcc usrs*/
                     if (bccText != string.Empty && bccText!=null)
                    {
                        var bccUsers = commonBLL.getEntityReferencesFromURLs(bccText, OrganizationService);
                        // check if all the list has emails 
                        bccList = commonBLL.FiltterTheListByEmail(bccUsers,false);
                    }

                    #endregion
                    // retrieve from:
                    EntityReference from = commonBLL.RetriveEmailFrom();
                    // to create and send email 
                    commonBLL.CreateAndSendEmail(from,language, notificationTemplate, RegardingObject, toList,ccList, bccList,null);
                }
                // to create sms and portal notifications to contact
                if(ToContact !=null  )
                {
                    // incase notify send email = false   so the contact entity will be null so we will retrieve the contact entity....
                    if (Contact == null){
                        Contact = CRMAccessLayer.RetrieveEntity(ToContact.Id.ToString(), "contact", new string[] { "ldv_preferredlanguagecode", "mobilephone", "emailaddress1" });
                    }
                    // to create sms + portal notifications to contact
                    commonBLL.CreateSMSAndPortalNotificationToContact(Contact, notificationTemplate,RegardingObject,null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(LoggerHandler.GetMethodFullName(), ex);
            }
        }
        public void SendNotificationToRepresintatives(EntityReference Notifications, EntityReference Account,EntityReference regardingObject,string startingPattern,string endingPattern )
        {
            if (Notifications == null && Notifications.Id == Guid.Empty)
                return;

            var englishMessage = string.Empty;
            var arabicMessage = string.Empty;
            var englishSms = string.Empty;
            var arabicSms = string.Empty;
            var from = commonBLL.RetriveEmailFrom();
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
                        <condition attribute='ldv_accountid' operator='eq' uitype='account' value='{"+ Account.Id+ @"}' />
                        <condition attribute='ldv_roletypecodes' operator='contain-values'>
                            <value>1</value>
                        </condition>
                      </filter>                    
                    </link-entity>
                  </entity>
                </fetch>";

            var representatives = OrganizationService.RetrieveMultiple(new FetchExpression(fetchxml));


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
                var notificationTemplateCopy = notificationTemplate;

                if (!string.IsNullOrEmpty(englishMessage))
                    notificationTemplateCopy["ldv_englishemailmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), englishMessage, startingPattern, endingPattern, OrganizationService);
                if (!string.IsNullOrEmpty(arabicMessage))
                    notificationTemplateCopy["ldv_arabicemailmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), arabicMessage, startingPattern, endingPattern, OrganizationService);
                if (!string.IsNullOrEmpty(englishSms))
                    notificationTemplateCopy["ldv_englishsmsmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), englishSms, startingPattern, endingPattern, OrganizationService);
                if (!string.IsNullOrEmpty(arabicSms))
                    notificationTemplateCopy["ldv_arabicsmsmessage"] = CrmStringHandler.SubstituteWithCustomPattern(item.ToEntityReference(), arabicSms, startingPattern, endingPattern, OrganizationService);

                if(item.Contains("ldv_preferredlanguagecode"))
                    language = (Language)(item["ldv_preferredlanguagecode"] as OptionSetValue).Value;

                if (notifySendEmail)
                    commonBLL.CreateAndSendEmail(from, language, notificationTemplateCopy, regardingObject, new List<EntityReference>() { item.ToEntityReference() },null, null, null);
                if (notifySendSMS)
                    commonBLL.CreateSMSAndPortalNotificationToContact(item, notificationTemplateCopy, regardingObject, null);
            }
        }
        #endregion
    }
}
