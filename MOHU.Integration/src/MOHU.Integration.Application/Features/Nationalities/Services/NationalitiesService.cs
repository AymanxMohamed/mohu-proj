using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Countries.Constants;
using MOHU.Integration.Domain.Features.Countries.Enums;

namespace MOHU.Integration.Application.Features.Nationalities.Services;

public class NationalitiesService(
    IElmInformationCenterCountriesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterCountriesClient, ElmCountryResponse, Country>(
        configurationService,
        client,
        crmContext,
        CountriesConstants.LogicalName,
        Country.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y),
    INationalitiesService
{
    protected override QueryExpression GetCrmEntitiesByElmReferenceIdsQuery(List<int> ids)
    {
        var query = base.GetCrmEntitiesByElmReferenceIdsQuery(ids);
        
        query.Criteria.AddCondition(
            ConditionExpressionFactory.CreateConditionExpression(
                columnLogicalName: CountriesConstants.Fields.ElmEntityType,
                conditionOperator: ConditionOperator.Equal,
                value: (int)ElmEntityTypeEnum.Nationality));
        
        return query;
    }
}