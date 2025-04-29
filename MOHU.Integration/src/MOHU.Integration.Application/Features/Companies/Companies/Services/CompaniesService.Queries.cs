using Common.Crm.Domain.Common.Enums;
using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Common.Constants;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Application.Features.Companies.Companies.Services;

public partial class CompaniesService
{
    public async Task<Company> GetByElmReferenceId(long elmReferenceId)
    {
        var company = (await GetAsync(GetActiveCompanyByElmReferenceIdQuery(elmReferenceId))).FirstOrDefault();

        if (company is null)
        {
            throw new NotFoundException($"Company with elm entity Id {elmReferenceId} not found");
        }
        
        return company;
    }

    private static QueryExpression GetActiveCompanyByElmReferenceIdQuery(long elmReferenceId)
    {
        return QueryExpressionFactory.CreateQueryExpression(
            entityLogicalName: CompaniesConstants.LogicalName,
            conditionExpressions:
            [
                ConditionExpressionFactory.CreateStatusCondition(StatusEnum.Active),
                ConditionExpressionFactory
                    .CreateConditionExpression(
                        CommonConstants.Fields.IntegrationDetails.ElmReferenceId,
                        ConditionOperator.Equal,
                        elmReferenceId),
            ]);
    }

    private async Task<List<Company>> GetAsync(QueryExpression? queryExpression) => 
        (await GetEntitiesAsync(queryExpression)).Select(Company.Create).ToList();

    private async Task<List<Entity>> GetEntitiesAsync(QueryExpression? queryExpression)
    {
        var companies = await crmContext.ServiceClient.RetrieveMultipleAsync(queryExpression);
        
        return companies?.Entities?.ToList() ?? [];
    }
}