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
    public class UpdateEmailByNotificationTemplate : CustomStepBase
    {

        #region "Input Parameters"

        [Input("Notification Template")]
        [ReferenceTarget("ldv_notificationtemplate")]
        [RequiredArgument]
        public InArgument<EntityReference> NotificationTemplate { get; set; }

        [Input("Created Email")]
        [ReferenceTarget("email")]
        [RequiredArgument]
        public InArgument<EntityReference> CreatedEmail { get; set; }

        [Input("Use Context In Regarding")]
        [Default("False")]
        public InArgument<bool> UseContextInRegarding { get; set; }
        

        [Input("Regarding Entity SchemaName")]
        public InArgument<string> RegardingEntitySchemaName { get; set; }

        [Input("Regarding Entity id")]
        public InArgument<string> RegardingEntityId { get; set; }

        [Input("Send EMail")]
        [Default("False")]
        public InArgument<bool> SendEmail { get; set; }

        [Output("Email Created")]
        [ReferenceTarget("email")]
        public OutArgument<EntityReference> EmailUpdated { get; set; }

        #endregion
        public override void ExtendedExecute()
        {
            var sendNotificationBll = new SendNotification(OrganizationService, Tracer, LanguageCode);

            EntityReference notificationTemplate =
              new EntityReference
                  (NotificationTemplate.Get<EntityReference>(ExecutionContext).LogicalName,
                 NotificationTemplate.Get<EntityReference>(ExecutionContext).Id);

            EntityReference createdEmail=
                new EntityReference
                  (CreatedEmail.Get<EntityReference>(ExecutionContext).LogicalName,
                 CreatedEmail.Get<EntityReference>(ExecutionContext).Id);
            bool sendEmail = SendEmail.Get<bool>(ExecutionContext);
            EntityReference regardingEntity = null;
            if (UseContextInRegarding.Get<bool>(ExecutionContext))
                regardingEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            else
            {
                if (RegardingEntitySchemaName.Get<string>(ExecutionContext) == null|| RegardingEntityId.Get<string>(ExecutionContext) == null)
                    throw new InvalidWorkflowException(string.Format("regardingEntity string is empty while you chose to not use workflow context in regarding"));

                regardingEntity = new EntityReference(
                      RegardingEntitySchemaName.Get<string>(ExecutionContext),
                      new Guid( RegardingEntityId.Get<string>(ExecutionContext)));
            }


            EntityReference emailCreatedRefrence = sendNotificationBll.UpdateCreatetedEmail(regardingEntity,createdEmail,notificationTemplate , sendEmail, Tracer);


            EmailUpdated.Set(ExecutionContext, emailCreatedRefrence);
        }

       
    }
}
