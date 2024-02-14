using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Bll.Notification.Entities;
using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;

namespace LinkDev.Common.Crm.Bll.Notification
{
    public enum SendNotificatioLanguage
    {

        English = 1033,

        Arabic = 1025

    }
    public enum NotificationPartyType { User = 1, Contact = 2, Account = 3, Team = 4, Queue = 5 };
    public enum NotificationType
    {
        Null,
        Creation,
        Reminder,
        Esclation
    };
    public class SendNotification : BllBase
    {
        #region Variables

        #endregion

        #region Constructors
        public SendNotification(IOrganizationService organizationService, ILogger logger, string languageCode)
            : base(organizationService, logger, languageCode)
        {
        }

        #endregion

        #region public
        public void SendEsclationOrNotificationToQueue(
            EntityReference from,
            EntityReference notificationTemplate,
            EntityReference taskEntityRefrence,
            NotificationType notificationType, EntityReference task)
        {
            if (task?.Id != Guid.Empty)
            {
                taskEntityRefrence = task;
            }
            // this function working only on task entity
            if (!taskEntityRefrence.LogicalName.ToLower().Equals("task"))
                throw new InvalidOperationException(string.Format("Prmiary entity should be 'Task' not '{0}'", taskEntityRefrence.LogicalName));

            var taskEntity =
                OrganizationService.Retrieve(taskEntityRefrence.LogicalName, taskEntityRefrence.Id, new ColumnSet(true));

            // Get the queue that hold the given task
            var query = new QueryExpression("queueitem");
            query.ColumnSet.AllColumns = true;
            query.Criteria.AddCondition("objectid", ConditionOperator.Equal, taskEntityRefrence.Id);

            var queueItemList =
                OrganizationService.RetrieveMultiple(query);

            if (queueItemList == null || queueItemList.Entities.Count <= 0)
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"The given task with Id '{taskEntityRefrence.Id}' had not been found in any queue item record\r\n", SeverityLevel.Warning);
                return;
            }

            var queueItemEntity = queueItemList[0];

            if (!queueItemEntity.Contains("queueid"))
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"The Queue Item with Id '{queueItemEntity.Id}' has no Queue Details\r\n", SeverityLevel.Warning);
                return;
            }

            var queueEntity =
                OrganizationService.Retrieve("queue", ((EntityReference)queueItemEntity["queueid"]).Id, new ColumnSet(true));

            var notificationTypeRelationship = string.Empty;
            switch (notificationType)
            {
                case NotificationType.Creation:
                    notificationTypeRelationship = "new_notification_party_group_creation";
                    break;
                case NotificationType.Reminder:
                    notificationTypeRelationship = "new_notification_party_group_notifications";
                    break;
                case NotificationType.Esclation:
                    notificationTypeRelationship = "new_notification_party_group_escalation";
                    break;
                default:
                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Unknown notification relationship\r\n", SeverityLevel.Warning);
                    return;
            }
            //Get 
            // Get the notification Group for the queue
            //var queryQueueNotificationGroupList =
            //    @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
            //      <entity name='new_notificationgroup'>
            //        <attribute name='new_notificationgroupid' />
            //        <attribute name='new_name' />
            //        <attribute name='createdon' />
            //        <order attribute='new_name' descending='false' />
            //        <link-entity name='" + notificationTypeRelationship + @"' from='new_notificationgroupid' to='new_notificationgroupid' visible='false' intersect='true'>
            //          <link-entity name='new_notificationparty' from='new_notificationpartyid' to='new_notificationpartyid' alias='ag'>
            //            <link-entity name='systemuser' from='systemuserid' to='new_userlookup' link-type='inner' alias='ah'>
            //              <link-entity name='queuemembership' from='systemuserid' to='systemuserid' visible='false' intersect='true'>
            //                <link-entity name='queue' from='queueid' to='queueid' alias='ai'>
            //                  <filter type='and'>
            //                    <condition attribute='queueid' operator='eq'  uitype='queue' value='{" + queueEntity.Id + @"}' />
            //                  </filter>
            //                </link-entity>
            //              </link-entity>
            //            </link-entity>
            //          </link-entity>
            //        </link-entity>
            //      </entity>
            //    </fetch>";

            var queryQueueNotificationGroupList = @"
                            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                              <entity name='new_notificationgroup'>
                                <attribute name='new_notificationgroupid' />
                                <attribute name='new_name' />
                                <attribute name='createdon' />
                                <order attribute='new_name' descending='false' />
                                <link-entity name='new_notification_queue_group' from='new_notificationgroupid' to='new_notificationgroupid' link-type='inner' alias='notification_group_queue' intersect='true'>
                                  <filter>
                                    <condition attribute='queueid' operator='eq'  uitype='queue' value='{" + queueEntity.Id + @"}' />
                                  </filter>
                                </link-entity>
                              </entity>
                            </fetch>";


            var queueNotificationGroupList =
                OrganizationService.RetrieveMultiple(new FetchExpression(queryQueueNotificationGroupList));

            if (queueNotificationGroupList == null || queueNotificationGroupList.Entities.Count <= 0)
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"No Queue Notification List found\r\n", SeverityLevel.Warning);
                return;
            }



            // Many to many table: queue notification list
            #region getting notification parties
            foreach (Entity queueNotificationGroup in queueNotificationGroupList.Entities)
            {

                // Get the notification Group PartyList
                var querynotificationGroupNotificationPartyList =
                    @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                          <entity name='new_notificationgroup'>
                            <all-attributes />
                            <order attribute='new_name' descending='false' />
                            <filter type='and'>
                                  <condition attribute='new_notificationgroupid' operator='eq' uitype='new_notificationgroup' value='{" + queueNotificationGroup.Id + @"}' />
                            </filter>
                            <link-entity name='" + notificationTypeRelationship + @"' from='new_notificationgroupid' to='new_notificationgroupid'  alias='aer'  visible='false' intersect='true'>                 
                               <all-attributes />
                                <link-entity name='new_notificationparty' from='new_notificationpartyid' to='new_notificationpartyid' alias='ae' />
                                <all-attributes />
                            </link-entity>
                          </entity>
                        </fetch>";

                var notificationGroupNotificationPartyList =
                    OrganizationService.RetrieveMultiple(new FetchExpression(querynotificationGroupNotificationPartyList));

                if (notificationGroupNotificationPartyList == null || notificationGroupNotificationPartyList.Entities.Count <= 0)
                {
                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"No Group Notification PartyList found for '{queueNotificationGroup.Id}'", SeverityLevel.Warning);
                    continue;
                }

                foreach (Entity notificationGroupNotificationParty in notificationGroupNotificationPartyList.Entities)
                {
                    EntityReference to = null;
                    var partyAliased = notificationGroupNotificationParty["aer.new_notificationpartyid"] as AliasedValue;

                    Entity party =
                        OrganizationService.Retrieve("new_notificationparty", (Guid)partyAliased.Value, new ColumnSet(true));

                    int notificationPartyType = ((OptionSetValue)party.Attributes["new_type"]).Value;
                    if (notificationPartyType == (int)NotificationPartyType.User)
                    {
                        to = party["new_userlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Contact)
                    {
                        to = party["new_contactlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Account)
                    {
                        to = party["new_accountlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Team)
                    {
                        to = party["new_teamlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Queue)
                    {
                        to = party["new_queuelookup"] as EntityReference;
                    }

                    try
                    {
                        Notify(from, to, notificationTemplate, ((EntityReference)taskEntity["regardingobjectid"]), true, false, string.Empty, taskEntityRefrence);
                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email sent to '{ to.LogicalName}' with Id '{to.Id}'\r\n", SeverityLevel.Info);
                    }
                    catch (Exception exception)
                    {
                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email failed to sent to '{to.LogicalName}' with Id '{to.Id}' due to error '{exception.Message}'", SeverityLevel.Error);
                    }
                }
                #endregion
            }
        }


        public void SendEsclationOrNotificationForSpecificQueue(
            EntityReference from,
            EntityReference notificationTemplate,
            EntityReference regardingEntity,
            NotificationType notificationType, EntityReference queue)
        {

            var queueEntity =
                OrganizationService.Retrieve("queue", queue.Id, new ColumnSet(true));

            var notificationTypeRelationship = string.Empty;
            switch (notificationType)
            {
                case NotificationType.Creation:
                    notificationTypeRelationship = "new_notification_party_group_creation";
                    break;
                case NotificationType.Reminder:
                    notificationTypeRelationship = "new_notification_party_group_notifications";
                    break;
                case NotificationType.Esclation:
                    notificationTypeRelationship = "new_notification_party_group_escalation";
                    break;
                default:
                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Unknown notification relationship\r\n", SeverityLevel.Warning);
                    return;
            }
            //Get 
            // Get the notification Group for the queue
            var queryQueueNotificationGroupList =
                @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                  <entity name='new_notificationgroup'>
                    <attribute name='new_notificationgroupid' />
                    <attribute name='new_name' />
                    <attribute name='createdon' />
                    <order attribute='new_name' descending='false' />
                    <link-entity name='" + notificationTypeRelationship + @"' from='new_notificationgroupid' to='new_notificationgroupid' visible='false' intersect='true'>
                      <link-entity name='new_notificationparty' from='new_notificationpartyid' to='new_notificationpartyid' alias='ag'>
                        <link-entity name='systemuser' from='systemuserid' to='new_userlookup' link-type='inner' alias='ah'>
                          <link-entity name='queuemembership' from='systemuserid' to='systemuserid' visible='false' intersect='true'>
                            <link-entity name='queue' from='queueid' to='queueid' alias='ai'>
                              <filter type='and'>
                                <condition attribute='queueid' operator='eq'  uitype='queue' value='{" + queueEntity.Id + @"}' />
                              </filter>
                            </link-entity>
                          </link-entity>
                        </link-entity>
                      </link-entity>
                    </link-entity>
                  </entity>
                </fetch>";

            var queueNotificationGroupList =
                OrganizationService.RetrieveMultiple(new FetchExpression(queryQueueNotificationGroupList));

            if (queueNotificationGroupList == null || queueNotificationGroupList.Entities.Count <= 0)
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"No Queue Notification List found\r\n", SeverityLevel.Warning);
                return;
            }



            // Many to many table: queue notification list
            #region getting notification parties
            foreach (Entity queueNotificationGroup in queueNotificationGroupList.Entities)
            {

                // Get the notification Group PartyList
                var querynotificationGroupNotificationPartyList =
                    @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                          <entity name='new_notificationgroup'>
                            <all-attributes />
                            <order attribute='new_name' descending='false' />
                            <filter type='and'>
                                  <condition attribute='new_notificationgroupid' operator='eq' uitype='new_notificationgroup' value='{" + queueNotificationGroup.Id + @"}' />
                            </filter>
                            <link-entity name='" + notificationTypeRelationship + @"' from='new_notificationgroupid' to='new_notificationgroupid'  alias='aer'  visible='false' intersect='true'>                 
                               <all-attributes />
                                <link-entity name='new_notificationparty' from='new_notificationpartyid' to='new_notificationpartyid' alias='ae' />
                                <all-attributes />
                            </link-entity>
                          </entity>
                        </fetch>";

                var notificationGroupNotificationPartyList =
                    OrganizationService.RetrieveMultiple(new FetchExpression(querynotificationGroupNotificationPartyList));

                if (notificationGroupNotificationPartyList == null || notificationGroupNotificationPartyList.Entities.Count <= 0)
                {
                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"No Group Notification PartyList found for '{queueNotificationGroup.Id}'", SeverityLevel.Warning);
                    continue;
                }

                foreach (Entity notificationGroupNotificationParty in notificationGroupNotificationPartyList.Entities)
                {
                    EntityReference to = null;
                    var partyAliased = notificationGroupNotificationParty["aer.new_notificationpartyid"] as AliasedValue;

                    Entity party =
                        OrganizationService.Retrieve("new_notificationparty", (Guid)partyAliased.Value, new ColumnSet(true));

                    int notificationPartyType = ((OptionSetValue)party.Attributes["new_type"]).Value;
                    if (notificationPartyType == (int)NotificationPartyType.User)
                    {
                        to = party["new_userlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Contact)
                    {
                        to = party["new_contactlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Account)
                    {
                        to = party["new_accountlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Team)
                    {
                        to = party["new_teamlookup"] as EntityReference;
                    }
                    else if (notificationPartyType == (int)NotificationPartyType.Queue)
                    {
                        to = party["new_queuelookup"] as EntityReference;
                    }

                    try
                    {
                        Notify(from, to, notificationTemplate, regardingEntity, true, false, string.Empty, regardingEntity);
                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email sent to '{ to.LogicalName}' with Id '{to.Id}'\r\n", SeverityLevel.Info);
                    }
                    catch (Exception exception)
                    {
                        Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email failed to sent to '{to.LogicalName}' with Id '{to.Id}' due to error '{exception.Message}'", SeverityLevel.Error);
                    }
                }
                #endregion
            }
        }





        public NotificationPrimitives GetPreferedLanguageAndMobile(bool withSms, Entity user, string mobileField, string toType, Guid toGuid, string mobile, int userPreferredLanguage)
        {
            NotificationPrimitives notificationPrimitives = new NotificationPrimitives();
            //partyList = new List<EntityReference> { toUser };
            var columnSet = new ColumnSet(new string[]{
                                CommonFields.PreferredLanguage});
            if (withSms)
            {
                columnSet.AddColumn(mobileField);
            }
            user = OrganizationService.Retrieve(toType, toGuid, columnSet);
            notificationPrimitives.user = user;
            if (withSms)
            {
                if (user.Attributes.Contains(mobileField))
                    notificationPrimitives.mobile = user.Attributes[mobileField].ToString();
            }
            //Get Preferred Language 
            if (user.Attributes.Contains(CommonFields.PreferredLanguage))
            {
                notificationPrimitives.userPreferredLanguage =
                    ((OptionSetValue)user.Attributes[CommonFields.PreferredLanguage]).Value;
            }
            else
            {
                notificationPrimitives.userPreferredLanguage = (int)SendNotificatioLanguage.Arabic;
            }
            return notificationPrimitives;
        }

        public EntityReference Notify(
            EntityReference fromUser,
            EntityReference toUser,
            EntityReference notificationTemplate,
            EntityReference regardingObject,
            bool withMail,
            bool withSms,
            string mobileField,
            EntityReference primaryEntity,
            bool stopSendingEmailForAttachments = false,
            bool IncludeAttachmentInNotesToBeSent = false)
        {

            EntityReference emailCreatedReference = null;
            var emailId = Guid.Empty;
            var outMailTitle = "";
            var outMail = "";
            var outSms = "";
            string EmailField = "";
            try
            {

                string arabicMailTitle, englishMailTitle, arabicMailMessage, englishMailMessage, arabicSmsMessage, englishSmsMessage;
                var sendEmail = true;
                // Party List
                List<EntityReference> partyList = null;
                //Default = Arabic
                int userPreferredLanguage = (int)SendNotificatioLanguage.Arabic;
                // CC Party List :TBD
                var ccPartyList = new List<EntityReference>();
                //Check "To" value for UserProfile or Contact or Account or SystemUser
                Guid toGuid;
                var toType = string.Empty;
                var mobile = string.Empty;
                var user = new Entity();


                //Get to whom the Email will be sent
                if (toUser != null && toUser.Id != Guid.Empty)
                {
                    toGuid = toUser.Id;
                    toType = toUser.LogicalName;


                    QueryExpression query;
                    partyList = new List<EntityReference> { toUser };
                    NotificationPrimitives notificationPrimitives = GetPreferedLanguageAndMobile(withSms, user, mobileField, toType, toGuid, mobile, userPreferredLanguage);
                    if (notificationPrimitives != null)
                    {
                        user = notificationPrimitives.user;
                        if (withSms)
                        {
                            mobile = notificationPrimitives.mobile;
                        }
                        userPreferredLanguage = notificationPrimitives.userPreferredLanguage;
                    }

                    #region Is To CrmUser
                    if (toUser.LogicalName.ToLower().Equals("systemuser"))
                    {

                    }
                    #endregion
                    #region Is To CrmTeam
                    if (toUser.LogicalName.ToLower().Equals("team"))
                    {
                        if (withMail)
                        {
                            var teamUsers =
                                RetrieveMultiple(
                                    "teammembership",
                                    new string[] { "teamid" },
                                    new object[] { toUser.Id },
                                    new string[] { "systemuserid" });
                            if (teamUsers.Count > 0)
                            {
                                foreach (var item in teamUsers)
                                {
                                    EntityReference teamUser = new EntityReference("systemuser", (Guid)item.Attributes["systemuserid"]);
                                    partyList.Add(teamUser);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Is To CrmQueue
                    if (toUser.LogicalName.ToLower().Equals("queue"))
                    {
                        //partyList = new List<EntityReference> { toUser };

                        //if (withSms)
                        //{
                        //    //Get Preferred Language
                        //    var columnSet = new ColumnSet(new string[]{
                        //         CommonFields.PreferredLanguage,
                        //        mobileField
                        //});

                        //    user = OrganizationService.Retrieve(toType, toGuid, columnSet);


                        //    if (user.Attributes.Contains(mobileField))
                        //        mobile = user.Attributes[mobileField].ToString();
                        //    if (user.Attributes.Contains(CommonFields.PreferredLanguage))
                        //    {
                        //        userPreferredLanguage =
                        //            ((OptionSetValue)user.Attributes[ CommonFields.PreferredLanguage]).Value;
                        //    }
                        //}
                    }
                    #endregion
                    #region Is To Contact
                    else if (toUser.LogicalName.ToLower().Equals("contact"))
                    {
                        EmailField = "emailaddress1";
                        var Fields = new List<String>();
                        Fields.Add(CommonFields.PreferredLanguage);
                        //Fields.Add(EmailField);

                        if (withMail)
                        {
                            Fields.Add(EmailField);
                        }

                        if (withSms)
                        {
                            Fields.Add(mobileField);
                        }

                        var columnSet = new ColumnSet(
                               Fields.ToArray()
                       );
                        // user = OrganizationService.Retrieve(toType, toGuid, columnSet);
                        // partyList = new List<EntityReference> { toUser };


                        if (!user.Attributes.Contains(EmailField))
                            withMail = false;

                        if (withSms && !string.IsNullOrEmpty(mobileField) && user.Attributes.Contains(mobileField))
                            mobile = user.Attributes[mobileField].ToString();
                        else
                            withSms = false;


                        //if (user.Attributes.Contains( CommonFields.PreferredLanguage))
                        //{
                        //    userPreferredLanguage =
                        //        ((OptionSetValue)user.Attributes[ CommonFields.PreferredLanguage]).Value;
                        //}

                    }
                    #endregion
                    #region Is To Account
                    else if (toUser.LogicalName.ToLower().Equals("account"))
                    {
                        // partyList = new List<EntityReference> { toUser };
                        //if (withSms)
                        {
                            //Get Preferred Language
                            //    var columnSet = new ColumnSet(new string[]{
                            //         CommonFields.PreferredLanguage,
                            //        mobileField
                            //});
                            //  user = OrganizationService.Retrieve(toType, toGuid, columnSet);
                            //Get Preferred Language
                            if (withSms && !string.IsNullOrEmpty(mobileField) && user.Attributes.Contains(mobileField))
                                mobile = user.Attributes[mobileField].ToString();
                            else
                                withSms = false;
                        }
                    }

                    #endregion
                    #region Other
                    else
                    {
                        // partyList = new List<EntityReference> { toUser };

                        // if (withSms)
                        {
                            //Get Preferred Language
                            //var columnSet = new ColumnSet(new string[]{
                            //    mobileField
                            //});

                            //user = OrganizationService.Retrieve(toType, toGuid, columnSet);

                            if (withSms && !string.IsNullOrEmpty(mobileField) && user.Attributes.Contains(mobileField))
                                mobile = user.Attributes[mobileField].ToString();
                            else
                                withSms = false;
                        }
                    }

                    #endregion
                }
                var preferredLanguage = (SendNotificatioLanguage)userPreferredLanguage;
                if (notificationTemplate != null)
                {
                    #region Get Notification Template
                    var notiTempQ1 =
                        OrganizationService.Retrieve(
                        "ldv_notificationtemplate",
                        notificationTemplate.Id,
                        new ColumnSet(true));
                    #endregion

                    //Get English Notification Template
                    if (notiTempQ1 != null && sendEmail)
                    {
                        var mailTitle = "";
                        var mail = "";
                        var sms = "";

                        if (preferredLanguage == SendNotificatioLanguage.English)
                        {
                            mailTitle = notiTempQ1.Contains("ldv_englishemailtitle") ? (notiTempQ1["ldv_englishemailtitle"] as string) : "";
                            mail = notiTempQ1.Contains("ldv_englishemailmessage") ? (notiTempQ1["ldv_englishemailmessage"] as string) : "";
                            sms = notiTempQ1.Contains("ldv_englishsmsmessage") ? (notiTempQ1["ldv_englishsmsmessage"] as string) : "";
                        }
                        else if (preferredLanguage == SendNotificatioLanguage.Arabic)
                        {
                            mailTitle = notiTempQ1.Contains("ldv_arabicemailtitle") ? (notiTempQ1["ldv_arabicemailtitle"] as string) : "";
                            mail = notiTempQ1.Contains("ldv_arabicemailmessage") ? (notiTempQ1["ldv_arabicemailmessage"] as string) : "";
                            sms = notiTempQ1.Contains("ldv_arabicsmsmessage") ? (notiTempQ1["ldv_arabicsmsmessage"] as string) : "";
                        }

                        outMailTitle = GetMessageWithValues(mailTitle, primaryEntity);
                        outMail = GetMessageWithValues(mail, primaryEntity);
                        outSms = GetMessageWithValues(sms, primaryEntity);

                        emailId = CreateEmail(
                                fromUser,
                                partyList,
                                ccPartyList,
                                regardingObject,
                                outMailTitle,
                                outMail);

                        if (IncludeAttachmentInNotesToBeSent)
                            AttachAnnotationFromEmailTemplateToEmail(emailId, notificationTemplate.Id);

                        if (partyList.Count > 0 && partyList[0].LogicalName != null && partyList[0].LogicalName != string.Empty)
                        {
                            if (!stopSendingEmailForAttachments && emailId != null && emailId != Guid.Empty)
                            {
                                try
                                {
                                    SendEmail(emailId);
                                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email Sent successfully.", SeverityLevel.Info);
                                }
                                catch (Exception exceptionInSendingEmail)
                                {
                                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Failed to send email.", SeverityLevel.Error);
                                    Logger.LogException(LoggerHandler.GetMethodFullName(), exceptionInSendingEmail);
                                }
                            }
                        }


                        if (withSms)
                        {
                            //Create SMS regarding teh application
                            var userReference = user.ToEntityReference();
                            CreateSms(regardingObject, mobile, outMailTitle, outSms);
                            Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Sms Created successfully.", SeverityLevel.Info);
                        }
                    }
                }

                // Save the email id that had been sent
                if (emailId != Guid.Empty)
                    emailCreatedReference = new EntityReference("email", emailId);
            }
            catch (Exception err)
            {
                throw;
                // Logger.LogException(err, PriorityLevel.HIGH, err.Message, organizationService);
            }
            return emailCreatedReference;
        }

        public EntityReference NotifyRelationship(
            EntityReference fromUser,
            List<EntityReference> partyIds,
            EntityReference notificationTemplate,
            EntityReference regardingObject,
            EntityReference primaryEntity,
            bool stopSendingEmailForAttachments)
        {


            EntityReference emailCreatedReference = null;

            if (partyIds.Count <= 0)
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"No parties to notify", SeverityLevel.Warning);
                return emailCreatedReference;
            }

            #region Get Notification Template
            var notiTempQ1 =
            OrganizationService.Retrieve(
            "ldv_notificationtemplate",
            notificationTemplate.Id,
            new ColumnSet(true));
            #endregion

            var mailTitle = notiTempQ1.Contains("ldv_englishemailtitle") ? (notiTempQ1["ldv_englishemailtitle"] as string) : "";
            var mail = notiTempQ1.Contains("ldv_englishemailmessage") ? (notiTempQ1["ldv_englishemailmessage"] as string) : "";
            var outMailTitle = GetMessageWithValues(mailTitle, primaryEntity);
            var outMail = GetMessageWithValues(mail, primaryEntity);

            var emailId =
                CreateEmail(fromUser, partyIds, new List<EntityReference>(), regardingObject, outMailTitle, outMail);
            emailCreatedReference = new EntityReference("email", emailId);

            if (!stopSendingEmailForAttachments && emailId != null && emailId != Guid.Empty)
            {
                SendEmail(emailId);
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email Sent successfully.", SeverityLevel.Info);
            }

            if (notiTempQ1.Contains("ldv_usesms") && notiTempQ1.GetAttributeValue<bool>("ldv_usesms"))
            {
                var sms = notiTempQ1.Contains("ldv_englishsmsmessage") ? (notiTempQ1["ldv_englishsmsmessage"] as string) : "";
                var outSms = GetMessageWithValues(sms, primaryEntity);

                if (!string.IsNullOrEmpty(outSms))
                {
                    foreach (var item in partyIds)
                    {
                        var tmp = OrganizationService.Retrieve(item.LogicalName, item.Id, new ColumnSet(new[] { "mobilephone" }));
                        if (tmp.Contains("mobilephone"))
                        {
                            var mobilenumber = tmp.GetAttributeValue<string>("mobilephone");

                            CreateSms(regardingObject, mobilenumber, outMailTitle, outSms);
                            Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Sms Created successfully.", SeverityLevel.Info);
                        }
                        else
                        {
                            Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Outsms is empty", SeverityLevel.Warning);
                        }
                    }

                }
                else
                {
                    Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Outsms is empty", SeverityLevel.Warning);
                }
            }
            else
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Use sms is disabled", SeverityLevel.Info);
            }

            return emailCreatedReference;
        }

        public Guid CreateEmail(EntityReference from, List<EntityReference> partyIds, List<EntityReference> ccpartyIds, EntityReference regardingObject, string subject, string message)
        {
            try
            {
                if (from == null || from.Id == Guid.Empty)
                    throw new Exception("'from' in the email message can't be null");

                Entity[] toPartyList = new Entity[partyIds.Count];
                Entity[] ccPartyList = new Entity[ccpartyIds.Count];

                for (int i = 0; i < partyIds.Count; i++)
                {
                    if (partyIds[i] == null || partyIds[i].Id == Guid.Empty)
                        throw new Exception("'to' in the email message can't be null");

                    Entity toParty = new Entity("activityparty");
                    toParty.Attributes.Add("partyid", partyIds[i]);
                    toPartyList[i] = toParty;
                }
                for (int i = 0; i < ccpartyIds.Count; i++)
                {
                    if (!(ccpartyIds[i] == null || ccpartyIds[i].Id == Guid.Empty))
                    {

                        Entity ccParty = new Entity("activityparty");
                        ccParty.Attributes.Add("partyid", ccpartyIds[i]);
                        ccPartyList[i] = ccParty;
                    }
                }

                var fromParty = new Entity("activityparty");
                fromParty.Attributes.Add("partyid", from);

                var collFromParty = new EntityCollection
                {
                    EntityName = "systemuser"
                };
                collFromParty.Entities.Add(fromParty);

                Entity email = new Entity("email");
                email.Attributes.Add("subject", subject);
                email.Attributes.Add("description", message);
                email.Attributes.Add("from", collFromParty);
                email.Attributes.Add("to", toPartyList);
                if (ccPartyList.Length > 0)
                {
                    email.Attributes.Add("cc", ccPartyList);
                }
                if (regardingObject.LogicalName.ToLower() != "task" && regardingObject.LogicalName.ToLower() != "serviceappointment")
                    email.Attributes.Add("regardingobjectid", (EntityReference)regardingObject);

                Guid emailGuid = OrganizationService.Create(email);

                return emailGuid;
            }
            catch (Exception ex)
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"SendNotificationBLL-CreateEmail: Exception: {ex.Message}", SeverityLevel.Error);
                throw;
            }
        }

        public EntityReference UpdateEmail(string subject, string message, EntityReference email, ILogger Tracer)
        {

            Entity Updatedemail = new Entity(email.LogicalName, email.Id);
            Updatedemail.Attributes.Add("subject", subject);
            Updatedemail.Attributes.Add("description", message);
            OrganizationService.Update(Updatedemail);
            Logger.LogComment(LoggerHandler.GetMethodFullName(), $"Email updated successfully.", SeverityLevel.Info);
            return Updatedemail.ToEntityReference();
        }

        public void SendEmail(Guid EmailID)
        {
            try
            {
                // Create a SendEmail request.

                SendEmailRequest requestSendEmail = new SendEmailRequest();
                requestSendEmail.EmailId = EmailID;
                requestSendEmail.TrackingToken = "";
                requestSendEmail.IssueSend = true;

                // Send the e-mail message.
                SendEmailResponse responseSendEmail = (SendEmailResponse)OrganizationService.Execute(requestSendEmail);
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"SendNotificationBLL-SendEmail: Email Sent successfully.", SeverityLevel.Info);

            }
            catch (Exception ex)
            {
                Logger.LogComment(LoggerHandler.GetMethodFullName(), $"SendNotificationBLL-SendEmail: Exception: {ex.Message}", SeverityLevel.Error);
                throw;
            }
        }

        public void CreateSms(EntityReference regardingId, string mobileNumber, string subject, string message)
        {
            try
            {

                CreateSms(regardingId, mobileNumber, subject, message, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CreateSms(EntityReference regardingId, string mobileNumber, string subject, string message, string title)
        {
            try
            {
                Entity sms = new Entity("ldv_sms");
                sms.Attributes.Add("subject", subject);
                sms.Attributes.Add("ldv_mobilenumber", mobileNumber);
                sms.Attributes.Add("description", message);
                sms.Attributes.Add("ldv_sendsms", true);
                if (regardingId.LogicalName.ToLower() != "serviceappointment")
                    sms.Attributes.Add("regardingobjectid", regardingId);

                // sms.Attributes.Add("regardingobjectid", regardingId);
                if (!string.IsNullOrEmpty(title))
                    sms.Attributes.Add("subject", title);
                var guid =
                OrganizationService.Create(sms);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public EntityReference GetEntityRefrences(string message, EntityReference primaryEntity)
        {

            message = message.Trim();

            var parenthesesPattern = @"\{[a-zA-Z0-9:_]*[^\}\{]\}";
            string variablePattern = @"[a-zA-Z_]+[a-zA-Z_0-9]*";

            var parenthesesRgx = new Regex(parenthesesPattern);
            var variableRgx = new Regex(variablePattern);

            var parenthesesMatch = parenthesesRgx.Match(message);
            if (!parenthesesMatch.Success)
                throw new FormatException(string.Format(@"'{0}' doesn't meet the format, Must be like this {1} variable:variable {2}", message, "{", "}"));

            message = message.Trim(new char[] { '{', '}' });

            var variablesMatches = variableRgx.Matches(message);


            var primaryEntityWithLookup = primaryEntity;
            for (int i = 0; i < variablesMatches.Count; i++)
            {
                var lookupName = variablesMatches[i].Value;
                primaryEntityWithLookup = GetFieldValue(primaryEntityWithLookup, lookupName);
            }

            if (variablesMatches.Count <= 0)
                primaryEntityWithLookup = null;

            return primaryEntityWithLookup;
        }

        public string GetMessageWithValues(string message, EntityReference primaryEntity)
        {
            return CrmStringHandler.Substitute(primaryEntity, message, OrganizationService);
        }

        public Guid CreateEmailAttachment(EntityReference emailReference, string subject, string filename, string mimeType, string base64)
        {

            var activitymimeattachment = new Entity("activitymimeattachment")
            {
                ["subject"] = subject,
                ["filename"] = filename,
                ["body"] = base64,
                ["mimetype"] = mimeType,
                ["objectid"] = emailReference,
                ["objecttypecode"] = "email"
            };

            return OrganizationService.Create(activitymimeattachment);
        }

        public string GetEntityEmailableAttribute(IOrganizationService OrganizationService, string entityLogicalName)
        {
            try
            {
                RetrieveEntityRequest retrieveEntityRequest = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.Attributes,
                    LogicalName = entityLogicalName
                };
                RetrieveEntityResponse retrieveEntityResponse = (RetrieveEntityResponse)OrganizationService.Execute(retrieveEntityRequest);
                EntityMetadata entityMetadata = retrieveEntityResponse.EntityMetadata;

                if (entityMetadata != null && entityMetadata.IsActivityParty != null && (bool)entityMetadata.IsActivityParty)
                {
                    AttributeMetadata attributeMetadata = (from p in entityMetadata.Attributes
                                                           where p.AttributeTypeName == AttributeTypeDisplayName.StringType && ((Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata)p).Format == StringFormat.Email
                                                           select p).OrderBy(x => x.ColumnNumber).ToList()[0];

                    return attributeMetadata.LogicalName;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public List<Entity> GetAllTasksOwners(EntityReference targetEntity)
        {
            #region QueryExpression to get all tasks owners
            var taskQuery = new QueryExpression("task");
            taskQuery.ColumnSet.AddColumns("ownerid", "ldv_decisionmadebysystemuserid");
            var requestLink = taskQuery.AddLink(targetEntity.LogicalName, "regardingobjectid", targetEntity.LogicalName + "id");
            //requestLink.EntityAlias = "ab";
            requestLink.LinkCriteria.AddCondition(targetEntity.LogicalName + "id", ConditionOperator.Equal, targetEntity.Id);
            #endregion
            EntityCollection relatedEntities = OrganizationService.RetrieveMultiple(taskQuery);
            //  if (!relatedEntities.Entities.Any()) tracingService.Trace($" There are no related tasks for {targetEntity.LogicalName } , of id  { targetEntity.Id} ");
            return relatedEntities.Entities.ToList();
        }
        public List<Entity> CheckIfOwnerIsInvolvedInOneOfTheQueue(Guid queueId, Guid to)
        {
            #region check if owner is involved in one of the   queue 
            var queueQuery = new QueryExpression("queue");
            queueQuery.Distinct = true;
            queueQuery.Criteria.AddCondition("queueid", ConditionOperator.Equal, queueId);
            var QEqueue_queuemembership = queueQuery.AddLink("queuemembership", "queueid", "queueid");
            var queue_systemuser = QEqueue_queuemembership.AddLink("systemuser", "systemuserid", "systemuserid");
            queue_systemuser.LinkCriteria.AddCondition("systemuserid", ConditionOperator.Equal, to);
            #endregion
            EntityCollection queueEntity = OrganizationService.RetrieveMultiple(queueQuery);
            if (!queueEntity.Entities.Any())
            {
                return null;
            }
            return queueEntity.Entities.ToList();
        }
        public void EscalationAndNotificationToTasksOwner(IOrganizationService organizationService, EntityReference targetEntity, EntityReference epmActivity,
            EntityReference from, EntityReference notificationTemplate, NotificationType notificationType,
            bool isSendToAllQueues, string exceptQueuesIDs, ILogger Tracer)
        {
            if (targetEntity?.Id != Guid.Empty)
            {
                Tracer.LogComment(LinkDev.Common.Crm.Logger.LoggerHandler.GetMethodFullName(), $"in method EscalationAndNotificationToTasksOwner", SeverityLevel.Info);
                #region QueryExpression to get all tasks owners

                //var  taskQuery = new QueryExpression("task");
                //taskQuery.ColumnSet.AddColumns("ownerid", "ldv_decisionmadebysystemuserid");
                //var requestLink = taskQuery.AddLink(targetEntity.LogicalName, "regardingobjectid", targetEntity.LogicalName + "id");
                ////requestLink.EntityAlias = "ab";
                //requestLink.LinkCriteria.AddCondition(targetEntity.LogicalName+"id", ConditionOperator.Equal, targetEntity.Id);

                #endregion
                List<Entity> relatedEntities = GetAllTasksOwners(targetEntity);
                if (relatedEntities.Any())
                {
                    List<Guid> systemUserIds = new List<Guid>();
                    foreach (Entity task in relatedEntities)
                    {
                        EntityReference toEntityReference = task.GetAttributeValue<EntityReference>("ldv_decisionmadebysystemuserid");
                        Guid to = Guid.Empty;
                        if (toEntityReference?.Id != Guid.Empty && toEntityReference != null)
                        {
                            to = toEntityReference.Id;
                            //check id not repeated
                            if (!systemUserIds.Contains(to))
                            {
                                //check queue not in except
                                if (!isSendToAllQueues && to != Guid.Empty && toEntityReference != null)
                                {
                                    string[] queueIds = exceptQueuesIDs.Split(',');
                                    bool existInQueue = false;
                                    foreach (var queueId in queueIds)
                                    {
                                        if (queueId != null && new Guid(queueId) != Guid.Empty)
                                        {
                                            List<Entity> queueEntity = CheckIfOwnerIsInvolvedInOneOfTheQueue(new Guid(queueId), to);
                                            if (queueEntity != null)
                                            {
                                                existInQueue = true;
                                            }
                                        }
                                    }
                                    if (!existInQueue)
                                    {
                                        systemUserIds.Add(toEntityReference.Id);
                                    }
                                }
                                else
                                {
                                    systemUserIds.Add(toEntityReference.Id);
                                }
                            }
                        }
                    }
                    if (systemUserIds.Any())
                    {
                        foreach (Guid systemUserId in systemUserIds)
                        {
                            Notify(from, new EntityReference("systemuser", systemUserId), notificationTemplate, epmActivity, true, false, string.Empty, epmActivity);
                        }
                    }
                }
                else
                {
                    Tracer.LogComment(LinkDev.Common.Crm.Logger.LoggerHandler.GetMethodFullName(), $"There are no related tasks for { targetEntity.LogicalName } , of id  { targetEntity.Id}", SeverityLevel.Info);
                }

                //notify
                //    if (!isSendToAllQueues && to!=Guid.Empty && toEntityReference != null)
                //    {
                //        string[] queueIds = exceptQueuesIDs.Split(',');
                //        foreach (var queueId in queueIds)
                //        {
                //            if (queueId!=null && new Guid( queueId)!=Guid.Empty )
                //            {
                //                #region check if owner is involved in one of the queue 
                //                //var queueQuery = new QueryExpression("queue");
                //                //queueQuery.Distinct = true;
                //                //queueQuery.Criteria.AddCondition("queueid", ConditionOperator.Equal, queueId);
                //                //var QEqueue_queuemembership = queueQuery.AddLink("queuemembership", "queueid", "queueid");
                //                //var queue_systemuser = QEqueue_queuemembership.AddLink("systemuser", "systemuserid", "systemuserid");
                //                //queue_systemuser.LinkCriteria.AddCondition("systemuserid", ConditionOperator.Equal, to);
                //                #endregion
                //                //EntityCollection queueEntity = organizationService.RetrieveMultiple(queueQuery);

                //                List<Entity> queueEntity = CheckIfOwnerIsInvolvedInOneOfTheQueue(new Guid(queueId), to);
                //                Tracer.LogComment(LinkDev.Common.Crm.Logger.LoggerHandler.GetMethodFullName(), $"queues Entities count { queueEntity.Count }", SeverityLevel.Info);
                //                if (!queueEntity.Any() )
                //                {
                //                    Notify(from, new EntityReference("systemuser", to), notificationTemplate, epmActivity, true, false, string.Empty, epmActivity, true);
                //                }
                //            }                            
                //        }
                //    }
                //    else if( to != Guid.Empty && toEntityReference != null)
                //    {
                //        Notify(from, new EntityReference("systemuser", to), notificationTemplate, epmActivity, true, false, string.Empty, epmActivity, true);
                //    }
                //}
            }
        }
        /// <summary>
        /// This method used to update email subject and body
        /// </summary>
        /// <param name="regardingEntity"></param>
        /// <param name="createdEmail"></param>
        /// <param name="notificationTemplate"></param>
        public EntityReference UpdateCreatetedEmail(EntityReference primaryEntity, EntityReference createdEmail, EntityReference notificationTemplate, bool sendEmail, ILogger Tracer)
        {
            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"primaryEntity :  {primaryEntity.LogicalName} ,id: {primaryEntity.Id} ,notificationTemplate : {notificationTemplate.Id}", SeverityLevel.Info);

            #region Get Notification Template
            var notiTempQ1 =
                OrganizationService.Retrieve(
                "ldv_notificationtemplate",
                notificationTemplate.Id,
                new ColumnSet(true));
            #endregion
            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"notiTempQ1 id :  {notiTempQ1.Id}", SeverityLevel.Info);

            //Get English Notification Template
            if (notiTempQ1 != null)
            {
                string mailTitle = notiTempQ1.Contains("ldv_englishemailtitle") ? (notiTempQ1["ldv_englishemailtitle"] as string) : "";
                string mail = notiTempQ1.Contains("ldv_englishemailmessage") ? (notiTempQ1["ldv_englishemailmessage"] as string) : "";
                string outMailTitle = GetMessageWithValues(mailTitle, primaryEntity);
                string outMail = GetMessageWithValues(mail, primaryEntity);
                Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"outMailTitle :  {outMailTitle}", SeverityLevel.Info);
                Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"outMail :  {outMail}", SeverityLevel.Info);

                createdEmail = UpdateEmail(outMailTitle, outMail, createdEmail, Tracer);
                if (sendEmail)
                {
                    SendEmail(createdEmail.Id);
                }
                return createdEmail;
            }
            return createdEmail;
        }
        #endregion

        #region Private

        private void AttachAnnotationFromEmailTemplateToEmail(Guid emailId, Guid notificationTemplateId)
        {
            var query = new QueryExpression()
            {

                EntityName = "annotation",
                ColumnSet = new ColumnSet(new string[] { "isdocument", "filename", "documentbody", "mimetype" }),
                Criteria = new FilterExpression()
                {
                    Conditions =
                        {
                            new ConditionExpression("objectid",ConditionOperator.Equal,notificationTemplateId.ToString())
                        }
                }
            };

            var retrievedAnnotation =
                    OrganizationService.RetrieveMultiple(query);

            var requestWithResults = new ExecuteMultipleRequest()
            {
                // Assign settings that define execution behavior: continue on error, return responses. 
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };

            for (int i = 0; i < retrievedAnnotation.Entities.Count; i++)
            {
                var item = retrievedAnnotation.Entities[i];

                if (!item.Contains("isdocument") || !((bool)item["isdocument"]))
                    continue;

                if (!item.Contains("filename"))
                    throw new Exception($"File Name is missing 'file name' from annotation with id '{item.Id}'. Failed to send it as attachment in email");
                if (!item.Contains("documentbody"))
                    throw new Exception($"File Name is missing 'documentbody' from annotation with id '{item.Id}'. Failed to send it as attachment in email");
                if (!item.Contains("mimetype"))
                    throw new Exception($"File Name is missing 'mimetype' from annotation with id '{item.Id}'. Failed to send it as attachment in email");

                var attachment = new Entity("activitymimeattachment");
                attachment["filename"] = item["filename"].ToString();
                attachment["body"] = item["documentbody"].ToString();
                attachment["mimetype"] = item["mimetype"].ToString();
                attachment["attachmentnumber"] = i;
                attachment["objectid"] = new EntityReference("email", emailId);
                attachment["objecttypecode"] = "email";


                // Add a CreateRequest for each entity to the request collection.
                var createRequest = new CreateRequest { Target = attachment };
                requestWithResults.Requests.Add(createRequest);
            }
            // Execute all the requests in the request collection using a single web method call.
            if (requestWithResults.Requests.Count > 0)
            {
                var responseWithResults =
                    (ExecuteMultipleResponse)OrganizationService.Execute(requestWithResults);

                // Display the results returned in the responses.
                foreach (var responseItem in responseWithResults.Responses)
                {
                    // An error has occurred.
                    if (responseItem.Fault != null)
                        throw new Exception($"Error '{responseItem.Fault}' while trying to add annotation to email with Id ''");
                }
            }
        }

        private string GetFieldValue(string lookupName, string fieldName, EntityReference primaryEntity, bool isLookup)
        {
            Entity currEntity = new Entity();
            string fieldValue = string.Empty;

            if (isLookup)
            {
                ColumnSet retrievedCols = new ColumnSet(new string[]
                {
                    lookupName
                });
                currEntity = OrganizationService.Retrieve(primaryEntity.LogicalName, primaryEntity.Id, retrievedCols);

                if (currEntity.Attributes.Contains(lookupName))
                {
                    string entityType = ((EntityReference)currEntity.Attributes[lookupName]).LogicalName;
                    Guid entityId = ((EntityReference)currEntity.Attributes[lookupName]).Id;

                    ColumnSet retrievedLookupCols = new ColumnSet(new string[]
                    {
                        fieldName
                    });
                    Entity lookupEntity = OrganizationService.Retrieve(entityType, entityId, retrievedLookupCols);

                    fieldValue = GetFieldValueToString(lookupEntity, fieldName);
                }


            }
            else
            {
                ColumnSet retrievedCols = new ColumnSet(new string[]
                {
                    fieldName
                });
                currEntity = OrganizationService.Retrieve(primaryEntity.LogicalName, primaryEntity.Id, retrievedCols);

                if (currEntity.Attributes.Contains(fieldName))
                    fieldValue = GetFieldValueToString(currEntity, fieldName);
            }

            return fieldValue;
        }

        private EntityReference GetFieldValue(EntityReference primaryEntity, string lookupName)
        {
            Entity currEntity = new Entity();
            string fieldValue = string.Empty;

            ColumnSet retrievedCols = new ColumnSet(new string[] { lookupName });

            currEntity = OrganizationService.Retrieve(
                primaryEntity.LogicalName,
                primaryEntity.Id,
                retrievedCols);

            if (currEntity.Attributes.Contains(lookupName))
                return currEntity.Attributes[lookupName] as EntityReference;

            return null;
        }

        private string GetFieldValueToString(Entity entity, string fieldName)
        {
            if (entity.Attributes.Contains(fieldName))
            {
                if (entity.Attributes[fieldName] is String) //String
                    return ((String)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is OptionSetValue) //OptionSet
                {
                    string text = entity.FormattedValues[fieldName].ToString();
                    return text;
                    //return ((OptionSetValue)entity.Attributes[fieldName]).Value.ToString();
                }

                else if (entity.Attributes[fieldName] is DateTime) //DateTime
                    return ((DateTime)entity.Attributes[fieldName]).ToLocalTime().ToString();

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

                else if (entity.Attributes[fieldName] is Guid) //Guid
                    return (entity.Attributes[fieldName].ToString());

                else
                    return "";
            }
            else
                return "";
        }

        public List<Entity> RetrieveMultiple(string EntityName, string[] Attributes, object[] Values, string[] Columns)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                if (Attributes.Length != 0 && Values.Length != 0)
                {
                    QueryByAttribute query = new QueryByAttribute(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }
                    query.Attributes.AddRange(Attributes);
                    query.Values.AddRange(Values);

                    // Execute the retrieval.
                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Add(item);
                        }
                        return Entities;
                    }
                }
                else
                {
                    QueryExpression query = new QueryExpression(EntityName);
                    if (Columns.Length != 0)
                    {
                        query.ColumnSet = new ColumnSet(Columns);
                    }
                    else
                    {
                        query.ColumnSet = new ColumnSet() { AllColumns = true };
                    }

                    EntityCollection retrieved = OrganizationService.RetrieveMultiple(query);
                    if (retrieved.Entities.Count == 0)
                        return null;
                    else
                    {

                        foreach (var item in retrieved.Entities)
                        {
                            Entities.Add(item);
                        }
                        return Entities;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
