
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
using LinkDev.Common.Crm.Cs.NotificationTemplates.Helper;
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
        [Input("TO(Team)")]
        [ReferenceTarget("team")]
        public InArgument<EntityReference> ToTeam { get; set; }
        [Input("To(Contact)")]
        [ReferenceTarget("contact")]
        public InArgument<EntityReference> ToContact { get; set; }

        [Input("To(Account)")]
        [ReferenceTarget("account")]
        public InArgument<EntityReference> ToAccount { get; set; }

        [Input("To (Records URLs)")]
        [Default("")]
        public InArgument<string> ToRecordsURLs { get; set; }

        [Input("To (Specific Queue)")]
        [ReferenceTarget("queue")]
        public InArgument<EntityReference> ToQueue { get; set; }

        [Input("CC (Records URLs)")]
        [Default("")]
        public InArgument<string> CCRecordsURLs { get; set; }

        [Input("BCC (Records URLs)")]
        [Default("")]
        public InArgument<string> BCCRecordsURLs { get; set; }

        [Input("Use another entity as regarding")]
        [Default("False")]
        public InArgument<bool> IsAnotherRegarding { get; set; }

        [Input("RegardingId")]
        public InArgument<string> RegardingId { get; set; }
        [Input("RegardingSchemaName")]
        public InArgument<string> RegardingName { get; set; }

        #endregion

        protected override void Execute(CodeActivityContext executionContext)
        {
            SendNotificationLogics BL = new SendNotificationLogics(Notification, ToUser, ToTeam, ToAccount, ToContact, ToQueue, CCRecordsURLs, BCCRecordsURLs, ToRecordsURLs, IsAnotherRegarding, RegardingId, RegardingName);
            BL.ExecuteLogic(executionContext);
        }
    }

    public class SendNotificationLogics
    {
        #region variables:

        #region inout parameters:

        public InArgument<EntityReference> Notifications { get; set; }
        public InArgument<EntityReference> Users { get; set; }
        public InArgument<EntityReference> Teams { get; set; }
        public InArgument<EntityReference> Accounts { get; set; }
        public InArgument<EntityReference> Contacts { get; set; }
        public InArgument<EntityReference> Queues { get; set; }
        public InArgument<string> CCRecordsURLs { get; set; }
        public InArgument<string> ToRecordsURLs { get; set; }
        public InArgument<string> BCCRecordsURLs { get; set; }
        public InArgument<bool> IsAnotherRegarding { get; set; }
        public InArgument<string> RegardingId { get; set; }
        public InArgument<string> RegardingName { get; set; }


        #endregion


        public SendNotificationLogics(
           InArgument<EntityReference> notification, InArgument<EntityReference> toUser, InArgument<EntityReference> toTeam,
           InArgument<EntityReference> toAccount, InArgument<EntityReference> toContact,
           InArgument<EntityReference> toQueue, InArgument<string> cCRecordsURLs, InArgument<string> bCCRecordsURLs, InArgument<string> toRecordsURLs,
           InArgument<bool> isAnotherRegarding, InArgument<string> regardingId, InArgument<string> regardingName )
        {
            Notifications = notification;
            Users = toUser;
            Accounts = toAccount;
            Contacts = toContact;
            Queues = toQueue;
            CCRecordsURLs = cCRecordsURLs;
            ToRecordsURLs = toRecordsURLs;
            BCCRecordsURLs = bCCRecordsURLs;
            Teams = toTeam;
            IsAnotherRegarding = isAnotherRegarding;
            RegardingId = regardingId;
            RegardingName = regardingName;
        }

        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
           // NotificationTemplatesBLL logicLayer;
            EntityReference RegardingObject;
          //  logicLayer = new NotificationTemplatesBLL(service, tracingService);
            RegardingObject = new EntityReference(context.PrimaryEntityName, context.PrimaryEntityId);
          
            try
            {
                EntityReference notifications = Notifications.Get(executionContext);
                EntityReference user = Users.Get(executionContext);
               // EntityReference team = Teams.Get(executionContext);
                EntityReference account = Accounts.Get(executionContext);
                EntityReference contact =  Contacts.Get(executionContext);
                EntityReference queue =  Queues.Get(executionContext);
                string cCRecordsURL = CCRecordsURLs.Get(executionContext);
                string toRecordsURL = ToRecordsURLs.Get(executionContext);
                string bCCRecordsURL = BCCRecordsURLs.Get(executionContext);
                bool isAnotherRegarding = IsAnotherRegarding.Get(executionContext);
                string regardingId = RegardingId.Get(executionContext);
                string regardingName = RegardingName.Get(executionContext);


                EntityReference RegardingLookup = new EntityReference(regardingName, new Guid(regardingId));
                 
                tracingService.Trace($" after RetrieveRelatedApplicationByApplicationHeader  ");
                StageConfigurationsNotificationsBLL BLL = new StageConfigurationsNotificationsBLL(service, tracingService, RegardingObject);

                BLL.SendNotificationTemplate(user,/*team,*/ account, contact, queue, cCRecordsURL, bCCRecordsURL, toRecordsURL, notifications, RegardingObject  , RegardingLookup);
          } 
            catch (Exception ex)
            {
                throw ex;
                // log.Log($"ExecuteLogic has been finished with Error:'{ex.Message}'");
                // log.ExecutionFailed();
            }
            finally
            {
                //log.LogFunctionEnd();
            }




            #endregion







        }
    }
}