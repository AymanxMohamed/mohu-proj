using LinkDev.Common.Crm.Bll.Notification;
using LinkDev.Common.Crm.Cs.Base;
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
    public class EscalationAndNotificationForSpecificQueue : CustomStepBase
    {
        #region "Input Parameters"
        [Input("EntityReference From")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> From { get; set; }

        [Input("Notification Template")]
        [ReferenceTarget("ldv_notificationtemplate")]
        public InArgument<EntityReference> Notification { get; set; }

        [Input("Send Creation Notification")]
        [Default("false")]
        public InArgument<bool> isCreationNotification { get; set; }

        [Input("Send Reminder Notification")]
        [Default("false")]
        public InArgument<bool> isReminderNotification { get; set; }

        [Input("Send Esclation Notification")]
        [Default("false")]
        public InArgument<bool> isEsclationNotification { get; set; }


        [Input("Queue")]
        [ReferenceTarget("queue")]
        public InArgument<EntityReference> Queue { get; set; }

        #endregion

        public override void ExtendedExecute()
        {
            var sendNotificationBll = new SendNotification(OrganizationService, Tracer, LanguageCode);
            var fromWhom =
               new EntityReference
                   (From.Get<EntityReference>(ExecutionContext).LogicalName,
                  From.Get<EntityReference>(ExecutionContext).Id);

            var notificationTemplate =
                new EntityReference
                    (Notification.Get<EntityReference>(ExecutionContext).LogicalName,
                   Notification.Get<EntityReference>(ExecutionContext).Id);

            //EntityReference task = Task.Get(ExecutionContext);

            var notificationType = NotificationType.Null;

            if (isCreationNotification.Get<bool>(ExecutionContext))
            {
                notificationType = NotificationType.Creation;
            }
            else if (isReminderNotification.Get<bool>(ExecutionContext))
            {
                notificationType = NotificationType.Reminder;
            }
            else if (isEsclationNotification.Get<bool>(ExecutionContext))
            {
                notificationType = NotificationType.Esclation;
            }

            if (notificationType != NotificationType.Null)
            {
                Tracer.LogComment(Logger.LoggerHandler.GetMethodFullName(), $"Notify as '{notificationType}'\r\n", Logger.SeverityLevel.Info);
                sendNotificationBll.SendEsclationOrNotificationForSpecificQueue(
                    fromWhom,
                    notificationTemplate,
                    new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId),
                    notificationType, Queue.Get(ExecutionContext));
            }
        }
    }
}
