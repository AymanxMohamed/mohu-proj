// using Common.Crm.Infrastructure.Factories;
// using MOHU.Integration.Domain.Features.Individuals.Constants;
// using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;
//
// namespace MOHU.Integration.Application.Features.Individuals.Services;
//
// public partial class IndividualsService
// {
//     public List<Individual> GetByElmReferenceIds(List<int>? ids)
//     {
//         if (ids is null || ids.Count == 0)
//         {
//             return [];
//         }
//
//         var query = QueryExpressionFactory
//             .CreateQueryExpression(
//                 IndividualConstants.LogicalName,
//                 conditionExpressions: [ConditionExpressionFactory.CreateConditionExpression(
//                         columnLogicalName: IndividualConstants.Fields.IntegrationDetails.ElmReferenceId,
//                         conditionOperator: ConditionOperator.In,
//                         values: [..ids])]);
//
//
//         return _genericRepository.ListAll(query).Select(Individual.Create).ToList();
//     }
// }