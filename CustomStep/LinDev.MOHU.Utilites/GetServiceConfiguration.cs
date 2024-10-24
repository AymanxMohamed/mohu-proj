using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.IdentityModel.Metadata;
using Microsoft.Xrm.Sdk.Query;
using LinDev.MOHU.Utilites.Model;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;

namespace LinDev.MOHU.Utilites
{
    public class GetServiceConfiguration : CodeActivity
    {

        #region "Input Parameters"
        [Input("Service Id")]
        [RequiredArgument]
        public InArgument<string> ServiceId { get; set; }

        [Input("Stage Configuration")]
        [RequiredArgument]
        [ReferenceTarget("ldv_stageconfiguration")]

        public InArgument<EntityReference> StageConfiguration { get; set; }

        [Input("Target Entity Id")]
        [RequiredArgument]
        public InArgument<string> TargetEntityId { get; set; }

        [Input("Target Entit SchemaName")]
        [RequiredArgument]
        public InArgument<string> TargetEntitSchemaName { get; set; }
        #endregion

        #region "Output Parameters"
        [Output("Team")]
        [ReferenceTarget("team")]
        public OutArgument<EntityReference> Team { get; set; }
        #endregion



        // Variables to hold services and context
        protected CodeActivityContext executionContext;
        protected IWorkflowContext wfContext;
        protected IOrganizationServiceFactory serviceFactory;
        protected ITracingService tracingService;
        protected IOrganizationService service;

        protected override void Execute(CodeActivityContext context)
        {

            // Initialize the tracing service for logging
            tracingService = executionContext.GetExtension<ITracingService>();
            tracingService.Trace("Custom workflow activity execution started.");

            // Initialize the workflow context to access CRM data
            wfContext = executionContext.GetExtension<IWorkflowContext>();

            // Initialize the service factory to create an IOrganizationService instance
            serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();

            // Create the Organization Service to interact with CRM data
            service = serviceFactory.CreateOrganizationService(wfContext.UserId);


            // Retrieve input parameters
            string serviceId = ServiceId.Get(executionContext);
            string targetEntityId = TargetEntityId.Get(executionContext);
            string targetEntitySchemaName = TargetEntitSchemaName.Get(executionContext);
            EntityReference stageConfigration = StageConfiguration.Get(executionContext);


            // Validate and parse entity ID
            if (!Guid.TryParse(serviceId, out Guid parsedServiceID))
            {
                throw new InvalidPluginExecutionException("Invalid Entity ID format.");
            }


            try {

                ColumnSet columns = new ColumnSet("ldv_serviceconfigurationfields");
                Entity result =  service.Retrieve(ServiceDefinitionModel.EntitySchemaName, parsedServiceID, columns);
                string ldv_serviceconfigurationfields = result.GetAttributeValue<string>("ldv_serviceconfigurationfields"); // Multiline Text

                string[] fields = ldv_serviceconfigurationfields.Split(',');
                GetFieldsFromTargetEntity(targetEntitySchemaName,targetEntityId, fields);

            }
            catch (Exception ex){
                tracingService.Trace($"Error : {ex.Message}");
            }

            throw new NotImplementedException();
        }



        private void GetFieldsFromTargetEntity(string targetEntitySchemaName, string targetEntityId, string[] fieldsToFetchValues)
        {

            // Create the column set from the fieldsToFetchValues array
            ColumnSet columns = new ColumnSet(fieldsToFetchValues);

            // Ensure targetEntityId is a valid GUID
            if (!Guid.TryParse(targetEntityId, out Guid entityId))
            {
                tracingService.Trace("Invalid entity ID.");
            }

            try
            {
                // Retrieve the target entity record using the provided schema name, entity ID, and columns
                Entity targetEntity = service.Retrieve(targetEntitySchemaName, entityId, columns);

                Dictionary<string, object> schemaNamevaluePairs = new Dictionary<string, object>();
                // Check if the target entity contains the fields and log the values
                foreach (string field in fieldsToFetchValues)
                {
                    if (targetEntity.Contains(field))
                    {
                        schemaNamevaluePairs[field] = targetEntity[field];
                    }
                    else
                    {
                        tracingService.Trace($"Field {field} is not found on the entity.");
                    }
                }
                QueryInServiceConfigrationsEntity( schemaNamevaluePairs);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"Error retrieving entity: {ex.Message}");
            }
        }

        private void QueryInServiceConfigrationsEntity(Dictionary<string, object> schemaNamevaluePairs)
        {
            EntityReference stageConfigration = StageConfiguration.Get(executionContext);

            ColumnSet columns = new ColumnSet(true);


            // Create a QueryExpression to query the target entity
            QueryExpression query = new QueryExpression
            {
                EntityName = ServiceConfigurationModel.EntitySchemaName,
                ColumnSet = columns,
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And
                },
                TopCount = 1,
            };
            //Add stage configration id condition
            query.Criteria.AddCondition(new ConditionExpression(ServiceConfigurationModel.StageConfigration, ConditionOperator.Equal, stageConfigration.Id));

            // Add conditions dynamically from the schemaNamevaluePairs dictionary
            foreach (var pair in schemaNamevaluePairs)
            {
                string attributeName = pair.Key;
                object value = pair.Value;

                // Add a condition to the criteria based on the attribute name and value
                query.Criteria.AddCondition(new ConditionExpression(attributeName, ConditionOperator.Equal, value));
            }

            try
            {
                // Retrieve target service configuration
                EntityCollection serviceConfigurations = service.RetrieveMultiple(query);
                Entity selectedServiceConfigration = serviceConfigurations[0];
                if(selectedServiceConfigration.TryGetAttributeValue(ServiceConfigurationModel.PortalStatus, out EntityReference portalstatus) && selectedServiceConfigration.TryGetAttributeValue(ServiceConfigurationModel.StatusReason, out EntityReference statusreason) && selectedServiceConfigration.TryGetAttributeValue(ServiceConfigurationModel.Team, out EntityReference team))
                {
                    UpdateCaseStatus(portalstatus, statusreason);
                    Team.Set(executionContext, team);
                }


            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the query
                tracingService.Trace($"Error querying entity: {ex.Message}");
            }

        }

        private void UpdateCaseStatus(EntityReference statusReason, EntityReference portalStatus)
        {
            string targetEntityId = TargetEntityId.Get(executionContext);
            string targetEntitySchemaName = TargetEntitSchemaName.Get(executionContext);
            if (!Guid.TryParse(targetEntityId, out Guid entityId))
            {
                throw new ArgumentException("Invalid case ID.");
            }
            Entity targetEntity = new Entity(targetEntitySchemaName)
            {
                Id = entityId
            };

            targetEntity[CaseModel.PortalStatus] = portalStatus;
            targetEntity[CaseModel.StatusReason] = statusReason;

            service.Update(targetEntity);
        }
    }
}
