namespace MOHU.Integration.Application.Features.TicketCategories;

public interface ITicketCategoriesService
{
    Task EnsureValidCategoriesAsync(List<Guid>? categoryIds, int? origin = null);
    
    Task<bool> IsValidCategories(List<Guid> categoryIds, int? origin = null);
}