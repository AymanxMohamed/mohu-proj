//using Linkdev.MOE.CRM.DAL;
//using Linkdev.CRM.DataContracts.Enums;
//using LinkDev.Libraries.Common;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Messages;
//using Microsoft.Xrm.Sdk.Metadata;
//using Microsoft.Xrm.Sdk.Metadata.Query;
//using Microsoft.Xrm.Sdk.Query;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LinkDev.Common.Crm.Logger;
//using LinkDev.Common.Crm.Utilities;
//using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;

//namespace LinkDev.Common.Crm.Cs.NotificationTemplates.Helper
//{
//    public class CommonBLL //: LinkDev.Common.Crm.Bll.Base.BllBase
//    {
//        #region Variables
//        CRMAccessLayer CRMAccessLayer;
//        Language language;
//        ITracingService tracingService;
//        IOrganizationService OrganizationService;
//        #endregion

//        #region Common Bll

      
//        public CommonBLL(IOrganizationService service, ITracingService TracingService)

//        {
//            language = Language.Arabic;
//            tracingService = TracingService;
//            OrganizationService = service;
//            CRMAccessLayer = new CRMAccessLayer(service, tracingService);

//            tracingService.Trace($" in CommonBLL constarctor");


//        }
//        public void CreateAndSendEmail(EntityReference From, Language preferredLanguage, Entity notificationTemplate, EntityReference regardingObject, List<EntityReference> toParty, List<EntityReference> ccList, List<EntityReference> bccList, EntityReference notificationConfig)
//        {
//            try
//            {

//                string MailTitle = string.Empty, MailMessage = string.Empty, MailTitleWithFields = string.Empty, MailWithFields = string.Empty;

//                if (preferredLanguage == Language.English)
//                {
//                    MailTitleWithFields = notificationTemplate.Attributes.Contains("ldv_englishemailtitle") ? notificationTemplate.Attributes["ldv_englishemailtitle"].ToString() : "";
//                    MailWithFields = notificationTemplate.Attributes.Contains("ldv_englishemailmessage") ? notificationTemplate.Attributes["ldv_englishemailmessage"].ToString() : "";
//                }
//                else
//                {
//                    MailTitleWithFields = notificationTemplate.Attributes.Contains("ldv_arabicemailtitle") ? notificationTemplate.Attributes["ldv_arabicemailtitle"].ToString() : "";
//                    MailWithFields = notificationTemplate.Attributes.Contains("ldv_arabicemailmessage") ? notificationTemplate.Attributes["ldv_arabicemailmessage"].ToString() : "";
//                }

//                #region send and create email
//                MailTitle = GetMessageWithValues(MailTitleWithFields, OrganizationService, regardingObject);
//                MailMessage = GetMessageWithValues(MailWithFields, OrganizationService, regardingObject);
//                Guid emailID = CRMAccessLayer.CreateEmail(From, toParty, regardingObject, MailTitle, MailMessage, ccList, bccList, notificationConfig);
//                if (emailID != Guid.Empty)
//                    CRMAccessLayer.SendEmail(emailID);
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"CreateAndSendEmail ex {ex}");

//            }
//        }

//        public void CreateSMS(Language preferredLanguage, Entity notificationTemplate, EntityReference regardingObject, string mobile, EntityReference stageNotificationConfig)
//        {
//            try
//            {
//                string SMSMessage = string.Empty, SMSWithFields = string.Empty;
//                string subject = notificationTemplate.Attributes.Contains("ldv_name") ? (notificationTemplate.Attributes["ldv_name"]).ToString() : string.Empty;

//                if (preferredLanguage == Language.English)
//                    SMSWithFields = notificationTemplate.Attributes.Contains("ldv_englishsmsmessage") ? notificationTemplate.Attributes["ldv_englishsmsmessage"].ToString() : "";
//                else
//                    SMSWithFields = notificationTemplate.Attributes.Contains("ldv_arabicsmsmessage") ? notificationTemplate.Attributes["ldv_arabicsmsmessage"].ToString() : "";

//                SMSMessage = GetMessageWithValues(SMSWithFields, OrganizationService, regardingObject);
//                Guid activityId = CreateSms(regardingObject, mobile, SMSMessage, subject, stageNotificationConfig, preferredLanguage);
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"CreateSMS ex {ex}");

//            }

//        }

//        public void CreatePortalNotifications(Guid userid, Entity notificationTemplate, EntityReference regardingObject, EntityReference stageNotificationConfig)
//        {
//            try
//            {
//                string arabicPortalMessage = string.Empty, englishPortalMessage = string.Empty, englishPortalWithFields = string.Empty, arabicPortalWithFields = string.Empty;
//                string subject = notificationTemplate.Attributes.Contains("ldv_name") ? (notificationTemplate.Attributes["ldv_name"]).ToString() : string.Empty;
//                arabicPortalWithFields = notificationTemplate.Attributes.Contains("ldv_arabicportalmessage") ? notificationTemplate.Attributes["ldv_arabicportalmessage"].ToString() : "";
//                englishPortalWithFields = notificationTemplate.Attributes.Contains("ldv_englishportalmessage") ? notificationTemplate.Attributes["ldv_englishportalmessage"].ToString() : "";

//                arabicPortalMessage = GetMessageWithValues(arabicPortalWithFields, OrganizationService, regardingObject);
//                englishPortalMessage = GetMessageWithValues(englishPortalWithFields, OrganizationService, regardingObject);
//                Guid arabicActivityId = CreatePortalNotification(userid, regardingObject, englishPortalMessage, arabicPortalMessage, subject, stageNotificationConfig);
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"CreatePortalNotifications ex {ex}");

//            }

//        }

//        #region Helpers:

//        public List<EntityReference> FiltterTheListByEmail(List<EntityReference> ToList, bool iscontact)
//        {
//            #region variables:
//            int contactNumber = 0;
//            List<EntityReference> contactList = ToList.Where(x => x.LogicalName == "contact").ToList();
//            List<EntityReference> accountList = ToList.Where(x => x.LogicalName == "account").ToList();
//            List<EntityReference> systemUserList = ToList.Where(x => x.LogicalName == "systemuser").ToList();
//            List<EntityReference> toList = new List<EntityReference>();
//            #endregion

//            if (contactList != null && contactList.Count > 0)
//            {
//                #region get the contacts who has emails 
//                ConditionExpression conditionIds = new ConditionExpression();
//                conditionIds.AttributeName = "contactid";
//                conditionIds.Operator = ConditionOperator.In;
//                foreach (var item in contactList)
//                    conditionIds.Values.Add(item.Id);


//                var QEcontact = new QueryExpression("contact");
//                QEcontact.ColumnSet.AddColumns("emailaddress1", "contactid", "ldv_preferredlanguagecode");
//                // Define filter QEcontact.Criteria
//                QEcontact.Criteria.AddCondition("emailaddress1", ConditionOperator.NotNull);
//                QEcontact.Criteria.AddCondition(conditionIds);
//                // retrieve contacts that has emails
//                var contacts = CRMAccessLayer.RetrieveMultipleRequest(QEcontact);

//                // get the contact list:
//                if (contacts != null && contacts.Entities.Count > 0)
//                {
//                    foreach (var item in contacts.Entities)
//                    {
//                        // get the first contact language and it will be tha default language to all the contact in the list:
//                        // the counter "contact number" = 0n till i get the first contact who has preffered language value then the counter will be = 1 and no need to get the preferred language value
//                        if (item.Attributes.Contains("ldv_preferredlanguagecode") && contactNumber == 0 && iscontact)
//                        {
//                            language = (Language)(((OptionSetValue)(item.Attributes["ldv_preferredlanguagecode"])).Value);
//                            contactNumber++;
//                        }
//                        toList.Add(new EntityReference(item.LogicalName, item.Id));
//                    }
//                }
//                #endregion
//            }
//            if (accountList != null && accountList.Count > 0)
//            {
//                #region get the accounts who has emails 

//                ConditionExpression conditionIds = new ConditionExpression();
//                conditionIds.AttributeName = "accountid";
//                conditionIds.Operator = ConditionOperator.In;
//                foreach (var item in accountList)
//                    conditionIds.Values.Add(item.Id);

//                var QEaccountt = new QueryExpression("account");
//                QEaccountt.ColumnSet.AddColumns("accountid");
//                QEaccountt.Criteria.AddCondition("emailaddress1", ConditionOperator.NotNull);
//                QEaccountt.Criteria.AddCondition(conditionIds);

//                var accounts = CRMAccessLayer.RetrieveMultipleRequest(QEaccountt);

//                // get the account list:
//                if (accounts != null && accounts.Entities.Count > 0)
//                {
//                    foreach (var item in accounts.Entities)
//                        toList.Add(new EntityReference(item.LogicalName, item.Id));
//                }
//                #endregion
//            }
//            if (systemUserList != null && systemUserList.Count > 0)
//            {
//                #region get the crm users who has emails 
//                ConditionExpression conditionIds = new ConditionExpression();
//                conditionIds.AttributeName = "systemuserid";
//                conditionIds.Operator = ConditionOperator.In;
//                foreach (var item in systemUserList)
//                    conditionIds.Values.Add(item.Id);

//                var QEsystemuser = new QueryExpression("systemuser");
//                QEsystemuser.ColumnSet.AddColumns("systemuserid");
//                QEsystemuser.Criteria.AddCondition("internalemailaddress", ConditionOperator.NotNull);
//                QEsystemuser.Criteria.AddCondition(conditionIds);

//                var systemusers = CRMAccessLayer.RetrieveMultipleRequest(QEsystemuser);

//                // get the system list:
//                if (systemusers != null && systemusers.Entities.Count > 0)
//                {
//                    foreach (var item in systemusers.Entities)
//                        toList.Add(new EntityReference(item.LogicalName, item.Id));
//                }
//                #endregion
//            }
//            return toList;
//        }
//        public bool CheckTheQueueHasEmail(string id)
//        {
//            bool isValid = false;

//            var QEqueue = new QueryExpression("queue");
//            QEqueue.ColumnSet.AddColumns("queueid");
//            QEqueue.Criteria.AddCondition("emailaddress", ConditionOperator.NotNull);
//            QEqueue.Criteria.AddCondition("queueid", ConditionOperator.Equal, id);


//            var result = CRMAccessLayer.RetrieveMultipleRequest(QEqueue);
//            if (result != null && result.Entities.Count > 0)
//                isValid = true;

//            return isValid;
//        }
//        public void CreateSMSAndPortalNotificationToContactList(List<Entity> contactLst, Entity notificationTemplate, EntityReference RegardingObject, EntityReference notificationConfig)
//        {
//            if (contactLst == null && contactLst.Count == 0) return;
//            foreach (var contact in contactLst)
//            {
//                CreateSMSAndPortalNotificationToContact(contact, notificationTemplate, RegardingObject, notificationConfig);
//            }
//        }
//        public void CreateSMSAndPortalNotificationToContact(Entity contact, Entity notificationTemplate, EntityReference RegardingObject, EntityReference notificationConfig)
//        {
//            if (contact == null) return;
//            bool notifySendSMS = (notificationTemplate.Attributes.Contains("ldv_usesms") ? (bool)(notificationTemplate.Attributes["ldv_usesms"]) : false);
//            bool notifySendPortal = (notificationTemplate.Attributes.Contains("ldv_useportalnotification") ? (bool)(notificationTemplate.Attributes["ldv_useportalnotification"]) : false);
//            if (contact.Attributes.Contains("ldv_preferredlanguagecode"))
//            {
//                int languageValue = ((OptionSetValue)contact.Attributes["ldv_preferredlanguagecode"]).Value;
//                language = (Language)languageValue;
//            }
//            if (notifySendSMS && contact.Attributes.Contains("mobilephone"))
//            {
//                string mobile = contact.Attributes["mobilephone"].ToString();
//                CreateSMS(language, notificationTemplate, RegardingObject, mobile, notificationConfig);
//            }
//            if (notifySendPortal && contact.Id != Guid.Empty)
//                CreatePortalNotifications(contact.Id, notificationTemplate, RegardingObject, notificationConfig);

//        }
//        public string GetMessageWithValues(string Message, IOrganizationService crmService, EntityReference regardingObject)
//        {
//            var substitutedString = CrmStringHandler.Substitute(regardingObject, Message, crmService);
//            return substitutedString;
//        }

//        public string GetFieldValue(string lookupName, string fieldName, IOrganizationService crmService, bool isLookup, EntityReference regardingObject)
//        {
//            Entity currEntity = new Entity();
//            string fieldValue = string.Empty;

//            if (isLookup)
//            {
//                var retrievedCols = new string[] { lookupName };
//                currEntity = CRMAccessLayer.RetrieveEntity(regardingObject.Id.ToString(), regardingObject.LogicalName, retrievedCols);

//                if (currEntity.Attributes.Contains(lookupName))
//                {
//                    string entityType = ((EntityReference)currEntity.Attributes[lookupName]).LogicalName;
//                    Guid entityId = ((EntityReference)currEntity.Attributes[lookupName]).Id;

//                    var retrievedLookupCols = new string[] { fieldName };
//                    Entity lookupEntity = CRMAccessLayer.RetrieveEntity(entityId.ToString(), entityType, retrievedLookupCols);

//                    fieldValue = GetFieldValueToString(lookupEntity, fieldName);
//                }

//            }
//            else
//            {
//                var retrievedCols = new string[] { fieldName };
//                currEntity = CRMAccessLayer.RetrieveEntity(regardingObject.Id.ToString(), regardingObject.LogicalName, retrievedCols);

//                if (currEntity.Attributes.Contains(fieldName))
//                    fieldValue = GetFieldValueToString(currEntity, fieldName);
//            }

//            return fieldValue;
//        }
//        public string GetFieldValueToString(Entity entity, string fieldName)
//        {
//            if (entity.Attributes.Contains(fieldName))
//            {
//                if (entity.Attributes[fieldName] is String) //String
//                    return ((String)entity.Attributes[fieldName]).ToString();

//                else if (entity.Attributes[fieldName] is OptionSetValue) //OptionSet
//                {
//                    if (entity.FormattedValues.Contains(fieldName))
//                        return (string)entity.FormattedValues[fieldName];
//                    else
//                        return ((OptionSetValue)entity.Attributes[fieldName]).Value.ToString();
//                }

//                else if (entity.Attributes[fieldName] is DateTime) //DateTime
//                    return ((DateTime)entity.Attributes[fieldName]).ToString();

//                else if (entity.Attributes[fieldName] is BooleanManagedProperty) //Boolean
//                    return ((BooleanManagedProperty)entity.Attributes[fieldName]).Value.ToString();

//                else if (entity.Attributes[fieldName] is float) //float
//                    return ((float)entity.Attributes[fieldName]).ToString();

//                else if (entity.Attributes[fieldName] is int) //Integer
//                    return ((int)entity.Attributes[fieldName]).ToString();

//                else if (entity.Attributes[fieldName] is EntityReference) //Lookup
//                    return ((EntityReference)entity.Attributes[fieldName]).Name;

//                else if (entity.Attributes[fieldName] is Money) //Crm Money
//                    return ((Money)entity.Attributes[fieldName]).Value.ToString();

//                else if (entity.Attributes[fieldName] is decimal) //Decimal
//                    return ((decimal)entity.Attributes[fieldName]).ToString();

//                else
//                    return "";
//            }
//            else
//                return "";
//        }
//        public List<EntityReference> getEntityReferencesFromURLs(string _dynamicURLParams, IOrganizationService service)
//        {
//            try
//            {

//                if (_dynamicURLParams == null || _dynamicURLParams == string.Empty)
//                {
//                    return new List<EntityReference>();
//                }
//                //split parameter if multiple dynamic urls
//                var dynamicUrlsArray = _dynamicURLParams.Replace(" ", "").Split(',');

//                List<EntityReference> Results = new List<EntityReference>();
//                //loop on dynamic urls to retrieve entity reference 
//                for (int i = 0; i < dynamicUrlsArray.Length; i++)
//                {
//                    if (dynamicUrlsArray[i].Replace(" ", "") == string.Empty)
//                        continue;
//                    string[] urlParts = dynamicUrlsArray[i].Trim().Split("?".ToArray());
//                    string[] urlParams = urlParts[1].Split("&".ToCharArray());
//                    string objectTypeCode = urlParams[0].Replace("etn=", "");
//                    string objectId = urlParams[1].Replace("id=", "").Replace("{", "").Replace("}", "");
//                    //MetadataFilterExpression entityFilter = new MetadataFilterExpression(LogicalOperator.And);
//                    //entityFilter.Conditions.Add(new MetadataConditionExpression("ObjectTypeCode", MetadataConditionOperator.Equals, Convert.ToInt32(objectTypeCode)));
//                    //EntityQueryExpression entityQueryExpression = new EntityQueryExpression()
//                    //{
//                    //    Criteria = entityFilter
//                    //};
//                    //RetrieveMetadataChangesRequest retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest()
//                    //{
//                    //    Query = entityQueryExpression,
//                    //    ClientVersionStamp = null
//                    //};
//                    //RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)service.Execute(retrieveMetadataChangesRequest);

//                    //EntityMetadata entityMetadata = (EntityMetadata)response.EntityMetadata[0];


//                    //string entityName = entityMetadata.SchemaName.ToLower();

//                    string entityName = urlParams.Where(w => w.Contains("etn")).FirstOrDefault().Replace("etn=", "");
//                    Results.Add(new EntityReference(entityName, Guid.Parse(objectId)));
//                }
//                return Results;
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidPluginExecutionException("#" + _dynamicURLParams + "#" + ex.Message);
//            }
//        }
//        /// <summary>
//        /// get email from field from moe configuration entity
//        /// </summary>
//        /// <returns></returns>
//        public EntityReference RetriveEmailFrom()
//        {
//            tracingService.Trace($" in RetriveEmailFrom");

//            EntityReference from = new EntityReference();

//            var configuration = new QueryExpression("ldv_configuration");
//            configuration.ColumnSet.AddColumns("ldv_value");
//            configuration.Criteria.AddCondition("ldv_name", ConditionOperator.Equal, "AdminId");
//            /////////////////////////


//            //// Instantiate QueryExpression QEldv_moeconfiguration
//            //var QEldv_moeconfiguration = new QueryExpression("ldv_moeconfiguration");
//            //// Add columns to QEldv_moeconfiguration.ColumnSet
//            //QEldv_moeconfiguration.ColumnSet.AddColumns("ldv_from");

//            var result = CRMAccessLayer.RetrieveMultiple(configuration);
//            if (result != null && result.Count > 0)
//            {
//                string fromId = result[0].Attributes.Contains("ldv_value") ? (string)(result[0].Attributes["ldv_value"]) : string.Empty;
//                if (fromId != string.Empty)
//                {
//                    from = new EntityReference("systemuser", new Guid(fromId));
//                }
//            }
//            else
//                return null;
//            return from;
//        }
//        public EntityReference RetrieveRelatedApplicationByApplicationHeader(EntityReference applicationHeader)
//        {
//            if (applicationHeader?.Id == Guid.Empty) return null;

//            EntityReference relatedApplicationlookup = new EntityReference();
//            var relatedApplicationEntity = CRMAccessLayer.RetrieveEntity(applicationHeader.Id, "ldv_applicationheader", new string[] { ApplicationHeaderEntity.Regarding });
//            if (relatedApplicationEntity != null)
//            {
//                if (relatedApplicationEntity.Attributes.Contains(ApplicationHeaderEntity.Regarding))
//                {
//                    relatedApplicationlookup = relatedApplicationEntity.GetAttributeValue<EntityReference>(ApplicationHeaderEntity.Regarding);
//                }
//            }
//            return relatedApplicationlookup;
//        }

//        //Commented by Marina to remove Langauage Reference to be updated by Suzan
//        public Guid CreateSms(EntityReference regardingId, string mobileNumber, string message, string subject, EntityReference notificationConfig, Language lang = Language.Arabic)
//        {


//            Guid activityId = Guid.Empty;
//            try
//            {
//                //TODO : Check SMS entity in CRM

//                Entity SMS = new Entity("ldv_sms");
//                SMS.Attributes.Add("ldv_mobilenumber", mobileNumber);
//                SMS.Attributes.Add("description", message);
//                SMS.Attributes.Add("regardingobjectid", regardingId);
//                SMS.Attributes.Add("ldv_sendsms", true);
//                SMS.Attributes.Add("subject", subject);
//                SMS.Attributes.Add("ldv_notificationconfigurationid", notificationConfig);


//                int smsLanguage = (int)Language.Arabic; //Arabic

//                if (lang == Language.English)
//                {
//                    smsLanguage = (int)Language.English;
//                }
//                SMS.Attributes.Add("ldv_language", new OptionSetValue(smsLanguage));

//                activityId = OrganizationService.Create(SMS);
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"CreateSms ex {ex}");

//                throw;
//            }

//            return activityId;
//        }


//        /// <summary>
//        /// create the portal notification template using the below parameters
//        /// </summary>
//        /// <param name="regardingId"></param>
//        /// <param name="message"></param>
//        /// <param name="lang"></param>
//        /// <returns></returns>
//        public Guid CreatePortalNotification(Guid userid, EntityReference regardingId, string englishMessage, string arabicMessage, string subject, EntityReference notificationConfig)
//        {


//            Guid activityId = Guid.Empty;
//            try
//            {
//                //TODO : Check SMS entity in CRM
//                EntityReference user = new EntityReference("contact", userid);
//                Entity PortalNotification = new Entity("ldv_portalnotification");
//                PortalNotification.Attributes.Add("description", englishMessage);
//                PortalNotification.Attributes.Add("ldv_arabicdescription", arabicMessage);
//                PortalNotification.Attributes.Add("ldv_contact", user);
//                PortalNotification.Attributes.Add("regardingobjectid", regardingId);
//                PortalNotification.Attributes.Add("ldv_showportal", true);
//                PortalNotification.Attributes.Add("subject", subject);
//                PortalNotification.Attributes.Add("ldv_notificationconfigurationid", notificationConfig);

//                activityId = OrganizationService.Create(PortalNotification);
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"CreatePortalNotification ex {ex}");

//                throw;
//            }

//            return activityId;
//        }

//        #endregion
//        #endregion
//    }
//}
