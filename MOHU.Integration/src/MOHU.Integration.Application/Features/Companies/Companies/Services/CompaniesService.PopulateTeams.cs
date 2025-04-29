using Common.Crm.Domain.Common.Enums;
using Common.Crm.Domain.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Application.Features.Companies.Companies.Services;

public partial class CompaniesService
{
    public Task MapDeactivatedCompaniesToNewCompanies()
    {
        var sourceCompanies  = GetDeactivatedCompaniesWithTeams();
        
        var allCompanies = GetAllSystemCompaniesThatContainsSicCode();
        
        MapCompanies(sourceCompanies, allCompanies);
        
        genericRepository.UpdateMany(allCompanies);
        
        genericRepository.Commit();
        
        return Task.CompletedTask;
    }

    public List<Entity> GetDeactivatedCompaniesWithTeams()
    {
        var query = QueryExpressionFactory.CreateQueryExpression(
            entityLogicalName: CompaniesConstants.LogicalName,
            conditionExpressions:
            [
                ConditionExpressionFactory.CreateStatusCondition(StatusEnum.InActive),
                ConditionExpressionFactory.CreateConditionExpression(CompaniesConstants.Fields.TeamId,
                    ConditionOperator.NotNull),
                ConditionExpressionFactory.CreateConditionExpression(CompaniesConstants.Fields.SicCode,
                    ConditionOperator.NotNull)
            ]);
        
       return genericRepository.ListAll(query).ToList();
    }

    public List<Entity> GetAllSystemCompaniesThatContainsSicCode()
    {
        return genericRepository.ListAll(
                QueryExpressionFactory.CreateQueryExpression(
                    CompaniesConstants.LogicalName,
                    conditionExpressions: [ConditionExpressionFactory.CreateConditionExpression(CompaniesConstants.Fields.SicCode, ConditionOperator.NotNull)]))
            .ToList();
    }

    public static void MapCompanies(List<Entity> sourceCompanies, List<Entity> destinationCompanies)
    {
        sourceCompanies.MapEntitiesMatchingCriteria(
            destinationCompanies, 
            (sourceCompany, destinationCompany) => 
                sourceCompany.IsAttributeEqual<string>(destinationCompany, CompaniesConstants.Fields.SicCode) && 
                destinationCompany.Id != sourceCompany.Id);
    }

    public static void ForceMap(Entity source, Entity destination)
    {
        if (source.Attributes.Contains(CompaniesConstants.Fields.SicCode) &&
            destination.Attributes.Contains(CompaniesConstants.Fields.TeamId))
        {
            destination[CompaniesConstants.Fields.TeamId] = source[CompaniesConstants.Fields.TeamId];
        }
    }
}