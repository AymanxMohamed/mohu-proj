// *********************************************************************
// Created by : Latebound Constants Generator 1.2023.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://mohudev.crm4.dynamics.com
// Filename   : C:\Users\Hossam.Moustafa\Desktop\MOHU Text Note Bad\TicketCategory.cs
// Created    : 2024-02-26 14:48:34
// *********************************************************************
namespace MOHU.Integration.Domain.Entitiy
{
    /// <summary>DisplayName: Ticket Category, OwnershipType: UserOwned, IntroducedVersion: 1.0</summary>
    public partial  class TicketCategory
    {
        public const string EntityName = "ldv_casecategory";
        public const string EntityCollectionName = "ldv_casecategories";

        public  static class Fields
        {
            #region Attributes

            /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
            public const string PrimaryKey = "ldv_casecategoryid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 450, Format: Text</summary>
            public const string PrimaryName = "ldv_name";
            /// <summary>Type: Lookup, RequiredLevel: None</summary>
            public const string _draft_CustomerType = "ldv_customertypeid";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string _drafts_HiddenFields = "ldv_hiddenfields";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ArabicDescription = "ldv_arabicdescription";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 800, Format: Text</summary>
            public const string ArabicName = "ldv_arabicname";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_department</summary>
            public const string BankDepartment = "ldv_bankdepartmentid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Beneficiary type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Beneficiarytype = "ldv_beneficiarytypecode";
            /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Case Type, OptionSetType: Picklist</summary>
            public const string CaseType = "ldv_casetypecode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: category</summary>
            public const string Category = "ldv_categoryid";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ClosureReason2 = "ldv_closurereasons";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
            public const string Code = "ldv_code";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Complain Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ComplainType = "ldv_complaintypecode";
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_department</summary>
            public const string ConcernedDepartment = "ldv_concerneddepartmentid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: CustomerType, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string CustomerType = "ldv_customertypecode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string EnglishDescription = "ldv_englishdescription";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
            public const string EnglishName = "ldv_englishname";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string ErrorMessageofMandatoryFieldofOptionalField = "ldv_errormessageofandatoryfieldofoptionalfie";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser,team</summary>
            public const string EscalationLevel1 = "ldv_escalationlevel1id";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser,team</summary>
            public const string EscalationLevel2 = "ldv_escalationlevel2id";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser,team</summary>
            public const string EscalationLevel3 = "ldv_escalationlevel3id";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 9000</summary>
            public const string HideFieldsSchemaName = "ldv_hidefieldsschemaname";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string ImportSequenceNumber = "importsequencenumber";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsCancellableByCustomer = "ldv_iscancellablebycustomer";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsFCR = "ldv_isfcr";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsSendEscalationL1 = "ldv_issendescalationl1";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsSendEscalationL2 = "ldv_issendescalationl2";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsSendEscalationL3 = "ldv_issendescalationl3";
          
            public const string Location = "ldv_locationcode";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string Mandatory = "ldv_ismandatory";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string MandatoryFieldofOptionalFields = "ldv_mandatoryfieldofoptionalfields";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_casecategory</summary>
            public const string ParentCategory = "ldv_parentcategoryid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Priority, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Priority = "ldv_prioritycode";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string RecordCreatedOn = "overriddencreatedon";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_department</summary>
            public const string SAMA_ConcernedDepartment = "ldv_samaconcerneddepartment";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Season, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Season = "ldv_seasoncode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Severity, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Severity = "ldv_severitycode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 9000</summary>
            public const string ShowFieldsSchemaName = "ldv_showfieldsschemaname";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ShowOnMerchantPortal = "ldv_showonmerchantportal";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ShowOnPortal = "ldv_isshowonportal";
           
            /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
            public const string Status = "statecode";
            /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
            public const string StatusReason = "statuscode";
           
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_casecategory</summary>
            public const string SubCategory = "ldv_subcategoryid";
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_service</summary>
            public const string TicketType = "ldv_tickettypeid";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            /// <summary>Type: BigInt, RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>
            public const string VersionNumber = "versionnumber";

            #endregion Attributes
        }



        #region Relationships

        /// <summary>Parent: "Category" Child: "TicketCategory" Lookup: "Category"</summary>
        public const string RelM1_TicketCategoryCategory = "ldv_category_casecategory_Categoryid";
        /// <summary>Parent: "Department" Child: "TicketCategory" Lookup: "BankDepartment"</summary>
        public const string RelM1_TicketCategoryBankDepartment = "ldv_department_ldv_casecategory_bankdepartmentid";
        /// <summary>Parent: "Department" Child: "TicketCategory" Lookup: "ConcernedDepartment"</summary>
        public const string RelM1_TicketCategoryConcernedDepartment = "ldv_ldv_department_ldv_casecategory_concerneddepartmentid";
        /// <summary>Parent: "Department" Child: "TicketCategory" Lookup: "SAMA_ConcernedDepartment"</summary>
        public const string RelM1_TicketCategorySAMA_ConcernedDepartment = "ldv_ldv_department_ldv_casecategory_SAMAConcernedDepartment";
        /// <summary>Parent: "ServiceDefinition" Child: "TicketCategory" Lookup: "TicketType"</summary>
        public const string RelM1_TicketCategoryTicketType = "ldv_service_ldv_casecategory_TicketTypeid";
        /// <summary>Parent: "TicketCategory" Child: "TicketCategory" Lookup: "ParentCategory"</summary>
        public const string Rel1M_TicketCategoryParentCategory = "ldv_casecategory_casecategory_parentcategoryid";
        /// <summary>Parent: "TicketCategory" Child: "TicketCategory" Lookup: "SubCategory"</summary>
        public const string Rel1M_TicketCategorySubCategory = "ldv_casecategory_ldv_casecategory_SubCategoryid";
        /// <summary>Parent: "TicketCategory" Child: "FieldSharing" Lookup: ""</summary>
        public const string Rel1M_ldv_casecategory_PrincipalObjectAttributeAccesses = "ldv_casecategory_PrincipalObjectAttributeAccesses";
        /// <summary>Parent: "TicketCategory" Child: "Note" Lookup: ""</summary>
        public const string Rel1M_ldv_casecategory_Annotations = "ldv_casecategory_Annotations";
        /// <summary>Parent: "TicketCategory" Child: "CategoryFields" Lookup: ""</summary>
        public const string Rel1M_ldv_casecategory_categoryfields_subcategoryid = "ldv_casecategory_categoryfields_subcategoryid";
        /// <summary>Parent: "TicketCategory" Child: "DocumentSetting" Lookup: ""</summary>
        public const string Rel1M_ldv_casecategory_documentsetting_ticketcategoryid = "ldv_casecategory_documentsetting_ticketcategoryid";
        /// <summary>Parent: "TicketCategory" Child: "DocumentSetting" Lookup: ""</summary>
        public const string Rel1M_ldv_casecategory_documentsetting_ticketsubcategoryid = "ldv_casecategory_documentsetting_ticketsubcategoryid";
        /// <summary>Parent: "TicketCategory" Child: "CaseManagement" Lookup: "MainCategory"</summary>
        public const string Rel1M_CaseManagementMainCategory = "ldv_casecategory_incident_MainCategoryid";
        /// <summary>Parent: "TicketCategory" Child: "CaseManagement" Lookup: "SubCategory"</summary>
        public const string Rel1M_CaseManagementSubCategory = "ldv_casecategory_incident_SubCategoryid";
        /// <summary>Parent: "TicketCategory" Child: "CaseManagement" Lookup: "SecondarySubCategory"</summary>
        public const string Rel1M_CaseManagementSecondarySubCategory = "ldv_casecategory_incident_SecondarySubCategoryid";

        #endregion Relationships

        #region OptionSets

        public enum Beneficiarytype_OptionSet
        {
            InternalHajj = 1,
            ExternalHajj = 2
        }
        public enum CaseType_OptionSet
        {
            Inquiry = 1,
            Feedback = 2,
            Complaint = 3,
            Request = 4
        }
        public enum ComplainType_OptionSet
        {
            Momentary = 1,
            NotMomentary = 2
        }
        public enum CustomerType_OptionSet
        {
            Individual = 1,
            Merchant = 2,
            Both = 3
        }
      
        public enum Location_OptionSet
        {
            Mekkah = 1,
            Madina = 2,
            BorderCrossings = 3,
            Arrafat = 4,
            Muzdalifa = 5,
            Mina = 6,
            Jada = 7
        }
        public enum Priority_OptionSet
        {
            High = 1,
            Medium = 2,
            Low = 3
        }
        public enum Season_OptionSet
        {
            Hajj = 1,
            Umrah = 2
        }
        public enum Severity_OptionSet
        {
            _45Days = 16,
            _3workingdays = 14,
            _10workingdays = 15,
            _45WorkingDays = 13,
            _5Days = 11,
            _21Days = 12,
            _1 = 1,
            _2 = 2,
            _3 = 3,
            _4 = 4,
            _5 = 5,
            _1Claim = 6,
            _2Claim = 7,
            _3Claim = 8,
            _4Claim = 9,
            _5Claim = 10
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

        #endregion OptionSets
    }
}
