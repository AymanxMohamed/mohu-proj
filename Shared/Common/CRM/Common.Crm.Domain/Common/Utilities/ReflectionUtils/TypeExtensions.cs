using System.Reflection;

namespace Common.Crm.Domain.Common.Utilities.ReflectionUtils;

public static class TypeExtensions
{
    public static ColumnSet GetColumnSet(this Type type)
    {
        var fieldConstants = type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f is { IsLiteral: true, IsInitOnly: false } && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!)
            .ToArray();

        return new ColumnSet(fieldConstants);
    }
}