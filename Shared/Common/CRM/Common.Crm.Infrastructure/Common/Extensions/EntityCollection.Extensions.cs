namespace Common.Crm.Infrastructure.Common.Extensions;

public static class EntityCollectionExtensions
{
    public static PaginationResponse<Entity> ToPaginationResponse(this EntityCollection entityCollection)
    {
        return PaginationResponse<Entity>.Create(entityCollection);
    }
    
    public static PaginationResponse<TItem> ToPaginationResponse<TItem>(
        this EntityCollection entityCollection,
        Func<Entity, TItem> converter)
    {
        return PaginationResponse<TItem>.Create(entityCollection, converter);
    }
}