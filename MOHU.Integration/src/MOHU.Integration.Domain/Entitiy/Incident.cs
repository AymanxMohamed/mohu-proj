using MOHU.Integration.Domain.Features.Tickets;

namespace MOHU.Integration.Domain.Entitiy
{
    public class Incident
    {
        public static class Fields
        {
            public const string AccountId = "accountid";
            public const string ActivitiesComplete = "activitiescomplete";
            public const string ActualServiceUnits = "actualserviceunits";
            public const string BilledServiceUnits = "billedserviceunits";
            public const string BlockedProfile = "blockedprofile";
            public const string caseage = "caseage";
            public const string CaseOriginCode = "caseorigincode";
            public const string CaseTypeCode = "casetypecode";
            public const string CheckEmail = "checkemail";
            public const string ContactId = "contactid";
            public const string ContractDetailId = "contractdetailid";
            public const string ContractId = "contractid";
            public const string ContractServiceLevelCode = "contractservicelevelcode";
            public const string CreatedBy = "createdby";
            public const string CreatedByExternalParty = "createdbyexternalparty";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomerContacted = "customercontacted";
            public const string CustomerId = "customerid";
            public const string CustomerSatisfactionCode = "customersatisfactioncode";
            public const string deactivatedon = "deactivatedon";
            public const string DecrementEntitlementTerm = "decremententitlementterm";
            public const string Description = "description";
            public const string EmailAddress = "emailaddress";
            public const string EntitlementId = "entitlementid";
            public const string EntityImage = "entityimage";
            public const string EntityImage_Timestamp = "entityimage_timestamp";
            public const string EntityImage_URL = "entityimage_url";
            public const string EntityImageId = "entityimageid";
            public const string EscalatedOn = "escalatedon";
            public const string ExchangeRate = "exchangerate";
            public const string ExistingCase = "existingcase";
            public const string FirstResponseByKPIId = "firstresponsebykpiid";
            public const string FirstResponseSent = "firstresponsesent";
            public const string FirstResponseSLAStatus = "firstresponseslastatus";
            public const string FollowupBy = "followupby";
            public const string FollowUpTaskCreated = "followuptaskcreated";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IncidentId = "incidentid";
            public const string Id = "incidentid";
            public const string IncidentStageCode = "incidentstagecode";
            public const string InfluenceScore = "influencescore";
            public const string IsDecrementing = "isdecrementing";
            public const string IsEscalated = "isescalated";
            public const string KbArticleId = "kbarticleid";
            public const string lastinteraction = "lastinteraction";
            public const string LastOnHoldTime = "lastonholdtime";
            public const string ldv_AgentClosurereason = "ldv_agentclosurereason";
            public const string ldv_AgentEmployeeDecisioncode = "ldv_agentemployeedecisioncode";
            public const string ldv_ApplicationHeaderid = "ldv_applicationheaderid";
            public const string ldv_AssigningUserid = "ldv_assigninguserid";
            public const string ldv_Beneficiarytypecode = "ldv_beneficiarytypecode";
            public const string ldv_CastingOfficerDecisioncode = "ldv_castingofficerdecisioncode";
            public const string ldv_CastingOfficerNeededInformation = "ldv_castingofficerneededinformation";
            public const string ldv_ClosureDate = "ldv_closuredate";
            public const string ldv_ClosureReason = "ldv_closurereason";
            public const string ldv_ClosureReasons = "ldv_closurereasons";
            public const string ldv_CompaniesAdministrationdecisioncode = "ldv_companiesadministrationdecisioncode";
            public const string ldv_CompaniesAdministrationNeededInformation = "ldv_companiesadministrationneededinformation";
            public const string ldv_CompaniesServiceDecisioncode = "ldv_companiesservicedecisioncode";
            public const string ldv_CompaniesServiceNeededInformation = "ldv_companiesserviceneededinformation";
            public const string ldv_CoordinationCouncilDecisioncode = "ldv_coordinationcouncildecisioncode";
            public const string ldv_CoordinationCouncilNeededInformation = "ldv_coordinationcouncilneededinformation";
            public const string ldv_CurrentTaskid = "ldv_currenttaskid";
            public const string ldv_Description = "ldv_description";
            public const string ldv_EmployeeNeededInformation = "ldv_employeeneededinformation";
            public const string ldv_ErrorCodeid = "ldv_errorcodeid";
            public const string ldv_externalticketnumber = "ldv_externalticketnumber";
            public const string ldv_FirstResponseSLAId = "ldv_firstresponseslaid";
            public const string ldv_IsFCR = "ldv_isfcr";
            public const string ldv_Isservicedeskupdated = "ldv_isservicedeskupdated";
            public const string ldv_IsSubmitted = "ldv_issubmitted";
            public const string ldv_istashirupdated = "ldv_istashirupdated";
            public const string ldv_IsThereclassificationforInquirycode = "ldv_isthereclassificationforinquirycode";
            public const string ldv_Locationcode = "ldv_locationcode";
            public const string ldv_MainCategoryid = "ldv_maincategoryid";
            public const string ldv_MinistrysHajjAgencyDecisisoncode = "ldv_ministryshajjagencydecisisoncode";
            public const string ldv_MinistrysHajjAgencyNeededInformation = "ldv_ministryshajjagencyneededinformation";
            public const string ldv_MinistrysUmarahAgencyDecisisoncode = "ldv_ministrysumarahagencydecisisoncode";
            public const string ldv_MinistrysUmarahAgencyNeededInformation = "ldv_ministrysumarahagencyneededinformation";
            public const string ldv_NeededInformation = "ldv_neededinformation";
            public const string ldv_Pendingonrepresentativesincedate = "ldv_pendingonrepresentativesincedate";
            public const string ldv_portalstatusid = "ldv_portalstatusid";
            public const string ldv_processid = "ldv_processid";
            public const string ldv_QualityOfficerDecisioncode = "ldv_qualityofficerdecisioncode";
            public const string ldv_QualityOfficerDecisioncodes = "ldv_qualityofficerdecisioncodes";
            public const string ldv_QualityOfficerNeededInformation = "ldv_qualityofficerneededinformation";
            public const string ldv_QualityOfficerNeededInformations = "ldv_qualityofficerneededinformations";
            public const string ldv_RepresentativeDecisioncode = "ldv_representativedecisioncode";
            public const string ldv_Seasoncode = "ldv_seasoncode";
            public const string ldv_SecondarySubCategoryid = "ldv_secondarysubcategoryid";
            public const string ldv_servicedeskdecisioncode = "ldv_servicedeskdecisioncode";
            public const string ldv_servicedeskneededinformation = "ldv_servicedeskneededinformation";
            public const string ldv_servicedeskresolution = "ldv_servicedeskresolution";
            public const string ldv_serviceid = "ldv_serviceid";
            public const string ldv_sahabstatusreason = "ldv_sahabstatusreason";

            public const string ldv_requesttypeid = "ldv_requesttypeid";
            public const string ldv_StageConfigurationid = "ldv_stageconfigurationid";
            public const string ldv_stagenameid = "ldv_stagenameid";
            public const string ldv_SubCategoryid = "ldv_subcategoryid";
            public const string ldv_SubmissionDate = "ldv_submissiondate";
            public const string ldv_substatusid = "ldv_substatusid";
            public const string ldv_tasherdecisioncode = "ldv_tasherdecisioncode";
            public const string ldv_UmrahscompanyserviceDecisioncode = "ldv_umrahscompanyservicedecisioncode";
            public const string ldv_UmrahscompanyserviceNeededInformation = "ldv_umrahscompanyserviceneededinformation";
            public const string MasterId = "masterid";
            public const string Merged = "merged";
            public const string MessageTypeCode = "messagetypecode";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedByExternalParty = "modifiedbyexternalparty";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string msdyn_copilotengaged = "msdyn_copilotengaged";
            public const string msdyn_iotalert = "msdyn_iotalert";
            public const string msdyn_precreateattachmentsid = "msdyn_precreateattachmentsid";
            public const string msdyn_precreatenotesid = "msdyn_precreatenotesid";
            public const string nextsla = "nextsla";
            public const string NumberOfChildIncidents = "numberofchildincidents";
            public const string OnHoldTime = "onholdtime";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string ParentCaseId = "parentcaseid";
            public const string PrimaryContactId = "primarycontactid";
            public const string PriorityCode = "prioritycode";
            public const string ProcessId = "processid";
            public const string ProductId = "productid";
            public const string ProductSerialNumber = "productserialnumber";
            public const string ResolveBy = "resolveby";
            public const string ResolveByKPIId = "resolvebykpiid";
            public const string ResolveBySLAStatus = "resolvebyslastatus";
            public const string ResponseBy = "responseby";
            public const string ResponsibleContactId = "responsiblecontactid";
            public const string RouteCase = "routecase";
            public const string SentimentValue = "sentimentvalue";
            public const string ServiceStage = "servicestage";
            public const string SeverityCode = "severitycode";
            public const string SLAId = "slaid";
            public const string SLAInvokedId = "slainvokedid";
            public const string SocialProfileId = "socialprofileid";
            public const string StageId = "stageid";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string SubjectId = "subjectid";
            public const string TicketNumber = "ticketnumber";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string Title = "title";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string TraversedPath = "traversedpath";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
            public const string Referencingincident_existingcase = "incident_existingcase";
            public const string Referencingincident_master_incident = "incident_master_incident";
            public const string Referencingincident_parent_incident = "incident_parent_incident";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets:
            /// ldv_servicesubstatus</summary>
            public const string StatusReason = "ldv_substatusid";
            /// <summary>Type: Lookup, RequiredLevel: None, Targets: ldv_servicesubstatus</summary>
            public const string PortalStatus = "ldv_portalstatusid";

            //IntegrationStatus
            public const string IntegrationStatus = "ldv_integrationstatuscode";

            public const string IntegrationClosureReason = "ldv_integrationclosurereason";

            public const string IntegrationClosureDate = "ldv_integrationclosuredate";
                
            public const string IntegrationUpdatedBy = "ldv_IntegrationUpdatedBy";
            
            public const string IntegrationComment = "ldv_IntegrationComment";
            
            public const string IntegrationLastActionDate = "ldv_IntegrationLastActionDate";

            public const string ExternalTicketNumber = "ldv_externalticketnumber";
            public const string ExternalTicketId = "ldv_externalcaseid";


            public const string IsServiceDeskUpdated = "ldv_isservicedeskupdated";

            public const string IsKadanaUpdated = "ldv_iskadanaupdated";

            public const string IsTashirUpdated = "ldv_istashirupdated";
            public const string IsSahabUpdated = "ldv_issendrequesttosahab";
            public const string IsAlmatarUpdated = "ldv_isalmatarupdated";


            //Tasheer
            public const string TaasherTicketNumber = "ldv_taasherticketnumber";
            public const string TaasherTicketId = "ldv_tasheerticketid";

            //Kadana
            public const string KidanaTicketNumber = "ldv_kidanaticketnumber";
            public const string KidanaTicketId = "ldv_kidanaticketid";

            //Service Desk
            public const string ServiceDeskTicketNumber = "ldv_servicedeskticketnumber";
            public const string ServiceDeskTicketId = "ldv_servicedeskticketid";
            
            // Sahab
            public const string SahabTicketNumber = "ldv_sahabticketnumber";
            public const string SahabTicketId = "ldv_sahabticketid";

            //Al-Matar
            public const string AlMatarTicketNumber = "ldv_almatarticketnumber";
            public const string AlMatarTicketId = "ldv_almatarticketid";

        }

        public const string EntityLogicalName = "incident";

        public const string EntitySchemaName = "Incident";

        public const string PrimaryIdAttribute = "incidentid";

        public const string PrimaryNameAttribute = "title";

        public const string EntityLogicalCollectionName = "incidents";

        public const string EntitySetName = "incidents";


    }
}
