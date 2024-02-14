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
    public class NotifyFromUnknownContext : CustomStepBase
    {
        #region "Input Parameters"

        [Input("EntityReference From")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> From { get; set; }

        [Input("Notification Template")]
        [ReferenceTarget("ldv_notificationtemplate")]
        public InArgument<EntityReference> Notification { get; set; }

        [Input("Use the given Context")]
        [Default("true")]
        public InArgument<bool> UseContext { get; set; }



        [Input("Context LogicalName")]
        public InArgument<string> ContextLogicalName { get; set; }
        [Input("context Id")]
        public InArgument<string> ContextId { get; set; }



        [Input("Emailable Entity ('In Case Use Context' = No)")]
        public InArgument<string> EmailableEntity { get; set; }

        [Input("Use Workflow Context As Regarding")]
        [Default("true")]
        public InArgument<bool> UseContextInRegarding { get; set; }

        [Input("Regarding Entity (In Case 'Use Workflow Context As Regarding' = No)")]
        public InArgument<string> RegardingEntity { get; set; }

        [Input("Send EMail")]
        [Default("False")]
        public InArgument<bool> Email { get; set; }

        [Input("Include Attachment in Notes To Be Sent in mail")]
        [Default("False")]
        public InArgument<bool> IncludeAttachmentInNotesToBeSent { get; set; }

        [Input("Stop Sending Email For Attachments")]
        [Default("False")]
        public InArgument<bool> StopSendingEmailForAttachments { get; set; }

        [Input("Send SMS")]
        [Default("False")]
        public InArgument<bool> SMS { get; set; }

        [Input("Mobile Field (Schemaname)")]
        public InArgument<string> MobileShemaName { get; set; }

        [Output("Email Created")]
        [ReferenceTarget("email")]
        public OutArgument<EntityReference> EmailCreated { get; set; }

        #endregion

        public override void ExtendedExecute()
        {
            var sendNotificationBll = new SendNotification(OrganizationService, Tracer,LanguageCode);

            var context = new EntityReference(ContextLogicalName.Get<string>(ExecutionContext), new Guid(ContextId.Get<string>(ExecutionContext)));

            // From Whom
            //
            if (Notification.Get<EntityReference>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} is null", "From"));

            var fromWhom =
                new EntityReference
                    (From.Get<EntityReference>(ExecutionContext).LogicalName,
                   From.Get<EntityReference>(ExecutionContext).Id);



            // Notification Template
            //
            if (Notification.Get<EntityReference>(ExecutionContext) == null)
                throw new Exception(string.Format("{0} is null", "Notification"));

            var notificationTemplate =
                new EntityReference
                    (Notification.Get<EntityReference>(ExecutionContext).LogicalName,
                   Notification.Get<EntityReference>(ExecutionContext).Id);


            // Send message to whom
            //
            EntityReference toWhom = null;
            if (UseContext.Get<bool>(ExecutionContext))
               // toWhom = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
                toWhom =context;
            else
            {
                if (EmailableEntity.Get<string>(ExecutionContext) == null)
                    throw new InvalidWorkflowException(string.Format("Emailable string is empty while you chose to not use workflow Context"));

                toWhom =
                    sendNotificationBll.GetEntityRefrences(
                    "{" + EmailableEntity.Get<string>(ExecutionContext) + "}",
                   context);
            }

            EntityReference regardingEntity = null;
            if (UseContextInRegarding.Get<bool>(ExecutionContext))
                regardingEntity = context;
            else
            {
                if (RegardingEntity.Get<string>(ExecutionContext) == null)
                    throw new InvalidWorkflowException(string.Format("regardingEntity string is empty while you chose to not use workflow context in regarding"));

                regardingEntity =
                    sendNotificationBll.GetEntityRefrences(
                     "{" + RegardingEntity.Get<string>(ExecutionContext) + "}",
                    context);
            }



            string mobile = null;
            if (SMS.Get<bool>(ExecutionContext))
            {
                if (MobileShemaName.Get<string>(ExecutionContext) == string.Empty)
                    throw new InvalidWorkflowException(string.Format("MobileShemaName string is empty while you chose to use sms"));

                mobile = MobileShemaName.Get<string>(ExecutionContext);
            }

            EntityReference emailCreatedRefrence = sendNotificationBll.Notify(
                fromWhom,
                toWhom,
                notificationTemplate,
                regardingEntity,
                Email.Get(ExecutionContext),
                SMS.Get(ExecutionContext),
                mobile,
                context,
                StopSendingEmailForAttachments.Get(ExecutionContext),
                IncludeAttachmentInNotesToBeSent.Get(ExecutionContext));


            EmailCreated.Set(ExecutionContext, emailCreatedRefrence);
        }
    }
}
