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
    public class GetEntityReferencePrimitives : CodeActivity
    {
        //[Input("EntityReference")]
        //[ReferenceTarget("ldv_applicationheader")]
        //public InArgument<EntityReference> EntityReference { get; set; }

        [Output("EntityReferenceId")]
        public OutArgument<string> EntityReferenceId { get; set; }

        [Output("EntityReferenceSchemaName")]
        public OutArgument<string> EntityReferenceName { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            GetEntityReferencePrimitivesLogic BL = new GetEntityReferencePrimitivesLogic( EntityReferenceId, EntityReferenceName);
            BL.ExecuteLogic(context);
        }
    }
    public class GetEntityReferencePrimitivesLogic
    {
        //public InArgument<EntityReference> EntityReference { get; set; }
        public OutArgument<string> EntityReferenceId { get; set; }
        public OutArgument<string> EntityReferenceName { get; set; }
        protected CRMAccessLayer DAL;

        public GetEntityReferencePrimitivesLogic(/*InArgument<EntityReference> entityReference,*/ OutArgument<string> entityReferenceId, OutArgument<string> entityReferenceName)
        {
            //EntityReference = entityReference;
            EntityReferenceId = entityReferenceId;
            EntityReferenceName = entityReferenceName;
        }
        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            try
            {
               // EntityReference entityReference = EntityReference.Get(executionContext);
                EntityReferenceId.Set(executionContext,null);
                EntityReferenceName.Set(executionContext,null);

                var entityId = context.PrimaryEntityId;
                var entityLogicalName = context.PrimaryEntityName;

                if (entityId != Guid.Empty && entityLogicalName != string.Empty)
                {
                        EntityReferenceId.Set(executionContext, entityId.ToString());
                        EntityReferenceName.Set(executionContext, entityLogicalName);
                }
                //else
                //{
                //    Entity targetEntity = (Entity)context.InputParameters["Target"];
                //    if (targetEntity?.Id != Guid.Empty && targetEntity.LogicalName != string.Empty )
                //    {
                //        EntityReferenceId.Set(executionContext, targetEntity.Id.ToString());
                //        EntityReferenceName.Set(executionContext, targetEntity.LogicalName);
                //    }                 
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //log.LogExecutionEnd();
            }
        }

    }
}
