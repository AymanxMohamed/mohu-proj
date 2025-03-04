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
}