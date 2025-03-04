using Newtonsoft.Json;

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
        Limit = limit ?? DefaultPageSize;
        Offset = offset ?? (DefaultPage - 1) * DefaultPageSize;
        SortColumn = sortColumn;
        SortCriteria = sortCriteria ?? [];
        FilterList = filterList ?? [];
    }
    
    [JsonProperty("limit")]
    public int? Limit { get; set; }
    
    [JsonProperty("offset")]
    public int? Offset { get; set; }
    
    [JsonProperty("sortColumn")]
    public ElmSortItem? SortColumn { get; set; }
    
    [JsonProperty("filterList")]
    public List<FilterItem> FilterList { get; set; } = [];
    
    [JsonProperty("sortCriteria")]
    public List<ElmSortItem> SortCriteria { get; set; } = [];
    

    public static ElmFilterRequest Create(
        int page = DefaultPage,
        int pageSize = DefaultPageSize,
        ElmSortItem? sortColumn = null,
        List<ElmSortItem>? sortCriteria = null,
        List<FilterItem>? filterList = null) =>
        new(page: page, pageSize, sortColumn, sortCriteria, filterList);

    public ElmFilterRequest AddSortColumn(ElmSortItem sortColumn)
    {
        SortColumn ??= sortColumn;
        return this;
    }

    public ElmFilterRequest AddDefaultPaginationIfNull()
    {
        Limit ??= DefaultPageSize;
        Offset ??= (DefaultPage - 1) * DefaultPageSize;
        return this;
    }
}