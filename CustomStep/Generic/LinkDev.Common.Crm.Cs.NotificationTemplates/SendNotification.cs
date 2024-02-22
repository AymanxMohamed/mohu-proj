
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using Linkdev.CRM.DataContracts;
using LinkDev.Libraries.Common;
using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Bll.Base;
using LinkDev.Common.Crm.Logger;
//using LinkDev.MOE

namespace LinkDev.Common.Crm.Cs.NotificationTemplates
{
    public class SendNotification : CodeActivity//: CustomStepBase
    {

        #region "Input Parameters"
        [Input("Notification Template")]
        [ReferenceTarget("ldv_notificationtemplate")]
        [RequiredArgument]
        public InArgument<EntityReference> Notification { get; set; }


        [Input("TO(User)")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> ToUser { get; set; }

       

        [Input("To(Contact)")]
        [ReferenceTarget("contact")]
        public InArgument<EntityReference> ToContact { get; set; }

        [Input("To(Account)")]
        [ReferenceTarget("account")]
        public InArgument<EntityReference> ToAccount { get; set; }

       [Input("To (Records URLs)")]
        [Default("")]
        public InArgument<String> ToRecordsURLs { get; set; }

        [Input("To (Specific Queue)")]
        [ReferenceTarget("queue")]
        public InArgument<EntityReference> ToQueue { get; set; }

        [Input("CC (Records URLs)")]
        [Default("")]
        public InArgument<String> CCRecordsURLs { get; set; }

        [Input("BCC (Records URLs)")]
        [Default("")]
        public InArgument<String> BCCRecordsURLs { get; set; }



        #endregion
        public override void ExtendedExecute()
        {


            #region inout parameters:
            EntityReference Notifications = this.Notification != null ? this.Notification.Get(ExecutionContext) : null;
            EntityReference User = this.ToUser != null ? this.ToUser.Get(ExecutionContext) : null;
            EntityReference Account = this.ToAccount != null ? this.ToAccount.Get(ExecutionContext) : null;
            EntityReference Contact = this.ToContact != null ? this.ToContact.Get(ExecutionContext) : null;
            EntityReference Queue = this.ToQueue != null ? this.ToQueue.Get(ExecutionContext) : null;
            string CCRecordsURL = this.CCRecordsURLs != null ? this.CCRecordsURLs.Get(ExecutionContext) : string.Empty;
            string ToRecordsURL = this.ToRecordsURLs != null ? this.ToRecordsURLs.Get(ExecutionContext) : string.Empty;
            string BCCRecordsURL = this.BCCRecordsURLs != null ? this.BCCRecordsURLs.Get(ExecutionContext) : string.Empty;
            #endregion


            SendNotificationLogic.NotificationTemplatesBLL.SendNotificationTemplate(User, Account, Contact, Queue, CCRecordsURL, BCCRecordsURL, ToRecordsURL, Notifications, SendNotificationLogic.RegardingObject);

        }

        protected override void Execute(CodeActivityContext context)
        {
            SendNotificationLogic SendNotificationLogic = new SendNotificationLogic(OrganizationService,Tracer,new EntityReference(Context.PrimaryEntityName,Context.PrimaryEntityId));
            SendNotificationLogic.ExecuteLogic(context);
        }
    }

    public class SendNotificationLogic //: BllBase
    {
        #region variables:
        public NotificationTemplatesBLL NotificationTemplatesBLL;
        public EntityReference RegardingObject;
        ITracingService TracingService;
        #endregion

        #region constructor
        public SendNotificationLogic(IOrganizationService organizationService, ITracingService TracingService, EntityReference primaryEntity)
         //   :base(organizationService,  logger,  languageCode)
        {
            // Get the context service.
             RegardingObject = new EntityReference(primaryEntity.LogicalName, primaryEntity.Id);
            NotificationTemplatesBLL = new NotificationTemplatesBLL(organizationService, TracingService);
        }

        #endregion

    }
 }
