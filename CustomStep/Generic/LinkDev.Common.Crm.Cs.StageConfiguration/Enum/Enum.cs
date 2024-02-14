using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.Enum
{
    public enum RoutingConfigurationType
    {
        RoleConfiguration = 1,
        FieldName = 2,
        CurrentStageOwner = 3
    }

    public enum RoleConfigurationType
    {
        User = 1,
        Team = 2,
        Queue = 3
    }
    public enum FieldType
    {
        Lookup=1,
        Optionset=2,
        TwoOption=6
    }
    public enum GridType
    {
        FieldsToBeChanged = 1,
        FieldsToBeHistorically = 2,
        //FieldsToBeResitting = 3
    }
    public static class AssigningRouting
    {
        public const string Queue = "queue";
        public const string Team = "team";
        public const string User = "systemuser";
        public const string RoleConfiguration = "ldv_roleconfiguration";
    }
    public static class RoleConfigurationField
    {
        public const string Type = "ldv_type";
        public const string Queue = "ldv_queue";
        public const string Team = "ldv_team";
        public const string User = "ldv_user";
    }
    public class StageFields
    {
        public string CodeValue { set; get; }
        public string EntitySchemaName { set; get; }
        public string FieldSchemaName { set; get; }
        public string LookupEntitySchemaName { set; get; }
        public string Name { set; get; }
        public GuidItem StageFieldsId { set; get; }
        //public OptionSetItem RelationShipName { set; get; }
        public OptionSetItem FieldType { set; get; }
        public string TaskFieldSchemaName { set; get; }
        public bool IsNullable { set; get; } 
        /// <summary>
        /// Related to conditions fields // changed fields
        /// </summary>
        public bool HasCondition { set; get; }
        public ConditionFields ConditionField { set; get; }
        //public bool IsRepeated { set; get; }
        //public bool IsValidToChange { set; get; }
        //public bool IsFirst { set; get; }
        public int Index { set; get; }
    }
    public class ConditionFields
    {
        //public string FieldSchemaName { set; get; }
        public string FetchCondition { set; get; }
        public bool IsConditionMet { set; get; }

    }

    public class ChangedFieldTriggers
    {
        public string FieldSchemaName { set; get; }
        //public bool IsRepeated { set; get; }
        public List<StageFields> StageFields { set; get; }
        public int WithConditionCount { set; get; }
        public int WithOutConditionCount { set; get; }
        //public bool IsValidToChange { set; get; }

    }

    public class OptionSetItem
    {
        public string Label { get; set; }
        public int Id { get; set; }
    }
    public class GuidItem
    {
        public string Label { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
        //public EntityName EntityName { get; set; }
    }

    //public enum EntityName
    //{
    //    [Description("ldv_testcustomstep")]
    //    TestCustomStep = 1,
    //}
    }
