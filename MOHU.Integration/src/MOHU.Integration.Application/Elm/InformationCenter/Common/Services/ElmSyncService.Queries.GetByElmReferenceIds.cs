using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Common.Constants;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Services;

public partial class ElmSyncService<TElmClient, TElmEntity, TCrmEntity>
{
    public List<TCrmEntity> GetCrmEntitiesByElmReferenceIds(List<int>? ids)
    {
        if (ids is null || ids.Count == 0)
        {
            return [];
        }

        var query = GetCrmEntitiesByElmReferenceIdsQuery(ids);

        return GetCrmEntitiesByQuery(query);
    }
    
    public List<TCrmEntity> GetCrmEntitiesByQuery(QueryExpression query) => 
        _genericRepository
            .ListAll(query)
            .Select(factory)
            .ToList();

    protected virtual QueryExpression GetCrmEntitiesByElmReferenceIdsQuery(List<int> ids)
    {
        return QueryExpressionFactory
            .CreateQueryExpression(
                entityLogicalName,
                conditionExpressions: [ConditionExpressionFactory.CreateConditionExpression(
                    columnLogicalName: CommonConstants.Fields.IntegrationDetails.ElmReferenceId,
                    conditionOperator: ConditionOperator.In,
                    values: [..ids])]);
    }
}