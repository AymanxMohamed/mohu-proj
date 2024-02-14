using LinkDev.Common.Crm.Bll.Notification;
using LinkDev.Common.Crm.Cs.Base;

using Microsoft.Crm.Sdk.Messages;
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
   public class SendEmail : CustomStepBase
    {
        #region "Input Parameters"
        [RequiredArgument]
        [Input("Email")]
        [ReferenceTarget("email")]
        public InArgument<EntityReference> Email { get; set; }

        #endregion

        public override void ExtendedExecute()
        {
            var sendNotificationBll = new SendNotification(OrganizationService, Tracer, LanguageCode);
            sendNotificationBll.SendEmail(Email.Get(ExecutionContext).Id);
        }
    }
}
