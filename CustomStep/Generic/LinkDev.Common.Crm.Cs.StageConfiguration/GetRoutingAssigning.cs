using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
//using Linkdev.MOE.CRM.LoggerManagement;
using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using LinkDev.Common.Crm.Cs.StageConfiguration.Enum;
using LinkDev.CRM.Library.DAL;
using LinkDev.Libraries.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
//using LinkDev.Common.Crm.Cs.StageConfiguration.DAL;

namespace LinkDev.Common.Crm.Cs.StageConfiguration
{
    public class GetRoutingAssigning : CodeActivity
    {
        [RequiredArgument]
        [Input("Stage Configuration")]
        [ReferenceTarget("ldv_stageconfiguration")]
        public InArgument<EntityReference> StageConfiguration { get; set; }


        [Input("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public InArgument<EntityReference> ApplicationHeader { get; set; }

        [Output("User")]
        [ReferenceTarget("systemuser")]
        public OutArgument<EntityReference> User { get; set; }

        [Output("Team")]
        [ReferenceTarget("team")]
        public OutArgument<EntityReference> Team { get; set; }

        [Output("Queue")]
        [ReferenceTarget("queue")]
        public OutArgument<EntityReference> Queue { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            GetGetRoutingAssigningLogic BL = new GetGetRoutingAssigningLogic(StageConfiguration, ApplicationHeader, User, Team, Queue);
            BL.ExecuteLogic(executionContext);
        }
    }

    internal class GetGetRoutingAssigningLogic 
    {
        public InArgument<EntityReference> StageConfiguration { get; set; }
        public InArgument<EntityReference> ApplicationHeader { get; set; }
        public OutArgument<EntityReference> User { get; set; }
        public OutArgument<EntityReference> Team { get; set; }
        public OutArgument<EntityReference> Queue { get; set; }

        //protected CrmLog log;
       // protected CRMAccessLayer DAL;
        protected StageConfigurationBLL logicLayer;
      //  public TextFileLogger loggerInServer = new TextFileLogger(@"C:\Program Files\Link Development\Box\LogFile.txt");

        public GetGetRoutingAssigningLogic(InArgument<EntityReference> stageConfiguration, InArgument<EntityReference> applicationHeader, OutArgument<EntityReference> user, OutArgument<EntityReference> team, OutArgument<EntityReference> queue )
        {
            StageConfiguration = stageConfiguration;
            ApplicationHeader = applicationHeader;
            User = user;
            Team = team;
            Queue = queue;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {    
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            // log = new CrmLog(executionContext);
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            logicLayer = new StageConfigurationBLL(service, tracingService, executionContext);
            try
            {
                EntityReference stageConfiguration = (StageConfiguration.Get(executionContext));
                EntityReference appHeader = (ApplicationHeader.Get(executionContext));
                User.Set(executionContext, null);
                Queue.Set(executionContext, null);
                Team.Set(executionContext, null);

                //check if application header exist as input parameter and get "target entity" if exist.  
                Entity targetEntity = new Entity();  
                if (appHeader != null)
                {
                    targetEntity = logicLayer.RetrieveTargetEntityFromApplicationHeader(appHeader);
                }
                else //get target entity from context
                {
                    targetEntity = (Entity)context.InputParameters["Target"];
                }

                #region Get Assigning Value

                if (stageConfiguration != null)
                {                   
                    if (stageConfiguration.Id != Guid.Empty)
                    {
                        //// 1*- Get the Fetch Value == stage routing configuration list 
                        List<Entity> result = logicLayer.RetrieveStageRoutingConfigurationFields( 
                                stageConfiguration.Id );
                        if (result.Any())
                        {                           
                            ////2*- loop for all condition and retrive the right one and if it is the only one retrieve it and get that assigning Routing                            
                            EntityReference assigningRoutingResult =
                                logicLayer.RetrieveRoutingAssigningRecord(result,  targetEntity, stageConfiguration.Id, tracingService);
                            if (assigningRoutingResult != null)
                            {
                                if (assigningRoutingResult.Id != Guid.Empty)
                                {
                                    //3-Check field Data type to assign to the propear fields user , team or queue
                                    string assigningFieldLogicalName = assigningRoutingResult.LogicalName;
                                    if (assigningFieldLogicalName != String.Empty)
                                    {                                        
                                        string assigningFieldDataType =
                                            logicLayer.DiscoverFieldType(assigningRoutingResult.LogicalName);
                                        if (assigningFieldDataType != String.Empty)
                                        {
                                            if (assigningRoutingResult.LogicalName == AssigningRouting.Team)
                                            {                                              
                                                Team.Set(executionContext, assigningRoutingResult);
                                            }
                                            else if (assigningRoutingResult.LogicalName == AssigningRouting.User)
                                            {
                                                User.Set(executionContext, assigningRoutingResult);
                                            }
                                            else if (assigningRoutingResult.LogicalName == AssigningRouting.Queue)
                                            {
                                                Queue.Set(executionContext, assigningRoutingResult);
                                            }
                                            else if (assigningRoutingResult.LogicalName == AssigningRouting.RoleConfiguration)
                                            {
                                                StageConfigurationBLL getconfigrecord = new StageConfigurationBLL(service,tracingService,executionContext);
                                                EntityReference assingingLookup = new EntityReference(null);
                                                assingingLookup = getconfigrecord.GetRoleConfigurationFields(new Entity(assigningRoutingResult.LogicalName, assigningRoutingResult.Id));

                                                if (assingingLookup.LogicalName == AssigningRouting.Team)
                                                {                                          
                                                    Team.Set(executionContext, assingingLookup);
                                                }
                                                else if (assingingLookup.LogicalName == AssigningRouting.User)
                                                {
                                                    User.Set(executionContext, assingingLookup);
                                                }
                                                else if (assingingLookup.LogicalName == AssigningRouting.Queue)
                                                {
                                                    Queue.Set(executionContext, assingingLookup);
                                                }
                                            }
                                            else
                                            {
                                                tracingService.Trace($"assigningFieldDataType is Empty");

                                            }
                                        }
                                        else
                                        {
                                            tracingService.Trace($"AssigningFieldDataType is wrong ");

                                        }
                                    }
                                }
                            }
                            else {
                                tracingService.Trace($"There is no assigningRoutingResult ");

                            }

                        }
                        else {
                            tracingService.Trace($"There is no routting ");

                        }

                    }
                }
                else {
                    tracingService.Trace($"Configuration is Wrong");

                }
                #endregion

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
        }
    }
}