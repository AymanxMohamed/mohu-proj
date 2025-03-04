namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class ElmFilterRequest
{
    private const int DefaultPage = 1;

    public const int DefaultPageSize = 10;

    public ElmFilterRequest()
    {   
    }
    
    private ElmFilterRequest(
        int page = DefaultPage,
        int pageSize = DefaultPageSize,
        ElmSortItem? sortColumn = null,
        List<ElmSortItem>? sortCriteria = null,
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
        ElmSortItem? sortColumn = null,
        List<ElmSortItem>? sortCriteria = null,
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
    public ElmSortItem? SortColumn { get; set; }

    public List<FilterItem> FilterList { get; set; } = [];

    public List<ElmSortItem> SortCriteria { get; set; } = [];

    public static ElmFilterRequest Create(
        int page = DefaultPage,
        int pageSize = DefaultPageSize,
        ElmSortItem? sortColumn = null,
        List<ElmSortItem>? sortCriteria = null,
        List<FilterItem>? filterList = null) =>
        new(page: page, pageSize, sortColumn, sortCriteria, filterList);

    public void AddSortColumn(ElmSortItem sortColumn) => SortColumn = sortColumn;
}