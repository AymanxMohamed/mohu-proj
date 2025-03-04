namespace Common.Crm.Domain.Common.Extensions
{
    public static partial class EntityExtensions
    {
        public static void RemoveId(this Entity entity)
        {
            entity.Id = Guid.Empty;
            
            if (entity.Attributes.Contains(entity.GetPrimaryKey()))
            {
                entity.Attributes.Remove(entity.GetPrimaryKey());
            }
        }

        public static void ReplaceId(this Entity entity, Guid? id = null)
        {
            entity.Id = id ?? Guid.NewGuid();

            entity[entity.GetPrimaryKey()] = entity.Id;
        }


        public static void EnsureCanCreateFrom(
            this Entity entity, 
            string objectToCreate,
            string logicalName)
        {
            if (entity is null || entity.LogicalName != logicalName)
            {
                throw new InvalidOperationException($"{objectToCreate} can be created only from an ${logicalName}");
            }
        }
    }
}