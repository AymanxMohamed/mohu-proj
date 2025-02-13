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

        public static void AssignIfNotNull(this Entity entity, string attributeLogicalName, object? value)
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