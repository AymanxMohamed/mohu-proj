namespace MOHU.Integration.Domain.Features.TicketStatuses.Constants;

public class TicketStatusesConstants
{
    public const string LogicalName = "ldv_servicesubstatus";
    
    public static class Fields
    {
        public const string Id = "ldv_servicesubstatusid";
        public const string Name = "ldv_name";
        public const string Code = "ldv_code";
        public const string EnglishName = "ldv_name_en";
        public const string ArabicName = "ldv_name_ar";
    }

    public static class Statuses
    {
        public static readonly Guid Resolved = new("e1384045-65aa-ef11-b8e9-6045bd9ec6ef");

    }
}