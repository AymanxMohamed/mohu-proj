using Newtonsoft.Json;

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
    
    [JsonProperty("propertyName")]
    public string PropertyName { get; set; } = null!;

    [JsonProperty("orderDir")]
    public string OrderDir { get; set; } = null!;

    public static ElmSortItem Create(string propertyName, string orderDir = Ascending)
        => new(propertyName, orderDir);

    public static ElmSortItem CreateDesc(string propertyName)
        => new(propertyName, Descending);
}