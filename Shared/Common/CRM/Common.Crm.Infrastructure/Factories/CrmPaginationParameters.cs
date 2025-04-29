namespace Common.Crm.Infrastructure.Factories;

public class CrmPaginationParameters
{
    public int Page { get; init; } = 1;
    
    public int PageSize { get; init; } = 10;

    public static CrmPaginationParameters Create()
    {
        return new CrmPaginationParameters();
    }
}