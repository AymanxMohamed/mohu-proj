using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Model
{
    public class MOHUStageConfiguration
    {
        public string EntityLogicalName = "ldv_ministageconfiguration";
        public string Id = "ldv_ministageconfigurationid";

        public string AssigningType = "ldv_assigningtypecode";
        public string User = "ldv_userid";
        public string Team = "ldv_teamid";
        public string Queue = "ldv_queueid";
        public string AssigningFieldLogicalName = "ldv_assigningfieldlogicalname";
        public string PortalStatus = "ldv_portal_servicesubstatusid";
        public string StatusReason = "ldv_servicesubstatusid";
        public string Status = "ldv_servicestatusid";



        public string SendbackAssigningType = "ldv_sendbackassigningtype";

        public string SendbackUser = "ldv_sendbackuserid";
        public string SendbackTeam = "ldv_sendbackteamid";
        public string SendbackQueue = "ldv_sendbackqueueid";

        public string SendbackStatus = "ldv_sendbackstatusid";
        public string SendbackStatusReason = "ldv_sendbackstatusreason";
        public string SendbackPortalStatus = "ldv_sendbackportalstatusid";

        public string SendbackFarStatus = "ldv_sendbackfromfarstagestatusid";
        public string SendbackFarStatusReason = "ldv_sendbackfromfarstagestatusreasonid";
        public string SendbackFarPortalStatus = "ldv_sendbackfromfarstageportalstatusid";

        public string RejectStatus = "ldv_rejectstatusid";
        public string RejectStatusReason = "ldv_rejectstatusreasonid";
        public string RejectPortalStatus = "ldv_rejectportalstatusid";



        public string SendbackAssigningFieldLogicalName = "ldv_sendbackassigningfieldlogicalname";
        
        
     
    

    }

    public enum AssignType
    {
        User=1,
        Team=2,
        Field=3
    }
}
