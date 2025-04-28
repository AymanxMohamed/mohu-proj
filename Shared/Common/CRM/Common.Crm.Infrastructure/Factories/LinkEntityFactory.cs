using Common.Crm.Domain.Common.ColumnSets.Constants;

namespace Common.Crm.Infrastructure.Factories
{
    public static class LinkEntityFactory
    {
        public static LinkEntity Create(
            string childEntityName, 
            string parentEntityName, 
            string foreignKeyName,
            string primaryKeyName,
            JoinOperator joinOperator = JoinOperator.LeftOuter,
            string? entityAlias = null,
            ColumnSet? columns = null)
        {
            var linkEntity = new LinkEntity(
                linkFromEntityName: childEntityName, 
                linkToEntityName: parentEntityName,
                linkFromAttributeName: foreignKeyName,
                linkToAttributeName: primaryKeyName,
                joinOperator);

            if (entityAlias != null)
                linkEntity.EntityAlias = entityAlias;

            linkEntity.Columns = columns ?? ColumnSetConstants.AllColumns;

            return linkEntity;
        }
        
        
        public static LinkEntity CreateLinkToPrimary(
            string childEntityName, 
            string parentEntityName, 
            string foreignKeyName,
            JoinOperator joinOperator = JoinOperator.LeftOuter,
            string? entityAlias = null,
            ColumnSet? columns = null)
        {
           return Create(
                childEntityName, 
                parentEntityName,
                foreignKeyName,
                primaryKeyName: parentEntityName + "id",
                joinOperator,
                entityAlias,
                columns);
        }
    }
}