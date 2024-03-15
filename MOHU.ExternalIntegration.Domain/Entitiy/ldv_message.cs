using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Domain.Entitiy
{
    public partial class ldv_message
    {

        public static class Fields
        {
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string ldv_arabicmessage = "ldv_arabicmessage";
            public const string ldv_code = "ldv_code";
            public const string ldv_englishmessage = "ldv_englishmessage";
            public const string ldv_messageId = "ldv_messageid";
            public const string Id = "ldv_messageid";
            public const string ldv_name = "ldv_name";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
        }

        public const string EntityLogicalName = "ldv_message";

        public const string EntitySchemaName = "ldv_message";

        public const string PrimaryIdAttribute = "ldv_messageid";

        public const string PrimaryNameAttribute = "ldv_name";

        public const string EntityLogicalCollectionName = "ldv_messages";

        public const string EntitySetName = "ldv_messages";
    }
}
