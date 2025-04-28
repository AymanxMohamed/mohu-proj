namespace Common.Crm.Domain.Common.Extensions
{
    public static partial class EntityExtensions
    {
        public static double? GetDoubleFromAliasedValue(this Entity? entity, string alias, string logicalName)
        {
            var aliasedValue = entity?.GetAliasedValue(alias, logicalName);

            if (aliasedValue is null)
            {
                return null;
            }

            return Convert.ToDouble(aliasedValue.Value.ToString());
        }

        public static AliasedValue GetAliasedValue(this Entity entity, string alias, string logicalName)
        {
            return entity.GetAttributeValue<AliasedValue>($"{alias}.{logicalName}");
        }
        
        public static Entity? GetAliasedEntity(this Entity entity, string alias, string logicalName)
        {
            var linkedEntityAttributes = entity.Attributes
                .Where(kv => kv.Key.StartsWith(alias + "."))
                .ToDictionary(
                    kv => kv.Key[(alias.Length + 1)..],
                    kv => ((AliasedValue)kv.Value).Value
                );

            if (linkedEntityAttributes.Count == 0)
            {
                return null;
            }
            
            var linkedEntity = new Entity
            {
                LogicalName = logicalName,
                Id = (Guid)linkedEntityAttributes[$"{logicalName}id"]
            };

            foreach (var attribute in linkedEntityAttributes)
            {
                linkedEntity.Attributes.Add(attribute.Key, attribute.Value);
            }

            return linkedEntity;
        }

        public static void AssignIfNotNull<TValue>(this Entity entity, string attributeLogicalName, TValue? value)
        {
            if (value is null)
            {
                return;
            }

            entity[attributeLogicalName] = value;
        }
        
        public static void AssignIfContains(this Entity entity, string attributeLogicalName, object value)
        {
            if (!entity.Attributes.ContainsKey(attributeLogicalName))
            {
                return;
            }

            entity[attributeLogicalName] = value;
        }

        public static bool HasAttributeValue(this Entity entity, string attributeLogicalName)
        {
            return entity.Attributes.ContainsKey(attributeLogicalName) && entity[attributeLogicalName] != null;
        }
    }
}