using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration
{
    public class CreateApplicationHeader : CodeActivity
    {
        [Input("EntityReferenceId")]
        public InArgument<string> EntityReferenceId { get; set; }

        [Input("EntityReferenceSchemaName")]
        public InArgument<string> EntityReferenceName { get; set; }

        [Output("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public OutArgument<EntityReference> ApplicationHeader { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            CreateApplicationHeaderLogic BL = new CreateApplicationHeaderLogic(ApplicationHeader, EntityReferenceId, EntityReferenceName);
            BL.ExecuteLogic(context);
        }
    }
    public class CreateApplicationHeaderLogic
    {
        public OutArgument<EntityReference> ApplicationHeader { get; set; }
        public InArgument<string> EntityReferenceId { get; set; }
        public InArgument<string> EntityReferenceName { get; set; }

        CreateApplicationHeaderBLL createApplicationHeader;
        StageConfigurationBLL logicLayer;
        protected CRMAccessLayer DAL;
        public CreateApplicationHeaderLogic(OutArgument<EntityReference> applicationHeader, InArgument<string> entityReferenceId, InArgument<string> entityReferenceName)
        {
            ApplicationHeader = applicationHeader;
            EntityReferenceId = entityReferenceId;
            EntityReferenceName = entityReferenceName;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            DAL = new CRMAccessLayer(service);
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();
            createApplicationHeader = new CreateApplicationHeaderBLL(service, tracingService);

            logicLayer = new StageConfigurationBLL(service,tracingService,executionContext);
            try
            {
                ApplicationHeader.Set(executionContext,null);
                string entityId = EntityReferenceId.Get(executionContext);
                string entityLogicalName = EntityReferenceName.Get(executionContext);

                //var entityId = context.PrimaryEntityId;
                //var entityLogicalName = context.PrimaryEntityName;
                if (entityId!= string.Empty && entityLogicalName!=string.Empty)
                {
                    Entity target = DAL.RetrivePrimaryEntityOfBpf(entityLogicalName,new Guid( entityId));
                    if (target?.Id!=Guid.Empty)
                    {
                        tracingService.Trace(" Final ");
                        EntityReference applicationHeader = createApplicationHeader.CreateAppHeaderFromRequest(target);
                        if (applicationHeader != null)
                        {
                            ApplicationHeader.Set(executionContext, applicationHeader);
                        }
                    }                  
                }
            }
            catch (Exception e)
            {
                throw e;
            }          
        }
    }
}
