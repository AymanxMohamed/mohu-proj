namespace Common.Crm.Domain.Common.OptionSets.Extensions;

public static class OptionSetExtensions
{
    public static TEnum? ToEnum<TEnum>(this OptionSetValue? optionSetValue) 
        where TEnum : struct, Enum
    {
        if (optionSetValue == null)
            return default;

        return Enum.IsDefined(typeof(TEnum), optionSetValue.Value)
            ? (TEnum)(object)optionSetValue.Value
            : default;
    }
    
    public static OptionSetValue? ToOptionSetValue<TEnum>(this TEnum? enumValue) 
        where TEnum : struct, Enum
    {
        return enumValue.HasValue ? new OptionSetValue(Convert.ToInt32(enumValue.Value)) : null;
    }
    
    public static TDestinationEnum? ToEnum<TEnum, TDestinationEnum>(this TEnum? enumValue)
        where TEnum : struct, Enum
        where TDestinationEnum : struct, Enum
    {
        if (enumValue is null)
        {
            return null;
        }
        
        var underlyingValue = Convert.ChangeType(enumValue.Value, Enum.GetUnderlyingType(typeof(TEnum)));
        return (TDestinationEnum)Enum.ToObject(typeof(TDestinationEnum), underlyingValue);
    }
    
    public static TDestinationEnum? ToEnum<TEnum, TDestinationEnum>(this TEnum enumValue)
        where TEnum : struct, Enum
        where TDestinationEnum : struct, Enum
    {
        var underlyingValue = Convert.ChangeType(enumValue, Enum.GetUnderlyingType(typeof(TEnum)));
        return (TDestinationEnum)Enum.ToObject(typeof(TDestinationEnum), underlyingValue);
    }
}