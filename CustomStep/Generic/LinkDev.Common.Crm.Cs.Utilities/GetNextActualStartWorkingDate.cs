using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Cs.Utilities.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{
    public class GetNextActualStartWorkingDate : CustomStepBase
    {
        [Input("SLA Entity")]
        [RequiredArgument]
        [AttributeTarget("sla", "objecttypecode")]
        public InArgument<OptionSetValue> SLAEntity { get; set; }

        [Output("Next Actual Start Working Date")]
        public OutArgument<DateTime> NextActualStartWorkingDate { get; set; }



        public override void ExtendedExecute()
        {
            int SLAEntityValue = SLAEntity.Get(ExecutionContext).Value;
            GetDifferenceBetweenTwoDatesWithWorkingHoursBLL getDifferenceBetweenTwoDatesWithWorkingHoursBLL = new GetDifferenceBetweenTwoDatesWithWorkingHoursBLL(OrganizationService);
            Entity CustomerServiceScheduale = getDifferenceBetweenTwoDatesWithWorkingHoursBLL.GetCustomerServiceSchedule(SLAEntityValue);
            List<TimeSpan> StartandEndWorkingHours = getDifferenceBetweenTwoDatesWithWorkingHoursBLL.GetStartandEndWorkingHoursFromCustomerServiceSchedual(CustomerServiceScheduale);
            DateTime startTime = DateTime.Now.Date.AddDays(-1) + StartandEndWorkingHours[0];
            DateTime endTime = DateTime.Now.Date.AddDays(-1) + StartandEndWorkingHours[1];
            List<DayOfWeek> DaysOff = new List<DayOfWeek>
            {
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };
            if (((EntityCollection)CustomerServiceScheduale["calendarrules"]).Entities.Count > 0 && ((EntityCollection)CustomerServiceScheduale["calendarrules"])[0].Contains("pattern"))
            {
                List<string> Pattern = ((EntityCollection)CustomerServiceScheduale["calendarrules"])[0]["pattern"].ToString().Split(';').ToList();
                if (Pattern[2].Contains("BYDAY"))
                {
                    DaysOff.Clear();
                    if (!Pattern[2].Contains("SU"))
                    {
                        DaysOff.Add(DayOfWeek.Sunday);
                    }
                    if (!Pattern[2].Contains("MO"))
                    {
                        DaysOff.Add(DayOfWeek.Monday);
                    }
                    if (!Pattern[2].Contains("TU"))
                    {
                        DaysOff.Add(DayOfWeek.Tuesday);
                    }
                    if (!Pattern[2].Contains("WE"))
                    {
                        DaysOff.Add(DayOfWeek.Wednesday);
                    }
                    if (!Pattern[2].Contains("TH"))
                    {
                        DaysOff.Add(DayOfWeek.Thursday);
                    }
                    if (!Pattern[2].Contains("FR"))
                    {
                        DaysOff.Add(DayOfWeek.Friday);
                    }
                    if (!Pattern[2].Contains("SA"))
                    {
                        DaysOff.Add(DayOfWeek.Saturday);
                    }
                }
            }
            List<DateTime> Holiday = getDifferenceBetweenTwoDatesWithWorkingHoursBLL.GetListOfDaysFromCustomerServiceSchedule(CustomerServiceScheduale);
            DateTime nextActualStartWorkingDate = default;
            if (DateTime.Now >= startTime && DateTime.Now <= endTime)
            {
                nextActualStartWorkingDate = GetsuitableStartDateworkingdate(DateTime.Now.Date, DaysOff, Holiday);
                if (nextActualStartWorkingDate.Date == DateTime.Now.Date)
                {
                    nextActualStartWorkingDate = DateTime.Now;
                }
                else
                {
                    nextActualStartWorkingDate = nextActualStartWorkingDate.AddDays(-1) + StartandEndWorkingHours[0];
                }
            }
            else if (DateTime.Now > endTime)
            {
                nextActualStartWorkingDate = GetsuitableStartDateworkingdate(DateTime.Now.Date.AddDays(1), DaysOff, Holiday);
                nextActualStartWorkingDate = nextActualStartWorkingDate.AddDays(-1) + StartandEndWorkingHours[0];
            }
            else if (DateTime.Now < startTime)
            {
                nextActualStartWorkingDate = GetsuitableStartDateworkingdate(DateTime.Now.Date, DaysOff, Holiday);
                nextActualStartWorkingDate = nextActualStartWorkingDate.AddDays(-1) + StartandEndWorkingHours[0];
            }
            NextActualStartWorkingDate.Set(ExecutionContext, nextActualStartWorkingDate);
        }

        DateTime GetsuitableStartDateworkingdate(DateTime SearchDate, List<DayOfWeek> DaysOff, List<DateTime> Holiday)
        {
            DateTime nextActualStartWorkingDate = default;

            for (int i = 0; i < 30; i++)
            {
                if (!Holiday.Contains(SearchDate.Date.AddDays(i)))
                {
                    if (!DaysOff.Contains(SearchDate.Date.AddDays(i).DayOfWeek))
                    {
                        nextActualStartWorkingDate = SearchDate.Date.AddDays(i);
                        break;
                    }
                }
            }
            if (nextActualStartWorkingDate == default)
            {
                throw new InvalidPluginExecutionException("Error while finding a suitable start date");
            }
            return nextActualStartWorkingDate;
        }
    }
}
