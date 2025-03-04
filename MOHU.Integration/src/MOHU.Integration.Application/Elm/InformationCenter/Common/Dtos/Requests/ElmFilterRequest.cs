namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class ElmFilterRequest
{
    private const int DefaultPage = 1;

    public const int DefaultPageSize = 10;

    private ElmFilterRequest()
    {
    }
    
    private ElmFilterRequest(
        int page = DefaultPage,
        int pageSize = DefaultPageSize,
        SortItem? sortColumn = null,
        List<SortItem>? sortCriteria = null,
        List<FilterItem>? filterList = null)
        : this(
            limit: pageSize, 
            offset: (page - 1) * pageSize, 
            sortColumn: sortColumn,
            sortCriteria: sortCriteria, 
            filterList: filterList)
    {
    }
    
    private ElmFilterRequest(
        int? limit = null,  
        int? offset = null, 
        SortItem? sortColumn = null,
        List<SortItem>? sortCriteria = null,
        List<FilterItem>? filterList = null)
    {
        Limit = limit;
        Offset = offset;
        SortColumn = sortColumn;
        SortCriteria = sortCriteria ?? [];
        FilterList = filterList ?? [];
    }
    
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public SortItem? SortColumn { get; set; }

    public List<FilterItem> FilterList { get; set; } = [];

    public List<SortItem> SortCriteria { get; set; } = [];

    public static ElmFilterRequest Create(
        int page = DefaultPage,
        int pageSize = DefaultPageSize,
        SortItem? sortColumn = null,
        List<SortItem>? sortCriteria = null,
        List<FilterItem>? filterList = null) =>
        new(page: page, pageSize, sortColumn, sortCriteria, filterList);
}