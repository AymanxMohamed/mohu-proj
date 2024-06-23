using Linkdev.Maan.Core.Helper;
using LinkDev.MAAN.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace LinDev.MOHU.Utilites.Logic
{
    public class CustomPluginTimeCalculationLogic : StepLogic<LinDev.MOHU.Utilites.GetEntityReferencePrimitives>
    {
        protected override void ExecuteLogic()
        {
            //IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            //IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            //_service = factory.CreateOrganizationService(context.UserId);

            // Step 1: Retrieving Input Parameters.
            string regardingId = context.InputParameters["regardingId"] as string;
            string calendarId = context.InputParameters["calendarId"] as string;
            string requestType = context.InputParameters["requestType"] as string;
            string slaItemId = context.InputParameters["slaItemId"] as string;
            string entityName = context.InputParameters["entityName"] as string;
            try
            {
                // implement this requestType for any new SLA KPi instance creation.
                if (requestType.Equals("getEndTime", StringComparison.CurrentCultureIgnoreCase))
                {
                    int warningDuration = (int)context.InputParameters["firstInputDuration"];
                    int failureDuration = (int)context.InputParameters["secondInputDuration"];
                    DateTime warningStartTime = (DateTime)context.InputParameters["firstInputDate"];
                    DateTime failureStartTime = (DateTime)context.InputParameters["secondInputDate"];

                    // Step 2 : Add the custom Logic to calculate the WarningTime and FailureTime
                    string returnCalendarId = CalculateWarningAndFailureTime(regardingId, calendarId, slaItemId, entityName, warningStartTime, failureStartTime, warningDuration, failureDuration, out DateTime warningTime, out DateTime failureTime);

                    // Step 3 : return the output values.
                    context.OutputParameters["firstOutputValue"] = DateTime.SpecifyKind(warningTime, DateTimeKind.Utc).ToString();
                    context.OutputParameters["secondOutputValue"] = DateTime.SpecifyKind(failureTime, DateTimeKind.Utc).ToString();
                    context.OutputParameters["returnCalendarId"] = returnCalendarId;
                    return;
                }

                // implement this requestType for finding Paused time for any new SLA KPi instance after it resumed.
                if (requestType.Equals("getElapsedTime", StringComparison.CurrentCultureIgnoreCase))
                {
                    DateTime casePausedTime = (DateTime)context.InputParameters["firstInputDate"];
                    DateTime caseResumedTime = (DateTime)context.InputParameters["secondInputDate"];
                    int existingElapsedTime = (int)context.InputParameters["firstInputDuration"];
                    // Step 2 : Add the custom Logic to calculate the elapsedTime between startTime(paused) and endTime(resumed)
                    double elapsedTimeInMinutes = CalculateElapsedTime(regardingId, calendarId, slaItemId, entityName, casePausedTime, caseResumedTime, existingElapsedTime);

                    // Step 3 : return the output values.
                    context.OutputParameters["firstOutputValue"] = elapsedTimeInMinutes.ToString();
                    context.OutputParameters["secondOutputValue"] = elapsedTimeInMinutes.ToString();
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
           

        }

        // in this example, we're using Custom Field(new_country) on the Case entity to apply the required calendar.

        private string CalculateWarningAndFailureTime(string regardingId, string calendarId, string slaItemId, string entityName, DateTime warningStartTime, DateTime failureStartTime, int warningDuration, int failureDuration, out DateTime warningTime, out DateTime failureTime)
        {
            OrganizationResponse customizedTimeCalculationResponse;
            warningTime = DateTime.UtcNow;
            failureTime = DateTime.UtcNow;
            int newWarningTime = warningDuration;
            int newFailureTime = failureDuration;
            int customCode = 0;

            // Step 1: fetch the Case Entity record	
            Entity caseRecord = FetchCaseRecord(entityName, regardingId);

            if (caseRecord.Attributes.Contains("new_country"))
            {
                customCode = (int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value);

                // Example 1: Override calendar at runtime: Choose Calendar based on any custom logic
                if (customCode == 0)
                {
                    // fetch IST calendar & override CalendarId
                    IST_CALENDAR = FetchCalendar("IST_CALENDAR", _service);
                    calendarId = IST_CALENDAR;
                }
                else if (customCode == 1)
                {
                    // fetch PST calendar & override CalendarId
                    PST_CALENDAR = FetchCalendar("PST_CALENDAR", _service);
                    calendarId = PST_CALENDAR;
                }
            }
            // use OOB SLATimeCalculation Custom Action to do actual calculation_
            OrganizationRequest requestTimeCalculation = new OrganizationRequest("msdyn_SLATimeCalculation");

            requestTimeCalculation["requestType"] = "getEndTime";
            requestTimeCalculation["calendarId"] = calendarId;

            // calculate warning time
            if (warningDuration != -1)
            {
                requestTimeCalculation["startTime"] = warningStartTime;
                requestTimeCalculation["minutes"] = newWarningTime;
                customizedTimeCalculationResponse = _service.Execute(requestTimeCalculation);
                customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue1);
                warningTime = DateTime.SpecifyKind(DateTime.Parse((string)outputValue1), DateTimeKind.Utc);
            }

            // calculate Failure time
            if (failureDuration != -1)
            {
                requestTimeCalculation["startTime"] = failureStartTime;
                requestTimeCalculation["minutes"] = newFailureTime;
                customizedTimeCalculationResponse = _service.Execute(requestTimeCalculation);
                customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue2);
                failureTime = DateTime.SpecifyKind(DateTime.Parse((string)outputValue2), DateTimeKind.Utc);
            }

            return calendarId;
        }

        private double CalculateElapsedTime(string regardingId, string calendarId, string slaItemId, string entityName, DateTime casePausedTime, DateTime caseResumedTime, int existingElapsedTime)
        {
            if (caseRecord.Attributes.Contains("new_country"))
            {
                if ((int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value) == 0)
                {
                    // fetch IST id
                    IST_CALENDAR = FetchCalendar("IST_CALENDAR", _service);
                    calendarId = IST_CALENDAR;
                }
                else if ((int)(((OptionSetValue)(caseRecord.Attributes["new_country"])).Value) == 1)
                {
                    // fetch PST  id
                    PST_CALENDAR = FetchCalendar("PST_CALENDAR", _service);
                    calendarId = PST_CALENDAR;
                }
            }

            // use OOB SLATimeCalculation Custom Action to do actual calculation_
            OrganizationRequest requestTimeCalculation = new OrganizationRequest("msdyn_SLATimeCalculation");
            requestTimeCalculation["startTime"] = casePausedTime;
            requestTimeCalculation["endTime"] = caseResumedTime;
            requestTimeCalculation["requestType"] = "getElapsedTime";
            requestTimeCalculation["calendarId"] = calendarId;

            // calculate elapsed time
            customizedTimeCalculationResponse = _service.Execute(requestTimeCalculation);
            customizedTimeCalculationResponse.Results.TryGetValue("returnValue", out object outputValue1);
            double totalElapsedTime = existingElapsedTime + Double.Parse(outputValue1.ToString());
            return totalElapsedTime;
        }

    }
}
