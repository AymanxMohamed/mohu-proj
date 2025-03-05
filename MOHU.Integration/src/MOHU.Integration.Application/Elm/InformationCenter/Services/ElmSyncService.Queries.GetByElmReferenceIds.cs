using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.Common.Constants;

namespace MOHU.Integration.Application.Elm.InformationCenter.Services;

public partial class ElmSyncService<TElmClient, TElmEntity, TCrmEntity>
{
    private List<TCrmEntity> GetCrmEntitiesByElmReferenceIds(List<int>? ids)
    {
        if (ids is null || ids.Count == 0)
        {
            return [];
        }

        var query = QueryExpressionFactory
            .CreateQueryExpression(
                entityLogicalName,
                conditionExpressions: [ConditionExpressionFactory.CreateConditionExpression(
                    columnLogicalName: CommonConstants.Fields.IntegrationDetails.ElmReferenceId,
                    conditionOperator: ConditionOperator.In,
                    values: [..ids])]);


        return _genericRepository.ListAll(query).Select(factory).ToList();
    }
}