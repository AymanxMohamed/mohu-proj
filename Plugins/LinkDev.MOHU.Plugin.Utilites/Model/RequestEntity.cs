using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Model
{
    public static class RequestEntity
    {
        public const string Name = "title";
        public const string RenwalName = "ldv_contractrenewalnumber";
        public const string CreatedOn = "createdon";
        public const string CurrentTask = "ldv_currenttaskid";// "ldv_currenttaskid";
        public const string ApplicationHeader = "ldv_applicationheaderid"; //ldv_applicationheaderid
        public const string Contact = "ldv_applicantid";//  
        public const string Account = "ldv_accountid";//  
        public const string Customer = "customerid";
        public const string Service = "ldv_serviceid";
        public const string ServiceSubStatus = "ldv_substatusid";
        public const string ServiceStatus = "ldv_statusid";
        public const string PortaServiceSubStatus = "ldv_portalstatusid";
        public const string Process = "ldv_processid";
        public const string statuscode = "statuscode";
        public const string statecode = "statecode";
        public const string ProcessId = "processid";


    }
    public static class ServiceEntity
    {
        public const string EntityLogicalName = "ldv_service";
        public const string Process  = "ldv_processid";

    }

    public static class CategoryEntity
    {
        public const string EntityLogicalName = "ldv_casecategory";
        public const string IDLogicalName = "ldv_casecategoryid";
        public const string SlaHourLevel1 = "ldv_slahourlevel1id";
        public const string SlaHourLevel2 = "ldv_slahourlevel2id";
        public const string SlaHourLevel3 = "ldv_slahourlevel3id";

    }

    public static class SlaHoursEntity
    {
        public const string EntityLogicalName = "ldv_slahours";
        public const string IDLogicalName = "ldv_slahoursid";
        public const string WarningDurationHours = "ldv_warningdurationhours";
        public const string WarningDurationMinutes = "ldv_warningdurationminutes";
        public const string FailureDurationHours = "ldv_failuredurationhours";
        public const string FailureDurationMinutes = "ldv_failuredurationminutes";
    }

    public static class TaskEntity
    {
        public const string EntityLogicalName = "task";
        public const string IDLogicalName = "activityid";
        public const string SubCategory = "ldv_subcategoryid";

    }

    public static class ActionNames
    {
        public const string ActionLevel1 = "new_CustomPluginTimeCalculationd87ba2edce32ef1184096045bd8d9989";
        public const string ActionLevel2 = "new_ActionSLATimerCustomPluginTimeCalculationLevel28a5b068cb933ef1184096045bd8d9989";
        public const string ActionLevel3 = "new_ActionSLATimerCustomPluginTimeCalculationLevel3b0e030b5bc33ef11840a000d3a48ff6a";


    }
}
