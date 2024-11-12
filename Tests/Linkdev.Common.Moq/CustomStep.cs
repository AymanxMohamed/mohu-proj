using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Moq;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

 
using Microsoft.Xrm.Tooling.Connector;
using System.Web.Configuration;
 
using Linkdev.Common.Moq.Model;
using LinkDev.Common.Crm.Cs.NotificationTemplates;

namespace Linkdev.Common.Moq
{
    class CustomStep
    {
        public static IOrganizationService service;
        static void Main(string[] args)
        {
            Dictionary<string, object> inputs;
            Guid PrimaryEntityID = new Guid("efa3ba0b-99ac-4537-9ad1-bbe735429171");
            string PrimaryEntitySchemaName = /*"task";//*/ "incident";   /*"contact";  */

            //Guid processstage = Guid.Empty;

            //  Activity activity = new SendNotification();
            Activity activity = new SendNotification();


            #region Variables Intializations Admin
            // Crm  AdminUserId  
            Guid AdminUserId = new Guid("415a5fdc-62bd-ee11-9079-000d3aa84c36");


            #endregion

            #region   Intializations
            var invoker = new WorkflowInvoker(activity);
            //create our mocks
            var factoryMock = new Mock<IOrganizationServiceFactory>();
            var tracingServiceMock = new Mock<ITracingService>();
            var workflowContextMock = new Mock<IWorkflowContext>();
            //set up a mock service for CRM organization service
            //var connection = CrmConnection.Parse(@"Url=" + Credential.Url + "; Username=" + Credential.UserName + "; Password=" + Credential.Password + ";");


            //IOrganizationService service = GetCrmConnection("https://stc-dev-v9.linkdev.com/XRMServices/2011/Organization.svc", @"CRM365DC\crmadmin", "linkP@ss");

              service = getCRMAccess();


               //createTicket();

            //IOrganizationService service = new CRMAccess().GetAccessToCRM();
            //set up a mock workflowcontext
            workflowContextMock.Setup(t => t.InitiatingUserId).Returns(AdminUserId);
            workflowContextMock.Setup(t => t.CorrelationId).Returns(Guid.NewGuid());
            workflowContextMock.Setup(t => t.UserId).Returns(AdminUserId);
            workflowContextMock.Setup(t => t.PrimaryEntityId).Returns(PrimaryEntityID);
            workflowContextMock.Setup(t => t.PrimaryEntityName).Returns(PrimaryEntitySchemaName);
            var workflowContext = workflowContextMock.Object;
            //set up a mock tracingservice - will write output to console
            tracingServiceMock.Setup(t => t.Trace(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((t1, t2) => Console.WriteLine(t1, t2));
            var tracingService = tracingServiceMock.Object;
            //set up a mock servicefactory
            factoryMock.Setup(t => t.CreateOrganizationService(AdminUserId)).Returns(service);
            var factory = factoryMock.Object;
            invoker.Extensions.Add<ITracingService>(() => tracingService);
            invoker.Extensions.Add<IWorkflowContext>(() => workflowContext);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);
            #endregion

            #region GetEntityReferencePrimitives
            //var inputs = new Dictionary<string, object>
            //    {

            //    // {"EntityReference", new EntityReference("ldv_stageconfiguration", new Guid("{52B825EE-C591-E911-A97B-000D3A23DFA3}"))} ,

            //};
            #endregion

            #region Get Stage

            //var inputs = new Dictionary<string, object>
            //{
            //     { "EntityReferenceId",PrimaryEntityID.ToString()},
            //    { "EntityReferenceName",PrimaryEntitySchemaName}
            //    //,
            //   //{"ProcessStage", new EntityReference("processstage",processstage)} ,

            //};
            #endregion

            #region GetRequestAndApplicationHeader
            //var inputs = new Dictionary<string, object>
            //{
            //      {"EntityReferenceId", PrimaryEntityID.ToString()} ,
            //      {"EntityReferenceName", PrimaryEntitySchemaName} ,
            //};
            #endregion




            #region Esclation 
            //var inputs = new Dictionary<string, object>
            //{
            //     { "From",new EntityReference("systemuser",AyaUserId)} ,
            //    { "Notification", new EntityReference("ldv_notificationtemplate", new Guid(EmailTemplate))} ,
            //    {"isCreationNotification",true },
            //    {"isReminderNotification",true },
            //    {"isEsclationNotification",true }
            //    };
            #endregion




            #region Change ActiveStage and BBPF           
            //inputs = new Dictionary<string, object>
            //{
            //       { "Process" , new EntityReference("workflow", new Guid("{86F0D37D-F162-4B27-A09D-76782969FEEE}"))},
            //       { "ProcessStage" , new EntityReference("processstage", new Guid("{7334ef49-eea3-44fa-8c6b-f570efa3f630}"))},
            //    {"InstanceLogicalName", "ldv_bpfepmchangeeventpermitdate" },
            //    {"TargetEntity",new EntityReference("ldv_eventpermitmanagement",new Guid("ECAFFE52-56E5-4A4A-98DE-7A395394151B")) }
            //};
            #endregion

            #region CreateFileLocations         
            //inputs = new Dictionary<string, object>
            //    {
            //           { "ServiceSetting" , new EntityReference("ldv_service", new Guid("{FD05BC95-3EB8-E911-A9CE-000D3A23DFA3}"))},
            //          // { "ProcessStage" , new EntityReference("processstage", new Guid("{b0fbd6eb-8e3a-4f3f-aca1-bb6f3c3753c0}"))},
            //        {"RegardingId", "{7645DAFD-70C3-E911-A9EB-000D3A23DFA3}" },
            //        {"RegardingLogicalName", "ldv_eventpermitmanagement" },

            //        {"ApplicationHeader",new EntityReference("ldv_applicationheader",new Guid("{7E45DAFD-70C3-E911-A9EB-000D3A23DFA3}")) }
            //    };
            #endregion

            #region EscalationAndNotificationToTasksOwner         
            //inputs = new Dictionary<string, object>
            //    {
            //            {"From" , new EntityReference("systemuser", new Guid("{8F13A769-C991-E911-A97B-000D3A23DFA3}"))},
            //            {"Notification" , new EntityReference("ldv_notificationtemplate", new Guid("{36232DFD-ACC8-E911-A9F7-000D3A23DFA3}"))},
            //            {"EntityId", "{50CC11DD-C7CD-E911-A9FB-000D3A23DFA3}" },
            //            {"EntityLogicalName", "ldv_eventpermitmanagement" },
            //            {"IsSendToAllQueues",false },
            //            {"ExceptQueuesIDs", "{304FDB28-61B8-E911-A9CE-000D3A23DFA3},{7B798056-32A2-E911-A999-000D3A23DFA3}"}
            //    };
            #endregion

            #region CheckCurrentProcess
            //inputs = new Dictionary<string, object>
            //{
            //       { "EntityReferenceId" , "2cf1e761-6ed9-e911-aa0a-000d3a23dfa3"},
            //       { "EntityReferenceName" , "ldv_bpfcasemanagementfeedback" },
            //};
            #endregion

            #region UpdateRecordsFrom1ToMWithGivenValues
            //inputs = new Dictionary<string, object>
            //{
            //       { "EntityLogicalName" , "ldv_permi"},
            //       { "EntityId" , "a79b1cc2-973b-ea11-aae3-000d3a23dfa3" },
            //    {"Related1ToMEntitySchemaName","ldv_eventpermitmanagement" },

            //    {"IsLookup" ,true },
            //    {"LookupSchemaName" ,"ldv_projectmanagersystemuserid" },
            //    {"EntityIdValue","8f13a769-c991-e911-a97b-000d3a23dfa3" },
            //    {"EntitySchemaName","systemuser" }

            //};
            #endregion

            #region GetLastCreatedDate
            //inputs = new Dictionary<string, object>
            //{
            //    { "EntityLogicalName" , "ldv_eventpermitmanagement"},
            //    { "EntityId" , "494B61D3-BAD4-E911-A9FE-000D3A23DFA3" },
            //    {"IsCreate" ,false },
            //    {"Permit" ,null },
            //};
            #endregion

            #region InstanceSchemaName
            //inputs = new Dictionary<string, object>
            //    {
            //        {"InstanceSchemaName","ldv_bpf_fffa5ab1d4754ef8a09abfe993ca502e" } ,  
            //        { "LookupId",  "{88A70076-5B31-4756-A40F-B6D0C76AD0CF}"} ,
            //        { "LookupSchemaName", "ldv_eventpermitmanagement" }
            //    };
            #endregion

            #region Cascading1ToMWithGivenValues
            //inputs = new Dictionary<string, object>
            //    {
            //        {"EntityLogicalName","" } ,
            //        { "EntityId",  ""} ,
            //        { "Related1ToMEntitySchemaName", "" },
            //        { "IsLookup",  "true"} ,
            //        { "LookupSchemaName",  ""} ,
            //        { "EntityIdValue",  ""} ,
            //        { "EntitySchemaName",  ""} ,
            //    };
            #endregion
            //var x = 10;
            #region  ValidatePortalDocuments
            //inputs = new Dictionary<string, object>
            //    {
            //            {"ServiceSetting" , new EntityReference("ldv_service", new Guid("{754B7A39-EBD4-E911-AA01-000D3A23DFA3}"))},
            //            {"RegardingId", "E07AF65E-5DEF-44DB-BEB7-886B388CCAC7" },
            //            {"RegardingLogicalName", "ldv_eventpermitmanagement" },
            //            {"ProcessStage" , new EntityReference("processstage", new Guid("{fd94774a-a96f-4063-a0fd-282fb638d4a8}"))},
            //    };
            #endregion

            #region  IncrementDate
            //inputs = new Dictionary<string, object>
            //    {
            //            {"DateValue", DateTime.Now },
            //            {"IncrementDays", 2 },
            //            {"IncrementYears", 3 },
            //            {"IncrementMin", 5 },

            //    };
            #endregion

            #region create task

            //inputs = new Dictionary<string, object>
            //    {

            //    {"StageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("7b82a773-0575-ea11-8255-000d3a20f0aa"))} ,
            //    { "ApplicationHeader",new EntityReference("ldv_applicationheader", new Guid("{1B61FD0C-E77C-EA11-8267-000D3A20F0AA}"))},
            //    { "RequestId","1361fd0c-e77c-ea11-8267-000d3a20f0aa"},
            //    { "RequestSchemaName","ldv_conflictresolution"},

            //    };
            #endregion

            #region Perform Action
            //inputs = new Dictionary<string, object>
            //{

            //      {"EntityReferenceId", "7bd5e185-1d77-ea11-8257-000d3a20f0aa"} ,
            //      {"EntityReferenceName", "new_conflictresolutionsbpfconflictresolution"} ,


            //};
            #endregion

            #region ResetFields
            //inputs = new Dictionary<string, object>
            //    {

            //        {"StageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("7b82a773-0575-ea11-8255-000d3a20f0aa"))} , // prequ
            //        { "ApplicationHeader", new EntityReference("ldv_applicationheader", new Guid("f559cbc7-2977-ea11-8257-000d3a20f0aa"))} ,

            //    };
            #endregion

            #region NotificationRelationShip
            //inputs = new Dictionary<string, object>
            //{
            //   // { "From",new EntityReference("systemuser", new Guid("96179c9c-727d-ea11-8269-000d3a20f0aa"))} ,
            //    { "Notification", new EntityReference("ldv_notificationtemplate", new Guid("7d1cb513-bb7d-ea11-826a-000d3a20f0aa"))} ,
            //    {"UseContext",true },
            //    {"Entity1LogicalnamePath","ldv_conflictresolutionid"  },
            //    {"Entity2LogicalName","ldv_sessionauthorityattendee" },
            //    {"IsM2M",false },
            //    {"Entity1LookupLogicalNameAtEntity2","ldv_relatedtechnicalsecretariatsessionid" },
            //    {"IntersectEntityLogicalName","" },
            //    {"FieldLogicalNameThatContainsRecipient","ldv_administrativeauthorityid" },
            //    {"UseContextInRegarding",true },
            //    {"RegardingEntity","" },
            //    {"StackholderInTemplateHaveValuesRelatedToRecipient",true },
            //    {"StopSendingEmailForAttachments",false },


            //};

            #endregion

            #region AssignTheRequest
            //inputs = new Dictionary<string, object>
            //{

            //    {"RequestId","{019C0B10-8A82-EA11-8278-000D3A20F0AA}"  },
            //    {"RequestSchemaName","ldv_conflictresolution" },
            //    {"AssignToUser",true },
            //    { "User", new EntityReference("systemuser", new Guid("0DE6BDED-8179-EA11-825F-000D3A20F0AA"))} ,


            //};

            #endregion


            DateTime today = DateTime.Now;
            #region CreateSessionAuthorityAttendees

            //inputs = new Dictionary<string, object>
            //{

            //   {"TechnicalSession", new EntityReference("ldv_relatedtechnicalsession", new Guid("bc0ace56-0087-ea11-8287-000d3a20f0aa"))} ,
            //   {"ConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("bce8bb8e-fd86-ea11-8287-000d3a20f0aa"))} ,

            //};
            #endregion

            #region GetLastActiveTechnicalSession
            //inputs = new Dictionary<string, object>
            //{

            //   {"ConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("bce8bb8e-fd86-ea11-8287-000d3a20f0aa"))} ,

            //};
            #endregion




            #region ChangeBpfInstanceStage               
            //inputs = new Dictionary<string, object>
            //{
            //       { "MoveToNextStage" , true },
            //       { "BackToPreviousStage" ,false},
            //    {"MoveToSpecificStage", false },
            //    {"ProcessStage",null }
            //};

            #endregion


            #region assigninig / ResettingStageFields/ ToBeChangeStageFields /History

            //inputs = new Dictionary<string, object>
            //{

            //    //    //{ "Notification", new EntityReference("ldv_notificationtemplate", new Guid("{CADD410C-7485-EA11-8281-000D3A20F0AA}"))} ,
            //    //    //{"UseContext",true },
            //    //    //{"ContextEntityId", "" },
            //    //    //{"StackholderInTemplateHaveValuesRelatedToRecipient",true },
            //    //    //{"ToEntityId" , "{44CA1F98-1578-EA11-825A-000D3A20F0AA}" },
            //    //    //{"ToEntityLogicalName" , "ldv_administrativeauthority" },



            //       {"StageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("{46037BAB-FF77-EA11-825A-000D3A20F0AA}"))} ,
            //       {"ApplicationHeader", new EntityReference("ldv_applicationheader", new Guid("b8104b06-b99a-ea11-82b1-000d3a20f0aa"))} ,
            //       {"OnlyClearFields",false },
            //       {"OnlyChangeFields",true },
            //       {"ClearAndChangeFields",false },

            //};
            #endregion

            #region PushConflictResolution
            //inputs = new Dictionary<string, object>
            //{
            //        {"RequestId", "a1c8deae-41aa-ea11-82c9-000d3a20f0aa" } , 
            //        { "RequestLogicalName", "ldv_conflictresolution"} ,

            //};
            #endregion

            #region CreateUpdateCRAppealRequst
            //inputs = new Dictionary<string, object>
            //{
            //        { "ParentConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("{CCAE8427-30B9-EA11-82EA-000D3A20F0AA}"))},
            //    {"AdministrativeAuthority",new EntityReference() }
            //};
            #endregion

            //inputs = new Dictionary<string, object> { };

            #region MyRegion
            //Entity newCrAppealRequest = new Entity(ConflictResolution.SchemaName);

            //newCrAppealRequest.Attributes.Add(ConflictResolution.Fields.ProcessId, new Guid("D8FC48D7-01C5-40F1-A7F1-F7E8FA24377D"));
            //newCrAppealRequest.Attributes.Add(ConflictResolution.Fields.StageId, new Guid("d2717661-ffe4-48ff-8765-ace6b179cadf"));

            //Guid newCrAppealRequestId = service.Create(newCrAppealRequest);
            #endregion

            #region GenerateFollowUpRecords - GenerateSendbackRecords
            //inputs = new Dictionary<string, object>
            //{
            //        { "DirectConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("{7BDA9849-40BA-EA11-82ED-000D3A20F0AA}"))},
            //        { "ParentConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("{59911CA8-4FB9-EA11-82EA-000D3A20F0AA}"))},
            //};
            #endregion
            #region FinalSubRequestMinistryDecision
            //inputs = new Dictionary<string, object>
            //{
            //        { "DirectConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("{C31854AE-E0BE-EA11-82F6-000D3A20F0AA}"))},
            //        //{ "ParentConflictResolution", new EntityReference()},
            //        { "Service", new EntityReference("ldv_service", new Guid("{9FDC6993-C0A4-EA11-82BD-000D3A20F0AA}"))}, ////"{1392673C-DA74-EA11-8255-000D3A20F0AA}"))},
            //           { "ParentConflictResolution", new EntityReference("ldv_conflictresolution", new Guid("{F11925FA-A6BE-EA11-82F6-000D3A20F0AA}"))},
            //};
            #endregion



            #region Esclation 
            //inputs = new Dictionary<string, object>
            //{
            //     { "From",new EntityReference("systemuser",new Guid("6DAAC2BF-75BF-E611-80C1-005056AF329A"))} ,
            //    { "Notification", new EntityReference("ldv_notificationtemplate", new Guid("{0EA18FF0-ED95-EB11-BC88-005056AFB4AE}"))} ,
            //    {"isCreationNotification",true },
            //    {"isReminderNotification",false },
            //    {"isEsclationNotification",false },
            //    { "Team", new EntityReference("team", new Guid("{C82945B1-AB97-EB11-BC89-005056AF4010}"))} ,

            //    };
            #endregion



            #region DeactiveRequestAndFinalizeInstance
            //inputs = new Dictionary<string, object>
            //{
            //     { "InstanceId",PrimaryEntityID.ToString()},
            //    { "InstanceSchemaName",PrimaryEntitySchemaName},
            //    {"DeactiveRequest","yes" },
            //    {"FinalizeInstance","yes"},

            //};
            #endregion




            #region ClearFlag
            //inputs = new Dictionary<string, object>
            //{
            //     { "FlagSchemaName", "ldv_isroutetocustomerrelation"} ,
            //    { "TargetIncidentEntity", new EntityReference("incident", new Guid("006ad426-0ae3-eb11-a98a-000d3a4573e4"))} ,


            //};

            #endregion
            #region ChangeBpfInstanceAndActiveStage               

            //inputs = new Dictionary<string, object>
            //{
            //    { "Process", new EntityReference("workflow", new Guid("EE6920DD-8246-41DA-BF58-E6B998C10DA5"))} ,
            //       {"ProcessStage", new EntityReference("processstage", new Guid("0F7FD233-380B-48A7-BBBA-3685E839FB40")) },
            //       { "InstanceLogicalName" ,"ldv_bpfinquiryprocess"},
            //       { "TargetEntityLogicalName" ,"incident"},
            //       { "TargetEntityId" ,"{4147CA4A-A3DE-EB11-A983-000D3A4573E4}"},
            //};
            #endregion






            #region ValidateCommentRecordExist
            // inputs = new Dictionary<string, object>
            //{
            //      {"EntityId", PrimaryEntityID.ToString()} ,
            //      {"EntityLogicalName", PrimaryEntitySchemaName} ,
            //};
            #endregion


            #region SendNotification 
            //inputs = new Dictionary<string, object>
            //{
            //        {"Notification", new EntityReference("ldv_notificationtemplate", new Guid("56c4d4e8-55cd-eb11-a962-000d3a4573e4"))} ,
            //        { "ToUser", new EntityReference("systemuser", new Guid("{6A969662-9714-EC11-A960-6045BD8810E2}"))} ,
            //         {"UseContextAsRegarding", true} ,
            //         {"RegardingEntityId", "a0f705ab-564c-ec11-a9c5-6045bd8810e2"} ,
            //         {"RegardingEntityLogicalName", "incident"} ,


            //};
            #endregion
            #region sms

            //inputs = new Dictionary<string, object>{
            //    { "Customer" , new EntityReference("account", new Guid("9b1eb582-d9df-eb11-a986-000d3a4573e4"))},
            //    { "Contact" , new EntityReference("contact", new Guid("97eabee8-5d2f-ec11-a996-6045bd8810e2"))},

            //    { "SmsEntity",new EntityReference("ldv_sms", new Guid("1012bc34-2d36-ec11-a9a3-6045bd8810e2"))} ,
            //    { "TemplateType" , new OptionSetValue(1) },
            //    {"MobileNo", "96654567866" },
            //    {"SMSBody", "Dear test ,Kindly note that your kind request CAS-01081-J7L1X4 has been submitted at Stc pay Thank you STC Pay Team" },
            //    {"CreatedOn", DateTime.Now },
            //    {"TicketNumber", "123456789" } 


            //};
            #endregion


            #region Notification  - GetRoutingAssigning-history
            //inputs = new Dictionary<string, object>
            //{
            //     { "ApplicationHeader",new EntityReference("ldv_applicationheader", new Guid("ee22e931-6d42-ec11-a9b1-6045bd8810e2"))} ,
            //    { "StageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("75afff33-a2c7-eb11-a957-000d3a4573e4"))} ,


            //};
            //inputs = new Dictionary<string, object>
            //{
            //    // { "applicationHeader",new EntityReference("ldv_applicationheader", new Guid("b65e2990-fd5b-ec11-a9df-6045bd8810e2"))} ,
            //    //{ "stageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("d0825ca8-3bcc-eb11-a960-000d3a4573e4"))} ,

            //      { "ApplicationHeader",new EntityReference("ldv_applicationheader", new Guid("b65e2990-fd5b-ec11-a9df-6045bd8810e2"))} ,
            //    { "StageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("d0825ca8-3bcc-eb11-a960-000d3a4573e4"))} ,

            //};
            #endregion
            #region Inputs 
            //  inputs = new Dictionary<string, object>
            //{
            //    {"EntityLogicalName","ldv_accountregistrationrequest" },
            //    {"EntityLogicalId","A97A3897-F2B2-E911-A9C1-000D3A23DFA3" },
            //    {"EntityRefrenceFieldLogicalName","ldv_accountid" }
            //    };
            //inputs = new Dictionary<string, object>
            //{
            //    {"CustId","291497" },

            //};


            #endregion
            string x = "2";
            #region Notification template
            inputs = new Dictionary<string, object>
            {
                 { "Notification" , new EntityReference("ldv_notificationtemplate", new Guid("6596098b-9f9c-ef11-8a6a-6045bd9ec6ef"))},
                 {"ToUser",  null},
                 { "ToTeam", null} ,
                 { "ToContact",new EntityReference("contact", new Guid("7d070a8a-09ca-ee11-9079-6045bd895e74"))} ,
                 { "ToAccount", null} ,
                 { "ToRecordsURLs",null },
                 { "ToEmailAddress",null },
                 { "ToQueue", null} ,
                 { "CCRecordsURLs",null },
                 { "BCCRecordsURLs",null },
                 { "IsAnotherRegarding",true },
                 { "RegardingId","efa3ba0b-99ac-4537-9ad1-bbe735429171" },
                 { "RegardingName","incident" },
            };
        #endregion

        #region MyRegion
        //inputs = new Dictionary<string, object>
        //        {

        //        {"stageConfiguration", new EntityReference("ldv_stageconfiguration", new Guid("{E70D2B35-EF8E-EF11-AC21-6045BDA22907}"))} ,
        //        { "applicationHeader",new EntityReference("ldv_applicationheader", new Guid("{7BB30E85-2F9E-EF11-8A6A-6045BD9EC6EF}"))},

        //        };
            #endregion
            //inputs = new Dictionary<string, object>();

            var outputs = invoker.Invoke(inputs);
        }
        public static Entity RetrieveEntityById(IOrganizationService service, string entityLogicalName, Guid guidEntityId)
        {

            Entity RetrievedEntityById = service.Retrieve(entityLogicalName, guidEntityId, new ColumnSet() { AllColumns = true }); //it will retrieve the all attrributes

            return RetrievedEntityById;


        }
        public static Entity RetrivePrimaryEntityOfBpf(string bpfEntityLogicalName, Guid bpfEntityId, IOrganizationService organizationService)
        {
            try
            {

                string entityLogicalName = String.Empty;
                Entity bpfEntity = organizationService.Retrieve(bpfEntityLogicalName, bpfEntityId, new ColumnSet(true));
                EntityReference request = new EntityReference();
                foreach (var attribute in bpfEntity.Attributes)
                {
                    if (attribute.Key.Contains("bpf_ldv_"))
                    {
                        entityLogicalName = attribute.Key;
                        request = (EntityReference)bpfEntity.Attributes[entityLogicalName];
                        if (request?.Id != Guid.Empty && request.LogicalName != string.Empty)
                        {
                            return new Entity(request.LogicalName, request.Id);
                        }
                    }
                }
                return null;
                #region Old
                //Logger.Log("in RetrivePrimaryEntityOfBPF fn.", LogLevel.Debug);
                //  var bpfEntityLogicalName = "ldv_bpftestcustomstep";
                //string bpfEntityId = "{25DA21C8-5DFD-E811-8119-000D3A393005}";
                //string entityLogicalName = String.Empty;
                //RetrieveEntityRequest metadataRequest = new RetrieveEntityRequest
                //{
                //    EntityFilters = EntityFilters.Attributes,
                //    LogicalName = bpfEntityLogicalName
                //};
                //var eMetadataResponse = (RetrieveEntityResponse)organizationService.Execute(metadataRequest);
                //var accountMetadata = eMetadataResponse.EntityMetadata.Attributes;
                //foreach (var attribute in accountMetadata)
                //{
                //    if (attribute.LogicalName.Contains("bpf_ldv_") && !attribute.LogicalName.Contains("idname"))
                //    {
                //        entityLogicalName = attribute.LogicalName;

                //        //Logger.Log(" entityLogicalName : " + entityLogicalName, LogLevel.Debug);
                //    }
                //}


                //if (entityLogicalName != String.Empty && bpfEntityId != Guid.Empty &&
                //    bpfEntityLogicalName != String.Empty)
                //{
                //    if (entityLogicalName != String.Empty)
                //    {
                //        Entity target = organizationService.Retrieve(bpfEntityLogicalName,
                //            bpfEntityId, new ColumnSet(entityLogicalName));
                //        EntityReference targetEntityForBpf =
                //            target.GetAttributeValue<EntityReference>(
                //                entityLogicalName); //entityLogicalName.Replace("bpf_", "").Replace("id", ""));
                //        // target.Attributes.Values

                //        //object x=target.Attributes[bpfEntityLogicalName];
                //        if (targetEntityForBpf != null)
                //        {
                //            Entity primaryEntity = organizationService.Retrieve(targetEntityForBpf.LogicalName,
                //                targetEntityForBpf.Id, new ColumnSet(true));
                //            //= targetEntityForBpf.Contains(entityLogicalName)
                //            //    ? targetEntityForBpf.GetAttributeValue<EntityReference>(entityLogicalName)
                //            //    : null;
                //            if (primaryEntity != null)
                //            {
                //                //Logger.Log(" primaryEntity : " + primaryEntity, LogLevel.Debug);
                //                return primaryEntity;
                //            }
                //        }
                //        else
                //        {
                //            //Logger.Log("primaryEntity is  null ", LogLevel.Debug);
                //        }
                //    }
                //    else
                //    {
                //        //Logger.Log("target is  null ", LogLevel.Debug);
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                //Logger.Log($"ExecuteLogic hass been finished with Error:'{ex.Message}'", LogLevel.Error);
                throw new InvalidWorkflowException($"ExecuteLogic hass been finished with Error:'{ex.Message}'");
            }
            finally
            {
                //Logger.LogFunctionEnd();
            }


        }
        public static EntityCollection RetrieveMultipleRequest(QueryBase query, IOrganizationService organizationService)
        {
            try
            {
                List<Entity> Entities = new List<Entity>();

                EntityCollection retrieved = organizationService.RetrieveMultiple(query);

                return retrieved;
            }
            catch (Exception ex)
            {
                //Logger.Log(ex);
                if (ex.InnerException != null)
                {
                    //Logger.Log(ex);
                }
                throw;
            }
        }

        public static Guid GetEntityByCode(int code, string entitySchemaName, IOrganizationService organizationService)
        {
            Guid id = Guid.Empty;
            if (code <= 0 && entitySchemaName == string.Empty) return Guid.Empty;
            var serviceSubStatusQuery = new QueryExpression(entitySchemaName);
            serviceSubStatusQuery.Distinct = true;
            serviceSubStatusQuery.ColumnSet.AddColumns(entitySchemaName + "id");
            serviceSubStatusQuery.Criteria.AddCondition("ldv_code", ConditionOperator.Equal, code);
            EntityCollection entityCollection = RetrieveMultipleRequest(serviceSubStatusQuery, organizationService);
            if (entityCollection.Entities.Any())
            {
                id = entityCollection.Entities[0].Contains(entitySchemaName + "id") ? entityCollection.Entities[0].GetAttributeValue<Guid>(entitySchemaName + "id") : Guid.Empty;

            }
            return id;
        }
        public static IOrganizationService GetCrmConnection(string uri, string userName, string password)
        {
            IOrganizationService organizationService = null;
            //try
            {
                Uri organizationUri = new Uri(uri);
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = userName;
                clientCredentials.UserName.Password = password;
                OrganizationServiceProxy Proxy = new OrganizationServiceProxy(organizationUri, null, clientCredentials, null);
                organizationService = Proxy as IOrganizationService;
                return organizationService;
            }
            //catch (Exception ex)
            //{
            //    return organizationService;
            //    throw new Exception(ex.Message);
            //}
        }
        static Guid GetTicketTypeProcessAsync(Guid ticketTypeId)
        {
            var ticketTypeEntity = service.Retrieve (ldv_service.EntityLogicalName, ticketTypeId, new ColumnSet(ldv_service.Fields.ldv_processid));
            var processId = ticketTypeEntity.GetAttributeValue<EntityReference>(ldv_service.Fields.ldv_processid).Id;
            return processId;
        }
        static void createTicket()
        {
            var entity = new Entity(Incident.EntityLogicalName);
            string customerId = "06f9baba-47fa-ee11-a1ff-6045bd9050cd";
            string Description = "test api";
            int Origin = 3;
            Guid CaseType = new Guid("E8015016-4BCB-EE11-9079-6045BD895C76");
            Guid CategoryId = new Guid("8D4324E3-39CC-EE11-907A-6045BD8C91C2");
            Guid SubCategoryId = new Guid("df9254fb-3ecc-ee11-907a-6045bd8c99bf");
            //Guid SubCategoryId1 = new Guid("E8015016-4BCB-EE11-9079-6045BD895C76");

            entity.Attributes.Add(Incident.Fields.CustomerId, new EntityReference(Contact.EntityLogicalName,new Guid( customerId)));
            entity.Attributes.Add(Incident.Fields.ldv_Description,  Description);
            entity.Attributes.Add(Incident.Fields.CaseOriginCode, new OptionSetValue( Origin));
            entity.Attributes.Add(Incident.Fields.ldv_serviceid, new EntityReference(ldv_service.EntityLogicalName,  CaseType));
            entity.Attributes.Add(Incident.Fields.ldv_MainCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, CategoryId));
            entity.Attributes.Add(Incident.Fields.ldv_SubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName,  SubCategoryId));
            entity.Attributes.Add(Incident.Fields.ldv_processid, new EntityReference("workflow",   GetTicketTypeProcessAsync( CaseType)));
            entity.Attributes.Add(Incident.Fields.ldv_IsSubmitted, true);
            //if (request.SubCategoryId1.HasValue)
            //    entity.Attributes.Add(Incident.Fields.ldv_SecondarySubCategoryid, new EntityReference(ldv_casecategory.EntityLogicalName, request.SubCategoryId1.Value));

            //if (request.BeneficiaryType.HasValue)
            //    entity.Attributes.Add(Incident.Fields.ldv_Beneficiarytypecode, new OptionSetValue(request.BeneficiaryType.Value));

            //if (request.Location.HasValue)
            //    entity.Attributes.Add(Incident.Fields.ldv_Locationcode, new OptionSetValue(request.Location.Value));

            var caseId = service. Create (entity);

           
        }
        public static CrmServiceClient getCRMAccess()
        {
            string ConnectionString = WebConfigurationManager.AppSettings["MOHJ"];
            CrmServiceClient createdConnetion = new CrmServiceClient(ConnectionString);
            return createdConnetion;
        }

    }

    class Credential
    {

        public const string UserName = @"";
        public const string Password = "";
        public const string Url = " ";
        

    }
}
