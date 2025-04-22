using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Domain.Features.ServiceDefinitions;
using MOHU.Integration.Domain.Features.ServiceDefinitions.Constants;

namespace MOHU.Integration.Application.Features.ServiceDefinitions.Repositories;

public class ServiceDefinitionRepository(IGenericRepository repository) : IServiceDefinitionRepository
{
    public PaginationResponse<ServiceDefinition> GetAll(
        FilterExpression? filterExpression = null, 
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null)
    {
       return repository
           .ListAllPaginated(GetQuery(
               filterExpression: filterExpression, 
               paginationParameters: paginationParameters,
               orderExpressions: orderExpressions))
           .Convert(ServiceDefinition.Create);
    }

    public QueryBase GetQuery(
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null,
        params ConditionExpression[] conditionExpressions)
    {
        return QueryExpressionFactory
            .CreateQueryExpression(
                entityLogicalName: ServiceDefinitionConstants.LogicalName,
                columnSet,
                isOrFilter,
                filterExpression,
                childFilters, 
                linkEntities, 
                paginationParameters,
                orderExpressions: orderExpressions,
                conditionExpressions: conditionExpressions);
    }
}