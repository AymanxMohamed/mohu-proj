namespace Common.Crm.Domain.Common.Extensions
{
    public static class AliasedValueExtensions
    {
        public static double? ToDouble(this AliasedValue? aliasedValue)
        {
            if (aliasedValue is null)
            {
                return null;
            }

            return Convert.ToDouble(aliasedValue.Value.ToString());
        }
                
        public static EntityReference? ToEntityReference(this AliasedValue? aliasedValue)
        {
            return (EntityReference?) aliasedValue?.Value;
        }
    }
}