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
}
