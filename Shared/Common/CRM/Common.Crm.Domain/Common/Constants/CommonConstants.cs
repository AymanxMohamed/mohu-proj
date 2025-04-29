namespace Common.Crm.Domain.Common.Constants;

public static class CommonConstants
{
    public static readonly List<string> BpfFields =
    [
        Fields.Process,
        Fields.Stage,
        Fields.TraversedPath,
        Fields.LinkProcessStage

    ];
        
    public static readonly List<string> StatusFields =
    [
        Fields.Status,
        Fields.StatusReasonOop
    ];
        
    public static class Fields
    {
        public const string Process = "processid";
        public const string Stage = "stageid";
        public const string TraversedPath = "traversedpath";
        public const string LinkProcessStage = "ldv_processstageid";
        public const string Status = "statecode";
        public const string StatusReasonOop = "statuscode";
        public const string CreatedOn = "createdon";
        public const string ModifiedOn = "modifiedon";
    }
}