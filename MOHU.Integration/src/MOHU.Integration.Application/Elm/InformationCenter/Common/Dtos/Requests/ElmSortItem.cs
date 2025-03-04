namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class ElmSortItem
{
    public const string Descending = "desc";
    public const string Ascending = "asc";

    public ElmSortItem()
    {
    }
    
    private ElmSortItem(string propertyName, string orderDir)
    {
        PropertyName = propertyName;
        OrderDir = orderDir;
    }

    public string PropertyName { get; set; } = null!;

    public string OrderDir { get; set; } = null!;

    public static ElmSortItem Create(string propertyName, string orderDir = Ascending)
        => new(propertyName, orderDir);

    public static ElmSortItem CreateDesc(string propertyName)
        => new(propertyName, Descending);
}