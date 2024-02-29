// *********************************************************************
// Created by : Latebound Constants Generator 1.2023.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://mohudev.crm4.dynamics.com
// Filename   : C:\Users\Hossam.Moustafa\Desktop\MOHU Text Note Bad\Category.cs
// Created    : 2024-02-26 14:48:34
// *********************************************************************
namespace MOHU.Integration.Domain.Entitiy
{
    /// <summary>OwnershipType: UserOwned, IntroducedVersion: 8.1.0.0</summary>
    public partial  class Category
    {
        public const string EntityName = "category";
        public const string EntityCollectionName = "categories ";


        public const string EntityLogicalName = "category";


        public static  class  Fields
        {

            #region Attributes

            /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
            public const string PrimaryKey = "categoryid";
            /// <summary>Type: String, RequiredLevel: SystemRequired, MaxLength: 155, Format: Text</summary>
            public const string PrimaryName = "title";
            /// <summary>Type: String, RequiredLevel: SystemRequired, MaxLength: 4000, Format: Text</summary>
            public const string CategoryNumber = "categorynumber";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: transactioncurrency</summary>
            public const string Currency = "transactioncurrencyid";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string Description = "description";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
            public const string DisplayOrder = "sequencenumber";
            /// <summary>Type: Decimal, RequiredLevel: None, MinValue: 0.000000000001, MaxValue: 100000000000, Precision: 12</summary>
            public const string ExchangeRate = "exchangerate";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string ImportSequenceNumber = "importsequencenumber";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: category</summary>
            public const string ParentCategory = "parentcategoryid";
          
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string RecordCreatedOn = "overriddencreatedon";

            public const string EnglishName = "ldv_englishname";
            public const string ArabicName = "ldv_arabicname";

            public const string ShowOnPortal = "ldv_isshowonportal";

            /// lookup 

            public const string TicketTypeid = "ldv_tickettypeid";

            // lookup 

            public const string SubCategory = "ldv_subcategoryid";


            #endregion Attributes

        }

        #region Relationships

        /// <summary>Parent: "Currency" Child: "Category" Lookup: "Currency"</summary>
        public const string RelM1_CategoryCurrency = "transactioncurrency_category";
        /// <summary>Entity 1: "KnowledgeArticle" Entity 2: "Category"</summary>
        public const string RelMM_knowledgearticle_category = "knowledgearticle_category";
        /// <summary>Parent: "Category" Child: "Category" Lookup: "ParentCategory"</summary>
        public const string Rel1M_CategoryParentCategory = "category_parent_category";
        /// <summary>Parent: "Category" Child: "TicketCategory" Lookup: "Category"</summary>
        public const string Rel1M_TicketCategoryCategory = "ldv_category_casecategory_Categoryid";
        /// <summary>Parent: "Category" Child: "CategoryFields" Lookup: ""</summary>
        public const string Rel1M_ldv_category_categoryfields_categoryid = "ldv_category_categoryfields_categoryid";

        #endregion Relationships
    }
}
