using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using LinkDev.CRM.Library.DAL;
using LinkDev.Libraries.Common;
using LinkDev.Common.Crm.Cs.StageConfiguration.Enum;
using Microsoft.Crm.Sdk.Messages;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

namespace LinkDev.Common.Crm.Cs.StageConfiguration
{
    public class GetStageConfiguration : CodeActivity
    {
        //// [RequiredArgument]
        [Input("Process Stage")]
        [ReferenceTarget("processstage")]
        public InArgument<EntityReference> ProcessStage { get; set; }
        //[RequiredArgument]
        [Input("EntityReferenceId")]
        public InArgument<string> EntityReferenceId { get; set; }
       // [RequiredArgument]
        [Input("EntityReferenceSchemaName")]
        public InArgument<string> EntityReferenceName { get; set; }


        [Output("Stage Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        public OutArgument<EntityReference> StageConfiguration { get; set; }
        protected override void Execute(CodeActivityContext executionContext)
        {
            GetStageConfigurationLogic BL = new GetStageConfigurationLogic(/*ProcessStage,*/ StageConfiguration, EntityReferenceId, EntityReferenceName);
            BL.ExecuteLogic(executionContext);
        }
    }
    public class GetStageConfigurationLogic
    {
        //public InArgument<EntityReference> ProcessStage { get; set; }
        public OutArgument<EntityReference> StageConfiguration { get; set; }
        public InArgument<string> EntityReferenceId { get; set; }
        public InArgument<string> EntityReferenceName { get; set; }
        protected CRMAccessLayer DAL;
        protected StageConfigurationBLL logicLayer;
        public GetStageConfigurationLogic(/*InArgument<EntityReference> processStage,*/ OutArgument<EntityReference> stageConfiguration, InArgument<string> entityReferenceId, InArgument<string> entityReferenceName)
        {
            //ProcessStage = processStage;
            StageConfiguration = stageConfiguration;
            EntityReferenceId = entityReferenceId;
            EntityReferenceName = entityReferenceName;
        }     
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            //log = new CrmLog(executionContext);
            logicLayer = new StageConfigurationBLL(service, tracingService, executionContext);           
            try
            {
                //log.LogFunctionStart();
                //log.Log("in ExecuteLogic fn.");
                //EntityReference processStage = ProcessStage.Get(executionContext);
                string entityReferenceId = EntityReferenceId.Get(executionContext);
                string entityReferenceName = EntityReferenceName.Get(executionContext);
                StageConfiguration.Set(executionContext, null);

                #region pass active stage


                //if (processStage != null)
                //{
                //    Guid processStageId = (ProcessStage.Get(executionContext)).Id;
                //    if (processStageId != Guid.Empty)
                //    {
                //        EntityReference stageConf = logicLayer.RetriveStageConfigurarionByProcessStage(processStageId);
                //        if (stageConf?.Id != Guid.Empty)
                //        {
                //            StageConfiguration.Set(executionContext, stageConf);
                //        }
                //        else
                //        {
                //            StageConfiguration.Set(executionContext, null);
                //        }
                //    }
                //}
                //else
                #endregion

                if (entityReferenceId != string.Empty && entityReferenceName != string.Empty)
                {
                    EntityReference stageConf = logicLayer.RetriveStageConfigurarionByProcessStage(new Guid(entityReferenceId), entityReferenceName, tracingService);
                   
                    if (stageConf?.Id != Guid.Empty)
                    {
                        StageConfiguration.Set(executionContext, stageConf);
                    }
                    else
                    {
                        StageConfiguration.Set(executionContext, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //log.Log($"ExecuteLogic has been finished with Error:'{ex.Message}'");
                //log.ExecutionFailed();
                // throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //log.LogExecutionEnd();
            }
        }
    }
}