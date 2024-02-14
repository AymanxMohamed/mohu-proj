using LinkDev.Common.Crm.Cs.StageConfiguration.BLL;
using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
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
    public class GetRequestAndApplicationHeader : CodeActivity
    {
        [Input("EntityReferenceId")]
        public InArgument<string> EntityReferenceId { get; set; }

        [Input("EntityReferenceSchemaName")]
        public InArgument<string> EntityReferenceName { get; set; }

        [Output("EntityReferenceId")]
        public OutArgument<string> RequestId { get; set; }

        [Output("EntityReferenceSchemaName")]
        public OutArgument<string> RequestName { get; set; }

        [Output("Application Header")]
        [ReferenceTarget("ldv_applicationheader")]
        public OutArgument<EntityReference> ApplicationHeader { get; set; }

        [Output("Current Task")]
        [ReferenceTarget("task")]
        public OutArgument<EntityReference> CurrentTask { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            GetRequestAndApplicationHeaderLogic BL = new GetRequestAndApplicationHeaderLogic(EntityReferenceId, EntityReferenceName, RequestId, RequestName, ApplicationHeader, CurrentTask);
            BL.ExecuteLogic(context);
        }
    }
    public class GetRequestAndApplicationHeaderLogic
    {
        public InArgument<string> EntityReferenceId { get; set; }
        public InArgument<string> EntityReferenceName { get; set; }
        public OutArgument<string> RequestId { get; set; }
        public OutArgument<string> RequestName { get; set; }
        public OutArgument<EntityReference> ApplicationHeader { get; set; }
        public OutArgument<EntityReference> CurrentTask { get; set; }
        protected CRMAccessLayer DAL;
        public GetRequestAndApplicationHeaderLogic(InArgument<string> entityReferenceId, InArgument<string> entityReferenceName,
                                                    OutArgument<string> requestId, OutArgument<string> requestName, OutArgument<EntityReference> applicationHeader, OutArgument<EntityReference> currentTask)
        {
            EntityReferenceId = entityReferenceId;
            EntityReferenceName = entityReferenceName;
            RequestId = requestId;
            RequestName = requestName;
            ApplicationHeader = applicationHeader;
            CurrentTask = currentTask;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            DAL = new CRMAccessLayer(service);
            try
            {
                string entityId = EntityReferenceId.Get(executionContext);
                string entityLogicalName = EntityReferenceName.Get(executionContext);
                ApplicationHeader.Set(executionContext, null);
                RequestId.Set(executionContext, null);
                RequestName.Set(executionContext, null);
                CurrentTask.Set(executionContext, null);

                if (entityId != string.Empty && entityLogicalName != string.Empty)
                {
                    Entity target = DAL.RetrivePrimaryEntityOfBpf(entityLogicalName, new Guid(entityId));
                    if (target?.Id != Guid.Empty && target.LogicalName !=string.Empty)
                    {
                        RequestId.Set(executionContext, target.Id.ToString());
                        RequestName.Set(executionContext, target.LogicalName);
                        Entity request= DAL.RetrieveEntity(target.Id, target.LogicalName,new string[] { RequestEntity.ApplicationHeader,RequestEntity.CurrentTask });
                        if ( request?.Id != Guid.Empty && request.LogicalName != string.Empty)
                        {
                            EntityReference applicationHeader = request.Contains(RequestEntity.ApplicationHeader) ? request.GetAttributeValue<EntityReference>(RequestEntity.ApplicationHeader) : null;
                            EntityReference currentTask = request.Contains(RequestEntity.CurrentTask) ? request.GetAttributeValue<EntityReference>(RequestEntity.CurrentTask) : null;

                            if (applicationHeader?.Id != Guid.Empty)
                            {
                                ApplicationHeader.Set(executionContext, applicationHeader);
                            }
                            if (currentTask?.Id != Guid.Empty)
                            {
                                CurrentTask.Set(executionContext, currentTask);
                            }
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
