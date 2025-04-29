using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Domain.Entitiy
{
    public partial class ldv_caserelatedfields
    {
        public const string EntityName = "ldv_caserelatedfields";
        public const string EntityCollectionName = "ldv_caserelatedfields";
        public const string EntityLogicalName = "ldv_caserelatedfields";
        public static class Fields
        {
            public const string Id = "ldv_caserelatedfieldsid";
            public const string Name = "ldv_name";

            public const string Source = "ldv_source";
            //public const string TicketNumber = "ldv_ticketnumber";
            public const string PartySource = "ldv_partysource";
            public const string SiteId = "ldv_siteid";
            public const string KidanaDescription = "ldv_kidanadescription";
            public const string CategoryDetails = "ldv_categorydetails";
            public const string ExternalStatus = "ldv_externalstatuscode";
            public const string ExternalReportDate = "ldv_externalreportdate";
            public const string ExternalStatusDate = "ldv_externalstatusdate";
            public const string KidanaExternalTicketId = "ldv_kidanaexternalticketid";
            public const string ExternalParty = "ldv_externalparty";

            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string OwnerId = "ownerid";
            public const string StateCode = "statecode";
        }

        #region OptionSets


        public enum ExternalStatus_OptionSet
        {
            CANCELLED = 1,
            CLOSED = 2,
            REJECTED = 3,
            OOS = 4,
            OOSC = 5,
            RESOLVED = 6
        }



        #endregion OptionSets
    }
}
