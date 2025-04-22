using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Domain.Features.ServiceDefinitions;

namespace MOHU.Integration.Application.Features.ServiceDefinitions.Repositories;

public interface IServiceDefinitionRepository
{
    PaginationResponse<ServiceDefinition> GetAll(
        FilterExpression? filterExpression = null, 
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null);
    
    QueryBase GetQuery(
        ColumnSet? columnSet = null,
        bool? isOrFilter = null,
        FilterExpression? filterExpression = null,
        List<FilterExpression>? childFilters = null,
        IEnumerable<LinkEntity>? linkEntities = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null,
        params ConditionExpression[] conditionExpressions);
}