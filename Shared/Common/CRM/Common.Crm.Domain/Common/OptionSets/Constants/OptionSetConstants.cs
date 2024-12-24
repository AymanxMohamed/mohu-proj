namespace Common.Crm.Domain.Common.OptionSets.Constants;

public class OptionSetConstants
{
    public static class Status
    {
        public const string FieldLogicalName = "statecode";
        public static readonly OptionSetValue Active = new(0);
        public static readonly OptionSetValue InActive = new(1);
    }
        
    public static class StatusReason
    {
        public const string FieldLogicalName = "statuscode";
        public static readonly OptionSetValue Active = new(1);
        public static readonly OptionSetValue InActive = new(2);
    }
}