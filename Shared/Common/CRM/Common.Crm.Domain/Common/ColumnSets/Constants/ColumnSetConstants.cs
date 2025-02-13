namespace Common.Crm.Domain.Common.ColumnSets.Constants;

public static class ColumnSetConstants
{
    public const string Status = "statecode";
    public const string StatusReason = "statuscode";
    public static readonly ColumnSet AllColumns = new(allColumns: true);
    public static readonly ColumnSet NoColumns = new(allColumns: false);
    public static readonly ColumnSet SystemStatusesColumns = new(Status, StatusReason);
    public static ColumnSet CreateColumnSet(params string[] columns) => new(columns);
}