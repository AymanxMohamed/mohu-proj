
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
    public class SendNotificationsByNotificationConfig : CustomStepBase
    {
        #region input parameter : 
        [Input("Stage Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        [RequiredArgument]
        public InArgument<EntityReference> stageConfiguration { get; set; }

        [Input("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public InArgument<EntityReference> applicationHeader { get; set; }

        public override void ExtendedExecute()
        {
            EntityReference stageConfiguration = this.stageConfiguration != null ? this.stageConfiguration.Get(ExecutionContext) : null;
            EntityReference applicationHeader = this.applicationHeader != null ? this.applicationHeader.Get(ExecutionContext) : null;

            SendNotificationsFromStageConfigurationLogic SendNotificationsLogic = new SendNotificationsFromStageConfigurationLogic(stageConfiguration, applicationHeader,OrganizationService,Tracer,LanguageCode);
            SendNotificationsLogic.Execute(new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId));
        }

        #endregion

    }


public class SendNotificationsFromStageConfigurationLogic : LinkDev.Common.Crm.Bll.Base.BllBase
    {
        
        #region variables:
        
        public NotificationTemplatesBLL NotificationTemplatesBLL;
        public EntityReference RegardingObject;
        EntityReference stageConfiguration;
        EntityReference applicationHeader;
        CommonBLL commonBLL;
        #endregion

        #region constructors:
        public SendNotificationsFromStageConfigurationLogic(EntityReference stageConfiguration, EntityReference applicationHeader, IOrganizationService _orgService,  Logger.ILogger tracer, string languageCode)
            :base(_orgService,tracer, languageCode)
        {
            this.applicationHeader = applicationHeader;
            this.stageConfiguration = stageConfiguration;
            RegardingObject = new EntityReference();
            
        }
        #endregion

        #region methods:
        public void Execute(EntityReference primaryEntity)
        {
            if (applicationHeader == null)
                RegardingObject = new EntityReference(primaryEntity.LogicalName, primaryEntity.Id);
            else
            {
                // retrieve the related application and par
                commonBLL = new CommonBLL(OrganizationService,Logger,LanguageCode);
                RegardingObject = commonBLL.RetrieveRelatedApplicationByApplicationHeader(applicationHeader);
            }
            StageConfigurationsNotificationsBLL BLL = new StageConfigurationsNotificationsBLL(OrganizationService,Logger,LanguageCode, RegardingObject);
            BLL.SendStageNotification(stageConfiguration.Id);
        }
        #endregion


    }

}