using LinkDev.Common.Crm.Bll.Notification;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Logger;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Notify
{
    public class EscalationAndNotificationToTasksOwner : CustomStepBase
    {
        #region "Input Parameters"
        [Input("EntityReference From")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> From { get; set; }

        [Input("Notification Template")]
        [ReferenceTarget("ldv_notificationtemplate")]
        public InArgument<EntityReference> Notification { get; set; }

        //[Input("Send Creation Notification")]
        //[Default("false")]
        //public InArgument<bool> isCreationNotification { get; set; }

        //[Input("Send Reminder Notification")]
        //[Default("false")]
        //public InArgument<bool> isReminderNotification { get; set; }

        //[Input("Send Esclation Notification")]
        //[Default("false")]
        //public InArgument<bool> isEsclationNotification { get; set; }

        [RequiredArgument]
        [Input("Entity Logical Name")]
        public InArgument<string> EntityLogicalName { get; set; }
        [RequiredArgument]
        [Input("Entity ID")]
        public InArgument<string> EntityId { get; set; }

        [Default("false")]
        [Input("Is Send To All Queues")]
        [RequiredArgument]
        public InArgument<bool> IsSendToAllQueues { get; set; }

        [Input("Exept Queues IDs(Comma Seprated)")]
        public InArgument<string> ExceptQueuesIDs { get; set; }
        #endregion

        public override void ExtendedExecute()
        {            
            #region Check if input paramaters are null
            if (EntityLogicalName.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format($"EntityLogicalName{ EntityLogicalName} is null "));
            if (EntityId.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format($"EntityId{ EntityId} is null "));
            if (IsSendToAllQueues.Get<bool>(ExecutionContext) == true && ExceptQueuesIDs.Get<string>(ExecutionContext) == null )
                throw new Exception(string.Format($"ExceptQueuesIDs { ExceptQueuesIDs} is null "));




            #endregion

            #region Map input paramaters 
            Guid epmActivityId = Context.PrimaryEntityId;
            string epmActivityLogicalName = Context.PrimaryEntityName;
            EntityReference epmActivity = new EntityReference(epmActivityLogicalName, epmActivityId);

            string entityLogicalName = EntityLogicalName.Get<string>(ExecutionContext);
            string entityId = EntityId.Get<string>(ExecutionContext);
            bool isSendToAllQueues = IsSendToAllQueues.Get<bool>(ExecutionContext);
            string exceptQueuesIDs= ExceptQueuesIDs.Get<string>(ExecutionContext);
            var fromWhom = new EntityReference
                 (From.Get<EntityReference>(ExecutionContext).LogicalName,
                  From.Get<EntityReference>(ExecutionContext).Id);

            var notificationTemplate =new EntityReference
                    (Notification.Get<EntityReference>(ExecutionContext).LogicalName,
                     Notification.Get<EntityReference>(ExecutionContext).Id);

            var notificationType = NotificationType.Null;

            //if (isCreationNotification.Get<bool>(ExecutionContext))
            //{
            //    notificationType = NotificationType.Creation;
            //}
            //else if (isReminderNotification.Get<bool>(ExecutionContext))
            //{
            //    notificationType = NotificationType.Reminder;
            //}
            //else if (isEsclationNotification.Get<bool>(ExecutionContext))
            //{
            //    notificationType = NotificationType.Esclation;
            //}

            #endregion

            #region call BLL method  EscalationAndNotificationToTasksOwner
            var sendNotificationBll = new SendNotification(OrganizationService, Tracer, LanguageCode);                        
                //Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"Notify as '{notificationType}'\r\n", Logger.SeverityLevel.Info);
                //tracingService.Trace($"in the beginning");
            Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"in the beginning2", SeverityLevel.Info);

            sendNotificationBll.EscalationAndNotificationToTasksOwner(OrganizationService,
                        new EntityReference(entityLogicalName,new Guid(entityId)),
                         epmActivity,
                        fromWhom,
                        notificationTemplate,
                        notificationType,
                        isSendToAllQueues, 
                        exceptQueuesIDs,
                        Tracer);          
            #endregion
        }
    }
}
