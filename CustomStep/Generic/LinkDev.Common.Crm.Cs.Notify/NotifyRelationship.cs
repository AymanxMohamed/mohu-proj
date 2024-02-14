using LinkDev.Common.Crm.Bll.Notification;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Utilities;
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
    public class NotifyRelationship : CustomStepBase
    {
        #region "Input Parameters"

        [Input("EntityReference From")]
        [ReferenceTarget("systemuser")]
        [RequiredArgument]
        public InArgument<EntityReference> From { get; set; }

        [Input("Notification Template")]
        [ReferenceTarget("ldv_notificationtemplate")]
        [RequiredArgument]
        public InArgument<EntityReference> Notification { get; set; }


        [Input("Use Context For Notification Substituation")]
        [Default("true")]
        public InArgument<bool> UseContextForNotificationSubstituation { get; set; } 

        [Input("Notification Substituation Entity Path ('In Case Use Context For Notification Substituation' = No)")]
        public InArgument<string> NotificationSubstituationEntityPath { get; set; }

        [Input("Use Workflow Context")]
        [Default("true")]
        public InArgument<bool> UseContext { get; set; }

        [Input("Entity 1 Logical Name Path ('In Case Use Context' = No)")]
        public InArgument<string> Entity1LogicalnamePath { get; set; } //

        [Input("Entity 2 Logical Name")]
        [RequiredArgument]
        public InArgument<string> Entity2LogicalName { get; set; }

        [Input("Is M2M Relationship")]
        [Default("false")]
        public InArgument<bool> IsM2M { get; set; }

        [Input("Entity 1 Lookup Logical Name at Entity 2 (In Case of 1:M Relationship)")]
        public InArgument<string> Entity1LookupLogicalNameAtEntity2 { get; set; }

        [Input("Intersect Entity Logical Name (In Case of M:M Relationship)")]
        public InArgument<string> IntersectEntityLogicalName { get; set; }

        [Input("Field Logical Name That Contains Recipient at Entity 2")]
        [RequiredArgument]
        public InArgument<string> FieldLogicalNameThatContainsRecipient { get; set; } 

        [Input("Use Workflow Context As Regarding")]
        [Default("true")]
        public InArgument<bool> UseContextInRegarding { get; set; } 

        [Input("Regarding Entity (In Case 'Use Workflow Context As Regarding' = No)")]
        public InArgument<string> RegardingEntity { get; set; } 


        [Input("Stop Sending Email For Attachments")]
        [Default("False")]
        public InArgument<bool> StopSendingEmailForAttachments { get; set; }

        [Output("Email Created")]
        [ReferenceTarget("email")]
        public OutArgument<EntityReference> EmailCreated { get; set; }

        #endregion

        public override void ExtendedExecute()
        {
            EmailCreated.Set(ExecutionContext, null);
            var recipients = new EntityReferenceCollection();
            var recipientsReferences = new EntityReferenceCollection();
            var sendNotificationBll = new SendNotification(OrganizationService, Tracer, LanguageCode);


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

            EntityReference primaryEntity;
            if (UseContextForNotificationSubstituation.Get(base.ExecutionContext))
            {
                primaryEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            }
            else
            {
                if (string.IsNullOrEmpty(NotificationSubstituationEntityPath.Get(ExecutionContext)))
                    throw new InvalidWorkflowException($"{nameof(NotificationSubstituationEntityPath)} is empty while you chose to not use workflow Context");


                primaryEntity = (EntityReference)
                    CrmStringHandler.SubstituteToAttribute(
                        new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId),
                        NotificationSubstituationEntityPath.Get(ExecutionContext),
                        OrganizationService);
            }

            EntityReference entity1Reference;
            if (UseContext.Get(base.ExecutionContext))
            {
                entity1Reference = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            }
            else
            {
                if (string.IsNullOrEmpty(Entity1LogicalnamePath.Get(ExecutionContext)))
                    throw new InvalidWorkflowException($"{nameof(Entity1LogicalnamePath)} is empty while you chose to not use workflow Context");


                entity1Reference = (EntityReference)
                    CrmStringHandler.SubstituteToAttribute(
                        new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId),
                        Entity1LogicalnamePath.Get(ExecutionContext),
                        OrganizationService);
            }

            if (IsM2M.Get(ExecutionContext))
            {
                if (string.IsNullOrEmpty(IntersectEntityLogicalName.Get(ExecutionContext)))
                    throw new InvalidWorkflowException($"{nameof(IntersectEntityLogicalName)} is empty while you chose M2M Relationship");

                recipients =
                    Tools.GetAssociatedEntities(
                        entity1Reference,
                        IntersectEntityLogicalName.Get(ExecutionContext),
                        Entity2LogicalName.Get(ExecutionContext),
                        OrganizationService);
            }
            else
            {

                recipients =
                    Tools.GetRelatedRecords(
                        entity1Reference,
                        Entity2LogicalName.Get(ExecutionContext),
                        Entity1LookupLogicalNameAtEntity2.Get(ExecutionContext),
                        OrganizationService);
            }

            foreach (var item in recipients)
            {

                var recipientReference = (EntityReference)
                            CrmStringHandler.SubstituteToAttribute(
                                item,
                                FieldLogicalNameThatContainsRecipient.Get(ExecutionContext),
                                OrganizationService);

                recipientsReferences.Add(recipientReference);
            }


            EntityReference regardingEntity = null;
            if (UseContextInRegarding.Get<bool>(ExecutionContext))
                regardingEntity = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
            else
            {
                if (RegardingEntity.Get<string>(ExecutionContext) == null)
                    throw new InvalidWorkflowException(string.Format("regardingEntity string is empty while you chose to not use workflow context in regarding"));

                regardingEntity =
                    sendNotificationBll.GetEntityRefrences(
                     "{" + RegardingEntity.Get<string>(ExecutionContext) + "}",
                    new EntityReference(
                        Context.PrimaryEntityName,
                        Context.PrimaryEntityId));
            }

            var createdEmail =
                sendNotificationBll.NotifyRelationship(
                             fromWhom,
                             recipientsReferences.ToList(),
                             notificationTemplate,
                             regardingEntity,
                             primaryEntity,
                             StopSendingEmailForAttachments.Get(ExecutionContext));

            EmailCreated.Set(ExecutionContext, createdEmail);
        }
    }
}
