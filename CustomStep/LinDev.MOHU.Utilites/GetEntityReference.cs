using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinDev.MOHU.Utilites.Logic;
using LinkDev.MAAN.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace LinDev.MOHU.Utilites
{
    public class GetEntityReference : CodeActivity
    {
        #region "Input Parameters"
        [Input("LogicalName")]
        [RequiredArgument]
        public InArgument<string> EntityLogicalName { get; set; }

        [Input("Id")]
        [RequiredArgument]
        public InArgument<string> EntityId { get; set; }
        #endregion

        #region "Output Parameters"
        [Output("Case")]
        [ReferenceTarget("incident")]
        public OutArgument<EntityReference> Case { get; set; }

        [Output("Survey Services")]
        [ReferenceTarget("ldv_surveyservices")]
        public OutArgument<EntityReference> SurveyServices { get; set; }
        #endregion

        // Variables to hold services and context
        protected CodeActivityContext executionContext;
        protected IWorkflowContext context;
        protected IOrganizationServiceFactory serviceFactory;
        protected ITracingService tracingService;
        protected IOrganizationService service;

        protected override void Execute(CodeActivityContext executionContext)
        {
            // Initialize the tracing service for logging
            tracingService = executionContext.GetExtension<ITracingService>();
            tracingService.Trace("Custom workflow activity execution started.");

            // Initialize the workflow context to access CRM data
            context = executionContext.GetExtension<IWorkflowContext>();

            // Initialize the service factory to create an IOrganizationService instance
            serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();

            // Create the Organization Service to interact with CRM data
            service = serviceFactory.CreateOrganizationService(context.UserId);

            SurveyServices.Set(executionContext, null);
            Case.Set(executionContext, null);

            // Retrieve input parameters
            string entityId = EntityId.Get(executionContext);
            string logicalName = EntityLogicalName.Get(executionContext);

            tracingService.Trace($"Input Entity ID: {entityId}");
            tracingService.Trace($"Input Logical Name: {logicalName}");

            // Validate and parse entity ID
            if (!Guid.TryParse(entityId, out Guid parsedEntityId))
            {
                throw new InvalidPluginExecutionException("Invalid Entity ID format.");
            }

            // Create an EntityReference using the logical name and ID
            EntityReference entityReference = new EntityReference(logicalName, parsedEntityId);
            if (logicalName== "incident")
            {
                Case.Set(executionContext, entityReference);
            }
            else if (logicalName == "ldv_surveyservices")
            {
                SurveyServices.Set(executionContext, entityReference);
            }
            // Set the output parameter (EntityReference)

            tracingService.Trace("EntityReference created successfully.");
        }
    }
}