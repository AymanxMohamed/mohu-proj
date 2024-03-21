// *********************************************************************
// Created by : Latebound Constants Generator 1.2023.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://mohudev.crm4.dynamics.com
// Filename   : C:\Users\Hossam.Moustafa\Desktop\Mohaj individual\CaseManagement.cs
// Created    : 2024-02-22 23:57:42
// *********************************************************************
namespace MOHU.Integration.Domain.Entitiy
{
    /// <summary>DisplayName: Case Management, OwnershipType: UserOwned, IntroducedVersion: 5.0.0.0</summary>
    public partial class CaseManagement
    {
        public const string EntityName = "incident";

        public const string EntityCollectionName = "incidents";

        public const string EntityLogicalName = "incident";

        public static  class Fields 
        {

            #region Attributes

            /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
            public const string PrimaryKey = "incidentid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Text</summary>
            public const string PrimaryName = "title";
            /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
            public const string DeprecatedStageId = "stageid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
            public const string DeprecatedTraversedPath = "traversedpath";
            /// <summary>Type: Lookup (Logical), RequiredLevel: None, Targets: account</summary>
            public const string Account = "accountid";
          
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string ActivitiesComplete = "activitiescomplete";
            
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000</summary>
            public const string ActualServiceUnits = "actualserviceunits";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string AgentClosurereason = "ldv_agentclosurereason";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string AgentEmployeeDecision = "ldv_agentemployeedecisioncode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_applicationheader</summary>
            public const string ApplicationHeader = "ldv_applicationheaderid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
            public const string AssigningUser = "ldv_assigninguserid";
            /// <summary>Type: Virtual, RequiredLevel: None</summary>
            public const string Attachments = "ldv_attachments";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Beneficiary type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Beneficiarytype = "ldv_beneficiarytypecode";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000</summary>
            public const string BilledServiceUnits = "billedserviceunits";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string BlockedProfile = "blockedprofile";
           
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: workflow</summary>
            public const string BPFName = "ldv_processid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: Text</summary>
            public const string CaseAge = "caseage";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Case Stage, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string CaseStage = "incidentstagecode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Case Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string CaseType = "casetypecode";
           
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string CastingOfficer_NeededInformation = "ldv_castingofficerneededinformation";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string CastingOfficerDecision = "ldv_castingofficerdecisioncode";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string CheckEmail = "checkemail";
          
            /// <summary>Type: Integer (Logical), RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
            public const string ChildCases = "numberofchildincidents";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string ClosureDate = "ldv_closuredate";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ClosureReason = "ldv_closurereason";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ClosureReason_2 = "ldv_closurereasons";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string CompaniesAdministration_NeededInformation = "ldv_companiesadministrationneededinformation";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string CompaniesAdministrationDecision = "ldv_companiesadministrationdecisioncode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string CompaniesService_NeededInformation = "ldv_companiesserviceneededinformation";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string CompaniesServiceDecision = "ldv_companiesservicedecisioncode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Priority, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ComplainPriority = "prioritycode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Service Level, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ComplainType = "contractservicelevelcode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_department</summary>
            public const string ConcernedDepartment = "ldv_concerneddepartmentid";
            /// <summary>Type: Lookup (Logical), RequiredLevel: None, Targets: contact</summary>
            public const string Contact = "contactid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
            public const string Contact1 = "primarycontactid";
          
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: contract</summary>
            public const string Contract = "contractid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: contractdetail</summary>
            public const string ContractLine = "contractdetailid";
          
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string CoordinationCouncil_NeededInformation = "ldv_coordinationcouncilneededinformation";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string CoordinationCouncilDecision = "ldv_coordinationcouncildecisioncode";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string CopilotEngaged = "msdyn_copilotengaged";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: transactioncurrency</summary>
            public const string Currency = "transactioncurrencyid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: task</summary>
            public const string CurrentTask = "ldv_currenttaskid";
            /// <summary>Type: Customer, RequiredLevel: SystemRequired, Targets: account,contact</summary>
            public const string Customer = "customerid";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string CustomerContacted = "customercontacted";
            /// <summary>Type: EntityName, RequiredLevel: ApplicationRequired</summary>
            public const string CustomerType = "customeridtype";
          
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string DeactivatedOn = "deactivatedon";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
            public const string DecrementEntitlementTerms = "decremententitlementterm";
           
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string Decrementing = "isdecrementing";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string DepartmentClosureReason = "ldv_departmentclosurereason";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string DepartmentDecision = "ldv_departmentdecisioncode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string DepartmentNeededInformation = "ldv_departmentneededinformation";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string Description = "description";
            /// <summary>Type: Memo, RequiredLevel: ApplicationRequired, MaxLength: 2000</summary>
            public const string Description1 = "ldv_description";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Email</summary>
            public const string EmailAddress = "emailaddress";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string Employee_NeededInformation = "ldv_employeeneededinformation";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: entitlement</summary>
            public const string Entitlement = "entitlementid";
           
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>
            public const string EntityImage = "entityimage";
            /// <summary>Type: BigInt (Logical), RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>
          
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_errorscode</summary>
            public const string ErrorCode = "ldv_errorcodeid";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string EscalatedOn = "escalatedon";
            /// <summary>Type: Decimal, RequiredLevel: None, MinValue: 0.000000000001, MaxValue: 100000000000, Precision: 12</summary>
            public const string ExchangeRate = "exchangerate";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: incident</summary>
            public const string ExistingCase = "existingcase";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string FirstResponseBy = "responseby";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: slakpiinstance</summary>
            public const string FirstResponseByKPI = "firstresponsebykpiid";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string FirstResponseSent = "firstresponsesent";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: slakpiinstance</summary>
            public const string FirstResponseSLA = "ldv_firstresponseslaid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: First Response SLA Status, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string FirstResponseSLAStatus = "firstresponseslastatus";
           
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string FollowUpBy = "followupby";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string FollowupTaskCreated = "followuptaskcreated";
          
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string ImportSequenceNumber = "importsequencenumber";
            
            /// <summary>Type: Double, RequiredLevel: None, MinValue: 0, MaxValue: 1000000000, Precision: 2</summary>
            public const string InfluenceScore = "influencescore";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string InternalUseOnly = "merged";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: msdyn_iotalert</summary>
            public const string IoTAlert = "msdyn_iotalert";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsEscalated = "isescalated";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsFCR = "ldv_isfcr";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsKadanaUpdated = "ldv_iskadanaupdated";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsServiceDeskUpdated = "ldv_isservicedeskupdated";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsSubmitted = "ldv_issubmitted";
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
            public const string IsTashirUpdated = "ldv_istashirupdated";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Is Classification Exist for Inquiry?, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string IsThereClassificationforInquiry = "ldv_isthereclassificationforinquirycode";
           
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string KadanaDecision = "ldv_kadanadecisioncode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string KadanaNeededInformation = "ldv_kadananeededinformation";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string kadanaResolution = "ldv_kadanaresolution";
            
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: kbarticle</summary>
            public const string KnowledgeBaseArticle = "kbarticleid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Text</summary>
            public const string LastInteraction = "lastinteraction";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string LastOnHoldTime = "lastonholdtime";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
            public const string LastSLAapplied = "slainvokedid";
            /// <summary>Type: Virtual (Logical), RequiredLevel: None</summary>
          
            /// <summary>Type: Picklist, RequiredLevel: ApplicationRequired, DisplayName: Location, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Location = "ldv_locationcode";
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_casecategory</summary>
            public const string MainCategory = "ldv_maincategoryid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: incident</summary>
            public const string MasterCase = "masterid";
           
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string MinistrysHajjAgencyDecisison = "ldv_ministryshajjagencydecisisoncode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string MinistrysHajjAgency_NeededInformation = "ldv_ministryshajjagencyneededinformation";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string MinistrysUmarahAgency_NeededInformation = "ldv_ministrysumarahagencyneededinformation";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string MinistrysUmarahAgencyDecision = "ldv_ministrysumarahagencydecisisoncode";
            
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string NeededInformation = "ldv_neededinformation";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 400, Format: Text</summary>
            public const string NextSla = "nextsla";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
            public const string OnHoldTimeMinutes = "onholdtime";
            /// <summary>Type: Picklist, RequiredLevel: ApplicationRequired, DisplayName: Case Origin, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Origin = "caseorigincode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: incident</summary>
            public const string ParentCase = "parentcaseid";
         
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string Pendingonrepresentativesincedate = "ldv_pendingonrepresentativesincedate";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_servicesubstatus</summary>
            public const string PortalStatus = "ldv_portalstatusid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 400, Format: Text</summary>
            public const string PreCreateEntityAttachmentsId = "msdyn_precreateattachmentsid";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string PreCreateNotesId = "msdyn_precreatenotesid";
          
            /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
            public const string ProcessId = "processid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: product</summary>
            public const string Product = "productid";
           
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string QualityOfficer_NeededInformation = "ldv_qualityofficerneededinformation";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string QualityOfficer_NeededInformation_2 = "ldv_qualityofficerneededinformations";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string QualityOfficerDecision = "ldv_qualityofficerdecisioncode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string QualityOfficerDecision_2 = "ldv_qualityofficerdecisioncodes";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Post Message type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ReceivedAs = "messagetypecode";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string RecordCreatedOn = "overriddencreatedon";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string RepresentativeDecision = "ldv_representativedecisioncode";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string RequestNumber = "ticketnumber";
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_service</summary>
            public const string RequestType = "ldv_serviceid";
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
            public const string ResolveBy = "resolveby";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: slakpiinstance</summary>
            public const string ResolveByKPI = "resolvebykpiid";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: ResolveBy SLA Status, OptionSetType: Picklist, DefaultFormValue: 1</summary>
            public const string ResolveBySLAStatus = "resolvebyslastatus";
           
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
            public const string ResponsibleContact = "responsiblecontactid";
           
            /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
            public const string RouteCase = "routecase";
           
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Satisfaction, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Satisfaction = "customersatisfactioncode";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Season, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string Season = "ldv_seasoncode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_casecategory</summary>
            public const string SecondarySubCategory = "ldv_secondarysubcategoryid";
            /// <summary>Type: Double, RequiredLevel: None, MinValue: -100000000000, MaxValue: 100000000000, Precision: 2</summary>
            public const string SentimentValue = "sentimentvalue";
            /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
            public const string SerialNumber = "productserialnumber";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ServiceDeskDecision = "ldv_servicedeskdecisioncode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ServiceDeskNeededInformation = "ldv_servicedeskneededinformation";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string ServiceDeskResolution = "ldv_servicedeskresolution";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Service Stage, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string ServiceStage = "servicestage";
           
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: sla</summary>
            public const string SLA = "slaid";
           
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: socialprofile</summary>
            public const string SocialProfile = "socialprofileid";
            
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_stageconfiguration</summary>
            public const string StageConfiguration = "ldv_stageconfigurationid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: processstage</summary>
            public const string StageName = "ldv_stagenameid";
           
            /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
            public const string Statusoob = "statecode";
            /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
            public const string Statusoob1 = "statuscode";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_servicesubstatus</summary>
            public const string StatusReason = "ldv_substatusid";
           
            /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: ldv_casecategory</summary>
            public const string SubCategory = "ldv_subcategoryid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: subject</summary>
            public const string Subject = "subjectid";
           
            /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
            public const string SubmissionDate = "ldv_submissiondate";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string TasherDecision = "ldv_tasherdecisioncode";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string TasherNeededInformation = "ldv_tasherneededinformation";
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string TasherResolution = "ldv_tasherresolution";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
           
            /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 2000</summary>
            public const string Umrahscompanyservice_NeededInformation = "ldv_umrahscompanyserviceneededinformation";
            /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Quality Decision, OptionSetType: Picklist, DefaultFormValue: -1</summary>
            public const string UmrahsCompanyServiceDecision = "ldv_umrahscompanyservicedecisioncode";
            /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            /// <summary>Type: BigInt, RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>
            public const string VersionNumber = "versionnumber";

            #endregion Attributes

        }



        #region Relationships

        /// <summary>Parent: "ProcessStage" Child: "CaseManagement" Lookup: "DeprecatedStageId"</summary>
        public const string RelM1_CaseManagementDeprecatedStageId = "processstage_incident";
        /// <summary>Parent: "SLA" Child: "CaseManagement" Lookup: "LastSLAapplied"</summary>
        public const string RelM1_CaseManagementLastSLAapplied = "sla_cases";
        /// <summary>Parent: "Company" Child: "CaseManagement" Lookup: "Customer"</summary>
        public const string RelM1_CaseManagementCustomer = "incident_customer_accounts";
        /// <summary>Parent: "Individual" Child: "CaseManagement" Lookup: "ResponsibleContact"</summary>
        public const string RelM1_CaseManagementResponsibleContact = "contact_as_responsible_contact";
        /// <summary>Parent: "Individual" Child: "CaseManagement" Lookup: "Customer"</summary>
        public const string RelM1_CaseManagementCustomer1 = "incident_customer_contacts";
        /// <summary>Parent: "Individual" Child: "CaseManagement" Lookup: "Contact"</summary>
        public const string RelM1_CaseManagementContact = "contact_as_primary_contact";
        /// <summary>Parent: "Contract" Child: "CaseManagement" Lookup: "Contract"</summary>
        public const string RelM1_CaseManagementContract = "contract_cases";
        /// <summary>Parent: "ContractLine" Child: "CaseManagement" Lookup: "ContractLine"</summary>
        public const string RelM1_CaseManagementContractLine = "contract_detail_cases";
        /// <summary>Parent: "Entitlement" Child: "CaseManagement" Lookup: "Entitlement"</summary>
        public const string RelM1_CaseManagementEntitlement = "entitlement_cases";
        /// <summary>Parent: "Article" Child: "CaseManagement" Lookup: "KnowledgeBaseArticle"</summary>
        public const string RelM1_CaseManagementKnowledgeBaseArticle = "kbarticle_incidents";
        /// <summary>Parent: "Product" Child: "CaseManagement" Lookup: "Product"</summary>
        public const string RelM1_CaseManagementProduct = "product_incidents";
        /// <summary>Parent: "SLAKPIInstance" Child: "CaseManagement" Lookup: "FirstResponseByKPI"</summary>
        public const string RelM1_CaseManagementFirstResponseByKPI = "slakpiinstance_incident_firstresponsebykpi";
        /// <summary>Parent: "SLAKPIInstance" Child: "CaseManagement" Lookup: "ResolveByKPI"</summary>
        public const string RelM1_CaseManagementResolveByKPI = "slakpiinstance_incident_resolvebykpi";
        /// <summary>Parent: "SocialProfile" Child: "CaseManagement" Lookup: "SocialProfile"</summary>
        public const string RelM1_CaseManagementSocialProfile = "socialprofile_cases";
        /// <summary>Parent: "Subject" Child: "CaseManagement" Lookup: "Subject"</summary>
        public const string RelM1_CaseManagementSubject = "subject_incidents";
        /// <summary>Parent: "Currency" Child: "CaseManagement" Lookup: "Currency"</summary>
        public const string RelM1_CaseManagementCurrency = "TransactionCurrency_Incident";
        /// <summary>Parent: "SLA" Child: "CaseManagement" Lookup: "SLA"</summary>
        public const string RelM1_CaseManagementSLA = "manualsla_cases";
        /// <summary>Parent: "IoTAlert" Child: "CaseManagement" Lookup: "IoTAlert"</summary>
        public const string RelM1_CaseManagementIoTAlert = "msdyn_msdyn_iotalert_incident_IoTAlert";
        /// <summary>Parent: "FileAttachment" Child: "CaseManagement" Lookup: "Attachments"</summary>
        public const string RelM1_CaseManagementAttachments = "FileAttachment_Incident_ldv_Attachments";
        /// <summary>Parent: "User" Child: "CaseManagement" Lookup: "AssigningUser"</summary>
        public const string RelM1_CaseManagementAssigningUser = "ldv_systemuser_incident_AssigningUserid";
        /// <summary>Parent: "Process" Child: "CaseManagement" Lookup: "BPFName"</summary>
        public const string RelM1_CaseManagementBPFName = "ldv_workflow_ministageconfiguration_processid_incident";
        /// <summary>Parent: "ProcessStage" Child: "CaseManagement" Lookup: "StageName"</summary>
        public const string RelM1_CaseManagementStageName = "ldv_processstage_ministageconfiguration_stagenameid_incident";
        /// <summary>Parent: "Task" Child: "CaseManagement" Lookup: "CurrentTask"</summary>
        public const string RelM1_CaseManagementCurrentTask = "ldv_task_incident_CurrentTaskid";
        /// <summary>Parent: "ApplicationHeader" Child: "CaseManagement" Lookup: "ApplicationHeader"</summary>
        public const string RelM1_CaseManagementApplicationHeader = "ldv_applicationheader_incident_ApplicationHeaderid";
        /// <summary>Parent: "ServiceDefinition" Child: "CaseManagement" Lookup: "RequestType"</summary>
        public const string RelM1_CaseManagementRequestType = "ldv_service_incident_serviceid";
        /// <summary>Parent: "ServiceSubStatus" Child: "CaseManagement" Lookup: "StatusReason"</summary>
        public const string RelM1_CaseManagementStatusReason = "ldv_servicesubstatus_incident_substatusid";
        /// <summary>Parent: "ServiceSubStatus" Child: "CaseManagement" Lookup: "PortalStatus"</summary>
        public const string RelM1_CaseManagementPortalStatus = "ldv_servicesubstatus_incident_portalstatusid";
        /// <summary>Parent: "StageConfiguration" Child: "CaseManagement" Lookup: "StageConfiguration"</summary>
        public const string RelM1_CaseManagementStageConfiguration = "ldv_stageconfiguration_incident_StageConfigurationid";
        /// <summary>Parent: "TicketCategory" Child: "CaseManagement" Lookup: "MainCategory"</summary>
        public const string RelM1_CaseManagementMainCategory = "ldv_casecategory_incident_MainCategoryid";
        /// <summary>Parent: "TicketCategory" Child: "CaseManagement" Lookup: "SubCategory"</summary>
        public const string RelM1_CaseManagementSubCategory = "ldv_casecategory_incident_SubCategoryid";
        /// <summary>Parent: "TicketCategory" Child: "CaseManagement" Lookup: "SecondarySubCategory"</summary>
        public const string RelM1_CaseManagementSecondarySubCategory = "ldv_casecategory_incident_SecondarySubCategoryid";
        /// <summary>Parent: "SLAKPIInstance" Child: "CaseManagement" Lookup: "FirstResponseSLA"</summary>
        public const string RelM1_CaseManagementFirstResponseSLA = "ldv_slakpiinstance_incident";
        /// <summary>Parent: "ErrorsCode" Child: "CaseManagement" Lookup: "ErrorCode"</summary>
        public const string RelM1_CaseManagementErrorCode = "ldv_errorscode_incident_ErrorCodeid";
        /// <summary>Parent: "Department" Child: "CaseManagement" Lookup: "ConcernedDepartment"</summary>
        public const string RelM1_CaseManagementConcernedDepartment = "ldv_department_incident_ConcernedDepartmentid";
        /// <summary>Entity 1: "KnowledgeBaseRecord" Entity 2: "CaseManagement"</summary>
        public const string RelMM_KnowledgeBaseRecord_Incident = "KnowledgeBaseRecord_Incident";
        /// <summary>Parent: "CaseManagement" Child: "CaseManagement" Lookup: "ExistingCase"</summary>
        public const string Rel1M_CaseManagementExistingCase = "incident_existingcase";
        /// <summary>Parent: "CaseManagement" Child: "CaseManagement" Lookup: "MasterCase"</summary>
        public const string Rel1M_CaseManagementMasterCase = "incident_master_incident";
        /// <summary>Parent: "CaseManagement" Child: "CaseManagement" Lookup: "ParentCase"</summary>
        public const string Rel1M_CaseManagementParentCase = "incident_parent_incident";
        /// <summary>Parent: "CaseManagement" Child: "ActivityParty" Lookup: ""</summary>
        public const string Rel1M_incident_activity_parties = "incident_activity_parties";
        /// <summary>Parent: "CaseManagement" Child: "FieldSharing" Lookup: ""</summary>
        public const string Rel1M_incident_principalobjectattributeaccess = "incident_principalobjectattributeaccess";
        /// <summary>Parent: "CaseManagement" Child: "Connection" Lookup: ""</summary>
        public const string Rel1M_incident_connections1 = "incident_connections1";
        /// <summary>Parent: "CaseManagement" Child: "Connection" Lookup: ""</summary>
        public const string Rel1M_incident_connections2 = "incident_connections2";
        /// <summary>Parent: "CaseManagement" Child: "QueueItem" Lookup: ""</summary>
        public const string Rel1M_Incident_QueueItem = "Incident_QueueItem";
        /// <summary>Parent: "CaseManagement" Child: "Note" Lookup: ""</summary>
        public const string Rel1M_Incident_Annotation = "Incident_Annotation";
        /// <summary>Parent: "CaseManagement" Child: "SLAKPIInstance" Lookup: ""</summary>
        public const string Rel1M_slakpiinstance_incident = "slakpiinstance_incident";
        /// <summary>Parent: "CaseManagement" Child: "CaseResolution" Lookup: ""</summary>
        public const string Rel1M_Incident_IncidentResolutions = "Incident_IncidentResolutions";
        /// <summary>Parent: "CaseManagement" Child: "Lead" Lookup: ""</summary>
        public const string Rel1M_OriginatingCase_Lead = "OriginatingCase_Lead";
        /// <summary>Parent: "CaseManagement" Child: "KnowledgeArticleIncident" Lookup: ""</summary>
        public const string Rel1M_knowledgearticle_incidents = "knowledgearticle_incidents";
        /// <summary>Parent: "CaseManagement" Child: "BPF8_Technicalcomplain_MomentaryHajjProcess" Lookup: ""</summary>
        public const string Rel1M_lk_phonetocaseprocess_incidentid = "lk_phonetocaseprocess_incidentid";
        /// <summary>Parent: "CaseManagement" Child: "Feedback" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_feedback_context = "msdyn_incident_feedback_context";
        /// <summary>Parent: "CaseManagement" Child: "KnowledgeFederatedArticleIncident" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_federatedarticleincident_IncidentId = "msdyn_incident_msdyn_federatedarticleincident_IncidentId";
        /// <summary>Parent: "CaseManagement" Child: "Intent" Lookup: ""</summary>
        public const string Rel1M_msdyn_intent_case = "msdyn_intent_case";
        /// <summary>Parent: "CaseManagement" Child: "IoTAlerttoCaseProcess" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_msdyn_iottocaseprocess = "bpf_incident_msdyn_iottocaseprocess";
        /// <summary>Parent: "CaseManagement" Child: "IoTAlert" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_iotalert_Case = "msdyn_incident_msdyn_iotalert_Case";
        /// <summary>Parent: "CaseManagement" Child: "ReadTracker" Lookup: ""</summary>
        public const string Rel1M_msdyn_readtracker_poly_incident = "msdyn_readtracker_poly_incident";
        /// <summary>Parent: "CaseManagement" Child: "CaseEnrichment" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_caseenrichment_caseid = "msdyn_incident_msdyn_caseenrichment_caseid";
        /// <summary>Parent: "CaseManagement" Child: "CaseSuggestion" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_casesuggestion_suggestedentity = "msdyn_incident_msdyn_casesuggestion_suggestedentity";
        /// <summary>Parent: "CaseManagement" Child: "CaseSuggestionRequestPayload" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_casesuggestionrequestpayload_caseid = "msdyn_incident_msdyn_casesuggestionrequestpayload_caseid";
        /// <summary>Parent: "CaseManagement" Child: "Suggestionrequestpayload" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_suggestionrequestpayload = "msdyn_incident_msdyn_suggestionrequestpayload";
        /// <summary>Parent: "CaseManagement" Child: "SuggestionInteraction" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_suggestioninteraction_msdyn_suggestedentity = "msdyn_incident_msdyn_suggestioninteraction_msdyn_suggestedentity";
        /// <summary>Parent: "CaseManagement" Child: "SuggestionInteraction" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_suggestioninteraction_msdyn_suggestionfor = "msdyn_incident_msdyn_suggestioninteraction_msdyn_suggestionfor";
        /// <summary>Parent: "CaseManagement" Child: "TeamsContactSuggestionbyAI" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_aicontactsuggestion_sourcerecord = "msdyn_incident_msdyn_aicontactsuggestion_sourcerecord";
        /// <summary>Parent: "CaseManagement" Child: "CasetopicIncidentmapping" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_casetopic_incident_incidentid = "msdyn_incident_msdyn_casetopic_incident_incidentid";
        /// <summary>Parent: "CaseManagement" Child: "Swarm" Lookup: ""</summary>
        public const string Rel1M_msdyn_swarm_incident = "msdyn_swarm_incident";
        /// <summary>Parent: "CaseManagement" Child: "EntityAttachment" Lookup: ""</summary>
        public const string Rel1M_msdyn_relatedentity_msdyn_entityattachment = "msdyn_relatedentity_msdyn_entityattachment";
        /// <summary>Parent: "CaseManagement" Child: "Conversation" Lookup: ""</summary>
        public const string Rel1M_msdyn_incident_msdyn_ocliveworkitem = "msdyn_incident_msdyn_ocliveworkitem";
        /// <summary>Parent: "CaseManagement" Child: "FileAttachment" Lookup: ""</summary>
        public const string Rel1M_incident_FileAttachments = "incident_FileAttachments";
        /// <summary>Parent: "CaseManagement" Child: "BPFRequestingComplaintsServiceDeskFlow" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_requestingcomplaintsservicedeskflow = "bpf_incident_ldv_requestingcomplaintsservicedeskflow";
        /// <summary>Parent: "CaseManagement" Child: "BPFInquiryRequestProcess" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpfinquiryrequestprocess = "bpf_incident_ldv_bpfinquiryrequestprocess";
        /// <summary>Parent: "CaseManagement" Child: "BPF9TC_MomentaryUmrahProcess" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpftcmomentaryumrahprocess = "bpf_incident_ldv_bpftcmomentaryumrahprocess";
        /// <summary>Parent: "CaseManagement" Child: "BPFRequestingComplaintTasherFlow" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpfrequestingcomplainttasherflow = "bpf_incident_ldv_bpfrequestingcomplainttasherflow";
        /// <summary>Parent: "CaseManagement" Child: "BPF10_TC_NotMomentaryHijjandUmrahProcess" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpf10tcnotmomentaryhijjandumrahprocess = "bpf_incident_ldv_bpf10tcnotmomentaryhijjandumrahprocess";
        /// <summary>Parent: "CaseManagement" Child: "BPF5_FC_InternalPilgrimspost_Hajj" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpffcinternalpilgrimsposthajj = "bpf_incident_ldv_bpffcinternalpilgrimsposthajj";
        /// <summary>Parent: "CaseManagement" Child: "BPFSuggestionsRequestProcess" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpfsuggestionrequestprocess = "bpf_incident_ldv_bpfsuggestionrequestprocess";
        /// <summary>Parent: "CaseManagement" Child: "BPF6_Financialcompensationcomplain" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpffinancialcompensationcomplain = "bpf_incident_ldv_bpffinancialcompensationcomplain";
        /// <summary>Parent: "CaseManagement" Child: "BPFRequestingComplaintKadanaFlow" Lookup: ""</summary>
        public const string Rel1M_bpf_incident_ldv_bpfrequestingcomplaintkadanaflow = "bpf_incident_ldv_bpfrequestingcomplaintkadanaflow";
        /// <summary>Entity 1: "CaseManagement" Entity 2: "CustomerAsset"</summary>
        public const string RelMM_msdyn_incident_msdyn_customerasset = "msdyn_incident_msdyn_customerasset";

        #endregion Relationships

        #region OptionSets

        public enum AgentEmployeeDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum Beneficiarytype_OptionSet
        {
            InternalHajj = 1,
            ExternalHajj = 2
        }
        public enum CaseStage_OptionSet
        {
            DefaultValue = 1
        }
        public enum CaseType_OptionSet
        {
            Question = 1,
            Problem = 2,
            Request = 3
        }
        public enum CastingOfficerDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum CompaniesAdministrationDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum CompaniesServiceDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum ComplainPriority_OptionSet
        {
            High = 1,
            Medium = 2,
            Low = 3
        }
        public enum ComplainType_OptionSet
        {
            Momentary = 1,
            NotMomentary = 2
        }
        public enum CoordinationCouncilDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum CustomerType_OptionSet
        {
        }
        public enum DepartmentDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum FirstResponseSLAStatus_OptionSet
        {
            InProgress = 1,
            NearingNoncompliance = 2,
            Succeeded = 3,
            Noncompliant = 4
        }
        public enum IsThereClassificationforInquiry_OptionSet
        {
            Noclassification = 1,
            ClassificationExist = 2
        }
        public enum KadanaDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
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
        public enum MinistrysHajjAgencyDecisison_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum MinistrysUmarahAgencyDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum Origin_OptionSet
        {
            CallCenter = 1,
            Email = 2,
            Nusuk = 3,
            Externalgate = 4,
            SocialMedia = 5
        }
        public enum QualityOfficerDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum QualityOfficerDecision_2_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum ReceivedAs_OptionSet
        {
            PublicMessage = 0,
            PrivateMessage = 1
        }
        public enum RepresentativeDecision_OptionSet
        {
            CloseTheTicket = 1,
            TicketTransfer = 2
        }
        public enum ResolveBySLAStatus_OptionSet
        {
            InProgress = 1,
            NearingNoncompliance = 2,
            Succeeded = 3,
            Noncompliant = 4
        }
        public enum Satisfaction_OptionSet
        {
            VerySatisfied = 5,
            Satisfied = 4,
            Neutral = 3,
            Dissatisfied = 2,
            VeryDissatisfied = 1
        }
        public enum Season_OptionSet
        {
            Hajj = 1,
            Umrah = 2
        }
        public enum ServiceDeskDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum ServiceStage_OptionSet
        {
            Identify = 0,
            Research = 1,
            Resolve = 2
        }
        public enum Severity_OptionSet
        {
            DefaultValue = 1
        }
        public enum Statusoob_OptionSet
        {
            Active = 0,
            Resolved = 1,
            Cancelled = 2
        }
        //public enum Statusoob_OptionSet
        //{
        //    InProgress = 1,
        //    OnHold = 2,
        //    WaitingforDetails = 3,
        //    Researching = 4,
        //    ProblemSolved = 5,
        //    InformationProvided = 1000,
        //    Cancelled = 6,
        //    Merged = 2000
        //}
        public enum TasherDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }
        public enum UmrahsCompanyServiceDecision_OptionSet
        {
            CloseTheTicket = 1,
            NeedmoreDetails = 2
        }

        #endregion OptionSets






    }
}
