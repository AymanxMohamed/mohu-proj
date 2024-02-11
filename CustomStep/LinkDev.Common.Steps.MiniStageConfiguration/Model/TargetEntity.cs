using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Model
{
    public class TargetEntity
    {
        public string Status =/* "ldv_statusid";//*/ "ldv_servicestatusid";
        public string StatusReason = /*"ldv_statusreasonid";//*/ "ldv_servicesubstatusid";
        public string PortalStatus = /*"ldv_portalstatus";//*/"ldv_portal_servicesubstatusid";


    }
}
