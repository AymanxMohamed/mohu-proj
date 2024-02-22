
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
using LinkDev.Common.Crm.Cs.NotificationTemplates.Helper;
using LinkDev.Common.Crm.Cs.Base;

namespace LinkDev.Common.Crm.Cs.NotificationTemplates
{
    public class SendNotificationsByNotificationConfig : CodeActivity
    {
        #region input parameter : 
        [Input("Stage Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        [RequiredArgument]
        public InArgument<EntityReference> stageConfiguration { get; set; }

        [Input("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        [RequiredArgument]
        public InArgument<EntityReference> applicationHeader { get; set; }
        #endregion
       

        protected override void Execute(CodeActivityContext context)
        {
          
            SendNotificationsFromStageConfigurationLogic SendNotificationsLogic = new SendNotificationsFromStageConfigurationLogic(stageConfiguration, applicationHeader );
            SendNotificationsLogic.ExecuteLogic( context );

        }



    }


public class SendNotificationsFromStageConfigurationLogic 
    {

        #region variables:
        public InArgument<EntityReference> StageConfiguration { get; set; }
        public InArgument<EntityReference> ApplicationHeader { get; set; }
        public NotificationTemplatesBLL NotificationTemplatesBLL;
        public EntityReference RegardingObject;
       // protected CRMAccessLayer DAL;
        CommonBLL commonBLL;
        #endregion

        #region constructors:
        public SendNotificationsFromStageConfigurationLogic(InArgument<EntityReference> stageConfiguration, InArgument<EntityReference> applicationHeader)
        {
            ApplicationHeader = applicationHeader;
            StageConfiguration = stageConfiguration;
            RegardingObject = new EntityReference();
        }
        #endregion

        #region methods:
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
            EntityReference stageConfiguration = StageConfiguration.Get(executionContext);
            EntityReference applicationHeader = ApplicationHeader.Get(executionContext);
            //  DAL = new CRMAccessLayer(service);
            // retrieve the related application and par
            commonBLL = new CommonBLL(service, tracingService);
             RegardingObject = commonBLL.RetrieveRelatedApplicationByApplicationHeader(applicationHeader);
            StageConfigurationsNotificationsBLL BLL = new StageConfigurationsNotificationsBLL(service, tracingService, RegardingObject);
            BLL.SendStageNotification(stageConfiguration.Id);
        }
        #endregion


    }

}