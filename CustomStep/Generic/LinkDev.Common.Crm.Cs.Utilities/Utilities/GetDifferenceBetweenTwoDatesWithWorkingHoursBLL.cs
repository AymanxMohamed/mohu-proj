using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities.Utilities
{
    public class GetDifferenceBetweenTwoDatesWithWorkingHoursBLL
    {
        IOrganizationService Service;
        public GetDifferenceBetweenTwoDatesWithWorkingHoursBLL(IOrganizationService service)
        {
            this.Service = service;
        }

        public double GetDifferendeBetweenTwoDatesInWorkingHours(DateTime StartDate, DateTime EndDate, int EntityTypeCode, out double WorkingHoursPerDay)
        {

            double TotalNumberOfHours = 0;
            WorkingHoursPerDay = 0;
            Entity CustomerServiceScheduale = GetCustomerServiceSchedule(EntityTypeCode);
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
            List<TimeSpan> StartAndEndTimes = GetStartandEndWorkingHoursFromCustomerServiceSchedual(CustomerServiceScheduale);
            List<DateTime> VacationDays = GetListOfDaysFromCustomerServiceSchedule(CustomerServiceScheduale);
            List<DateTime> WorkingDaysInThePeriod = GetWorkingDays(StartDate, EndDate, VacationDays, DaysOff);
            if (StartAndEndTimes.Count > 0)
            {
                TimeSpan StartWorkingTime = StartAndEndTimes[0];
                TimeSpan EndWorkingTime = StartAndEndTimes[1];
                TotalNumberOfHours = GetWorkingHoursInPeriod(WorkingDaysInThePeriod, StartWorkingTime, EndWorkingTime, StartDate, EndDate, out WorkingHoursPerDay);
            }
            return TotalNumberOfHours;


        }

        #region Functions To calculate the Hours

        public List<TimeSpan> GetStartandEndWorkingHoursFromCustomerServiceSchedual(Entity CustomerServiceScheduale)
        {
            List<TimeSpan> StartandEndWorkingHoursFromCustomerServiceSchedual = new List<TimeSpan>();
            List<Entity> CalendarRulesFromCustomerServiceScheduale = GetCalendarRulesFromCalendar(CustomerServiceScheduale);
            if (CalendarRulesFromCustomerServiceScheduale.Count > 0)
            {
                StartandEndWorkingHoursFromCustomerServiceSchedual = GetStartandEndWorkingHoursFromInnerCalendarInCalendarRule(CalendarRulesFromCustomerServiceScheduale[0]);
            }
            return StartandEndWorkingHoursFromCustomerServiceSchedual;
        }
        List<Entity> GetCalendarRulesFromCalendar(Entity Calendar)
        {
            List<Entity> CalendarRulesList = new List<Entity>();
            if (Calendar != null && Calendar.Attributes.Contains("calendarrules") && Calendar.Attributes["calendarrules"] != null)
            {
                EntityCollection CalendarRules = ((EntityCollection)Calendar.Attributes["calendarrules"]);
                if (CalendarRules.Entities.Count > 0)
                {
                    CalendarRulesList = CalendarRules.Entities.ToList();

                }
            }
            return CalendarRulesList;
        }

        List<TimeSpan> GetStartandEndWorkingHoursFromInnerCalendarInCalendarRule(Entity CalendarRule)
        {
            List<TimeSpan> StartandEndWorkingHoursFromInnerCalendarInCalendarRule = new List<TimeSpan>();
            if (CalendarRule != null && CalendarRule.Attributes.Contains("innercalendarid") && CalendarRule.Attributes["innercalendarid"] != null)
            {
                Guid InnerCalendarID = ((EntityReference)CalendarRule.Attributes["innercalendarid"]).Id;
                Entity InnerCalendar = GetCalendarByID(InnerCalendarID);
                if (InnerCalendar != null)
                {
                    StartandEndWorkingHoursFromInnerCalendarInCalendarRule = GetStartandEndWorkingHoursFromInnerCalendar(InnerCalendar);
                }
            }
            return StartandEndWorkingHoursFromInnerCalendarInCalendarRule;
        }
        List<TimeSpan> GetStartandEndWorkingHoursFromInnerCalendar(Entity InnerCallendar)
        {
            List<TimeSpan> StartandEndWorkingHoursFromInnerCalendar = new List<TimeSpan>();
            List<Entity> CalendarRulesList = GetCalendarRulesFromCalendar(InnerCallendar);
            if (CalendarRulesList != null && CalendarRulesList.Count > 0)
                StartandEndWorkingHoursFromInnerCalendar = GetStartandEndWorkingHoursFromCalendarRulesList(CalendarRulesList);

            return StartandEndWorkingHoursFromInnerCalendar;
        }
        List<TimeSpan> GetStartandEndWorkingHoursFromCalendarRulesList(List<Entity> CalendarRules)
        {
            List<TimeSpan> StartandEndWorkingHoursFromCalendarRulesList = new List<TimeSpan>();
            if (CalendarRules != null & CalendarRules.Count > 0)
            {
                Entity CalendarRule = CalendarRules[0];
                StartandEndWorkingHoursFromCalendarRulesList = GetStartandEndWorkingHoursFromCalendarRule(CalendarRule);
            }
            return StartandEndWorkingHoursFromCalendarRulesList;
        }
        List<TimeSpan> GetStartandEndWorkingHoursFromCalendarRule(Entity CalendarRule)
        {
            List<TimeSpan> StartandEndWorkingHoursFromCalendarRule = new List<TimeSpan>();
            if (CalendarRule != null && CalendarRule.Attributes.Contains("duration") && CalendarRule.Attributes["duration"] != null && CalendarRule.Attributes.Contains("offset") && CalendarRule.Attributes["offset"] != null)
            {
                int Duration = int.Parse(CalendarRule.Attributes["duration"].ToString());
                int Offset = int.Parse(CalendarRule.Attributes["offset"].ToString());
                StartandEndWorkingHoursFromCalendarRule = CalculateStartandEndWorkingHours(Offset, Duration);
            }
            return StartandEndWorkingHoursFromCalendarRule;
        }

        List<TimeSpan> CalculateStartandEndWorkingHours(int Offset, int Duration)
        {
            int OffsetInHours = Offset / 60;
            TimeSpan StartTime = new TimeSpan(24, 0, 0).Add(new TimeSpan(OffsetInHours, 0, 0));
            double allDuration = Convert.ToDouble(Duration) / 60;
            int DurationInHours = Convert.ToInt32(allDuration);
            TimeSpan EndTime;
            if (DurationInHours == 24)
            {
                EndTime = StartTime.Add(new TimeSpan(DurationInHours - 1, 59, 0));
            }
            else
            {
                EndTime = StartTime.Add(new TimeSpan(DurationInHours, Convert.ToInt32((allDuration - Convert.ToDouble(DurationInHours)) * 60), 0));
            }
            List<TimeSpan> StartAndEnd = new List<TimeSpan>();
            StartAndEnd.Add(StartTime);
            StartAndEnd.Add(EndTime);
            return StartAndEnd;
        }
        double GetActualDiffernceBetweenTwoTimeSpansInHours(TimeSpan TS1, TimeSpan TS2)
        {
            return (((TS1.Hours * 60) + TS1.Minutes) - ((TS2.Hours * 60) + TS2.Minutes)) / 60.0;
        }
        double GetWorkingHoursInPeriod(List<DateTime> WorkingDaysPeriod, TimeSpan WorkingDayStartHours, TimeSpan WorkingDayEndHours, DateTime StartDate, DateTime EndDate, out double WorkingHoursPerDay)
        {
            double TotalNumberofHours = 0;
            //throw new InvalidPluginExecutionException("WorkingDaysPeriod.Count=" + WorkingDaysPeriod.Count.ToString());
            WorkingHoursPerDay = (WorkingDayEndHours - WorkingDayStartHours).TotalHours;
            if (WorkingDaysPeriod.Count != 0 && WorkingDaysPeriod.Count > 0)
            {
                //bool Edited = false;
                if (GetActualDiffernceBetweenTwoTimeSpansInHours(StartDate.TimeOfDay, WorkingDayEndHours) > 0)
                {
                    if (WorkingDaysPeriod.Count == 1)
                        WorkingDaysPeriod.Add(WorkingDaysPeriod[0].AddDays(1));
                    DateTime StartDateTemp = WorkingDaysPeriod[1];
                    StartDateTemp = new DateTime(StartDateTemp.Year, StartDateTemp.Month, StartDateTemp.Day, WorkingDayStartHours.Hours, WorkingDayStartHours.Minutes, WorkingDayStartHours.Seconds);
                    StartDate = StartDateTemp;
                    WorkingDaysPeriod.RemoveAt(0);

                }
                if (WorkingDaysPeriod.Count == 1)
                {
                    if (EndDate.Date < StartDate.Date)
                    {
                        TotalNumberofHours = 0;
                    }
                    else
                    {
                        TotalNumberofHours = GetActualDiffernceBetweenTwoTimeSpansInHours(EndDate.TimeOfDay, StartDate.TimeOfDay);//hours
                    }
                    //throw new InvalidPluginExecutionException(WorkingDaysPeriod[0].ToString() + " && " + EndDate.TimeOfDay + " && " + StartDate.TimeOfDay);
                }
                else
                {

                    //if(!Edited)
                    WorkingDaysPeriod[0] = new DateTime(WorkingDaysPeriod[0].Year, WorkingDaysPeriod[0].Month, WorkingDaysPeriod[0].Day, StartDate.TimeOfDay.Hours
                       , StartDate.TimeOfDay.Minutes, StartDate.TimeOfDay.Seconds);
                    WorkingDaysPeriod[WorkingDaysPeriod.Count - 1] = new DateTime(WorkingDaysPeriod[WorkingDaysPeriod.Count - 1].Year, WorkingDaysPeriod[WorkingDaysPeriod.Count - 1].Month, WorkingDaysPeriod[WorkingDaysPeriod.Count - 1].Day, EndDate.TimeOfDay.Hours
                        , EndDate.TimeOfDay.Minutes, EndDate.TimeOfDay.Seconds);
                    double NumberOfHourInFirstDay = GetActualDiffernceBetweenTwoTimeSpansInHours(WorkingDayEndHours, WorkingDaysPeriod[0].TimeOfDay);
                    WorkingDaysPeriod.RemoveAt(0);
                    double NumberOfHourInLastDay = GetActualDiffernceBetweenTwoTimeSpansInHours(WorkingDaysPeriod[WorkingDaysPeriod.Count - 1].TimeOfDay, WorkingDayStartHours);
                    WorkingDaysPeriod.RemoveAt(WorkingDaysPeriod.Count - 1);
                    double NumberOfHoursInPeriodBetweenFirstAndLast = 0;
                    if (WorkingDaysPeriod.Count > 0)
                    {
                        for (int i = 0; i < WorkingDaysPeriod.Count; i++)
                        {
                            NumberOfHoursInPeriodBetweenFirstAndLast += WorkingHoursPerDay;
                        }
                    }

                    TotalNumberofHours = NumberOfHoursInPeriodBetweenFirstAndLast + NumberOfHourInFirstDay + NumberOfHourInLastDay;
                    //throw new InvalidPluginExecutionException(NumberOfHoursInPeriodBetweenFirstAndLast.ToString() + "&&" + NumberOfHourInFirstDay.ToString() + " &&" + NumberOfHourInLastDay.ToString() + " &&" + TotalNumberofHours.ToString());



                }
            }
            return TotalNumberofHours;
        }


        #endregion

        #region Fucntions To Calculate VacationsDays

        Entity GetServiceSchedulefromSLA(int EntityTypeCode)
        {
            Entity CustomerServiceSchedule = null;
            QueryExpression QE = new QueryExpression("sla");
            QE.Criteria.Conditions.Add(new ConditionExpression("objecttypecode", ConditionOperator.Equal, EntityTypeCode));
            QE.Criteria.Conditions.Add(new ConditionExpression("statecode", ConditionOperator.Equal, 1));
            QE.ColumnSet = new ColumnSet(new string[] { "businesshoursid" });
            EntityCollection EntitySLAs = Service.RetrieveMultiple(QE);
            if (EntitySLAs.Entities.Count > 0)
            {
                Entity SLA = EntitySLAs[0];
                if (SLA != null && SLA.Attributes.Contains("businesshoursid") && SLA.Attributes["businesshoursid"] != null)
                {
                    Guid BusinessHoursID = ((EntityReference)SLA.Attributes["businesshoursid"]).Id;
                    Entity TempCustomerServiceSchedule = this.GetCalendarByID(BusinessHoursID);
                    if (TempCustomerServiceSchedule != null)
                    {
                        CustomerServiceSchedule = TempCustomerServiceSchedule;

                    }
                }
            }
            return CustomerServiceSchedule;

        }
        public Entity GetCustomerServiceSchedule(int EntityTypeCode)
        {
            Entity CustomerServiceSchedule = null;
            Entity TempCustomerServiceSchedule = GetServiceSchedulefromSLA(EntityTypeCode);
            if (TempCustomerServiceSchedule != null)
            {
                CustomerServiceSchedule = TempCustomerServiceSchedule;
            }
            else
            {
                List<Entity> CalendarDetails = GetCalenderDetail();
                if (CalendarDetails != null && CalendarDetails.Count > 0)
                {
                    CustomerServiceSchedule = CalendarDetails[0];
                }
            }
            return CustomerServiceSchedule;
        }

        public List<DateTime> GetListOfDaysFromCustomerServiceSchedule(Entity CustomerServiceScheduale)
        {
            //Entity CustomerServiceScheduale = GetCustomerServiceSchedule(service);
            List<DateTime> DaysInHoliday = new List<DateTime>();

            if (CustomerServiceScheduale != null)
            {
                Entity Holiday = GetHolidaysFromCustomerServiceSchedule(CustomerServiceScheduale);
                DaysInHoliday = GetListOfDaysFromHoliday(Holiday);

            }
            return DaysInHoliday;
        }
        Entity GetCalendarByID(Guid ID)
        {

            Entity Holiday = Service.Retrieve("calendar", ID, new ColumnSet(true));
            if (Holiday != null)
                return Holiday;
            else
                return null;

        }
        Entity GetHolidaysFromCustomerServiceSchedule(Entity CustomerServiceScheduale)
        {
            Entity Holiday = null;
            if (CustomerServiceScheduale != null && CustomerServiceScheduale.Attributes.Contains("holidayschedulecalendarid") && CustomerServiceScheduale.Attributes["holidayschedulecalendarid"] != null)
            {
                Guid CalendarID = ((EntityReference)CustomerServiceScheduale.Attributes["holidayschedulecalendarid"]).Id;
                Holiday = GetCalendarByID(CalendarID);
            }
            return Holiday;
        }
        List<DateTime> GetListOfDaysFromHoliday(Entity Holiday)
        {
            List<DateTime> ListOfDaysInHoliday = new List<DateTime>();

            if (Holiday != null && Holiday.GetAttributeValue<EntityCollection>("calendarrules").Entities != null)
            {
                List<Entity> Calendarrules = Holiday.GetAttributeValue<EntityCollection>("calendarrules").Entities.ToList();
                if (Calendarrules != null && Calendarrules.Count > 0)
                {
                    for (int i = 0; i < Calendarrules.Count; i++)
                    {
                        List<DateTime> DaysInCalendarRule = GetListOfDaysFromCalendarRule(Calendarrules[i]);
                        ListOfDaysInHoliday.AddRange(DaysInCalendarRule);
                    }
                }
            }
            return ListOfDaysInHoliday;
        }
        List<DateTime> GetListOfDaysFromCalendarRule(Entity CalendarRule)
        {
            List<DateTime> DaysInCalendarRule = new List<DateTime>();
            if (CalendarRule != null && CalendarRule.Attributes.Contains("effectiveintervalstart") && CalendarRule.Attributes["effectiveintervalstart"] != null && CalendarRule.Attributes.Contains("effectiveintervalend") && CalendarRule.Attributes["effectiveintervalend"] != null)
            {
                DateTime EffectiveIntervalStart = DateTime.Parse(CalendarRule.Attributes["effectiveintervalstart"].ToString());
                DateTime EffectiveIntervalEnd = DateTime.Parse(CalendarRule.Attributes["effectiveintervalend"].ToString()).AddDays(-1);
                DaysInCalendarRule = ConvertPeriodToDaysList(EffectiveIntervalStart, EffectiveIntervalEnd);

            }
            return DaysInCalendarRule;
        }
        List<DateTime> ConvertPeriodToDaysList(DateTime PeriodStartDate, DateTime PeriodEndDate)
        {
            List<DateTime> PeriodDays = new List<DateTime>();
            for (DateTime i = PeriodStartDate; i <= PeriodEndDate; i = i.AddDays(1))
            {
                PeriodDays.Add(i);
            }
            return PeriodDays;
        }
        List<Entity> GetCalenderDetail()
        {
            QueryExpression cal = new QueryExpression("calendar") { ColumnSet = { AllColumns = true } };
            cal.Criteria.AddCondition("type", ConditionOperator.Equal, 1);
            EntityCollection Holiday = Service.RetrieveMultiple(cal);
            if (Holiday.Entities.Count > 0)
            {
                return Holiday.Entities.ToList();

            }
            else return null;
        }
        List<DateTime> GetWorkingDays(DateTime StartDate, DateTime EndDate, List<DateTime> VacationDays, List<DayOfWeek> DaysOff)
        {
            DateTime CurrentDateinLoop = new DateTime();
            CurrentDateinLoop = StartDate.Date;
            List<DateTime> CurrentWorkingDays = new List<DateTime>();

            for (DateTime i = CurrentDateinLoop; i <= EndDate.Date; i = i.AddDays(1))
            {
                if (!DaysOff.Contains(i.DayOfWeek))
                {
                    bool Exist = false;
                    for (int J = 0; J < VacationDays.Count; J++)
                    {
                        if (VacationDays[J].Date == i)
                        {
                            Exist = true;
                            //VacationDays.RemoveAt(J);
                            break;
                        }
                    }
                    if (!Exist)
                    {
                        CurrentWorkingDays.Add(i);
                    }

                }
            }
            return CurrentWorkingDays;
        }


        #endregion
    }
}
