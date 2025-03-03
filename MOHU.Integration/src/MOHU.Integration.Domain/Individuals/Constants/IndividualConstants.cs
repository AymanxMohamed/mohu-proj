namespace MOHU.Integration.Domain.Individuals.Constants;

public static class IndividualConstants
{
    public const string LogicalName = "contact";

    public static class Fields
    {
        public const string Id = $"{LogicalName}{nameof(Id)}";
        public const string FirstName = "firstname";
        public const string LastName = "lastName";
        public const string EnglishName = "fullname";
        public const string ArabicName = "ldv_name_ar";
        public const string PhoneNumber = "ldv_phonenumber";
        public const string OriginCode = "ldv_origincode";
        public const string BirthDate = "birthdate";
        public const string PassportNumber = "governmentid";
        public const string HijriBirthDate = "ldv_hijribirthdate";
        public const string Email = "emailaddress1";
    }
}