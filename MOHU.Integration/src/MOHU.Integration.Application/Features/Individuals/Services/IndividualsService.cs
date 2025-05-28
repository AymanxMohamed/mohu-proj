using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using MOHU.Integration.Domain.Features.Individuals.Constants;
using CommonConstants = MOHU.Integration.Domain.Features.Common.Constants.CommonConstants;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Features.Individuals.Services;

public class IndividualsService(
    IElmInformationCenterApplicantDataClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterApplicantDataClient, ElmApplicant, Individual>(
        configurationService,
        client,
        crmContext,
        IndividualConstants.LogicalName,
        Individual.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y,
        GetQueryExpression),
    IIndividualsService
{
    private static QueryExpression GetQueryExpression(List<ElmApplicant> elmApplicants)
    {
        var emails = elmApplicants
            .Select(x => x.ContactInformation.Email)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x!)
            .ToList();
        
        var elmReferenceIds = elmApplicants
            .Select(x => x.Id)
            .ToList();
        
        return QueryExpressionFactory
            .CreateQueryExpression(
                IndividualConstants.LogicalName,
                isOrFilter: true,
                conditionExpressions: [
                    ConditionExpressionFactory.CreateConditionExpression(
                        columnLogicalName: CommonConstants.Fields.IntegrationDetails.ElmReferenceId,
                        conditionOperator: ConditionOperator.In,
                        values: [..elmReferenceIds]),
                    ConditionExpressionFactory.CreateConditionExpression(
                        columnLogicalName: IndividualConstants.Fields.ContactInformation.Email,
                        conditionOperator: ConditionOperator.In,
                        values: [..emails])]);
    }
}