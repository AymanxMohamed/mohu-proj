namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class FilterRequest
{
    public List<FilterItem> FilterList { get; set; } = [];
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public SortItem? SortColumn { get; set; }
    public List<SortItem> SortCriteria { get; set; } = [];
}