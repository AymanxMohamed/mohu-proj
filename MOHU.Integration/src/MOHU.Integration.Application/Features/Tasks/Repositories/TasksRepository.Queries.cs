using Common.Crm.Domain.Common.Constants;
using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Domain.Features.SlaKpiInstances.Constants;
using MOHU.Integration.Domain.Features.Tasks;
using MOHU.Integration.Domain.Features.Tasks.Constants;

namespace MOHU.Integration.Application.Features.Tasks.Repositories;

internal partial class TasksRepository(IGenericRepository genericRepository) : ITasksRepository
{
    public PaginationResponse<CrmTask> GetTicketTasks(
        Guid ticketId,
        FilterExpression? filterExpression = null,
        CrmPaginationParameters? paginationParameters = null,
        List<OrderExpression>? orderExpressions = null)
    {
        orderExpressions ??= [new OrderExpression(CommonConstants.Fields.CreatedOn, OrderType.Descending)];
        
        return genericRepository
            .ListAllPaginated(GetQuery(
                filterExpression: filterExpression,
                paginationParameters: paginationParameters,
                orderExpressions: orderExpressions,
                linkEntities: [
                    LinkEntityFactory
                        .CreateLinkToPrimary(
                            childEntityName: TaskConstants.LogicalName,
                            parentEntityName: SlaKpiInstanceConstants.LogicalName,
                            foreignKeyName: TaskConstants.Fields.SlaLevelOneTimer,
                            entityAlias: TaskConstants.RelatedEntities.SlaKpiInstance.SlaLevelOneTimer.Alies),
                    LinkEntityFactory
                        .CreateLinkToPrimary(
                            childEntityName: TaskConstants.LogicalName,
                            parentEntityName: SlaKpiInstanceConstants.LogicalName,
                            foreignKeyName: TaskConstants.Fields.SlaLevelTwoTimer,
                            entityAlias: TaskConstants.RelatedEntities.SlaKpiInstance.SlaLevelTwoTimer.Alies),
                    LinkEntityFactory
                        .CreateLinkToPrimary(
                            childEntityName: TaskConstants.LogicalName,
                            parentEntityName: SlaKpiInstanceConstants.LogicalName,
                            foreignKeyName: TaskConstants.Fields.SlaLevelThreeTimer,
                            entityAlias: TaskConstants.RelatedEntities.SlaKpiInstance.SlaLevelThreeTimer.Alies)],
                conditionExpressions: [ConditionExpressionFactory
                    .CreateConditionExpression(
                        columnLogicalName: TaskConstants.Fields.Regarding,
                        conditionOperator: ConditionOperator.Equal,
                        value: ticketId)]))
            .Convert(CrmTask.Create);
    }

    private static QueryBase GetQuery(
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
                entityLogicalName: TaskConstants.LogicalName,
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