namespace MOHU.Integration.Domain.Entitiy
{
    public partial class ActivityParty
    {

        public static class Fields
        {
            public const string ActivityId = "activityid";
            public const string ActivityPartyId = "activitypartyid";
            public const string Id = "activitypartyid";
            public const string AddressUsed = "addressused";
            public const string AddressUsedEmailColumnNumber = "addressusedemailcolumnnumber";
            public const string DoNotEmail = "donotemail";
            public const string DoNotFax = "donotfax";
            public const string DoNotPhone = "donotphone";
            public const string DoNotPostalMail = "donotpostalmail";
            public const string Effort = "effort";
            public const string ExchangeEntryId = "exchangeentryid";
            public const string InstanceTypeCode = "instancetypecode";
            public const string IsPartyDeleted = "ispartydeleted";
            public const string OwnerId = "ownerid";
            public const string ParticipationTypeMask = "participationtypemask";
            public const string PartyId = "partyid";
            public const string ResourceSpecId = "resourcespecid";
            public const string ScheduledEnd = "scheduledend";
            public const string ScheduledStart = "scheduledstart";
            public const string UnresolvedPartyName = "unresolvedpartyname";
            public const string VersionNumber = "versionnumber";
            public const string phonecall_activity_parties = "phonecall_activity_parties";
        }
        public const string EntityLogicalName = "activityparty";

        public const string EntitySchemaName = "ActivityParty";

        public const string PrimaryIdAttribute = "activitypartyid";

        public const string PrimaryNameAttribute = "partyidname";

        public const string EntityLogicalCollectionName = "activityparties";

        public const string EntitySetName = "activityparties";

    }
}
