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
        
    }
}