// *********************************************************************
// Created by : Latebound Constants Generator 1.2023.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://mohudev.crm4.dynamics.com
// Filename   : C:\Users\Hossam.Moustafa\Desktop\MOHU Text Note Bad\ServiceDefinition.cs
// Created    : 2024-02-26 14:48:34
// *********************************************************************
namespace MOHU.Integration.Domain.Entitiy
{
    /// <summary>DisplayName: Service Definition, OwnershipType: OrganizationOwned, IntroducedVersion: 1.0.0.0</summary>
    public partial class RequestType
    {
        public const string EntityName = "ldv_service";
        public const string EntityCollectionName = "ldv_services";
        public const string EntityLogicalName = "ldv_service";

        public static class Fields
        {
            #region Attributes

            /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
            public const string PrimaryKey = "ldv_serviceid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 400, Format: Text</summary>
            public const string PrimaryName = "ldv_name";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: processstage</summary>
            public const string _2ndStage = "ldv_processstageid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: queue</summary>
            public const string CancellationQueue = "ldv_queueid";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
            public const string Code = "ldv_code";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Text</summary>
            public const string DocumentSettingIdsEmirateReply = "ldv_documentsettingids";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string EntitySchemaName = "ldv_entityschemaname";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser,team</summary>
            public const string EscalationLevel1 = "ldv_escalationlevel1id";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser,team</summary>
            public const string EscalationLevel2 = "ldv_escalationlevel2id";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser,team</summary>
            public const string EscalationLevel3 = "ldv_escalationlevel3id";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string ImportSequenceNumber = "importsequencenumber";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string InstantApproval = "ldv_isinstantapproval";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsRequirePayment = "ldv_isrequiredpayment";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string LastOnHoldTime = "lastonholdtime";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
            public const string LastSLAApplied = "slainvokedid";
          
           
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string OnHoldTimeMinutes = "onholdtime";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: organization</summary>
            public const string OrganizationId = "organizationid";
           
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string PaymentCode = "ldv_paymentcode";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string PortalURL = "ldv_portalurl";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: workflow</summary>
            public const string Process = "ldv_processid";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string RecordCreatedOn = "overriddencreatedon";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 50000</summary>
            public const string RegulationsAr = "ldv_regulationsar";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 50000</summary>
            public const string RegulationsEn = "ldv_regulationsen";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
            public const string ServiceArabicName = "ldv_name_ar";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_servicecatalog</summary>
            public const string servicecatalog = "ldv_servicecatalog";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_servicecategory</summary>
            public const string ServiceCategory = "ldv_servicecategoryid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Service Definition Classification, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ServiceDefinitionClassification = "ldv_servicedefinistioncalssification";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
            public const string ServiceDescription = "ldv_servicedescription";
            /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 100, Format: Text</summary>
            public const string ServiceEnglishName = "ldv_name_en";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
            public const string ServiceOrderInServiceCatalogView = "ldv_serviceorderinservicecatalogview";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string serviceprerequisites = "ldv_serviceprerequisites";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ServiceRequiredDocuments = "ldv_servicerequireddocuments";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ServiceRequiredDocuments_Ar = "ldv_servicerequireddocuments_ar";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string ServiceSLAAr = "ldv_servicesla_ar";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string ServiceSLAEn = "ldv_servicesla_en";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Service Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ServiceType = "ldv_servicetypecode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
            public const string servicedescription_ar = "ldv_servicedescription_ar";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string serviceprerequisites_ar = "ldv_serviceprerequisites_ar";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ShowonPortal = "ldv_showonportal";
            /// <summary>Type: Lookup, RequiredLevel: Recommended, Targets: sla</summary>
            public const string SLA = "slaid";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
            public const string SLADuration = "ldv_sladuration";
           
            /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
            public const string Status = "statecode";
            /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
            public const string StatusReason = "statuscode";
           
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            /// <summary>Type: BigInt, RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>
            public const string VersionNumber = "versionnumber";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 500, Format: Text</summary>
            public const string YoutubeUrl = "ldv_youtubeurl";

            #endregion Attributes
        }



        #region Relationships

        /// <summary>Parent: "Organization" Child: "ServiceDefinition" Lookup: "OrganizationId"</summary>
        public const string RelM1_ServiceDefinitionOrganizationId = "organization_ldv_service";
        /// <summary>Parent: "SLA" Child: "ServiceDefinition" Lookup: "SLA"</summary>
        public const string RelM1_ServiceDefinitionSLA = "manualsla_ldv_service";
        /// <summary>Parent: "SLA" Child: "ServiceDefinition" Lookup: "LastSLAApplied"</summary>
        public const string RelM1_ServiceDefinitionLastSLAApplied = "sla_ldv_service";
        /// <summary>Parent: "ServiceCatalog" Child: "ServiceDefinition" Lookup: "servicecatalog"</summary>
        public const string RelM1_ServiceDefinitionservicecatalog = "ldv_ldv_servicecatalog_ldv_service_servicecatalog";
        /// <summary>Parent: "ServiceCategory" Child: "ServiceDefinition" Lookup: "ServiceCategory"</summary>
        public const string RelM1_ServiceDefinitionServiceCategory = "ldv_ldv_servicecategory_ldv_service_ServiceCategoryId";
        /// <summary>Parent: "Queue" Child: "ServiceDefinition" Lookup: "CancellationQueue"</summary>
        public const string RelM1_ServiceDefinitionCancellationQueue = "ldv_queue_ldv_service_queueid";
        /// <summary>Parent: "Process" Child: "ServiceDefinition" Lookup: "Process"</summary>
        public const string RelM1_ServiceDefinitionProcess = "ldv_workflow_ldv_service_processid";
        /// <summary>Parent: "User" Child: "ServiceDefinition" Lookup: "EscalationLevel1"</summary>
        public const string RelM1_ServiceDefinitionEscalationLevel1 = "ldv_ldv_service_systemuser_ldv_EscalationLevel1Id";
        /// <summary>Parent: "Team" Child: "ServiceDefinition" Lookup: "EscalationLevel1"</summary>
        public const string RelM1_ServiceDefinitionEscalationLevel11 = "ldv_ldv_service_team_ldv_EscalationLevel1Id";
        /// <summary>Parent: "User" Child: "ServiceDefinition" Lookup: "EscalationLevel2"</summary>
        public const string RelM1_ServiceDefinitionEscalationLevel2 = "ldv_ldv_service_systemuser_ldv_EscalationLevel2Id";
        /// <summary>Parent: "Team" Child: "ServiceDefinition" Lookup: "EscalationLevel2"</summary>
        public const string RelM1_ServiceDefinitionEscalationLevel21 = "ldv_ldv_service_team_ldv_EscalationLevel2Id";
        /// <summary>Parent: "User" Child: "ServiceDefinition" Lookup: "EscalationLevel3"</summary>
        public const string RelM1_ServiceDefinitionEscalationLevel3 = "ldv_ldv_service_systemuser_ldv_EscalationLevel3Id";
        /// <summary>Parent: "Team" Child: "ServiceDefinition" Lookup: "EscalationLevel3"</summary>
        public const string RelM1_ServiceDefinitionEscalationLevel31 = "ldv_ldv_service_team_ldv_EscalationLevel3Id";
        /// <summary>Parent: "ProcessStage" Child: "ServiceDefinition" Lookup: "_2ndStage"</summary>
        public const string RelM1_ServiceDefinition2ndStage = "ldv_processstage_ldv_service_ProcessStageId";
        /// <summary>Entity 1: "ServiceCatalog" Entity 2: "ServiceDefinition"</summary>
        public const string RelMM_ldv_ldv_servicecatalog_ldv_service = "ldv_ldv_servicecatalog_ldv_service";
        /// <summary>Entity 1: "StageConfiguration" Entity 2: "ServiceDefinition"</summary>
        public const string RelMM_ldv_ldv_stageconfiguration_ldv_service = "ldv_ldv_stageconfiguration_ldv_service";
        /// <summary>Parent: "ServiceDefinition" Child: "FieldSharing" Lookup: ""</summary>
        public const string Rel1M_ldv_service_PrincipalObjectAttributeAccesses = "ldv_service_PrincipalObjectAttributeAccesses";
        /// <summary>Parent: "ServiceDefinition" Child: "Note" Lookup: ""</summary>
        public const string Rel1M_ldv_service_Annotations = "ldv_service_Annotations";
        /// <summary>Parent: "ServiceDefinition" Child: "SLAKPIInstance" Lookup: ""</summary>
        public const string Rel1M_ldv_service_SLAKPIInstances = "ldv_service_SLAKPIInstances";
        /// <summary>Parent: "ServiceDefinition" Child: "ApplicationHeader" Lookup: ""</summary>
        public const string Rel1M_ldv_ldv_service_ldv_applicationheader_serviceid = "ldv_ldv_service_ldv_applicationheader_serviceid";
        /// <summary>Parent: "ServiceDefinition" Child: "StageConfiguration" Lookup: ""</summary>
        public const string Rel1M_ldv_ldv_service_ldv_stageconfiguration_serviceid = "ldv_ldv_service_ldv_stageconfiguration_serviceid";
        /// <summary>Parent: "ServiceDefinition" Child: "Task" Lookup: ""</summary>
        public const string Rel1M_ldv_ldv_service_task_servicedefinitionid = "ldv_ldv_service_task_servicedefinitionid";
        /// <summary>Parent: "ServiceDefinition" Child: "CaseManagement" Lookup: "RequestType"</summary>
        public const string Rel1M_CaseManagementRequestType = "ldv_service_incident_serviceid";
        /// <summary>Parent: "ServiceDefinition" Child: "DocumentSetting" Lookup: ""</summary>
        public const string Rel1M_ldv_ldv_service_ldv_documentsetting = "ldv_ldv_service_ldv_documentsetting";
        /// <summary>Parent: "ServiceDefinition" Child: "TicketCategory" Lookup: "TicketType"</summary>
        public const string Rel1M_TicketCategoryTicketType = "ldv_service_ldv_casecategory_TicketTypeid";
        /// <summary>Parent: "ServiceDefinition" Child: "Task" Lookup: ""</summary>
        public const string Rel1M_ldv_service_task_serviceid = "ldv_service_task_serviceid";
        /// <summary>Parent: "ServiceDefinition" Child: "QueueItem" Lookup: ""</summary>
        public const string Rel1M_ldv_service_queueitem_requesttypeid = "ldv_service_queueitem_requesttypeid";

        #endregion Relationships

        #region OptionSets

       
        public enum ServiceDefinitionClassification_OptionSet
        {
            BasicLicensingServices = 100000000,
            SupportServices = 100000001,
            Inspectionandcomplianceservices = 100000002,
            Sub_licensingservices = 100000003,
            Sub_licenserequests = 100000004,
            License = 100000005,
            Permit = 100000006,
            Certificate = 100000007
        }
        public enum ServiceType_OptionSet
        {
            MainLicensingServices = 1,
            SupportingLicensingServices = 2,
            CasesManagements = 3,
            InspectionandComplianceServices = 4
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
