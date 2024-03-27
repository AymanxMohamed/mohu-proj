using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.CRM.DataContracts.Enums
{
    public enum generatemodecode
    {
        NoDependentFields = 1,
        CreateDependentFieldsOnly = 2,
        CreateDependentFieldsAndGenerateSettings = 3
    }

    public enum BuilderListObjectType
    {
        Lookup = 1,
        OptionSet = 2
    }

    public enum SortField
    {
        [Description("createdon")]
        CreationDate = 1,

        [Description("ldv_submissiondate")]
        SubmissionDate	 = 2,

        [Description("ldv_lastmodifieddate")]
        LastModificationDate = 3,

        [Description("ldv_name")]
        ReferenceNumber = 4

    }

    public enum SortType
    {
        ASC = 1,
        DSC = 2,
    }

    public enum StatusValue
    {
        Success = 0,
        Fail = 1
    }

    [DataContract(Name = "RequirementLevel")] //Code Review : to be removed by MN
    public enum RequirementLevel
    {
        [EnumMember]
        Required,

        [EnumMember]
        Recommended,

        [EnumMember]
        Optional
    }
    [DataContract(Name = "RoleType")] //Code Review : to be removed by MN
    public enum RoleType
    {
        [EnumMember]
        User = 1,

        [EnumMember]
        Queue = 3,

        [EnumMember]
        Team = 2
    }

    public enum Language
    {
        Arabic = 1025,
        English = 1033

    }
    public enum StatuesType
    {
        Statues = 1,
        SubStatues = 2
    }
    enum RequestTypeEnum
    {
        ChangeName = 1,
        ChangeManager = 2,
        ChangeOwner = 3,
        AddStage = 4,
        RemoveStage = 5
    }
    enum GradeStatus
    {
        Currentlylicensed = 2,
        RequiredAction = 1
    }
    enum ApplicationGradeDecision
    {
        Approved = 1,
        Rejected = 2
    }

    //public enum ServiceType
    //{
    //    [Description("ldv_editlicenserequest")]
    //    AddStagesForSchools =4,
    //    [Description("ldv_editlicenserequest")]
    //    DecreaseStagesForSchools =5,
    //    [Description("ldv_editlicenserequest")]
    //    ChangeSchoolManager =2,
    //    [Description("ldv_editlicenserequest")]
    //    ChangeNameForSchoolOrNursery =1,
    //    [Description("ldv_editlicenserequest")]
    //    TransferOwnershipForSchoolsOrNurseries =3,
    //    [Description("ldv_workpermitrequest")]
    //    WorkPermit =6,
    //    [Description("ldv_renewalrequest")]
    //    PublicEducationRenewalRequest =7,
    //    [Description("ldv_publishingadvertisementrequest")]
    //    PublishingAdvertisment =8,
    //    [Description("ldv_accountfines")]
    //    AccountFinesItem =100
    //}
    public enum PaymentStatus
    {
        Success=1,
        Fail=2
    }
    public enum FineStatus
    {
        NotPaid = 1,
        Paid = 2,
        Cancelled =3
    }
}
