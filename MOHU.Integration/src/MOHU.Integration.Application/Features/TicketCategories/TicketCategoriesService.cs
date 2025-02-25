namespace MOHU.Integration.Application.Features.TicketCategories;

public class TicketCategoriesService(ICrmContext crmContext) : ITicketCategoriesService
{
    public async Task EnsureValidCategoriesAsync(List<Guid>? categoryIds, int? origin = null)
    {
        if (!await IsValidCategories(categoryIds, origin))
        {
            throw new BadRequestException("The provided categories are not valid.");
        }
    }
    
    public async Task<bool> IsValidCategories(List<Guid>? categoryIds, int? origin = null)
    {
        if (categoryIds == null || categoryIds.Count == 0)
        {
            return true;
        }

        var categoryQuery = new QueryExpression(ldv_casecategory.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(ldv_casecategory.Fields.Id),
        };
        
        var filter = new FilterExpression(LogicalOperator.And);
        categoryQuery.Criteria.AddFilter(filter);
        
        filter.AddCondition(new ConditionExpression(
            ldv_casecategory.Fields.Id,
            ConditionOperator.In, 
            categoryIds.Cast<object>().ToArray()));
        
        filter.AddCondition(new ConditionExpression(
            ldv_casecategory.Fields.StateCode,
            ConditionOperator.Equal, 
            0));
        
        filter.AddCondition(new ConditionExpression(
            ldv_casecategory.Fields.ShowOnPortal, 
            ConditionOperator.Equal,
            true));

        if (origin.HasValue)
        {
            filter.AddCondition(new ConditionExpression(
                ldv_casecategory.Fields.AvailableFor,
                ConditionOperator.ContainValues, 
                origin.Value));
        }
        
        var response = await crmContext.ServiceClient.RetrieveMultipleAsync(categoryQuery);

        var validCategoryIds = response.Entities.Select(c => c.Id).ToHashSet();

        return categoryIds.All(id => validCategoryIds.Contains(id));
    }
}