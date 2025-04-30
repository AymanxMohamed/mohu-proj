using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Domain.Entitiy
{
    public partial class ldv_integrationlogs
    {
        public const string EntityName = "ldv_integrationlogs";
        public const string EntityCollectionName = "ldv_integrationlogs";
        public const string EntityLogicalName = "ldv_integrationlogs";

        public static class Fields
        {
            public const string Id = "ldv_integrationlogsid";
            public const string Name = "ldv_name";

            public const string CaseId = "ldv_caseid";
            public const string CaseRelatedFieldsId = "ldv_caserelatedfieldsid";
            public const string IntegrationTypeCode = "ldv_integrationtypecode";
            public const string IntegrationOperationCode = "ldv_integrationoperationcode";
            public const string Trace = "ldv_trace";
            public const string ApiRequest = "ldv_apirequest";
            public const string Message = "ldv_message";
            public const string ExternalTicketId = "ldv_externalticketid";
            public const string ExternalTicketNumber = "ldv_externalticketnumber";
            public const string NusukStatusCode = "ldv_nusukstatuscode";

            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string OwnerId = "ownerid";
            public const string StateCode = "statecode";
        }

        #region OptionSets

        public enum IntegrationTypeCode_OptionSet
        {
            Tasheer = 1,
            ServiceDesk = 2,
            Kidana = 3,
            Sahab = 4,
            UpdateNusuk = 5,
            UpdateTasheer = 6,
            UpdateSD = 7,
            UpdateKidana = 8,
            SMSService = 753240001,
            AirPort = 9,
            GenesysCampaign = 10
        }

        public enum IntegrationOperationCode_OptionSet
        {
            Create = 1,
            Update = 2,
            Validate = 3
        }

        public enum NusukStatusCode_OptionSet
        {
            Submitted = 1,
            UnderProcessing = 2,
            Resolved = 3
        }

        #endregion OptionSets
    }
}
