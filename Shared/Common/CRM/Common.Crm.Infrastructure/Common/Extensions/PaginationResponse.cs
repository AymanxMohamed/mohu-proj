namespace Common.Crm.Infrastructure.Common.Extensions;

public class PaginationResponse<TItem>
{
    public required int Count { get; init; }
    
    public required int TotalCount { get; init; }
    
    public required bool HasNextPage { get; init; }
    
    public required List<TItem> Items { get; init; }

    public static PaginationResponse<Entity> Create(EntityCollection entityCollection)
    {
        return new PaginationResponse<Entity>
        {
            TotalCount = entityCollection.TotalRecordCount,
            HasNextPage = entityCollection.MoreRecords,
            Items = entityCollection.Entities.ToList(),
            Count = entityCollection.Entities.Count
        };
    }
    
    public static PaginationResponse<TItem> Create(EntityCollection entityCollection, Func<Entity, TItem> converter)
    {
        return new PaginationResponse<TItem>
        {
            TotalCount = entityCollection.TotalRecordCount,
            HasNextPage = entityCollection.MoreRecords,
            Items = entityCollection.Entities.Select(converter).ToList(),
            Count = entityCollection.Entities.Count
        };
    }
    
    public PaginationResponse<TDestination> Convert<TDestination>(Func<TItem, TDestination> converter)
    {
        return new PaginationResponse<TDestination>
        {
            TotalCount = TotalCount,
            HasNextPage = HasNextPage,
            Items = Items.Select(converter).ToList(),
            Count = Count
        };
    }
}