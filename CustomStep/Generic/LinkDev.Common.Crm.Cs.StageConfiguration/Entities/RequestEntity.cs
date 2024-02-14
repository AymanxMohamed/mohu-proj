using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.Entities
{
    public static class RequestEntity
    {
        public const string Name = "ldv_name";
        public const string RenwalName = "ldv_contractrenewalnumber";
        public const string CreatedOn = "createdon";
        public const string CurrentTask = "ldv_currenttaskid";// "ldv_currenttaskid";
        public const string ApplicationHeader = "ldv_applicationheaderid"; //ldv_applicationheaderid
        public const string Contact = "ldv_applicantid";//  
        public const string Account = "ldv_accountid";//  
        public const string Customer = "ldv_customerid";
        public const string Service = "ldv_serviceid";
        public const string ServiceSubStatus = "ldv_substatusid"; 
        public const string ServiceStatus = "ldv_statusid"; 
        public const string PortaServiceSubStatus = "ldv_portalstatusid";
        public const string Process = "ldv_processid";
        public const string statuscode = "statuscode";
        public const string statecode = "statecode";


    }

    public enum RequestEntity_Statecode
    {
        Inactive=1,
        Active=0
    }

    public enum RequestEntity_statuscode  
    {

    }
    public static class  IncidentEntity
    {
        public const string LogicalName = "incident";
        public const string CreatedOn = "createdon";
        public const string CurrentTask = "ldv_currenttaskid"; 
        public const string ApplicationHeader = "ldv_requestheaderid";  
        public const string Contact = "customerid"; 
        
        public const string Service = "ldv_servicesettingsid";
        public const string ServiceSubStatus = "ldv_crmsubstatusid"; 
        public const string ServiceStatus = "ldv_crmstatusid"; 
        public const string PortaServiceSubStatus = "ldv_portalstatusid";
        public const string Process = "ldv_processid";

    }
    public enum Incident_statuscode
    {
        Canceled = 6,
        InProgress = 1,
        OnHold = 2,
        ProblemSolved = 5,
        Merged = 2000,
        Researching = 4,
        WaitingforDetails = 3,

    }

}
