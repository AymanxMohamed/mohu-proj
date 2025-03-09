namespace MOHU.Integration.Domain.Features.Companies.Constants;

public static class CompaniesConstants
{
    public const string LogicalName = "account";

    public static class Fields
    {
        public const string Id = "accountid";
        public const string SicCode = "sic";
        public const string ServiceType = "ldv_servicetypecode";
        public const string OrganizationArabicName = "name";
        public const string OrganizationEnglishName = "ldv_name_ar";
        public const string OrganizationCountryId = "ldv_organizationcountryid";
        public const string LicenseNumber = "ldv_licensenumber";
        public const string TeamId = "ldv_teamid";
        public const string IsRepresentativeCompany = "ldv_isservicecompany";
        public const string ElmCompanyType = "ldv_elmcompanytypecode";
        public const string Email = "emailaddress1";
        public const string Address1Name = "address1_name";
        public const string Address2Name = "address2_name";
    }
}
