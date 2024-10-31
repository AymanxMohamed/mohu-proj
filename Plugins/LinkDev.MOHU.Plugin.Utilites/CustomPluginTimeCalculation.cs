using LinDev.MOHU.Utilites.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.MOHU.Plugin.Utilites
{
    public class CustomPluginTimeCalculation : IPlugin
    {
        ITracingService tracingService;
        IOrganizationService service;
        OrganizationResponse customizedTimeCalculationResponse;
        public void Execute(IServiceProvider serviceProvider)
        {

            tracingService =(ITracingService)serviceProvider.GetService(typeof(ITracingService));
          
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory =(IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            service = serviceFactory.CreateOrganizationService(context.UserId);
            tracingService.Trace($"in Execute ");

            int slaLevel = -1;
            // Ensure this plugin is for the correct action
            if (context.MessageName == ActionNames.ActionLevel1)
            {
                slaLevel = 1;
                tracingService.Trace($"Sla Level = 1 ");
            }
            else if (context.MessageName == ActionNames.ActionLevel2)
            {
                slaLevel = 2;
                tracingService.Trace($"Sla Level = 2 ");
            }
            else if (context.MessageName == ActionNames.ActionLevel3)
            {
                slaLevel = 3;
                tracingService.Trace($"Sla Level = 3 ");
            }
            else
            {
                 throw new InvalidPluginExecutionException("This plugin is registered for a different action. ");
            }
            // The InputParameters collection contains all the data passed in the message request.
            string regardingId = context.InputParameters["regardingId"].ToString();
            string calendarId = context.InputParameters["calendarId"].ToString();
            string requestType = context.InputParameters["requestType"].ToString();
            string slaItemId = context.InputParameters["slaItemId"].ToString();
            string previousInstanceId = context.InputParameters["previousInstanceId"].ToString();
            string entityName = context.InputParameters["entityName"].ToString();
            int warningDuration = (int)context.InputParameters["firstInputDuration"] ;
            int failureDuration = (int)context.InputParameters["secondInputDuration"] ;
            DateTime warningStartTime = (DateTime)context.InputParameters["firstInputDate"];
            DateTime failureStartTime = (DateTime) context.InputParameters["secondInputDate"];

            tracingService.Trace($"input parameter");
            tracingService.Trace($"calendarId: {calendarId}  , slaItemId: {slaItemId}  , requestType: {requestType} , previousInstanceId: {previousInstanceId}");
            tracingService.Trace($" regardingId: {regardingId} , entityName: {entityName}");
            tracingService.Trace($"warningDuration: {warningDuration}   , failureDuration: {failureDuration}");
            tracingService.Trace($"warningStartTime: {warningStartTime} , failureStartTime: {failureStartTime}");
            tracingService.Trace($"-----------------");


            try
            {
                // implement this requestType for any new SLA KPi instance creation.
                if (requestType.Equals("getEndTime", StringComparison.CurrentCultureIgnoreCase))
                {
                    tracingService.Trace("Calculating warning and failure times.");
                    DateTime warningTime;
                    DateTime failureTime;

                    string returnCalendarId = CalculateWarningAndFailureTime(regardingId, calendarId, slaItemId, entityName, warningStartTime,
                        failureStartTime, warningDuration, failureDuration, slaLevel, out   warningTime, out   failureTime);

                    #region MyRegion

                    tracingService.Trace($"Executing Custom SLA Time Calculation");
                    tracingService.Trace($"Current UTC Time: {DateTime.UtcNow}");
                    tracingService.Trace($"Calculated failureTime: {failureTime}");
                    tracingService.Trace($"Calculated warningTime: {warningTime}");
                    tracingService.Trace($"Using Calendar ID: {returnCalendarId}");
                    #endregion

                    ////   return the output values.
                    context.OutputParameters["firstOutputValue"] = warningTime .ToString();
                    context.OutputParameters["secondOutputValue"] = failureTime.ToString();
                    context.OutputParameters["returnCalendarId"] = returnCalendarId;
                    tracingService.Trace($"after CalculateWarningAndFailureTime");

                }
                // implement this requestType for finding Paused time for any new SLA KPi instance after it resumed.
                else if (requestType.Equals("getElapsedTime", StringComparison.CurrentCultureIgnoreCase))
                {
                    tracingService.Trace($"getElapsedTime");
                    DateTime casePausedTime = (DateTime)context.InputParameters["firstInputDate"];
                    DateTime caseResumedTime = (DateTime)context.InputParameters["secondInputDate"];
                    int existingElapsedTime = (int)context.InputParameters["firstInputDuration"];
                    // Step 2 : Add the custom Logic to calculate the elapsedTime between startTime(paused) and endTime(resumed)
                    double elapsedTimeInMinutes = CalculateElapsedTime(regardingId, calendarId, slaItemId, entityName, casePausedTime, caseResumedTime, existingElapsedTime);

                    // Step 3 : return the output values.
                    context.OutputParameters["firstOutputValue"] = elapsedTimeInMinutes.ToString();
                    context.OutputParameters["secondOutputValue"] = elapsedTimeInMinutes.ToString();
                    tracingService.Trace($"after CalculateWarningAndFailureTime");
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace("Exception: " + ex.ToString());
                throw new InvalidPluginExecutionException($"An error occurred: {ex.Message}");
            }
        }

        private string CalculateWarningAndFailureTime(string regardingId, string calendarId, string slaItemId,
            string entityName, DateTime warningStartTime, DateTime failureStartTime,
            int warningDuration, int failureDuration, int slaLevel, out DateTime warningTime, out DateTime failureTime)
        {
            warningTime = DateTime.UtcNow;
            failureTime = DateTime.UtcNow;
            int newWarningTime = warningDuration;
            int newFailureTime = failureDuration;
            int customCode = 0;



            tracingService.Trace($"in CalculateWarningAndFailureTime");
            
            // use OOB SLATimeCalculation Custom Action to do actual calculation_
            OrganizationRequest requestTimeCalculation = new OrganizationRequest("msdyn_SLATimeCalculation");
            requestTimeCalculation["requestType"] = "getendtime";
            requestTimeCalculation["calendarId"] = calendarId;


            // Retrieve the SLA hours and durations
            SLAHoursResult slaHoursResult = RetrieveSLAHours(entityName, regardingId);


            // Use the retrieved SLA hours and durations as needed
            int[] warningTimes = { slaHoursResult.Level1WarningTime, slaHoursResult.Level2WarningTime, slaHoursResult.Level3WarningTime };
            int[] failureTimes = { slaHoursResult.Level1FailureTime, slaHoursResult.Level2FailureTime, slaHoursResult.Level3FailureTime };

            bool isWarningTimeUpdated = false;
            bool isFailureTimeUpdated = false;

            for (int i = 0; i < slaLevel; i++)
            {
                if (warningTimes[i] != 0)
                {
                    if (!isWarningTimeUpdated)
                    {
                        newWarningTime = 0; // Reset only once
                        isWarningTimeUpdated = true;
                    }
                    newWarningTime += warningTimes[i];
                }

                if (failureTimes[i] != 0)
                {
                    if (!isFailureTimeUpdated)
                    {
                        newFailureTime = 0; // Reset only once
                        isFailureTimeUpdated = true;
                    }
                    newFailureTime += failureTimes[i];
                }
            }

            tracingService.Trace($"newWarningTime: {newWarningTime}, newFailureTime: {newFailureTime}");



            // calculate warning time
            if (warningDuration != -1)
            {
                tracingService.Trace($" in warningDuration");

                requestTimeCalculation["startTime"] = warningStartTime;
                requestTimeCalculation["minutes"] = newWarningTime;
                customizedTimeCalculationResponse =  service.Execute(requestTimeCalculation);
                tracingService.Trace($"after Execute");
                customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue1);
                warningTime = DateTime.SpecifyKind(DateTime.Parse((string)outputValue1), DateTimeKind.Utc);
                tracingService.Trace($"warningTime {warningTime}");

            }
            // calculate Failure time
            if (failureDuration != -1)
            {
                tracingService.Trace($" in failureDuration");
                requestTimeCalculation["startTime"] = failureStartTime;
                requestTimeCalculation["minutes"] = newFailureTime;
                customizedTimeCalculationResponse =  service.Execute(requestTimeCalculation);
                tracingService.Trace($"after Execute");
                customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue2);
                failureTime = DateTime.SpecifyKind(DateTime.Parse((string)outputValue2), DateTimeKind.Utc);
                tracingService.Trace($"failureTime {failureTime}");
            }
            return calendarId;
        }

        private double CalculateElapsedTime(string regardingId, string calendarId, string slaItemId, string entityName, DateTime casePausedTime, DateTime caseResumedTime, int existingElapsedTime)
        {

            // use OOB SLATimeCalculation Custom Action to do actual calculation_
            OrganizationRequest requestTimeCalculation = new OrganizationRequest("msdyn_SLATimeCalculation");
            requestTimeCalculation["startTime"] = casePausedTime;
            requestTimeCalculation["endTime"] = caseResumedTime;
            requestTimeCalculation["requestType"] = "getElapsedTime";
            requestTimeCalculation["calendarId"] = calendarId;

            // calculate elapsed time
            customizedTimeCalculationResponse =  service.Execute(requestTimeCalculation);
            customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue1);
            double totalElapsedTime = existingElapsedTime + Double.Parse(outputValue1.ToString());
            return totalElapsedTime;
        }
     
        Entity FetchCalendar(string entityName, string regardingId)
        {

            Entity caseRecord = new Entity();
            return caseRecord;
        }
      
        //Entity FetchRequestRecord(string entitySchemaName, string regardingId, string slaLookupName)
        //{
        //    var query = new QueryExpression();

        //    if (entitySchemaName == "task")
        //    {
        //        query = ExecuteRequestQuery("activityid", regardingId, slaLookupName);

        //    }
        //    else if (entitySchemaName == "incident")
        //    {
        //        query = ExecuteRequestQuery("incidentid", regardingId, slaLookupName);

        //    }
        //    var result = service.RetrieveMultiple(query)[0];

        //    return result;
        //}
        //QueryExpression ExecuteRequestQuery(string entitySchemaName, string regardingId, string slaLookupName)
        //{

        //    // Instantiate QueryExpression query
        //    var query = new QueryExpression("ldv_slahours");
        //    query.Distinct = true;
        //    // Add columns to query.ColumnSet
        //    query.ColumnSet.AddColumns("ldv_name", "createdon", "ldv_code", "ldv_slahoursid", "ldv_warningdurationminutes", "ldv_warningdurationhours", "ldv_failuredurationminutes", "ldv_failuredurationhours");
        //    query.AddOrder("createdon", OrderType.Descending);
        //    var query_statecode = 0;
        //    // Define filter query.Criteria
        //    query.Criteria.AddCondition("statecode", ConditionOperator.Equal, query_statecode);
        //    // Add link-entity aa
        //    var aa = query.AddLink("ldv_casecategory", "ldv_slahoursid", slaLookupName);
        //    aa.EntityAlias = "aa";
        //    // Add link-entity ad
        //    var ad = aa.AddLink(entitySchemaName, "ldv_casecategoryid", "ldv_subcategoryid");
        //    ad.EntityAlias = "ad";
        //    // Define filter ad.LinkCriteria
        //    ad.LinkCriteria.AddCondition("activityid", ConditionOperator.Equal, regardingId);
        //    //add execute query
        //    return query;
        //}

        public string GetSLAConfigurationCriteria()
        {
            tracingService.Trace($"GetSLAConfigurationCriteria");

            // Query to retrieve the configuration record where ldv_name = 'SLATimeDependent'
            QueryExpression query = new QueryExpression(ConfigurationEntity.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(ConfigurationEntity.ValueLogicalName)
            };
            query.Criteria.AddCondition(ConfigurationEntity.NameLogicalName, ConditionOperator.Equal, "SLATimeDependent");

            EntityCollection results = service.RetrieveMultiple(query);

            // Check if the record was found
            if (results.Entities.Count > 0)
            {
                // Extract the value from ldv_value field
                string slaConfiguredBy = results.Entities[0].GetAttributeValue<string>(ConfigurationEntity.ValueLogicalName);
                return slaConfiguredBy; // Output: 'Service' or 'SubCategory'
            }
            else
            {
                tracingService.Trace("SLATimeDependent configuration not found.");
                return null;
            }
        }

        //Entity FetchCaseCategory(string entitySchemaName, string regardingId)
        //{
        //    tracingService.Trace($"in FetchCaseCategory");

        //    // Create a query expression for ldv_casecategory
        //    var query = new QueryExpression(CategoryEntity.EntityLogicalName)
        //    {
        //        ColumnSet = new ColumnSet(CategoryEntity.SlaHourLevel1, CategoryEntity.SlaHourLevel2, CategoryEntity.SlaHourLevel3)
        //    };

        //    // Create a link entity to join with the task entity
        //    var taskLink = new LinkEntity(CategoryEntity.EntityLogicalName, TaskEntity.EntityLogicalName, CategoryEntity.IDLogicalName, TaskEntity.SubCategory, JoinOperator.Inner);
        //    taskLink.Columns.AddColumns(TaskEntity.IDLogicalName);

        //    // Filter the link entity to include only the task with the specific activityid
        //    taskLink.LinkCriteria.AddCondition(TaskEntity.IDLogicalName, ConditionOperator.Equal, new Guid(regardingId));

        //    // Add the link entity to the main query
        //    query.LinkEntities.Add(taskLink);

        //    // Retrieve the ldv_casecategory entity
        //    var result = service.RetrieveMultiple(query);

        //    // Check if the result contains any entities
        //    if (result.Entities.Count == 0)
        //    {
        //        //throw new InvalidPluginExecutionException($"No ldv_casecategory found for task with ID {regardingId}.");
        //        tracingService.Trace($"No ldv_casecategory found for task with ID {regardingId}.");
        //        return null;
        //    }

        //    // Return the first ldv_casecategory entity found (assuming there should be only one)
        //    return result.Entities[0];
        //}

        public Entity FetchEntityWithTaskLink(
                                string entitySchemaName,
                                string taskLinkAttribute,
                                string taskLinkField,
                                string regardingId,
                                ColumnSet columnSet)
        {
            tracingService.Trace($"Fetching entity: {entitySchemaName}");

            // Create a query expression for the provided entity
            var query = new QueryExpression(entitySchemaName)
            {
                ColumnSet = columnSet
            };

            // Create a link entity to join with the task entity
            var taskLink = new LinkEntity(entitySchemaName, TaskEntity.EntityLogicalName, taskLinkAttribute, taskLinkField, JoinOperator.Inner);
            taskLink.Columns.AddColumns(TaskEntity.IDLogicalName);

            // Filter the link entity to include only the task with the specific activityid
            taskLink.LinkCriteria.AddCondition(TaskEntity.IDLogicalName, ConditionOperator.Equal, new Guid(regardingId));

            // Add the link entity to the main query
            query.LinkEntities.Add(taskLink);

            // Retrieve the entity based on the query
            var result = service.RetrieveMultiple(query);

            // Check if the result contains any entities
            if (result.Entities.Count == 0)
            {
                tracingService.Trace($"No entity found for {entitySchemaName} with task ID {regardingId}.");
                return null;
            }

            // Return the first entity found (assuming there should be only one)
            return result.Entities[0];
        }



        Dictionary<Guid, Entity> FetchSLAHours(List<Guid> slaHoursIds)
        {
            tracingService.Trace($"in FetchSLAHours");

            if (slaHoursIds == null || slaHoursIds.Count == 0)
            {
                //throw new ArgumentException("SLA Hours IDs list cannot be null or empty.");
                tracingService.Trace("SLA Hours IDs list cannot be null or empty.");
            }

            var slaQuery = new QueryExpression(SlaHoursEntity.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(
                    SlaHoursEntity.IDLogicalName,
                    SlaHoursEntity.FailureDurationHours,
                    SlaHoursEntity.FailureDurationMinutes,
                    SlaHoursEntity.WarningDurationHours,
                    SlaHoursEntity.WarningDurationMinutes
                ),

                Criteria = new FilterExpression
                {
                    Conditions =
            {
                new ConditionExpression(SlaHoursEntity.IDLogicalName, ConditionOperator.In, slaHoursIds.ToArray())
            }
                }
            };

            var slaHoursEntities = service.RetrieveMultiple(slaQuery).Entities.ToList();
            if (slaHoursEntities.Count == 0)
            {
                //throw new InvalidPluginExecutionException("No SLA Hours records found.");
                tracingService.Trace("No SLA Hours records found.");
                return new Dictionary<Guid, Entity>();
            }

            return slaHoursEntities.ToDictionary(e => e.Id, e => e);
        }


        //SLAHoursResult RetrieveSLAHours(string entityName, string regardingId)
        //{
        //    var result = new SLAHoursResult();

        //    tracingService.Trace($"in RetrieveSLAHours");

        //    // Fetch the case category
        //    Entity caseCategoryEntity = FetchEntityWithTaskLink(
        //                                        CategoryEntity.EntityLogicalName,
        //                                        CategoryEntity.IDLogicalName,regardingId,
        //                                        new ColumnSet(CategoryEntity.SlaHourLevel1, CategoryEntity.SlaHourLevel2, CategoryEntity.SlaHourLevel3)
        //                                        );

        //    if (caseCategoryEntity == null)
        //    {
        //        tracingService.Trace($"No case category found for entity {entityName} with regarding ID {regardingId}.");
        //        return result;
        //    }

        //    // Retrieve SLA Hour Level IDs from the case category entity
        //    if (caseCategoryEntity.Contains(CategoryEntity.SlaHourLevel1) &&
        //        caseCategoryEntity.Contains(CategoryEntity.SlaHourLevel2) &&
        //        caseCategoryEntity.Contains(CategoryEntity.SlaHourLevel3))
        //    {
        //        Guid level1Id = caseCategoryEntity.GetAttributeValue<EntityReference>(CategoryEntity.SlaHourLevel1).Id;
        //        Guid level2Id = caseCategoryEntity.GetAttributeValue<EntityReference>(CategoryEntity.SlaHourLevel2).Id;
        //        Guid level3Id = caseCategoryEntity.GetAttributeValue<EntityReference>(CategoryEntity.SlaHourLevel3).Id;

        //        var slaHoursIds = new List<Guid> { level1Id, level2Id, level3Id };

        //        // Fetch the SLA hours for all three levels in one call
        //        var slaHoursEntities = FetchSLAHours(slaHoursIds);

        //        // Process each SLA Hours entity separately
        //        if (slaHoursEntities.TryGetValue(level1Id, out Entity slaHoursLevel1))
        //        {
        //            result.Level1WarningTime = GetDurationMinutes(slaHoursLevel1, SlaHoursEntity.WarningDurationHours, SlaHoursEntity.WarningDurationMinutes);
        //            result.Level1FailureTime = GetDurationMinutes(slaHoursLevel1, SlaHoursEntity.FailureDurationHours, SlaHoursEntity.FailureDurationMinutes);
        //        }

        //        if (slaHoursEntities.TryGetValue(level2Id, out Entity slaHoursLevel2))
        //        {
        //            result.Level2WarningTime = GetDurationMinutes(slaHoursLevel2, SlaHoursEntity.WarningDurationHours, SlaHoursEntity.WarningDurationMinutes);
        //            result.Level2FailureTime = GetDurationMinutes(slaHoursLevel2, SlaHoursEntity.FailureDurationHours, SlaHoursEntity.FailureDurationMinutes);
        //        }

        //        if (slaHoursEntities.TryGetValue(level3Id, out Entity slaHoursLevel3))
        //        {
        //            result.Level3WarningTime = GetDurationMinutes(slaHoursLevel3, SlaHoursEntity.WarningDurationHours, SlaHoursEntity.WarningDurationMinutes);
        //            result.Level3FailureTime = GetDurationMinutes(slaHoursLevel3, SlaHoursEntity.FailureDurationHours, SlaHoursEntity.FailureDurationMinutes);
        //        }
        //    }
        //    else
        //    {
        //        tracingService.Trace($"One or more SLA Hour Level fields missing in case category entity for entity {entityName} with regarding ID {regardingId}.");

        //    }

        //    return result;
        //}

        SLAHoursResult RetrieveSLAHours(string entityName, string regardingId)
        {
            var result = new SLAHoursResult();
            tracingService.Trace($"In RetrieveSLAHours");

            // Step 1: Check SLA configuration criteria (Service or SubCategory)
            string slaCriteria = GetSLAConfigurationCriteria(); // 'Service' or 'SubCategory'
            tracingService.Trace($"SLA is configured by: {slaCriteria}");

            // Step 2: Set entity logical name, link attribute, and column set dynamically
            string entityLogicalName;
            string linkAttribute;
            string slaHourLevel1;
            string slaHourLevel2;
            string slaHourLevel3;
            string linkField;

            if (slaCriteria == "Service")
            {
                entityLogicalName = ServiceEntity.EntityLogicalName;
                linkAttribute = ServiceEntity.IDLogicalName;
                slaHourLevel1 = ServiceEntity.SlaHourLevel1;
                slaHourLevel2 = ServiceEntity.SlaHourLevel2;
                slaHourLevel3 = ServiceEntity.SlaHourLevel3;
                linkField = TaskEntity.Service;

            }
            else
            {
                entityLogicalName = CategoryEntity.EntityLogicalName;
                linkAttribute = CategoryEntity.IDLogicalName;
                slaHourLevel1 = CategoryEntity.SlaHourLevel1;
                slaHourLevel2 = CategoryEntity.SlaHourLevel2;
                slaHourLevel3 = CategoryEntity.SlaHourLevel3;
                linkField = TaskEntity.SubCategory;
            }

            // Create column set using the appropriate SLA Hour fields
            var columnSet = new ColumnSet(slaHourLevel1, slaHourLevel2, slaHourLevel3);

            // Step 3: Fetch the relevant entity (either Service or Case Category)
            Entity relatedEntity = FetchEntityWithTaskLink(
                entityLogicalName,
                linkAttribute,
                linkField,
                regardingId,
                columnSet
            );

            if (relatedEntity == null)
            {
                tracingService.Trace($"No {entityLogicalName} found for entity {entityName} with regarding ID {regardingId}.");
                return result;
            }

            // Step 4: Retrieve SLA Hour Level IDs using the dynamically selected field names
            if (relatedEntity.Contains(slaHourLevel1) &&
                relatedEntity.Contains(slaHourLevel2) &&
                relatedEntity.Contains(slaHourLevel3))
            {
                Guid level1Id = relatedEntity.GetAttributeValue<EntityReference>(slaHourLevel1).Id;
                Guid level2Id = relatedEntity.GetAttributeValue<EntityReference>(slaHourLevel2).Id;
                Guid level3Id = relatedEntity.GetAttributeValue<EntityReference>(slaHourLevel3).Id;

                var slaHoursIds = new List<Guid> { level1Id, level2Id, level3Id };

                // Step 5: Fetch SLA hours for all levels in one call
                var slaHoursEntities = FetchSLAHours(slaHoursIds);

                // Step 6: Process each SLA Hours entity separately
                if (slaHoursEntities.TryGetValue(level1Id, out Entity slaHoursLevel1Entity))
                {
                    result.Level1WarningTime = GetDurationMinutes(slaHoursLevel1Entity, SlaHoursEntity.WarningDurationHours, SlaHoursEntity.WarningDurationMinutes);
                    result.Level1FailureTime = GetDurationMinutes(slaHoursLevel1Entity, SlaHoursEntity.FailureDurationHours, SlaHoursEntity.FailureDurationMinutes);
                }

                if (slaHoursEntities.TryGetValue(level2Id, out Entity slaHoursLevel2Entity))
                {
                    result.Level2WarningTime = GetDurationMinutes(slaHoursLevel2Entity, SlaHoursEntity.WarningDurationHours, SlaHoursEntity.WarningDurationMinutes);
                    result.Level2FailureTime = GetDurationMinutes(slaHoursLevel2Entity, SlaHoursEntity.FailureDurationHours, SlaHoursEntity.FailureDurationMinutes);
                }

                if (slaHoursEntities.TryGetValue(level3Id, out Entity slaHoursLevel3Entity))
                {
                    result.Level3WarningTime = GetDurationMinutes(slaHoursLevel3Entity, SlaHoursEntity.WarningDurationHours, SlaHoursEntity.WarningDurationMinutes);
                    result.Level3FailureTime = GetDurationMinutes(slaHoursLevel3Entity, SlaHoursEntity.FailureDurationHours, SlaHoursEntity.FailureDurationMinutes);
                }
            }
            else
            {
                tracingService.Trace($"One or more SLA Hour Level fields are missing in the {entityLogicalName} entity for {entityName} with regarding ID {regardingId}.");
            }

            return result;
        }


        int GetDurationMinutes(Entity entity, string hoursFieldName, string minutesFieldName)
        {
            var hours = (int)(entity.GetAttributeValue<decimal?>(hoursFieldName) ?? 0);
            var minutes = (int)(entity.GetAttributeValue<decimal?>(minutesFieldName) ?? 0);
            return hours * 60 + minutes;
        }


    }

    class SLAHoursResult
    {
        public int Level1WarningTime { get; set; }
        public int Level1FailureTime { get; set; }
        public int Level2WarningTime { get; set; }
        public int Level2FailureTime { get; set; }
        public int Level3WarningTime { get; set; }
        public int Level3FailureTime { get; set; }
    }




}
