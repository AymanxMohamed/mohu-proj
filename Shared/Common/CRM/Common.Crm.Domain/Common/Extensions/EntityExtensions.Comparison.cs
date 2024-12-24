namespace Common.Crm.Domain.Common.Extensions;

public static partial class EntityExtensions
{
    public static bool AreAttributesEqual(this Entity? source, Entity? target, List<string> attributeKeys)
    {
        if (source is null || target is null)
        {
            return false;
        }
        
        
        return attributeKeys.All(attributeKey => source.IsAttributeEqual(target, attributeKey));
    }

    public static bool IsAttributeEqual<T>(
        this Entity? source,
        Entity? target, 
        string attributeName, 
        Func<T?, T?, bool>? equalityComparer = null)
        where T : class
    {
        if (source is null || target is null)
        {
            return false;
        }

        if (!source.Attributes.ContainsKey(attributeName) || !target.Attributes.ContainsKey(attributeName))
        {
            return false;
        }

        var sourceValue = source.Attributes[attributeName] as T;
        var targetValue = target.Attributes[attributeName] as T;

        return equalityComparer?.Invoke(sourceValue, targetValue) ?? sourceValue?.Equals(targetValue) ?? false;
    }
    
    public static bool IsAttributeEqual(
        this Entity? source, 
        Entity? target, 
        string attributeName,
        Func<object?, object?, bool>? equalityComparer = null)
    {
        return IsAttributeEqual<object>(source, target, attributeName, equalityComparer);
    }
}