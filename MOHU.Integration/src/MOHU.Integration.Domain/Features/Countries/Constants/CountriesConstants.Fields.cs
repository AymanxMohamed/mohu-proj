namespace MOHU.Integration.Domain.Features.Countries.Constants;

public static partial class CountriesConstants
{
    public static class Fields
    {
        public const string Id = $"{LogicalName}{nameof(Id)}";
        public const string LdvId = "ldv_id";
        public const string Name = "ldv_name";
        public const string ArabicName = "ldv_name_ar";
        public const string EnglishName = "ldv_name_en";
        public const string Code = "ldv_code";
        public const string ElmEntityType = "ldv_elmentitytypecode";
    }
}