namespace Common.Crm.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository
    {
        // void Create<T>(T type);
        
        void Create(Entity entity);

        void CreateMany(
            string entityLogicalName,
            IEnumerable<Entity> relatedEntities,
            string relatedEntityLookupNameOnRequiredEntity,
            Dictionary<string, object>? attributes = null);
        
        
        void CreateMany(IEnumerable<Entity> entities);
        
        // void CreateMany<T>(IEnumerable<T> types);
        
        Entity GetById(string entityLogicalName, Guid id, ColumnSet? columnSet = null);
        
        Entity GetByEntityReference(EntityReference entityReference, ColumnSet? columnSet = null);
        
        // T GetByEntityReference<T>(EntityReference entityReference, ColumnSet? columnSet = null);
        
        // T GetById<T>(string entityLogicalName, Guid id, ColumnSet? columnSet = null);
        
        IEnumerable<Entity> ListAll(QueryBase query);
        
        // IEnumerable<T> ListAll<T>(QueryBase query);

        IEnumerable<Entity> ListAllFilteredByEntityReference(
            string logicalName,
            EntityReference entityReference,
            string lookupLogicalName,
            ColumnSet? columnSet = null);
        
        void Update(Entity entity);
        
        // void Update<T>(T type);
        
        void UpdateMany(IEnumerable<Entity> entities);
        
        // void UpdateMany<T>(IEnumerable<T> types);
        
        void Delete(Entity entity);
        
        // void Delete<T>(T type);
        
        void DeleteMany(IEnumerable<Entity> entities);

        void DeleteAllRecordsRelatedToEntity(
            string logicalName,
            EntityReference entityReference,
            string lookupLogicalName);
        
        // void DeleteMany<T>(IEnumerable<T> types);
        
        void Deactivate(Entity entity);
        
        void Commit();
    }
}