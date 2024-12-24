// *********************************************************************
// Created by : Latebound Constants Generator 1.2023.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://mohudev.crm4.dynamics.com
// Filename   : C:\Users\Hossam.Moustafa\Desktop\Mohaj individual\Individual.cs
// Created    : 2024-02-22 16:00:37
// *********************************************************************

namespace MOHU.Integration.Domain.Entitiy
{
    /// <summary>OwnershipType: UserOwned, IntroducedVersion: 5.0.0.0</summary>
    /// 


    public partial  class Individual
    {
        public const string EntityName = "contact";
        public const string EntityCollectionName = "contacts";
        public const string EntityLogicalName = "contact";

        public static class Fields
        {


            #region Attributes

            /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
            public const string PrimaryKey = "contactid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 160, Format: Text</summary>
            public const string PrimaryName = "fullname";
            /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
            public const string DeprecatedProcessStage = "stageid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
            public const string DeprecatedTraversedPath = "traversedpath";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string AccessFailedCount = "adx_identity_accessfailedcount";
            /// <summary>Type: Lookup (Logical), RequiredLevel: None, Targets: account</summary>
            public const string Account = "accountid";
            /// <summary>Type: Memo (Logical), RequiredLevel: None, MaxLength: 1000</summary>
            public const string Address1 = "address1_composite";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 1: Address Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Address1AddressType = "address1_addresstypecode";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 80, Format: Text</summary>
            public const string Address1City = "address1_city";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 80, Format: Text</summary>
            public const string Address1Country_Region = "address1_country";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address1County = "address1_county";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address1Fax = "address1_fax";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 1: Freight Terms, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Address1FreightTerms = "address1_freighttermscode";
            /// <summary>Type: Uniqueidentifier (Logical), RequiredLevel: None</summary>
            public const string Address1ID = "address1_addressid";
            /// <summary>Type: Double (Logical), RequiredLevel: None, MinValue: -90, MaxValue: 90, Precision: 5</summary>
            public const string Address1Latitude = "address1_latitude";
            /// <summary>Type: Double (Logical), RequiredLevel: None, MinValue: -180, MaxValue: 180, Precision: 5</summary>
            public const string Address1Longitude = "address1_longitude";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 200, Format: Text</summary>
            public const string Address1Name = "address1_name";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address1Phone = "address1_telephone1";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 20, Format: Text</summary>
            public const string Address1PostOfficeBox = "address1_postofficebox";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Address1PrimaryContactName = "address1_primarycontactname";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 1: Shipping Method, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Address1ShippingMethod = "address1_shippingmethodcode";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address1State_Province = "address1_stateorprovince";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address1Street1 = "address1_line1";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address1Street2 = "address1_line2";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address1Street3 = "address1_line3";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address1Telephone2 = "address1_telephone2";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address1Telephone3 = "address1_telephone3";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 4, Format: Text</summary>
            public const string Address1UPSZone = "address1_upszone";
            /// <summary>Type: Integer (Logical), RequiredLevel: None, MinValue: -1500, MaxValue: 1500</summary>
            public const string Address1UTCOffset = "address1_utcoffset";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 20, Format: Text</summary>
            public const string Address1ZIP_PostalCode = "address1_postalcode";
            /// <summary>Type: Memo (Logical), RequiredLevel: None, MaxLength: 1000</summary>
            public const string Address2 = "address2_composite";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 2: Address Type, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string Address2AddressType = "address2_addresstypecode";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 80, Format: Text</summary>
            public const string Address2City = "address2_city";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 80, Format: Text</summary>
            public const string Address2Country_Region = "address2_country";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address2County = "address2_county";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address2Fax = "address2_fax";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 2: Freight Terms, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string Address2FreightTerms = "address2_freighttermscode";
            /// <summary>Type: Uniqueidentifier (Logical), RequiredLevel: None</summary>
            public const string Address2ID = "address2_addressid";
            /// <summary>Type: Double (Logical), RequiredLevel: None, MinValue: -90, MaxValue: 90, Precision: 5</summary>
            public const string Address2Latitude = "address2_latitude";
            /// <summary>Type: Double (Logical), RequiredLevel: None, MinValue: -180, MaxValue: 180, Precision: 5</summary>
            public const string Address2Longitude = "address2_longitude";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Address2Name = "address2_name";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 20, Format: Text</summary>
            public const string Address2PostOfficeBox = "address2_postofficebox";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Address2PrimaryContactName = "address2_primarycontactname";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 2: Shipping Method , OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string Address2ShippingMethod = "address2_shippingmethodcode";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address2State_Province = "address2_stateorprovince";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address2Street1 = "address2_line1";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address2Street2 = "address2_line2";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address2Street3 = "address2_line3";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address2Telephone1 = "address2_telephone1";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address2Telephone2 = "address2_telephone2";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address2Telephone3 = "address2_telephone3";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 4, Format: Text</summary>
            public const string Address2UPSZone = "address2_upszone";
            /// <summary>Type: Integer (Logical), RequiredLevel: None, MinValue: -1500, MaxValue: 1500</summary>
            public const string Address2UTCOffset = "address2_utcoffset";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 20, Format: Text</summary>
            public const string Address2ZIP_PostalCode = "address2_postalcode";
            /// <summary>Type: Memo (Logical), RequiredLevel: None, MaxLength: 1000</summary>
            public const string Address3 = "address3_composite";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 3: Address Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Address3AddressType = "address3_addresstypecode";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 80, Format: Text</summary>
            public const string Address3City = "address3_city";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address3County = "address3_county";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address3Fax = "address3_fax";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 3: Freight Terms, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Address3FreightTerms = "address3_freighttermscode";
            /// <summary>Type: Uniqueidentifier (Logical), RequiredLevel: None</summary>
            public const string Address3ID = "address3_addressid";
            /// <summary>Type: Double (Logical), RequiredLevel: None, MinValue: -90, MaxValue: 90, Precision: 5</summary>
            public const string Address3Latitude = "address3_latitude";
            /// <summary>Type: Double (Logical), RequiredLevel: None, MinValue: -180, MaxValue: 180, Precision: 5</summary>
            public const string Address3Longitude = "address3_longitude";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 200, Format: Text</summary>
            public const string Address3Name = "address3_name";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 20, Format: Text</summary>
            public const string Address3PostOfficeBox = "address3_postofficebox";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Address3PrimaryContactName = "address3_primarycontactname";
            /// <summary>Type: Picklist (Logical), RequiredLevel: None, DisplayName: Address 3: Shipping Method , OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Address3ShippingMethod = "address3_shippingmethodcode";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address3Telephone1 = "address3_telephone1";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address3Telephone2 = "address3_telephone2";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address3Telephone3 = "address3_telephone3";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 4, Format: Text</summary>
            public const string Address3UPSZone = "address3_upszone";
            /// <summary>Type: Integer (Logical), RequiredLevel: None, MinValue: -1500, MaxValue: 1500</summary>
            public const string Address3UTCOffset = "address3_utcoffset";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 80, Format: Text</summary>
            public const string Address3Country_Region = "address3_country";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Address3State_Province = "address3_stateorprovince";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address3Street1 = "address3_line1";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address3Street2 = "address3_line2";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string Address3Street3 = "address3_line3";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 20, Format: Text</summary>
            public const string Address3ZIP_PostalCode = "address3_postalcode";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
            public const string Aging30 = "aging30";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: -922337203685477, MaxValue: 922337203685477, Precision: 4, CalculationOf: aging30</summary>
            public const string Aging30Base = "aging30_base";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
            public const string Aging60 = "aging60";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: -922337203685477, MaxValue: 922337203685477, Precision: 4, CalculationOf: aging60</summary>
            public const string Aging60Base = "aging60_base";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
            public const string Aging90 = "aging90";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: -922337203685477, MaxValue: 922337203685477, Precision: 4, CalculationOf: aging90</summary>
            public const string Aging90Base = "aging90_base";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
            public const string Anniversary = "anniversary";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
            public const string AnnualIncome = "annualincome";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: -922337203685477, MaxValue: 922337203685477, Precision: 4, CalculationOf: annualincome</summary>
            public const string AnnualIncomeBase = "annualincome_base";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string ArabicName = "ldv_name_ar";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Assistant = "assistantname";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string AssistantPhone = "assistantphone";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string Auto_created = "isautocreate";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string BackOfficeCustomer = "isbackofficecustomer";
            /// <summary>Type: DateTime, RequiredLevel: ApplicationRequired, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
            public const string BirthDate = "birthdate";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string BorderNumber = "ldv_bordernumber";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 1073741823</summary>
            public const string BusinessCard = "businesscard";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string BusinessPhone = "telephone1";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string BusinessPhone2 = "business2";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: Text</summary>
            public const string BusinessCardAttributes = "businesscardattributes";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string CallbackNumber = "callback";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 255, Format: Text</summary>
            public const string ChildrensNames = "childrensnames";
            /// <summary>Type: Customer, RequiredLevel: None, Targets: account,contact</summary>
            public const string CompanyName = "parentcustomerid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string CompanyPhone = "company";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ConfirmRemovePassword = "adx_confirmremovepassword";
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_country</summary>
            public const string CountryofResidence = "ldv_countryofresidenceid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string CreatedByIPAddress = "adx_createdbyipaddress";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string CreatedByUsername = "adx_createdbyusername";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string CreditHold = "creditonhold";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: -922337203685477, MaxValue: 922337203685477, Precision: 4, CalculationOf: creditlimit</summary>
            public const string CreditLimitBase = "creditlimit_base";
            /// <summary>Type: Money, RequiredLevel: None, MinValue: 0, MaxValue: 100000000000000, Precision: 2</summary>
            public const string CreditLimit = "creditlimit";

            /// <summary>Type: Lookup, RequiredLevel: None, Targets: transactioncurrency</summary>
            public const string Currency = "transactioncurrencyid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Customer Size, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string CustomerSize = "customersizecode";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Decision influence label, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Decisioninfluencelabels = "msdyn_decisioninfluencetag";
            /// <summary>Type: String (Logical), RequiredLevel: SystemRequired, MaxLength: 100, Format: Text</summary>

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Department = "department";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string Description = "description";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DisableWebTracking = "msdyn_disablewebtracking";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DonotallowBulkEmails = "donotbulkemail";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DonotallowBulkMails = "donotbulkpostalmail";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DonotallowEmails = "donotemail";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DonotallowFaxes = "donotfax";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DonotallowMails = "donotpostalmail";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string DonotallowPhoneCalls = "donotphone";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            public const string Education = "educationcode";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
            public const string Email = "emailaddress1";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
            public const string EmailAddress2 = "emailaddress2";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
            public const string EmailAddress3 = "emailaddress3";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string EmailConfirmed = "adx_identity_emailaddress1confirmed";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Employee = "employeeid";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>
            public const string EntityImage = "entityimage";
            /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
            public const string EntityImageId = "entityimageid";
            /// <summary>Type: BigInt (Logical), RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>

            /// <summary>Type: Decimal, RequiredLevel: None, MinValue: 0.000000000001, MaxValue: 100000000000, Precision: 12</summary>
            public const string ExchangeRate = "exchangerate";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string ExternalUserIdentifier = "externaluseridentifier";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Fax = "fax";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 50, Format: Text</summary>
            public const string FirstName = "firstname";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
            public const string FollowEmailActivity = "followemail";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Url</summary>
            public const string FTPSite = "ftpsiteurl";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string GDPROptout = "msdyn_gdproptout";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Gender, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Gender = "gendercode";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Has Children, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string HasChildren = "haschildrencode";

            /// <summary>Type: DateTime, RequiredLevel: ApplicationRequired, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
            public const string HijriBirthDate = "ldv_hijribirthdate";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string HomePhone = "telephone2";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string HomePhone2 = "home2";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
            public const string IDNumber = "ldv_idnumber";
            /// <summary>Type: Picklist, RequiredLevel: ApplicationRequired, DisplayName: ID Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string IDType = "ldv_idtypecode";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string ImportSequenceNumber = "importsequencenumber";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsAssistant = "msdyn_isassistantinorgchart";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsMinor = "msdyn_isminor";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsMinorwithParentalConsent = "msdyn_isminorwithparentalconsent";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string JobTitle = "jobtitle";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: msdyn_contactkpiitem</summary>
            public const string KPI = "msdyn_contactkpiid";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string LastDateIncludedinCampaign = "lastusedincampaign";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 50, Format: Text</summary>
            public const string LastName = "lastname";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string LastOnHoldTime = "lastonholdtime";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
            public const string LastSLAapplied = "slainvokedid";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string LastSuccessfulLogin = "adx_identity_lastsuccessfullogin";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>

            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Lead Source, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string LeadSource = "leadsourcecode";

            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string LocalLoginDisabled = "adx_identity_locallogindisabled";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string LockoutEnabled = "adx_identity_lockoutenabled";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string LockoutEndDate = "adx_identity_lockoutenddate";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string LoginEnabled = "adx_identity_logonenabled";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Manager = "managername";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string ManagerPhone = "managerphone";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
            public const string ManagingPartner = "msa_managingpartnerid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Marital Status, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string MaritalStatus = "familystatuscode";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string MarketingOnly = "marketingonly";

            /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
            public const string MasterID = "masterid";

            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string Merged = "merged";

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string MiddleName = "middlename";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 50, Format: Text</summary>
            public const string MobileNumber = "mobilephone";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string MobilePhoneConfirmed = "adx_identity_mobilephoneconfirmed";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string ModifiedByIPAddress = "adx_modifiedbyipaddress";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string ModifiedByUsername = "adx_modifiedbyusername";
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_country</summary>
            public const string Nationality = "ldv_nationalitycountryid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string NewPasswordInput = "adx_identity_newpassword";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Nickname = "nickname";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000</summary>
            public const string NoofChildren = "numberofchildren";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Not at Company Flag, OptionSetType: Picklist, DefaultFormValue: 0</summary>
            public const string NotatCompanyFlag = "msdyn_orgchangestatus";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string OnHoldTimeMinutes = "onholdtime";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 250, Format: Text</summary>
            public const string OrganizationName = "adx_organizationname";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: lead</summary>
            public const string OriginatingLead = "originatingleadid";

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Pager = "pager";
            /// <summary>Type: Lookup (Logical), RequiredLevel: None, Targets: contact</summary>
            public const string ParentContact = "parentcontactid";
            /// <summary>Type: EntityName, RequiredLevel: None</summary>
            public const string ParentCustomerType = "parentcustomeridtype";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>

            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ParticipatesinWorkflow = "participatesinworkflow";

            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 50, Format: Text</summary>
            public const string PassportNumber = "governmentid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 128, Format: Text</summary>
            public const string PasswordHash = "adx_identity_passwordhash";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Payment Terms, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string PaymentTerms = "paymenttermscode";

            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string PortalTermsAgreementDate = "msdyn_portaltermsagreementdate";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Day, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string PreferredDay = "preferredappointmentdaycode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: equipment</summary>
            public const string PreferredFacility_Equipment = "preferredequipmentid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Power Pages Languages, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string PreferredLanguage = "mspp_userpreferredlcid";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string PreferredLCIDDeprecated = "adx_preferredlcid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Method of Contact, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string PreferredMethodofContact = "preferredcontactmethodcode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: service</summary>
            public const string PreferredService = "preferredserviceid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Preferred Time, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string PreferredTime = "preferredappointmenttimecode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
            public const string PreferredUser = "preferredsystemuserid";

            /// <summary>Type: Lookup, RequiredLevel: None, Targets: pricelevel</summary>
            public const string PriceList = "defaultpricelevelid";
            /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
            public const string Process = "processid";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ProfileAlert = "adx_profilealert";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string ProfileAlertDate = "adx_profilealertdate";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4096</summary>
            public const string ProfileAlertInstructions = "adx_profilealertinstructions";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ProfileIsAnonymous = "adx_profileisanonymous";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string ProfileLastActivity = "adx_profilelastactivity";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string ProfileModifiedOn = "adx_profilemodifiedon";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 64000</summary>
            public const string PublicProfileCopy = "adx_publicprofilecopy";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string RecordCreatedOn = "overriddencreatedon";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Relationship Type, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string RelationshipType = "customertypecode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Role, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Role = "accountrolecode";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Salutation = "salutation";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string SecurityStamp = "adx_identity_securitystamp";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: msdyn_segment</summary>
            public const string SegmentId = "msdyn_segmentid";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string SendMarketingMaterials = "donotsendmm";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Shipping Method , OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string ShippingMethod = "shippingmethodcode";

            /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
            public const string SLA = "slaid";
            /// <summary>Type: String (Logical), RequiredLevel: None, MaxLength: 100, Format: Text</summary>

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string Spouse_PartnerName = "spousesname";

            /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
            public const string Status = "statecode";
            /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
            public const string StatusReason = "statuscode";

            /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
            public const string Subscription = "subscriptionid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 10, Format: Text</summary>
            public const string Suffix = "suffix";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string TeamsFollowed = "teamsfollowed";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 50, Format: Text</summary>
            public const string Telephone3 = "telephone3";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Territory, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string Territory = "territorycode";

            /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
            public const string TimeSpentbyme = "timespentbymeonemailandmeetings";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1500, MaxValue: 1500</summary>
            public const string TimeZone = "adx_timezone";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";

            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string TwoFactorEnabled = "adx_identity_twofactorenabled";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string UserName = "adx_identity_username";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            /// <summary>Type: BigInt, RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>
            public const string VersionNumber = "versionnumber";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string VisaNumber = "ldv_visanumber";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Url</summary>
            public const string Website = "websiteurl";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 150, Format: PhoneticGuide</summary>
            public const string YomiFirstName = "yomifirstname";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 450, Format: PhoneticGuide</summary>
            public const string YomiFullName = "yomifullname";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 150, Format: PhoneticGuide</summary>
            public const string YomiLastName = "yomilastname";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 150, Format: PhoneticGuide</summary>
            public const string YomiMiddleName = "yomimiddlename";

            public const string Age = "ldv_age";

            //ldv_mobilecountrycode

            public const string MobileCountryCode = "ldv_mobilecountrycode";


            #endregion Attributes  

        }

        #region Relationships

        /// <summary>Parent: "Currency" Child: "Individual" Lookup: "Currency"</summary>
        public const string RelM1_IndividualCurrency = "transactioncurrency_contact";
        /// <summary>Parent: "SLA" Child: "Individual" Lookup: "SLA"</summary>
        public const string RelM1_IndividualSLA = "manualsla_contact";
        /// <summary>Parent: "User" Child: "Individual" Lookup: "PreferredUser"</summary>
        public const string RelM1_IndividualPreferredUser = "system_user_contacts";
        /// <summary>Parent: "SLA" Child: "Individual" Lookup: "LastSLAapplied"</summary>
        public const string RelM1_IndividualLastSLAapplied = "sla_contact";
        /// <summary>Parent: "Company" Child: "Individual" Lookup: "CompanyName"</summary>
        public const string RelM1_IndividualCompanyName = "contact_customer_accounts";
        /// <summary>Parent: "ProcessStage" Child: "Individual" Lookup: "DeprecatedProcessStage"</summary>
        public const string RelM1_IndividualDeprecatedProcessStage = "processstage_contact";
        /// <summary>Parent: "Company" Child: "Individual" Lookup: "ManagingPartner"</summary>
        public const string RelM1_IndividualManagingPartner = "msa_contact_managingpartner";
        /// <summary>Parent: "Lead" Child: "Individual" Lookup: "OriginatingLead"</summary>
        public const string RelM1_IndividualOriginatingLead = "contact_originating_lead";
        /// <summary>Parent: "PriceList" Child: "Individual" Lookup: "PriceList"</summary>
        public const string RelM1_IndividualPriceList = "price_level_contacts";
        /// <summary>Parent: "Facility_Equipment" Child: "Individual" Lookup: "PreferredFacility_Equipment"</summary>
        public const string RelM1_IndividualPreferredFacility_Equipment = "equipment_contacts";
        /// <summary>Parent: "Service" Child: "Individual" Lookup: "PreferredService"</summary>
        public const string RelM1_IndividualPreferredService = "service_contacts";
        /// <summary>Parent: "ContactKPIItem" Child: "Individual" Lookup: "KPI"</summary>
        public const string RelM1_IndividualKPI = "msdyn_msdyn_contactkpiitem_contact_contactkpiid";
        /// <summary>Parent: "Segment" Child: "Individual" Lookup: "SegmentId"</summary>
        public const string RelM1_IndividualSegmentId = "msdyn_msdyn_segment_contact";
        /// <summary>Parent: "Country" Child: "Individual" Lookup: "Nationality"</summary>
        public const string RelM1_IndividualNationality = "ldv_ldv_country_contact_nationalitycountryid";
        /// <summary>Parent: "Country" Child: "Individual" Lookup: "CountryofResidence"</summary>
        public const string RelM1_IndividualCountryofResidence = "ldv_ldv_country_contact_countryofresidenceid";
        /// <summary>Entity 1: "Invitation" Entity 2: "Individual"</summary>
        public const string RelMM_adx_invitation_invitecontacts = "adx_invitation_invitecontacts";
        /// <summary>Entity 1: "Invitation" Entity 2: "Individual"</summary>
        public const string RelMM_adx_invitation_redeemedcontacts = "adx_invitation_redeemedcontacts";
        /// <summary>Entity 1: "SiteComponent" Entity 2: "Individual"</summary>
        public const string RelMM_powerpagecomponent_mspp_webrole_contact = "powerpagecomponent_mspp_webrole_contact";
        /// <summary>Entity 1: "MarketingList" Entity 2: "Individual"</summary>
        public const string RelMM_listcontact_association = "listcontact_association";
        /// <summary>Entity 1: "QuickCampaign" Entity 2: "Individual"</summary>
        public const string RelMM_BulkOperation_Contacts = "BulkOperation_Contacts";
        /// <summary>Entity 1: "CampaignActivity" Entity 2: "Individual"</summary>
        public const string RelMM_CampaignActivity_Contacts = "CampaignActivity_Contacts";
        /// <summary>Entity 1: "Invoice" Entity 2: "Individual"</summary>
        public const string RelMM_contactinvoices_association = "contactinvoices_association";
        /// <summary>Entity 1: "Order" Entity 2: "Individual"</summary>
        public const string RelMM_contactorders_association = "contactorders_association";
        /// <summary>Entity 1: "Quote" Entity 2: "Individual"</summary>
        public const string RelMM_contactquotes_association = "contactquotes_association";
        /// <summary>Parent: "Individual" Child: "Individual" Lookup: "MasterID"</summary>
        public const string Rel1M_IndividualMasterID = "contact_master_contact";
        /// <summary>Parent: "Individual" Child: "Individual" Lookup: "CompanyName"</summary>
        public const string Rel1M_IndividualCompanyName = "contact_customer_contacts";
        /// <summary>Parent: "Individual" Child: "FieldSharing" Lookup: ""</summary>
        public const string Rel1M_contact_principalobjectattributeaccess = "contact_principalobjectattributeaccess";
        /// <summary>Parent: "Individual" Child: "SLAKPIInstance" Lookup: ""</summary>
        public const string Rel1M_slakpiinstance_contact = "slakpiinstance_contact";
        /// <summary>Parent: "Individual" Child: "SocialActivity" Lookup: ""</summary>
        public const string Rel1M_socialactivity_postauthoraccount_contacts = "socialactivity_postauthoraccount_contacts";
        /// <summary>Parent: "Individual" Child: "Email" Lookup: ""</summary>
        public const string Rel1M_Contact_Email_EmailSender = "Contact_Email_EmailSender";
        /// <summary>Parent: "Individual" Child: "Connection" Lookup: ""</summary>
        public const string Rel1M_contact_connections2 = "contact_connections2";
        /// <summary>Parent: "Individual" Child: "ActivityParty" Lookup: ""</summary>
        public const string Rel1M_contact_activity_parties = "contact_activity_parties";
        /// <summary>Parent: "Individual" Child: "Note" Lookup: ""</summary>
        public const string Rel1M_Contact_Annotation = "Contact_Annotation";
        /// <summary>Parent: "Individual" Child: "Company" Lookup: ""</summary>
        public const string Rel1M_account_primary_contact = "account_primary_contact";
        /// <summary>Parent: "Individual" Child: "SocialProfile" Lookup: ""</summary>
        public const string Rel1M_Socialprofile_customer_contacts = "Socialprofile_customer_contacts";
        /// <summary>Parent: "Individual" Child: "SocialActivity" Lookup: ""</summary>
        public const string Rel1M_socialactivity_postauthor_contacts = "socialactivity_postauthor_contacts";
        /// <summary>Parent: "Individual" Child: "Connection" Lookup: ""</summary>
        public const string Rel1M_contact_connections1 = "contact_connections1";
        /// <summary>Parent: "Individual" Child: "Address" Lookup: ""</summary>
        public const string Rel1M_Contact_CustomerAddress = "Contact_CustomerAddress";
        /// <summary>Parent: "Individual" Child: "ExternalIdentity" Lookup: ""</summary>
        public const string Rel1M_adx_contact_externalidentity = "adx_contact_externalidentity";
        /// <summary>Parent: "Individual" Child: "Invitation" Lookup: ""</summary>
        public const string Rel1M_adx_invitation_invitecontact = "adx_invitation_invitecontact";
        /// <summary>Parent: "Individual" Child: "Invitation" Lookup: ""</summary>
        public const string Rel1M_adx_invitation_invitercontact = "adx_invitation_invitercontact";
        /// <summary>Parent: "Individual" Child: "Invitation" Lookup: ""</summary>
        public const string Rel1M_adx_invitation_redeemedContact = "adx_invitation_redeemedContact";
        /// <summary>Parent: "Individual" Child: "MultistepFormSession" Lookup: ""</summary>
        public const string Rel1M_adx_webformsession_contact = "adx_webformsession_contact";
        /// <summary>Parent: "Individual" Child: "Lead" Lookup: ""</summary>
        public const string Rel1M_lead_customer_contacts = "lead_customer_contacts";
        /// <summary>Parent: "Individual" Child: "Lead" Lookup: ""</summary>
        public const string Rel1M_lead_parent_contact = "lead_parent_contact";
        /// <summary>Parent: "Individual" Child: "BookableResource" Lookup: ""</summary>
        public const string Rel1M_contact_bookableresource_ContactId = "contact_bookableresource_ContactId";
        /// <summary>Parent: "Individual" Child: "CaseManagement" Lookup: ""</summary>
        public const string Rel1M_contact_as_responsible_contact = "contact_as_responsible_contact";
        /// <summary>Parent: "Individual" Child: "ContractLine" Lookup: ""</summary>
        public const string Rel1M_contractlineitem_customer_contacts = "contractlineitem_customer_contacts";
        /// <summary>Parent: "Individual" Child: "Contract" Lookup: ""</summary>
        public const string Rel1M_contract_billingcustomer_contacts = "contract_billingcustomer_contacts";
        /// <summary>Parent: "Individual" Child: "Contract" Lookup: ""</summary>
        public const string Rel1M_contract_customer_contacts = "contract_customer_contacts";
        /// <summary>Parent: "Individual" Child: "CaseManagement" Lookup: ""</summary>
        public const string Rel1M_incident_customer_contacts = "incident_customer_contacts";
        /// <summary>Parent: "Individual" Child: "CaseManagement" Lookup: ""</summary>
        public const string Rel1M_contact_as_primary_contact = "contact_as_primary_contact";
        /// <summary>Parent: "Individual" Child: "Entitlement" Lookup: ""</summary>
        public const string Rel1M_contact_entitlement_ContactId = "contact_entitlement_ContactId";
        /// <summary>Parent: "Individual" Child: "Entitlement" Lookup: ""</summary>
        public const string Rel1M_contact_entitlement_Customer = "contact_entitlement_Customer";
        /// <summary>Parent: "Individual" Child: "Invoice" Lookup: ""</summary>
        public const string Rel1M_invoice_customer_contacts = "invoice_customer_contacts";
        /// <summary>Parent: "Individual" Child: "Opportunity" Lookup: ""</summary>
        public const string Rel1M_opportunity_customer_contacts = "opportunity_customer_contacts";
        /// <summary>Parent: "Individual" Child: "Order" Lookup: ""</summary>
        public const string Rel1M_order_customer_contacts = "order_customer_contacts";
        /// <summary>Parent: "Individual" Child: "Quote" Lookup: ""</summary>
        public const string Rel1M_quote_customer_contacts = "quote_customer_contacts";
        /// <summary>Parent: "Individual" Child: "Opportunity" Lookup: ""</summary>
        public const string Rel1M_opportunity_parent_contact = "opportunity_parent_contact";
        /// <summary>Parent: "Individual" Child: "Playbook" Lookup: ""</summary>
        public const string Rel1M_msdyn_playbookinstance_contact = "msdyn_playbookinstance_contact";
        /// <summary>Parent: "Individual" Child: "TaggedRecord" Lookup: ""</summary>
        public const string Rel1M_msdyn_msdyn_taggedrecord_contact_msdyn_dynamicsrecordid = "msdyn_msdyn_taggedrecord_contact_msdyn_dynamicsrecordid";
        /// <summary>Parent: "Individual" Child: "MicrosoftOrgchartnodeentity" Lookup: ""</summary>
        public const string Rel1M_contact_msdyn_orgchartnode_msdyn_noderecord = "contact_msdyn_orgchartnode_msdyn_noderecord";
        /// <summary>Parent: "Individual" Child: "ContactKPIItem" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_contactkpiitem_contactid = "msdyn_contact_msdyn_contactkpiitem_contactid";
        /// <summary>Parent: "Individual" Child: "MostContacted" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_mostcontacted_regardingObjectId = "msdyn_contact_msdyn_mostcontacted_regardingObjectId";
        /// <summary>Parent: "Individual" Child: "MostContactedBy" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_mostcontactedby_regardingObjectId = "msdyn_contact_msdyn_mostcontactedby_regardingObjectId";
        /// <summary>Parent: "Individual" Child: "Dailykpisforcontact" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_dailycontactkpiitem_entityid = "msdyn_contact_dailycontactkpiitem_entityid";
        /// <summary>Parent: "Individual" Child: "PreferredAgent" Lookup: ""</summary>
        public const string Rel1M_msdyn_msdyn_preferredagent_contact_msdyn_recordId = "msdyn_msdyn_preferredagent_contact_msdyn_recordId";
        /// <summary>Parent: "Individual" Child: "Conversation" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_ocliveworkitem_Customer = "msdyn_contact_msdyn_ocliveworkitem_Customer";
        /// <summary>Parent: "Individual" Child: "OngoingconversationDeprecated" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_liveconversation_Customer = "msdyn_contact_msdyn_liveconversation_Customer";
        /// <summary>Parent: "Individual" Child: "ConversationParticipantInsights" Lookup: ""</summary>
        public const string Rel1M_msdyn_msdyn_conversationparticipantinsights_contact_msdyn_User = "msdyn_msdyn_conversationparticipantinsights_contact_msdyn_User";
        /// <summary>Parent: "Individual" Child: "SequenceTarget" Lookup: ""</summary>
        public const string Rel1M_msdyn_sequencetarget_contact_msdyn_target = "msdyn_sequencetarget_contact_msdyn_target";
        /// <summary>Parent: "Individual" Child: "salesroutingdiagnostic" Lookup: ""</summary>
        public const string Rel1M_msdyn_salesroutingdiagnostic_contact_msdyn_target = "msdyn_salesroutingdiagnostic_contact_msdyn_target";
        /// <summary>Parent: "Individual" Child: "sabackupdiagnostic" Lookup: ""</summary>
        public const string Rel1M_msdyn_sabackupdiagnostic_contact_msdyn_target = "msdyn_sabackupdiagnostic_contact_msdyn_target";
        /// <summary>Parent: "Individual" Child: "Insight" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_salessuggestion = "msdyn_contact_msdyn_salessuggestion";
        /// <summary>Parent: "Individual" Child: "LinkedEntityAttributeValidity" Lookup: ""</summary>
        public const string Rel1M_msdyn_linkeditemvalidity_polymorphic_contactid = "msdyn_linkeditemvalidity_polymorphic_contactid";
        /// <summary>Parent: "Individual" Child: "Actual" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_actual_ContactCustomer = "msdyn_contact_msdyn_actual_ContactCustomer";
        /// <summary>Parent: "Individual" Child: "Actual" Lookup: ""</summary>
        public const string Rel1M_msdyn_contact_msdyn_actual_ContactVendor = "msdyn_contact_msdyn_actual_ContactVendor";
        /// <summary>Parent: "Individual" Child: "ApplicationActionLog" Lookup: ""</summary>
        public const string Rel1M_ldv_ldv_applicationactionlog_contact_ldv_ActionTakenById = "ldv_ldv_applicationactionlog_contact_ldv_ActionTakenById";
        /// <summary>Parent: "Individual" Child: "Al_Mutamir" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_ldv_almutamir_997 = "ldv_contact_ldv_almutamir_997";
        /// <summary>Parent: "Individual" Child: "Hajj" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_ldv_hajj_63 = "ldv_contact_ldv_hajj_63";
        /// <summary>Parent: "Individual" Child: "BookAnElectricCar" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_ldv_bookanelectriccar_152 = "ldv_contact_ldv_bookanelectriccar_152";
        /// <summary>Parent: "Individual" Child: "Permits" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_ldv_permits_Customer = "ldv_contact_ldv_permits_Customer";
        /// <summary>Parent: "Individual" Child: "ApplicationHeader" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_ldv_applicationheader_contactid = "ldv_contact_ldv_applicationheader_contactid";
        /// <summary>Parent: "Individual" Child: "ApplicationHeader" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_applicationheader_Customer = "ldv_contact_applicationheader_Customer";
        /// <summary>Parent: "Individual" Child: "SalesCopilotInsight" Lookup: ""</summary>
        public const string Rel1M_msdyn_msdyn_salescopilotinsight_contact_msdyn_targetentityid = "msdyn_msdyn_salescopilotinsight_contact_msdyn_targetentityid";
        /// <summary>Parent: "Individual" Child: "QueueItem" Lookup: ""</summary>
        public const string Rel1M_ldv_contact_queueitem_Customer = "ldv_contact_queueitem_Customer";
        /// <summary>Entity 1: "Individual" Entity 2: "Lead"</summary>
        public const string RelMM_contactleads_association = "contactleads_association";
        /// <summary>Entity 1: "Individual" Entity 2: "Entitlement"</summary>
        public const string RelMM_entitlementcontacts_association = "entitlementcontacts_association";
        /// <summary>Entity 1: "Individual" Entity 2: "Contract"</summary>
        public const string RelMM_servicecontractcontacts_association = "servicecontractcontacts_association";

        #endregion Relationships

        #region OptionSets

        public enum Address1AddressType_OptionSet
        {
            BillTo = 1,
            ShipTo = 2,
            Primary = 3,
            Other = 4
        }
        public enum Address1FreightTerms_OptionSet
        {
            FOB = 1,
            NoCharge = 2
        }
        public enum Address1ShippingMethod_OptionSet
        {
            Airborne = 1,
            DHL = 2,
            FedEx = 3,
            UPS = 4,
            PostalMail = 5,
            FullLoad = 6,
            WillCall = 7
        }
        public enum Address2AddressType_OptionSet
        {
            DefaultValue = 1
        }
        public enum Address2FreightTerms_OptionSet
        {
            DefaultValue = 1
        }
        public enum Address2ShippingMethod_OptionSet
        {
            DefaultValue = 1
        }
        public enum Address3AddressType_OptionSet
        {
            DefaultValue = 1
        }
        public enum Address3FreightTerms_OptionSet
        {
            DefaultValue = 1
        }
        public enum Address3ShippingMethod_OptionSet
        {
            DefaultValue = 1
        }
        public enum CustomerSize_OptionSet
        {
            DefaultValue = 1
        }
        public enum Decisioninfluencelabels_OptionSet
        {
            Decisionmaker = 0,
            Influencer = 1,
            Blocker = 2,
            Unknown = 3
        }
        public enum Education_OptionSet
        {
            DefaultValue = 1
        }
        public enum Gender_OptionSet
        {
            Male = 1,
            Female = 2
        }
        public enum HasChildren_OptionSet
        {
            DefaultValue = 1
        }
        public enum IDType_OptionSet
        {
            NationalIdentity = 1,
            Accommodation = 2,
            Gulfcitizen = 3,
            Passportinthecaseofanon_citizenorresident = 4
        }
        public enum LeadSource_OptionSet
        {
            DefaultValue = 1
        }
        public enum MaritalStatus_OptionSet
        {
            Single = 1,
            Married = 2,
            Divorced = 3,
            Widowed = 4
        }
        public enum NotatCompanyFlag_OptionSet
        {
            NoFeedback = 0,
            NotatCompany = 1,
            Ignore = 2
        }
        public enum ParentCustomerType_OptionSet
        {
        }
        public enum PaymentTerms_OptionSet
        {
            Net30 = 1,
            _210Net30 = 2,
            Net45 = 3,
            Net60 = 4
        }
        public enum PreferredDay_OptionSet
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }
        public enum PreferredLanguage_OptionSet
        {
            Arabic = 1025,
            Basque_Basque = 1069,
            Bulgarian_Bulgaria = 1026,
            Catalan_Catalan = 1027,
            Chinese_China = 2052,
            Chinese_HongKongSAR = 3076,
            Chinese_Traditional = 1028,
            Croatian_Croatia = 1050,
            Czech_CzechRepublic = 1029,
            Danish_Denmark = 1030,
            Dutch_Netherlands = 1043,
            English = 1033,
            Estonian_Estonia = 1061,
            Finnish_Finland = 1035,
            French_France = 1036,
            Galician_Spain = 1110,
            German_Germany = 1031,
            Greek_Greece = 1032,
            Hebrew = 1037,
            Hindi_India = 1081,
            Hungarian_Hungary = 1038,
            Indonesian_Indonesia = 1057,
            Italian_Italy = 1040,
            Japanese_Japan = 1041,
            Kazakh_Kazakhstan = 1087,
            Korean_Korea = 1042,
            Latvian_Latvia = 1062,
            Lithuanian_Lithuania = 1063,
            Malay_Malaysia = 1086,
            NorwegianBokmal_Norway = 1044,
            Polish_Poland = 1045,
            Portuguese_Brazil = 1046,
            Portuguese_Portugal = 2070,
            Romanian_Romania = 1048,
            Russian_Russia = 1049,
            SerbianCyrillic_Serbia = 3098,
            SerbianLatin_Serbia = 2074,
            Slovak_Slovakia = 1051,
            Slovenian_Slovenia = 1060,
            SpanishTraditionalSort_Spain = 3082,
            Swedish_Sweden = 1053,
            Thai_Thailand = 1054,
            Turkish_Turkiye = 1055,
            Ukrainian_Ukraine = 1058,
            Vietnamese_Vietnam = 1066
        }
        public enum PreferredMethodofContact_OptionSet
        {
            Any = 1,
            Email = 2,
            Phone = 3,
            Fax = 4,
            Mail = 5
        }
        public enum PreferredTime_OptionSet
        {
            Morning = 1,
            Afternoon = 2,
            Evening = 3
        }
        public enum RelationshipType_OptionSet
        {
            DefaultValue = 1
        }
        public enum Role_OptionSet
        {
            DecisionMaker = 1,
            Employee = 2,
            Influencer = 3
        }
        public enum ShippingMethod_OptionSet
        {
            DefaultValue = 1
        }
        public enum Status_OptionSet
        {
            Active = 0,
            Inactive = 1
        }
        public enum StatusReason_OptionSet
        {
            Active = 1,
            Inactive = 2
        }
        public enum Territory_OptionSet
        {
            DefaultValue = 1
        }

        #endregion OptionSets



    }
}
