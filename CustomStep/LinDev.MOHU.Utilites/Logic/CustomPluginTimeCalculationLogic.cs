//using Linkdev.Maan.Core.Helper;
//using LinkDev.MAAN.Common;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Workflow;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Services.Description;

//namespace LinDev.MOHU.Utilites.Logic
//{
//    public class CustomPluginTimeCalculationLogic : StepLogic< CustomPluginTimeCalculation>
//    {
//        IOrganizationService _service;
//        ITracingService tracingService;
//        Entity caseRecord;
//        OrganizationResponse customizedTimeCalculationResponse;
//        protected override void ExecuteLogic()
//        {
//            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
//            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
//              _service = serviceFactory.CreateOrganizationService(context.UserId);
//              tracingService = executionContext.GetExtension<ITracingService>();
//            tracingService.Trace($"Strat");

//            // Step 1: Retrieving Input Parameters.
//            string regardingId = codeActivity.regardingId.Get(executionContext);
//            string calendarId = codeActivity.calendarId.Get(executionContext);
//            string requestType = codeActivity.requestType.Get(executionContext);
//            string slaItemId = codeActivity.slaItemId.Get(executionContext);
//            string entityName = codeActivity.entityName.Get(executionContext);
//            try
//            {

             

//                // implement this requestType for any new SLA KPi instance creation.
//                if (requestType.Equals("getEndTime", StringComparison.CurrentCultureIgnoreCase))
//                {
//                    tracingService.Trace($"in getEndTime");
//                    int slaLevel = codeActivity.SlaLevel.Get(executionContext);
//                    int warningDuration = codeActivity.firstInputDuration.Get(executionContext);
//                    int failureDuration = codeActivity.secondInputDuration.Get(executionContext);
//                    DateTime warningStartTime = codeActivity.firstInputDate.Get(executionContext);
//                    DateTime failureStartTime = codeActivity.secondInputDate.Get(executionContext);

//                    tracingService.Trace($"input parameter");

//                    tracingService.Trace($"regardingId {regardingId}");
//                    tracingService.Trace($"calendarId {calendarId}");
//                    tracingService.Trace($"requestType {requestType}");
//                    tracingService.Trace($"slaItemId {slaItemId}");
//                    tracingService.Trace($"entityName {entityName}");
//                    tracingService.Trace($"slaLevel {slaLevel}");
//                    tracingService.Trace($"warningDuration {warningDuration}");
//                    tracingService.Trace($"failureDuration {failureDuration}");
//                    tracingService.Trace($"warningStartTime {warningStartTime}");
//                    tracingService.Trace($"failureStartTime {failureStartTime}");
//                    tracingService.Trace($"-----------------");


//                    tracingService.Trace($"before CalculateWarningAndFailureTime");
//                    // Step 2 : Add the custom Logic to calculate the WarningTime and FailureTime
//                    string returnCalendarId = CalculateWarningAndFailureTime(  regardingId, calendarId, slaItemId, entityName, warningStartTime,
//                        failureStartTime, warningDuration, failureDuration, slaLevel, out DateTime warningTime, out DateTime failureTime);
                    
//                    tracingService.Trace($"  failureTime {failureTime}");
//                    tracingService.Trace($"  warningTime {warningTime}");
//                    tracingService.Trace($"  returnCalendarId {returnCalendarId}");

//                    #region MyRegion
                  
//                    warningTime = new DateTime(2024, 06, 26, 15, 03, 00);
//                    failureTime = new DateTime(2024, 06, 26, 17, 00, 00);
                  
//                    tracingService.Trace($"Executing Custom SLA Time Calculation");
//                    tracingService.Trace($"Current UTC Time: {DateTime.UtcNow}");
//                    tracingService.Trace($"Calculated failureTime: {failureTime}");
//                    tracingService.Trace($"Calculated warningTime: {warningTime}");
//                    tracingService.Trace($"Using Calendar ID: {returnCalendarId}");

//                    //context.OutputParameters["firstOutputValue"] = failureTime.ToString();
//                    //context.OutputParameters["secondOutputValue"] = warningTime.ToString();
//                    //context.OutputParameters["returnCalendarId"] = returnCalendarId; // Example calendar ID

//                    #endregion

//                    //// Step 3 : return the output values.
//                    codeActivity.firstOutputValue.Set(executionContext, warningTime.ToString());// DateTime.SpecifyKind(warningTime, DateTimeKind.Utc).ToString());
//                    codeActivity.secondOutputValue.Set(executionContext, failureTime.ToString());//  DateTime.SpecifyKind(failureTime, DateTimeKind.Utc).ToString());
//                    codeActivity.returnCalendarId.Set(executionContext, returnCalendarId);
//                    tracingService.Trace($"after CalculateWarningAndFailureTime");
                
//                }
//                // implement this requestType for finding Paused time for any new SLA KPi instance after it resumed.
//                else if (requestType.Equals("getElapsedTime", StringComparison.CurrentCultureIgnoreCase))
//                {
//                    DateTime casePausedTime = (DateTime)context.InputParameters["firstInputDate"];
//                    DateTime caseResumedTime = (DateTime)context.InputParameters["secondInputDate"];
//                    int existingElapsedTime = (int)context.InputParameters["firstInputDuration"];
//                    // Step 2 : Add the custom Logic to calculate the elapsedTime between startTime(paused) and endTime(resumed)
//                    double elapsedTimeInMinutes = CalculateElapsedTime( regardingId, calendarId, slaItemId, entityName, casePausedTime, caseResumedTime, existingElapsedTime);

//                    // Step 3 : return the output values.
                   
//                   codeActivity.firstOutputValue.Set(executionContext, elapsedTimeInMinutes.ToString());
//                   codeActivity.secondOutputValue.Set(executionContext, elapsedTimeInMinutes.ToString());
//                    tracingService.Trace($"after CalculateWarningAndFailureTime");

               
//                }
//            }
//            catch (Exception ex)
//            {
//                tracingService.Trace($"ex {ex.Message}");
//                return;
//            }
//        }
        
//        // in this example, we're using Custom Field(new_country) on the Case entity to apply the required calendar.
//        private string CalculateWarningAndFailureTime(string regardingId, string calendarId, string slaItemId, 
//            string entityName, DateTime warningStartTime, DateTime failureStartTime,
//            int warningDuration, int failureDuration,int slaLevel, out DateTime warningTime, out DateTime failureTime)
//        {
//            warningTime = DateTime.UtcNow;
//            failureTime = DateTime.UtcNow;
//            int newWarningTime = warningDuration;
//            int newFailureTime = failureDuration;
//            int customCode = 0;
//            tracingService.Trace($"in CalculateWarningAndFailureTime");
//            // use OOB SLATimeCalculation Custom Action to do actual calculation_
//            OrganizationRequest requestTimeCalculation = new OrganizationRequest("msdyn_SLATimeCalculation");
//            requestTimeCalculation["requestType"] = "getendtime";
//            requestTimeCalculation["calendarId"] = calendarId;
//            // calculate warning time
//            if (warningDuration != -1)
//            {
//                tracingService.Trace($" in warningDuration");

//                requestTimeCalculation["startTime"] = warningStartTime;
//                requestTimeCalculation["minutes"] = newWarningTime;
//                customizedTimeCalculationResponse = _service.Execute(requestTimeCalculation);
//                tracingService.Trace($"after Execute");
//                customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue1);
//                warningTime = DateTime.SpecifyKind(DateTime.Parse((string)outputValue1), DateTimeKind.Utc);
//                tracingService.Trace($"warningTime {warningTime}");

//            }
//            // calculate Failure time
//            if (failureDuration != -1)
//            {
//                tracingService.Trace($" in failureDuration");
//                requestTimeCalculation["startTime"] = failureStartTime;
//                requestTimeCalculation["minutes"] = newFailureTime;
//                customizedTimeCalculationResponse = _service.Execute(requestTimeCalculation);
//                tracingService.Trace($"after Execute");
//                customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue2);
//                failureTime = DateTime.SpecifyKind(DateTime.Parse((string)outputValue2), DateTimeKind.Utc);
//                tracingService.Trace($"failureTime {failureTime}");

//            }
//            return calendarId;
//        }

//        private double CalculateElapsedTime(string regardingId, string calendarId, string slaItemId, string entityName, DateTime casePausedTime, DateTime caseResumedTime, int existingElapsedTime)
//        {
//            //if (caseRecord.Attributes.Contains("new_country"))
//            //{
//            //    //if ((int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value) == 0)
//            //    //{
//            //    //    // fetch IST id
//            //    //    IST_CALENDAR = FetchCalendar("IST_CALENDAR" );
//            //    //    calendarId = IST_CALENDAR;
//            //    //}
//            //    //else if ((int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value) == 1)
//            //    //{
//            //    //    // fetch PST  id
//            //    //    PST_CALENDAR = FetchCalendar("PST_CALENDAR" );
//            //    //    calendarId = PST_CALENDAR;
//            //    //}
//            //}

//            // use OOB SLATimeCalculation Custom Action to do actual calculation_
//            OrganizationRequest requestTimeCalculation = new OrganizationRequest("msdyn_SLATimeCalculation");
//            requestTimeCalculation["startTime"] = casePausedTime;
//            requestTimeCalculation["endTime"] = caseResumedTime;
//            requestTimeCalculation["requestType"] = "getElapsedTime";
//            requestTimeCalculation["calendarId"] = calendarId;

//            // calculate elapsed time
//            customizedTimeCalculationResponse = _service.Execute(requestTimeCalculation);
//            customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue1);
//            double totalElapsedTime = existingElapsedTime + Double.Parse(outputValue1.ToString());
//            return totalElapsedTime;
//        }
//        Entity FetchRequestRecordx(string entityName, string regardingId) {
//        Entity caseRecord =new Entity();
//            return caseRecord;
//        }
//        Entity FetchCalendar(string entityName, string regardingId) {

//            Entity caseRecord = new Entity();
//            return caseRecord;
//        }
//        Entity FetchRequestRecord(string entityName, string regardingId)
//        {
//            var query = new QueryExpression();

//            if (entityName == "task")
//            {
//                query = ExecuteRequestQuery(entityName, regardingId, "activityid");

//            }
//            else if (entityName == "incident")
//            {
//                query = ExecuteRequestQuery(entityName, regardingId, "incidentid");

//            }
//            var result = service.RetrieveMultiple(query)[0];

//            return result;
//        }
//        QueryExpression ExecuteRequestQuery(string entityName, string regardingId, string regardingIdName)
//        {
//            var ag_regardingId = regardingId;

//            var query = new QueryExpression("ldv_casecategory");
//            query.Distinct = true; query.ColumnSet.AddColumns(
//                    "ldv_casecategoryid",
//                    "ldv_slahourlevel3code",
//                    "ldv_slahourlevel2code",
//                    "ldv_slahourlevel1code");
//            query.AddOrder("ldv_slahourlevel1code", OrderType.Ascending);

//            var ag = query.AddLink(entityName, "ldv_casecategoryid", "ldv_subcategoryid");
//            ag.EntityAlias = "ag";

//            ag.LinkCriteria.AddCondition(regardingIdName, ConditionOperator.Equal, ag_regardingId);
//            return query;

//        }
//    }
//}
