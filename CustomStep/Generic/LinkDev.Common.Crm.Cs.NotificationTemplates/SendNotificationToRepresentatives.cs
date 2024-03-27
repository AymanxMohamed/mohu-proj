//using LinkDev.Common.Crm.Cs.Base;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Workflow;
//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LinkDev.Common.Crm.Cs.NotificationTemplates
//{
//    public class SendNotificationToRepresentatives : CustomStepBase
//    {
//        private const string startingPattern = "#special_handling_contact_start#";
//        private const string endingPattern = "#special_handling_contact_end#";

//        #region "Input Parameters"
//        [Input("Notification Template")]
//        [RequiredArgument]
//        [ReferenceTarget("ldv_notificationtemplate")]
//        public InArgument<EntityReference> Notification { get; set; }

//        [Input("Account")]
//        [RequiredArgument]
//        [ReferenceTarget("account")]
//        public InArgument<EntityReference> Account { get; set; }

//        #endregion

//        public override void ExtendedExecute()
//        {
//            var SendNotificationLogic = new SendNotificationLogic(OrganizationService,Tracer,LanguageCode,new EntityReference(Context.PrimaryEntityName,Context.PrimaryEntityId));
//            var notifications = Notification.Get(ExecutionContext);
//            var account = Account.Get(ExecutionContext);
//            // to be implemented based on GEA Logic

//            //SendNotificationLogic.NotificationTemplatesBLL.SendNotificationToRepresintatives(notifications, account, account, startingPattern, endingPattern);
//        }
//    }
//}
