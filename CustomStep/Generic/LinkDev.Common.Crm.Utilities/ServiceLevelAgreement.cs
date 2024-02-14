using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace LinkDev.Common.Crm.Utilities
{
    public enum RuleType
    {
        Unknown = 0,
        WorkingHours = 1,
        BreakTime = 4,
        Holiday = 5
    }

    public class ServiceLevelAgreement
    {
        const string BYDAYPattern = "BYDAY=";
        const string FREQPattern = "FREQ=";
        const string INTERVALPattern = "INTERVAL=";

        IOrganizationService OrganizationService { get; set; }
        Entity BusinessClosureCalender;
        Entity HolidayCalender;
        Dictionary<DayOfWeek, CrmCalenderDay> DaysOfWeek;

        public ServiceLevelAgreement(IOrganizationService organizationService, EntityReference businessClosureCalender)
            : this(organizationService, organizationService.Retrieve(businessClosureCalender.LogicalName, businessClosureCalender.Id, new ColumnSet(true)))
        {
        }

        public ServiceLevelAgreement(IOrganizationService organizationService, Entity businessClosureCalender)
        {
            DaysOfWeek = new Dictionary<DayOfWeek, CrmCalenderDay>();
            OrganizationService = organizationService;
            BusinessClosureCalender = businessClosureCalender;

            Initialize();
        }

        void Initialize()
        {
            // Check for defined holidays
            if (BusinessClosureCalender.Contains(CalendarMetadata.HolidayScheduleCalendarId))
                HolidayCalender = OrganizationService.Retrieve(CalendarMetadata.LogicalName, ((EntityReference)BusinessClosureCalender[CalendarMetadata.HolidayScheduleCalendarId]).Id, new ColumnSet(true));

            if (BusinessClosureCalender.Contains(CalendarMetadata.CalendarRules))
            {
                var businessClosureRules = (EntityCollection)BusinessClosureCalender.Attributes[CalendarMetadata.CalendarRules];
                foreach (Entity rule in businessClosureRules.Entities)
                {
                    if (rule.Contains(CalendarMetadata.Pattern))
                    {
                        // Example on how it's saved in CRM 
                        // in the case is the same each day option is chosen: FREQ=WEEKLY;INTERVAL=1;BYDAY=SU,MO,TU,WE,TH
                        var pattern = rule[CalendarMetadata.Pattern].ToString();
                        var days = pattern.Substring(pattern.IndexOf(BYDAYPattern) + BYDAYPattern.Length).Split(',');

                        foreach (var day in days)
                        {
                            if (DaysOfWeek.ContainsKey(day.ConvertToDayOfWeek()))
                                throw new Exception("Duplicated days");

                            var tmpRules = new List<CrmCalenderDayRule>();

                            if (rule.Contains(CalendarRuleMetadata.InnerCalendarId))
                            {
                                var internalCalendarRef = rule[CalendarRuleMetadata.InnerCalendarId] as EntityReference;
                                var internalCalendar = OrganizationService.Retrieve(CalendarMetadata.LogicalName, internalCalendarRef.Id, new ColumnSet(true));

                                if (internalCalendar.Contains(CalendarMetadata.CalendarRules))
                                {
                                    var rulesCollection = internalCalendar[CalendarMetadata.CalendarRules] as EntityCollection;
                                    tmpRules = new List<CrmCalenderDayRule>(rulesCollection.Entities.Count);

                                    foreach (var tmpRulesItem in rulesCollection.Entities)
                                    {
                                        tmpRules.Add(new CrmCalenderDayRule()
                                        {
                                            Offset = tmpRulesItem.Contains(CalendarRuleMetadata.Offset) ? (int?)tmpRulesItem[CalendarRuleMetadata.Offset] : null,
                                            Duration = tmpRulesItem.Contains(CalendarRuleMetadata.Duration) ? (int?)tmpRulesItem[CalendarRuleMetadata.Duration] : null,
                                            Subcode = tmpRulesItem.Contains(CalendarRuleMetadata.Subcode) ? (int?)tmpRulesItem[CalendarRuleMetadata.Subcode] : null,
                                        });
                                    }
                                }
                            }

                            DaysOfWeek.Add(day.ConvertToDayOfWeek(), new CrmCalenderDay(day.ConvertToDayOfWeek(), tmpRules));
                        }
                    }
                }
            }

        }

        public CrmBusinessWorkingDurationResult CalculateActualWorkingDuration(DateTime startDate, DateTime endDate)
        {
            var CrmBusinessWorkingDurationResult = new CrmBusinessWorkingDurationResult();

            // Remove seconds
            var date1 = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
            var date2 = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0);

            // Iterate from start date till the end date
            for (var i = 0; new DateTime( date1.Year,date1.Month,date1.Day) <= new DateTime(date2.Year, date2.Month, date2.Day); date1 = date1.AddDays(1),i++)
            {
                // If not a business day, then continue
                if (!date1.IsBusinessDay(DaysOfWeek, HolidayCalender))
                    continue;

                var calenderDay = DaysOfWeek[date1.DayOfWeek];
                var startingOffset = calenderDay.StartingWorkingOffset.Value;
                var endingOffset = calenderDay.EndingWorkingOffset.Value;

                // first time
                if (i == 0)
                     startingOffset = date1.ToOffset();

                // reached to end date
                if (date1.IsSameDay(date2))
                    endingOffset = date2.ToOffset();

                var tmpWorkingHours = calenderDay.CalculateWorkingDuration(startingOffset, endingOffset);

                if (tmpWorkingHours > 0)
                {
                    CrmBusinessWorkingDurationResult.WorkingDurationInMinutes += tmpWorkingHours;
                    CrmBusinessWorkingDurationResult.WorkingDurationInDays += 1;
                }
            }
            return CrmBusinessWorkingDurationResult;
        }

    }

    public static class ServiceLevelAgreementExtensions
    {
        private const int MinutesInDay = 1440;


        /// <summary>
        /// Extension method to check if a point in time falls between 2 others (inclusive)
        /// </summary>
        /// <param name="input">Time to check</param>
        /// <param name="date1">Start of the time window</param>
        /// <param name="date2">End of the time window</param>
        /// <returns>True if the input is in the range or on the bounds</returns>
        public static bool IsBetween(this DateTime input, DateTime date1, DateTime date2)
        {
            return input >= date1 && input <= date2;
        }

        /// <summary>
        /// Method to check if a day is a business day according to customer service schedule
        /// </summary>
        /// <param name="dateToCheck"></param>
        /// <param name="daysOfWeek"></param>
        /// <param name="HolidayCalender"></param>
        /// <returns></returns>
        public static bool IsBusinessDay(this DateTime dateToCheck, Dictionary<DayOfWeek, CrmCalenderDay> daysOfWeek, Entity HolidayCalender)
        {
            var isWorkingDay = daysOfWeek.Values.Where(x => x.DayOfWeek == dateToCheck.DayOfWeek).FirstOrDefault();
            if (isWorkingDay == null)
                return false;

            if (HolidayCalender != null && HolidayCalender.Contains(CalendarMetadata.CalendarRules))
            {
                var calenderRules = HolidayCalender[CalendarMetadata.CalendarRules] as EntityCollection;

                var calendarRules = HolidayCalender.GetAttributeValue<EntityCollection>(CalendarMetadata.CalendarRules);

                foreach (var calendarRule in calendarRules.Entities)
                {
                    // Date is not stored as UTC
                    var startTime = calendarRule.GetAttributeValue<DateTime>(CalendarRuleMetadata.starttime);
                    var duration = calendarRule.GetAttributeValue<int>(CalendarRuleMetadata.Duration);
                    var endTime = startTime.AddMinutes(duration);

                    if (!IsBetween(dateToCheck, startTime, endTime))
                        continue;

                    // Date is covered by the holiday - check to see if the holiday is a full day
                    if (duration < MinutesInDay)
                    {
                        // Closure is not a full day, ignore
                        continue;
                    }

                    if (dateToCheck.Date == endTime.Date)
                    {
                        // The date to check matches the end bound which means either the calendar rules doesn't actually cover it (midnight overlap) or this ends part way through the day, so not a full closure
                        continue;
                    }

                    if (dateToCheck.Date == startTime.Date && !(startTime.Hour == 0 && startTime.Minute == 0 && startTime.Second == 0))
                    {
                        // The date to check is the start day which doesnt start at 00:00:00, therefore this is only a partial day
                        continue;
                    }

                    // Looks like a full closure at this point, this is not a business day
                    return false;
                }


            }

            return true;
        }

        public static DayOfWeek ConvertToDayOfWeek(this string dayOfWeekName)
        {
            switch (dayOfWeekName)
            {
                case "SU":
                    return System.DayOfWeek.Sunday;
                case "MO":
                    return System.DayOfWeek.Monday;
                case "TU":
                    return System.DayOfWeek.Tuesday;
                case "WE":
                    return System.DayOfWeek.Wednesday;
                case "TH":
                    return System.DayOfWeek.Thursday;
                case "FR":
                    return System.DayOfWeek.Friday;
                case "SA":
                    return System.DayOfWeek.Saturday;
                default:
                    throw new Exception("Unkown day of week");
            }
        }

        /// <summary>
        /// Check if two dates are in the same year, month and day
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>True: if two dates are in the same year, month and day</returns>
        public static bool IsSameDay(this DateTime date1, DateTime date2)
        {
            if (date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day)
                return true;
            return false;
        }

        public static int ToOffset(this DateTime date1)
        {
            return (date1.Hour * 60) + date1.Minute;
        }
    }

    public class CrmBusinessWorkingDurationResult
    {
        public int WorkingDurationInMinutes { get; set; }
        public int WorkingDurationInDays { get; set; }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var p = (CrmBusinessWorkingDurationResult)obj;
                return (WorkingDurationInMinutes.Equals(p.WorkingDurationInMinutes)) && (WorkingDurationInDays == p.WorkingDurationInDays);
            }
        }

        public override int GetHashCode()
        {
            return Tuple.Create(WorkingDurationInMinutes, WorkingDurationInDays).GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(WorkingDurationInMinutes)} = {WorkingDurationInMinutes}, {nameof(WorkingDurationInDays)} = {WorkingDurationInDays}";
        }
    }

    public class CrmCalenderDay
    {
        public DayOfWeek DayOfWeek { get; set; }
        public int TotalWorkingHours { get; private set; }
        public int? StartingWorkingOffset { get; private set; }
        public int? EndingWorkingOffset { get; private set; }
        public List<CrmCalenderDayRule> DayRules { get; set; }

        public CrmCalenderDay(DayOfWeek dayOfWeek, List<CrmCalenderDayRule> dayRules)
        {
            DayOfWeek = dayOfWeek;
            DayRules = dayRules;
            Initialize();
        }

        public CrmCalenderDay(DayOfWeek dayOfWeek)
            : this(dayOfWeek, new List<CrmCalenderDayRule>())
        {
        }

        void Initialize()
        {
            var tmpStartingWorkingOffset = 0;
            var tmpEndingWorkingOffset = 0;
            var tmpTotalWorkingHours = 0;

            var workingRule = DayRules.Where(x => x.RuleType == RuleType.WorkingHours).FirstOrDefault();

            tmpStartingWorkingOffset = workingRule.Offset.Value;
            tmpEndingWorkingOffset = workingRule.Offset.Value + workingRule.Duration.Value;
            tmpTotalWorkingHours = workingRule.Duration.Value;


            var breakTime = (from x in DayRules
                             where x.RuleType == RuleType.BreakTime
                             select x.Duration).Sum();

            if (breakTime != null)
                tmpTotalWorkingHours -= breakTime.Value;

            StartingWorkingOffset = tmpStartingWorkingOffset;
            EndingWorkingOffset = tmpEndingWorkingOffset;
            TotalWorkingHours = tmpTotalWorkingHours;
        }

        public int CalculateWorkingDuration(int startingOffset, int endingOffset)
        {
            if (endingOffset <= startingOffset)
                return 0;
            else if (startingOffset <= StartingWorkingOffset && endingOffset >= EndingWorkingOffset)
                return TotalWorkingHours;
            // starting offset is before starting working hours and ending offset is a point between the working hours
            else if (startingOffset <= StartingWorkingOffset && endingOffset >= StartingWorkingOffset && endingOffset <= EndingWorkingOffset)
            {
                var totalBreaksDuration = 0;

                foreach (var item in DayRules)
                {
                    if (item.RuleType == RuleType.BreakTime)
                    {
                        if (endingOffset >= item.EndOffset)
                            totalBreaksDuration += item.Duration.Value;
                        else if (endingOffset >= item.Offset && endingOffset <= item.EndOffset)
                            totalBreaksDuration += endingOffset - item.Offset.Value;

                    }
                }
                return endingOffset - StartingWorkingOffset.Value - totalBreaksDuration;
            }
            // starting offset is a point between the working hours and ending offset is after the working hours
            else if (startingOffset >= StartingWorkingOffset && startingOffset <= EndingWorkingOffset && endingOffset >= EndingWorkingOffset)
            {
                var totalBreaksDuration = 0;

                foreach (var item in DayRules)
                {
                    if (item.RuleType == RuleType.BreakTime)
                    {
                        if (startingOffset <= item.Offset)
                            totalBreaksDuration += item.Duration.Value;
                        else if (startingOffset >= item.Offset && startingOffset <= item.EndOffset)
                            totalBreaksDuration += item.EndOffset.Value - startingOffset;

                    }
                }
                return EndingWorkingOffset.Value - startingOffset - totalBreaksDuration;
            }
            // both starting and ending offset are points between working hours
            else if (startingOffset >= StartingWorkingOffset && startingOffset <= EndingWorkingOffset && endingOffset >= StartingWorkingOffset && endingOffset <= EndingWorkingOffset)
            {
                var totalBreaksDuration = 0;

                foreach (var item in DayRules)
                {
                    if (item.RuleType == RuleType.BreakTime)
                    {
                        if (startingOffset <= item.Offset && endingOffset >= item.EndOffset)
                            totalBreaksDuration += item.Duration.Value;
                        else if (startingOffset <= item.Offset && endingOffset >= item.Offset && endingOffset <= item.EndOffset)
                            totalBreaksDuration += endingOffset - item.Offset.Value;
                        else if (startingOffset >= item.Offset && startingOffset <= item.EndOffset && endingOffset >= item.EndOffset)
                            totalBreaksDuration += startingOffset - item.Offset.Value;
                    }
                }
                return endingOffset - startingOffset - totalBreaksDuration;
            }

            // else return 0
            return 0;
        }
    }

    public class CrmCalenderDayRule
    {
        public int? Offset { get; set; }
        public int? Duration { get; set; }
        public int? EndOffset { get { return Offset + Duration; } }
        public int? Subcode { get; set; }
        public RuleType RuleType
        {
            get
            {
                if (Subcode.HasValue)
                    return Enum.IsDefined(typeof(RuleType), Subcode.Value) ? (RuleType)Subcode.Value : RuleType.Unknown;
                return RuleType.Unknown;
            }
        }
        public CrmCalenderDayRule()
        {
        }
        public CrmCalenderDayRule(int? offset, int? duration, int? subcode)
        {
            Offset = offset;
            Duration = duration;
            Subcode = subcode;
        }
    }

    class CalendarMetadata
    {
        public const string LogicalName = "calendar";
        public const string Pattern = "pattern";
        public const string CalendarRules = "calendarrules";
        public const string HolidayScheduleCalendarId = "holidayschedulecalendarid";
    }

    class CalendarRuleMetadata
    {
        public const string Offset = "offset";
        public const string Duration = "duration";
        public const string Subcode = "subcode";
        public const string starttime = "starttime";
        public const string IsSelected = "isselected";
        public const string IsVariedName = "isvariedname";
        public const string InnerCalendarId = "innercalendarid";
    }
}