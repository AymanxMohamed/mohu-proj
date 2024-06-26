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
            if (context.MessageName == "new_CustomPluginTimeCalculationd87ba2edce32ef1184096045bd8d9989")
            {
                slaLevel = 1;
                tracingService.Trace($"Sla Level = 1 ");
            }
            else if (context.MessageName == "new_ActionSLATimerCustomPluginTimeCalculationLevel28a5b068cb933ef1184096045bd8d9989")
            {
                slaLevel = 2;
                tracingService.Trace($"Sla Level = 2 ");
            }
            else if (context.MessageName == "new_ActionSLATimerCustomPluginTimeCalculationLevel3b0e030b5bc33ef11840a000d3a48ff6a")
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

            // API name of action 
            //OrganizationRequest actionRequest = new OrganizationRequest("new_CustomPluginTimeCalculationd87ba2edce32ef1184096045bd8d9989");      
            //    tracingService.Trace(" CustomPluginTimeCalculationd attempt to exicute request");
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

                    //warningTime = new DateTime(2024, 06, 26, 19, 00, 00);
                    //  failureTime = new DateTime(2024, 06, 27, 20, 00, 00);
                    //string returnCalendarId = "39825827-0ED1-EE11-9079-6045BD895E74";

                    tracingService.Trace($"Executing Custom SLA Time Calculation");
                    tracingService.Trace($"Current UTC Time: {DateTime.UtcNow}");
                    tracingService.Trace($"Calculated failureTime: {failureTime}");
                    tracingService.Trace($"Calculated warningTime: {warningTime}");
                    tracingService.Trace($"Using Calendar ID: {returnCalendarId}");
                    #endregion

                    ////   return the output values.
                    context.OutputParameters["firstOutputValue"] = warningTime .ToString();
                    context.OutputParameters["secondOutputValue"] = failureTime.ToString();
                    context.OutputParameters["returnCalendarId"] = returnCalendarId; // Example calendar ID
                    tracingService.Trace($"after CalculateWarningAndFailureTime");

                }
                // implement this requestType for finding Paused time for any new SLA KPi instance after it resumed.
                else if (requestType.Equals("getElapsedTime", StringComparison.CurrentCultureIgnoreCase))
                {
                    tracingService.Trace($"getElapsedTime");
                    //DateTime casePausedTime = (DateTime)context.InputParameters["firstInputDate"];
                    //DateTime caseResumedTime = (DateTime)context.InputParameters["secondInputDate"];
                    //int existingElapsedTime = (int)context.InputParameters["firstInputDuration"];
                    //// Step 2 : Add the custom Logic to calculate the elapsedTime between startTime(paused) and endTime(resumed)
                    //double elapsedTimeInMinutes = CalculateElapsedTime(regardingId, calendarId, slaItemId, entityName, casePausedTime, caseResumedTime, existingElapsedTime);

                    //// Step 3 : return the output values.
                    //context.OutputParameters["firstOutputValue"] = elapsedTimeInMinutes.ToString();
                    //context.OutputParameters["secondOutputValue"] = elapsedTimeInMinutes.ToString();
                    //tracingService.Trace($"after CalculateWarningAndFailureTime");
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace("Exception: " + ex.ToString());
                throw new InvalidPluginExecutionException($"An error occurred: {ex.Message}");
            }
        }

        // in this example, we're using Custom Field(new_country) on the Case entity to apply the required calendar.
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
            string slaLookupName = string.Empty;

            if (slaLevel==1)
            {
                slaLookupName = "ldv_slahourlevel1id";
            }
            else if (slaLevel == 2)
            {
                slaLookupName = "ldv_slahourlevel2id";
            }
            else if (slaLevel == 3)
            {
                slaLookupName = "ldv_slahourlevel3id";
            }
            Entity entity=FetchRequestRecord(entityName, regardingId, slaLookupName);

        




            // calculate warning time
            if (warningDuration != -1)
            {
                tracingService.Trace($" in warningDuration");

                requestTimeCalculation["startTime"] = warningStartTime; /*DateTime.Now.AddHours()*/
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
            //if (caseRecord.Attributes.Contains("new_country"))
            //{
            //    //if ((int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value) == 0)
            //    //{
            //    //    // fetch IST id
            //    //    IST_CALENDAR = FetchCalendar("IST_CALENDAR" );
            //    //    calendarId = IST_CALENDAR;
            //    //}
            //    //else if ((int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value) == 1)
            //    //{
            //    //    // fetch PST  id
            //    //    PST_CALENDAR = FetchCalendar("PST_CALENDAR" );
            //    //    calendarId = PST_CALENDAR;
            //    //}
            //}

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
      
        Entity FetchRequestRecord(string entitySchemaName, string regardingId, string slaLookupName)
        {
            var query = new QueryExpression();

            if (entitySchemaName == "task")
            {
                query = ExecuteRequestQuery("activityid", regardingId, slaLookupName);

            }
            else if (entitySchemaName == "incident")
            {
                query = ExecuteRequestQuery("incidentid", regardingId, slaLookupName);

            }
            var result = service.RetrieveMultiple(query)[0];

            return result;
        }
        QueryExpression ExecuteRequestQuery(string entitySchemaName, string regardingId, string slaLookupName)
        {

            //warning/ failur 
            //min. /hour
            //
            //1 - get ticket category from task
            // 2 - get sla hours level 1 ,2,3 from ticket category
            //  3 - check
            // if leve3
            // time = level1 + level2 + l3vel3
            //else leve2
            // time = level1 + level2
            //else leve1
            // time = level1


            // Instantiate QueryExpression query
            var query = new QueryExpression("ldv_slahours");
            query.Distinct = true;
            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("ldv_name", "createdon", "ldv_code", "ldv_slahoursid", "ldv_warningdurationminutes", "ldv_warningdurationhours", "ldv_failuredurationminutes", "ldv_failuredurationhours");
            query.AddOrder("createdon", OrderType.Descending);
            var query_statecode = 0;
            // Define filter query.Criteria
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, query_statecode);
            // Add link-entity aa
            var aa = query.AddLink("ldv_casecategory", "ldv_slahoursid", slaLookupName);
            aa.EntityAlias = "aa";
            // Add link-entity ad
            var ad = aa.AddLink(entitySchemaName, "ldv_casecategoryid", "ldv_subcategoryid");
            ad.EntityAlias = "ad";
            // Define filter ad.LinkCriteria
            ad.LinkCriteria.AddCondition("activityid", ConditionOperator.Equal, regardingId);
            //add execute query
            return query;
        }


 


    }
}
