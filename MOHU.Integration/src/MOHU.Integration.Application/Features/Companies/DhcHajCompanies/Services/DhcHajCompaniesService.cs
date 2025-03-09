using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;
using MOHU.Integration.Domain.Features.Companies.Enums;

namespace MOHU.Integration.Application.Features.Companies.DhcHajCompanies.Services;

public class DhcHajCompaniesService(
    IElmInformationCenterDhcHajCompaniesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterDhcHajCompaniesClient, ElmDhcHajCompanyResponse, Company>(
        configurationService,
        client,
        crmContext,
        CompaniesConstants.LogicalName,
        Company.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y),
    IDhcHajCompaniesService
{
    protected override QueryExpression GetCrmEntitiesByElmReferenceIdsQuery(List<int> ids)
    {
        var query = base.GetCrmEntitiesByElmReferenceIdsQuery(ids);
        
        query.Criteria.AddCondition(
            ConditionExpressionFactory.CreateConditionExpression(
                columnLogicalName: CompaniesConstants.Fields.ElmCompanyType,
                conditionOperator: ConditionOperator.Equal,
                value: (int)ElmCompanyTypeEnum.DhcHajCompany));
        
        return query;
    }
}