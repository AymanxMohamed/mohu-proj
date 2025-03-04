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
}