using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Domain.Entitiy;
public partial class ldv_inspectiondetails
{
    public static class Fields
    {
        public const string CreatedBy = "createdby";
        public const string CreatedOn = "createdon";
        public const string CreatedOnBehalfBy = "createdonbehalfby";
        public const string ImportSequenceNumber = "importsequencenumber";
        public const string ldv_BasedonCode = "ldv_basedoncode";
        public const string ldv_calldependentfields = "ldv_calldependentfields";
        public const string ldv_dependentfieldentitylogicalname = "ldv_dependentfieldentitylogicalname";
        public const string ldv_displaynamear = "ldv_displaynamear";
        public const string ldv_displaynameen = "ldv_displaynameen";
        public const string ldv_entitylookuplogicalname = "ldv_entitylookuplogicalname";
        public const string ldv_fieldId = "ldv_fieldid";
        public const string Id = "ldv_fieldid";
        public const string ldv_actiondate = "ldv_actiondate";
        public const string ldv_statuscode = "ldv_statuscode";
        public const string ldv_inspector = "ldv_inspector";
        public const string ldv_comment = "ldv_comment";
        public const string ldv_caseid = "ldv_caseid";
        public const string ldv_regexpression = "ldv_regexpression";
        public const string ldv_typecode = "ldv_typecode";
        public const string ldv_validationquery = "ldv_validationquery";
        public const string ldv_Value = "ldv_value";
        public const string ldv_Xmlquery = "ldv_xmlquery";
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

    public const string EntityLogicalName = "ldv_inspectiondetails";

    public const string EntitySchemaName = "ldv_inspectiondetails";

    public const string PrimaryIdAttribute = "ldv_inspectiondetailsid";

    public const string PrimaryNameAttribute = "ldv_title";

    public const string EntityLogicalCollectionName = "ldv_inspectiondetailses";

    public const string EntitySetName = "ldv_inspectiondetailses";
}
